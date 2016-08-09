/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCMAudioSource : PXCMBase
{
    new public const Int32 CUID = unchecked((Int32)0xD8419523);

    /// <summary>
    /// DeviceInfo
    /// This structure describes the audio device information.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class DeviceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String did;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        internal Int32[] reserved;

        /// <summary>
        /// the constructor cleans all fields.
        /// </summary>
        public DeviceInfo()
        {
            name = "";
            did = "";
            reserved = new Int32[16];
        }
    };

    /// <summary>
    /// Scan the availble audio devices.
    /// </summary>
    /// <returns> the number of audio devices.</returns>
    public void ScanDevices()
    {
        PXCMAudioSource_ScanDevices(instance);
    }

    /// <summary>
    /// Get the number of available audio devices previously scanned.
    /// </summary>
    /// <returns> the number of audio devices.</returns>
    public Int32 QueryDeviceNum()
    {
        return PXCMAudioSource_QueryDeviceNum(instance);
    }

    /// <summary>
    /// Enumerate the audio devices previously scanned.
    /// </summary>
    /// <param name="didx"> The zero-based index to enumerate all devices.</param>
    /// <param name="dinfo"> The DeviceInfo structure to return the device information.</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    /// <returns> PXCM_STATUS_ITEM_UNAVAILABLE		No more devices.	</returns>
    public pxcmStatus QueryDeviceInfo(Int32 didx, out DeviceInfo dinfo)
    {
        return QueryDeviceInfoINT(instance, didx, out dinfo);
    }

    /// <summary>
    /// Get the currnet working device
    /// </summary>
    /// <param name="dinfo"> The working device info</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus QueryDeviceInfo(out DeviceInfo dinfo)
    {
        return QueryDeviceInfo(WORKING_PROFILE, out dinfo);
    }

    /// <summary>
    /// Set the audio device for subsequent module processing.
    /// </summary>
    /// <param name="dinfo"> The audio source</param>
    /// <returns> PXCM_STATUS_NO_ERROR				Successful execution.</returns>
    public pxcmStatus SetDevice(DeviceInfo dinfo)
    {
        return PXCMAudioSource_SetDevice(instance, dinfo);
    }

    /// <summary>
    /// Get the audio device volume
    /// </summary>
    /// <returns> the volume from 0 (min) to 1 (max).</returns>
    public Single QueryVolume()
    {
        return PXCMAudioSource_QueryVolume(instance);
    }

    /// <summary>
    /// Set the audio device volume
    /// </summary>
    /// <param name="volume"> The audio volume from 0 (min) to 1 (max).</param>
    /// <returns> PXCM_STATUS_NO_ERROR Executed successfully.</returns>
    public pxcmStatus SetVolume(Single volume)
    {
        return PXCMAudioSource_SetVolume(instance, volume);
    }

    /// <summary>
    /// Constructors and Disposers.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="delete"></param>
    internal PXCMAudioSource(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif
