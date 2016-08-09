/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;
#if NETFX_CORE
using System.Reflection;
#endif

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMMetadata : PXCMBase
{
    new public const Int32 CUID = 0x62398423;

    /// <summary>
    /// The function returns a unique identifier for the meta data storage.
    /// </summary>
    /// <returns> the unique identifier.</returns>
    public Int32 QueryUID()
    {
        return PXCMMetadata_QueryUID(instance);
    }

    /// <summary>
    /// The function retrieves the identifiers of all available meta data.
    /// </summary>
    /// <param name="idx"> The zero-based index to retrieve all identifiers.</param>
    /// <returns> the metadata identifier, or zero if not available.</returns>
    public Int32 QueryMetadata(Int32 idx)
    {
        return PXCMMetadata_QueryMetadata(instance, idx);
    }

    /// <summary>
    /// The function detaches the specified metadata.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <returns> PXCM_STATUS_NO_ERROR                Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE        The metadata is not found.</returns>
    public pxcmStatus DetachMetadata(Int32 id)
    {
        return PXCMMetadata_DetachMetadata(instance, id);
    }

    /// <summary>
    /// The function attaches the specified metadata.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <param name="buffer"> The metadata buffer.</param>
    /// <param name="size"> The metadata buffer size, in bytes.</param>
    /// <returns> PXC_STATUS_NO_ERROR                Successful execution.</returns>
    public pxcmStatus AttachBuffer(Int32 id, byte[] buffer)
    {
        return AttachBufferINT(instance, id, buffer);
    }

    /// <summary>
    /// The function returns the specified metadata buffer size.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <returns> the metadata buffer size, or zero if the metadata is not available.</returns>
    public Int32 QueryBufferSize(Int32 id)
    {
        return PXCMMetadata_QueryBufferSize(instance, id);
    }

    /// <summary>
    /// The function retrieves the specified metadata.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <param name="buffer"> The buffer to retrieve the metadata.</param>
    /// <returns> PXCM_STATUS_NO_ERROR         Successful execution.</returns>
    public pxcmStatus QueryBuffer(Int32 id, out Byte[] buffer)
    {
        return QueryBufferINT(instance, id, out buffer);
    }

    /// <summary>
    /// The function attaches an instance of a serializeable interface to be metadata storage.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <param name="instance"> The serializable instance.</param>
    /// <returns> PXCM_STATUS_NO_ERROR         Successful execution.</returns>
    public pxcmStatus AttachSerializable(Int32 id, PXCMBase slz)
    {
        return PXCMMetadata_AttachSerializable(instance, id, slz.instance);
    }

    /// <summary>
    /// The function creates an instance of a serializeable interface from the metadata storage.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <param name="cuid"> The interface identifier.</param>
    /// <param name="instance"> The serializable instance, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR         Successful execution.</returns>
    public pxcmStatus CreateSerializable(Int32 id, Int32 cuid, out PXCMBase slz)
    {
        IntPtr slz2;
        pxcmStatus sts = PXCMMetadata_CreateSerializable(instance, id, cuid, out slz2);
        slz = null;
        if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
        {
            slz = new PXCMBase(slz2, false).QueryInstance(cuid);
            slz.AddRef();
        }
        return sts;
    }

    /// <summary>
    /// The function creates an instance of a serializeable interface from the metadata storage.
    /// </summary>
    /// <param name="id"> The metadata identifier.</param>
    /// <param name="instance"> The serializable instance, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR         Successful execution.</returns>
    public pxcmStatus CreateSerializable<TT>(Int32 id, out TT slz) where TT : PXCMBase
    {
        PXCMBase slz2;
#if !NETFX_CORE
        pxcmStatus sts = CreateSerializable(id, Type2CUID[typeof(TT)], out slz2);
#else
			pxcmStatus sts = CreateSerializable(id, Type2CUID[typeof(TT).GetTypeInfo()], out slz2);
#endif
        slz = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? (TT)slz2 : null;
        return sts;
    }

    /* constructors and misc */
    internal PXCMMetadata(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
