/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMSyncPoint : PXCMBase
{
    new public const Int32 CUID = 0x50534853;
    public const Int32 TIMEOUT_INFINITE = -1;
    public const Int32 SYNCEX_LIMIT = 64;

    /* Member Functions */

    /// <summary>
    ///    The function synchronizes a single SP with timeout.
    /// </summary>
    /// <param name="timeout"> The timeout value in ms.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    /// <returns> PXCM_STATUS_EXEC_TIMEOUT    The timeout value is reached.</returns>
    public pxcmStatus Synchronize(Int32 timeout)
    {
        return PXCMSyncPoint_Synchronize(instance, timeout);
    }

    /// <summary>
    ///  The function synchronizes a single SP infinitely.
    /// </summary>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    public pxcmStatus Synchronize()
    {
        return Synchronize(TIMEOUT_INFINITE);
    }

    /// <summary>
    ///    The function synchronizes multiple SPs as well as OS events. Zero SPs or OS events are skipped automatically.
    /// If the idx argument is NULL, the function waits until all events are signaled.
    /// If the idx argument is not NULL, the function waits until any of the events is signaled and returns the index of the signalled events.
    /// </summary>
    /// <param name="n"> The number of SPs to be synchronized.</param>
    /// <param name="sps"> The SP array.</param>
    /// <param name="idx"> The event index, to be returned.</param>
    /// <param name="timeout"> The timeout value in ms.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    /// <returns> PXCM_STATUS_EXEC_TIMEOUT    The timeout value is reached.</returns>
    public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, out Int32 idx, Int32 timeout)
    {
        IntPtr[] sps2 = new IntPtr[sps.Length];
        for (int i = 0; i < sps.Length; i++)
            sps2[i] = sps[i] != null ? sps[i].instance : IntPtr.Zero;
        return PXCMSyncPoint_SynchronizeExA(sps.Length, sps2, out idx, timeout);
    }

    /// <summary>
    ///    The function synchronizes multiple SPs. Zero SPs are skipped automatically.
    /// If the idx argument is NULL, the function waits until all events are signaled.
    /// If the idx argument is not NULL, the function waits until any of the events is signaled and returns the index of the signalled events.
    /// </summary>
    /// <param name="sps"> The SP array.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    /// <returns> PXCM_STATUS_EXEC_TIMEOUT    The timeout value is reached.</returns>
    public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, Int32 timeout)
    {
        IntPtr[] sps2 = new IntPtr[sps.Length];
        for (int i = 0; i < sps.Length; i++)
            sps2[i] = (sps[i] != null) ? sps[i].instance : IntPtr.Zero;
        int idx = 0;
        return PXCMSyncPoint_SynchronizeExB(sps.Length, sps2, out idx, timeout);
    }


    /// <summary>
    ///    The function synchronizes multiple SPs. Zero SPs are skipped automatically.
    /// If the idx argument is NULL, the function waits until all events are signaled.
    /// If the idx argument is not NULL, the function waits until any of the events is signaled and returns the index of the signalled events.
    /// </summary>
    /// <param name="sps"> The SP array.</param>
    /// <param name="idx"> The event index, to be returned.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    /// <returns> PXCM_STATUS_EXEC_TIMEOUT    The timeout value is reached.</returns>
    [CLSCompliant(false)]
    public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps, out Int32 idx)
    {
        return SynchronizeEx(sps, out idx, TIMEOUT_INFINITE);
    }

    /// <summary>
    ///    The function synchronizes multiple SPs. Zero SPs are skipped automatically.
    /// If the idx argument is NULL, the function waits until all events are signaled.
    /// If the idx argument is not NULL, the function waits until any of the events is signaled and returns the index of the signalled events.
    /// </summary>
    /// <param name="sps"> The SP array.</param>
    /// <returns> PXCM_STATUS_NO_ERROR        Successful execution.</returns>
    /// <returns> PXCM_STATUS_EXEC_TIMEOUT    The timeout value is reached.</returns>
    public static pxcmStatus SynchronizeEx(PXCMSyncPoint[] sps)
    {
        return SynchronizeEx(sps, TIMEOUT_INFINITE);
    }

    /// <summary>
    /// A convenient function to release an array of objects
    /// </summary>
    /// <param name="sps"></param>
    /// <param name="startIndex"></param>
    /// <param name="nsps"></param>
    public static void ReleaseSP(PXCMSyncPoint[] sps, Int32 startIndex, Int32 nsps)
    {
        for (int i = startIndex; i < startIndex + nsps; i++)
        {
            if (sps[i] == null) continue;
            sps[i].Dispose();
            sps[i] = null;
        }
    }

    public static void ReleaseSP(PXCMSyncPoint[] sps)
    {
        ReleaseSP(sps, 0, sps.Length);
    }

    internal PXCMSyncPoint(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
