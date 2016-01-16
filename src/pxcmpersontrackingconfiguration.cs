/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or non-disclosure
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

    public partial class PXCMPersonTrackingConfiguration : PXCMBase
    {
        new public const Int32 CUID = 0x43544f50;

        [StructLayout(LayoutKind.Sequential, Size = 40)]
        internal struct Reserved
        {
            internal int res1, res2, res3, res4, res5, res6, res7, res8, res9, res10;
        }

        public enum TrackingStrategyType
        {
            STRATEGY_APPEARANCE_TIME,
            STRATEGY_CLOSEST_TO_FARTHEST,
            STRATEGY_FARTHEST_TO_CLOSEST,
            STRATEGY_LEFT_TO_RIGHT,
            STRATEGY_RIGHT_TO_LEFT
        };
        
        public partial class TrackingConfiguration
        {     
            public void Enable()
            {
                PXCMPersonTrackingConfiguration_TrackingConfiguration_Enable(instance);
            }

            public void Disable()
            {
                PXCMPersonTrackingConfiguration_TrackingConfiguration_Disable(instance);
            }

            public void EnableSegmentation()
		    {
                PXCMPersonTrackingConfiguration_TrackingConfiguration_EnableSegmentation(instance);
		    }

		    public void DisableSegmentation()
		    {
                PXCMPersonTrackingConfiguration_TrackingConfiguration_DisableSegmentation(instance);
		    }

            public void SetMaxTrackedPersons(Int32 maxTrackedPersons)
		    {
                PXCMPersonTrackingConfiguration_TrackingConfiguration_SetMaxTrackedPersons(instance, maxTrackedPersons);
		    }

            private IntPtr instance;
            internal TrackingConfiguration(IntPtr instance)
            {
                this.instance = instance;
            }
        };
 
        public partial class SkeletonJointsConfiguration
        {
            public enum SkeletonMode
		    {
			    AREA_UPPER_BODY,	    // all joints in upper body
			    AREA_UPPER_BODY_ROUGH,	// only 4 points - head, hands, chest
			    AREA_FULL_BODY_ROUGH,
			    AREA_FULL_BODY
		    };
            
 
            public void Enable()
            {
                PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_Enable(instance);
            }

            public void Disable()
            {
                PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_Disable(instance);
            }

            public void SetMaxTrackedPersons(Int32 maxTrackedPersons)
		    {
                PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_SetMaxTrackedPersons(instance, maxTrackedPersons);
		    }
		
            public void SetTrackingArea(SkeletonMode area)
		    {
                PXCMPersonTrackingConfiguration_SkeletonJointsConfiguration_SetTrackingArea(instance, area);
		    }

            private IntPtr instance;
            internal SkeletonJointsConfiguration(IntPtr instance)
            {
                this.instance = instance;
            }
        };

        public partial class PoseConfiguration
        {
            public void Enable()
            {
                PXCMPersonTrackingConfiguration_PoseConfiguration_Enable(instance);
            }

            public void Disable()
            {
                PXCMPersonTrackingConfiguration_PoseConfiguration_Disable(instance);
            }

            public void SetMaxTrackedPersons(Int32 maxTrackedPersons)
		    {
                PXCMPersonTrackingConfiguration_PoseConfiguration_MaxTrackedPersons(instance, maxTrackedPersons);
		    }

            private IntPtr instance;
            internal PoseConfiguration(IntPtr instance)
            {
                this.instance = instance;
            }
        };

        public partial class RecognitionConfiguration
        {
            public void Enable() {
                PXCMPersonTrackingConfiguration_RecognitionConfiguration_Enable(instance);
            }

            public void Disable() {
                PXCMPersonTrackingConfiguration_RecognitionConfiguration_Disable(instance);
            }

            public void SetDatabaseBuffer(Byte[] buffer)
            {
                PXCMPersonTrackingConfiguration_RecognitionConfiguration_SetDatabaseBuffer(instance, buffer, buffer.Length);
            }

            private IntPtr instance; 
            internal RecognitionConfiguration(IntPtr instance)
            {
                this.instance = instance;
            }
        };

        public TrackingConfiguration QueryTracking()
        {
            return QueryTrackingINT(instance);
        }

        public SkeletonJointsConfiguration QuerySkeletonJoints()
        {
            return QuerySkeletonJointsINT(instance);
        }


        public PoseConfiguration QueryPose()
        {
            return QueryPoseINT(instance);
        }


        public RecognitionConfiguration QueryRecognition()
        {
            return QueryRecognitionINT(instance);
        }

        public enum TrackingAngles
	    {
		    TRACKING_ANGLES_FRONTAL = 0,
		    TRACKING_ANGLES_PROFILE,
		    TRACKING_ANGLES_ALL
	    };

        /**
		    @brief Sets the range of user angles to be tracked
	    */
        public void SetTrackedAngles(TrackingAngles angles)
        {
            PXCMPersonTrackingConfiguration_SetTrackedAngles(instance, angles);
        }


        internal class EventMaps
        {
            internal Dictionary<OnFiredAlertDelegate, Object> alert = new Dictionary<OnFiredAlertDelegate, Object>();
            internal Object cs = new Object();
        };
        internal EventMaps maps;

#if PT_MW_DEV

        /** 
            @brief Enable alert messaging for a specific event
        */
        public pxcmStatus EnableAlert(PXCMPersonTrackingData.AlertType alertEvent)
        {
            return PXCMPersonTrackingConfiguration_EnableAlert(instance, alertEvent);
        }

        /** 
            @brief Enable all alert messaging events.
        */
        public void EnableAllAlerts()
        {
            PXCMPersonTrackingConfiguration_EnableAllAlerts(instance);
        }

        /** 
            @brief Test the activation status of the given alert.
        */
        public Boolean IsAlertEnabled(PXCMPersonTrackingData.AlertType alertEvent)
        {
            return PXCMPersonTrackingConfiguration_IsAlertEnabled(instance, alertEvent);
        }

        /** 
            @brief Disable alert messaging for a specific event.
        */
        public Boolean DisableAlert(PXCMPersonTrackingData.AlertType alertEvent)
        {
            return PXCMPersonTrackingConfiguration_DisableAlert(instance, alertEvent);
        }

        /** 
            @brief Disable messaging for all alerts.
        */
        public void DisableAllAlerts()
        {
            PXCMPersonTrackingConfiguration_DisableAllAlerts(instance);
        }
#endif

        public delegate void OnFiredAlertDelegate(PXCMPersonTrackingData.AlertData alertData);

#if PT_MW_DEV
        /** 
            @brief Register an event handler object for the alerts.        
        */
        public pxcmStatus SubscribeAlert(OnFiredAlertDelegate handler)
        {
            if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            Object proxy;
            pxcmStatus sts = SubscribeAlertINT(instance, handler, out proxy);
            if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) return sts;

            lock (maps.cs)
            {
                maps.alert[handler] = proxy;
            }
            return sts;
        }

        /** 
            @brief Unsubscribe an alert handler object.
        */
        public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate handler)
        {
            if (handler == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

            lock (maps.cs)
            {
                Object proxy;
                if (!maps.alert.TryGetValue(handler, out proxy))
                    return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;

                pxcmStatus sts = UnsubscribeAlertINT(instance, proxy);
                maps.alert.Remove(handler);
                return sts;
            }
        }
#endif

        internal PXCMPersonTrackingConfiguration(EventMaps maps, IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            this.maps = maps;
        }

    };

#if RSSDK_IN_NAMESPACE
}
#endif
