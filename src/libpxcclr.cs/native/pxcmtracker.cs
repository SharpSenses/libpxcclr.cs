/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMTracker : PXCMBase
    {
        [DllImport(DLLNAME)]
        internal extern static Boolean PXCMTracker_IsTracking(ETrackingState state);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal extern static pxcmStatus PXCMTracker_SetCameraParameters(IntPtr instsance, String filename);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal extern static pxcmStatus PXCMTracker_Set2DTrackFromFile(IntPtr instance, String filename, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold, [MarshalAs(UnmanagedType.Bool)] Boolean extensible);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_Set2DTrackFromImage(IntPtr instance, IntPtr image, out Int32 cosID, Single widthMM, Single heightMM, Single qualityThreshold, [MarshalAs(UnmanagedType.Bool)] Boolean extensible);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal extern static pxcmStatus PXCMTracker_Set3DTrack(IntPtr instance, String filename, out Int32 firstCosID, out Int32 lastCosID, [MarshalAs(UnmanagedType.Bool)] Boolean extensible);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_RemoveTrackingID(IntPtr instance, Int32 cosID);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_RemoveAllTrackingIDs(IntPtr instance);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_Set3DInstantTrack(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] Boolean egoMotion, Int32 framesToSkip);

        [DllImport(DLLNAME)]
        internal extern static Int32 PXCMTracker_QueryNumberTrackingValues(IntPtr instance);

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_QueryAllTrackingValues(IntPtr instance, IntPtr trackingValues);

        internal static pxcmStatus QueryAllTrackingValuesINT(IntPtr instance, out TrackingValues[] trackingValues)
        {
            Int32 nvalues = PXCMTracker_QueryNumberTrackingValues(instance);
            IntPtr tv2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TrackingValues)) * nvalues);
            pxcmStatus sts = PXCMTracker_QueryAllTrackingValues(instance, tv2);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                trackingValues = new TrackingValues[nvalues];
                for (int i = 0; i < nvalues; i++)
                {
                    trackingValues[i] = new TrackingValues();
                    Marshal.PtrToStructure(new IntPtr(tv2.ToInt64() + Marshal.SizeOf(typeof(TrackingValues)) * i), trackingValues[i]);
                }
            }
            else
            {
                trackingValues = null;
            }
            Marshal.FreeHGlobal(tv2);
            return sts;
        }

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_QueryTrackingValues(IntPtr instance, Int32 cosID, [Out] TrackingValues trackingValues);

        internal static pxcmStatus QueryTrackingValuesINT(IntPtr instance, Int32 cosID, out TrackingValues trackingValues)
        {
            trackingValues = new TrackingValues();
            return PXCMTracker_QueryTrackingValues(instance, cosID, trackingValues);
        }

        [DllImport(DLLNAME)]
        internal extern static pxcmStatus PXCMTracker_SetRegionOfInterest(IntPtr instance, PXCMRectI32 roi);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMTracker_QueryTrackerUtils(IntPtr instance);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
