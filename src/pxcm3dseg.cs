/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;

// This implementation is based off of the handconfiguration callback:
// \sw\sdk\sdk\trunk\framework\common\pxcclr.cs\src\pxcmhandconfiguration.cs

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

public partial class PXCM3DSeg : PXCMBase
{
    new public const Int32 CUID = 0x31494753;

    public delegate void OnAlertDelegate(PXCM3DSeg.AlertData data);

    /// <summary> 
    /// Allocate and return a copy of the module's most recent segmented image. 
    /// The returned object's Release method can be used to deallocate it. 
    /// </summary>
    public PXCMImage AcquireSegmentedImage()
    {
        IntPtr image = PXCM3DSeg_AcquireSegmentedImage(instance);
        return image != IntPtr.Zero ? new PXCMImage(image, true) : null;
    }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="delete"></param>
    internal PXCM3DSeg(IntPtr instance, Boolean delete)
        : base(instance, delete)
    {
    }

    /// <summary>
    /// AlertEvent
    /// Enumeratea all supported alert events.
    /// </summary>
    [Flags]
    public enum AlertEvent
    {
        ALERT_USER_IN_RANGE = 0,
        ALERT_USER_TOO_CLOSE,
        ALERT_USER_TOO_FAR,
    };

    /// <summary>
    /// AlertData
    /// Describe the alert parameters.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData
    {
        public Int64 timeStamp;
        public AlertEvent label;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        internal Int32[] reserved;

        public AlertData()
        {
            reserved = new Int32[5];
        }
    }

    /// <summary>
    /// Subscribe with an alert callback
    /// </summary>
    /// <param name="d"></param>
    public void Subscribe(OnAlertDelegate d)
    {
        SubscribeINT(d);
    }

    /// <summary>
    /// Set a frame ski interval
    /// </summary>
    /// <param name="skipInterval"></param>
    /// <returns></returns>
    public pxcmStatus SetFrameSkipInterval(int skipInterval)
    {
        return PXCM3DSeg_SetFrameSkipInterval(instance, skipInterval);
    }
};

#if RSSDK_IN_NAMESPACE
}
#endif