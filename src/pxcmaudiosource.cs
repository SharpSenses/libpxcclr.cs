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

        /**
            @struct DeviceInfo
            This structure describes the audio device information.
        */
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

            /**
                @brief the constructor cleans all fields.
            */
            public DeviceInfo()
            {
                name = "";
                did = "";
                reserved = new Int32[16];
            }
        };

        /**
            @brief Scan the availble audio devices.
            @return the number of audio devices.
        */
        public void ScanDevices()
        {
            PXCMAudioSource_ScanDevices(instance);
        }

        /**
            @brief Get the number of available audio devices previously scanned.
            @return the number of audio devices.
        */
        public Int32 QueryDeviceNum()
        {
            return PXCMAudioSource_QueryDeviceNum(instance);
        }

        /**
            @brief Enumerate the audio devices previously scanned.
            @param[in]  didx		The zero-based index to enumerate all devices.
            @param[out] dinfo		The DeviceInfo structure to return the device information. 
            @return PXCM_STATUS_NO_ERROR				Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE		No more devices.	
        */
        public pxcmStatus QueryDeviceInfo(Int32 didx, out DeviceInfo dinfo)
        {
            return QueryDeviceInfoINT(instance, didx, out dinfo);
        }

        /**
            @brief Get the currnet working device
            @param[out] dinfo		The working device info
            @return PXCM_STATUS_NO_ERROR				Successful execution.
        */
        public pxcmStatus QueryDeviceInfo(out DeviceInfo dinfo)
        {
            return QueryDeviceInfo(WORKING_PROFILE, out dinfo);
        }

        /**
            @brief Set the audio device for subsequent module processing.
            @param[in] dinfo		The audio source
            @return PXCM_STATUS_NO_ERROR				Successful execution.
        */
        public pxcmStatus SetDevice(DeviceInfo dinfo)
        {
            return PXCMAudioSource_SetDevice(instance, dinfo);
        }

        /**
            @brief Get the audio device volume
            @return the volume from 0 (min) to 1 (max).
        */
        public Single QueryVolume()
        {
            return PXCMAudioSource_QueryVolume(instance);
        }

        /**
            @brief Set the audio device volume
            @param volume    The audio volume from 0 (min) to 1 (max).
            @return PXCM_STATUS_NO_ERROR Executed successfully.
        */
        public pxcmStatus SetVolume(Single volume)
        {
            return PXCMAudioSource_SetVolume(instance, volume);
        }

        /* Constructors & Disposers */
        internal PXCMAudioSource(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
