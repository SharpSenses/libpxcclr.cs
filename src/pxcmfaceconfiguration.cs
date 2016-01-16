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

    public partial class PXCMFaceConfiguration : PXCMBase
    {
        new public const Int32 CUID = 0x47464346;

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
            STRATEGY_RIGHT_TO_LEFT,
        };

        public enum SmoothingLevelType
        {
            SMOOTHING_DISABLED,
            SMOOTHING_MEDIUM,
            SMOOTHING_HIGH,
        };

        [StructLayout(LayoutKind.Sequential)]
        public class DetectionConfiguration
        {
            [MarshalAs(UnmanagedType.Bool)]
            public Boolean isEnabled;
            public Int32 maxTrackedFaces;
            public SmoothingLevelType smoothingLevel;
            internal Reserved reserved;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class LandmarksConfiguration
        {
            [MarshalAs(UnmanagedType.Bool)]
            public Boolean isEnabled;
            public Int32 maxTrackedFaces;
            public SmoothingLevelType smoothingLevel;
            public Int32 numLandmarks;
            internal Reserved reserved;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class PoseConfiguration
        {
            [MarshalAs(UnmanagedType.Bool)]
            public Boolean isEnabled;
            public Int32 maxTrackedFaces;
            public SmoothingLevelType smoothingLevel;
            internal Reserved reserved;
        };

        public partial class ExpressionsConfiguration
        {
            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public class ExpressionsProperties
            {
                [MarshalAs(UnmanagedType.Bool)]
                public Boolean isEnabled;
                public Int32 maxTrackedFaces;
                internal Reserved reserved;
            }

            public ExpressionsProperties properties
            {
                get
                {
                    return instance.configs.expressions.properties;
                }
                set
                {
                    instance.configs.expressions.properties = value;
                }
            }

            /*
                @brief Enables expression module.
            */
            public void Enable()
            {
                properties.isEnabled = true;
            }

            /*
                @brief Disables expression module.
            */
            public void Disable()
            {
                properties.isEnabled = false;
            }

            public Boolean IsEnabled()
            {
                return properties.isEnabled;
            }

            /*
                @brief Enables all available expressions.
            */
            public void EnableAllExpressions()
            {
                PXCMFaceConfiguration_ExpressionsConfiguration_EnableAllExpressions(instance.configs.expressionInstance);
            }

            /*
                @brief Disables all available expressions.
            */
            public void DisableAllExpressions()
            {
                PXCMFaceConfiguration_ExpressionsConfiguration_DisableAllExpressions(instance.configs.expressionInstance);
            }

            /*
                @brief Enables specific expression.
                @param[in] expression - single face expression.
                @return PXCM_STATUS_NO_ERROR - success.
                PXCM_STATUS_PARAM_UNSUPPORTED - expression is unsupported.
            */
            public pxcmStatus EnableExpression(PXCMFaceData.ExpressionsData.FaceExpression expression)
            {
                return PXCMFaceConfiguration_ExpressionsConfiguration_EnableExpression(instance.configs.expressionInstance, expression);
            }

            /*
                @brief Disables specific expression.
                @param[in] expression - single face expression.
            */
            public void DisableExpression(PXCMFaceData.ExpressionsData.FaceExpression expression)
            {
                PXCMFaceConfiguration_ExpressionsConfiguration_DisableExpression(instance.configs.expressionInstance, expression);
            }

            /*
                @brief Checks if expression is currently enabled in configuration.
                @param[in] expression - single face expression
                @return true - enabled, false - disabled.
            */
            public Boolean IsExpressionEnabled(PXCMFaceData.ExpressionsData.FaceExpression expression)
            {
                return PXCMFaceConfiguration_ExpressionsConfiguration_IsExpressionEnabled(instance.configs.expressionInstance, expression);
            }

            private PXCMFaceConfiguration instance;

            internal ExpressionsConfiguration(PXCMFaceConfiguration instance)
            {
                this.instance = instance;
            }
        };

        public ExpressionsConfiguration QueryExpressions()
        {
            if (configs.expressionInstance == IntPtr.Zero) return null;
            return new ExpressionsConfiguration(this);
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public partial class RecognitionConfiguration
        {
            public const Int32 STORAGE_NAME_SIZE = 50;

            public enum RecognitionRegistrationMode
            {
                REGISTRATION_MODE_CONTINUOUS,	//registers users automatically
                REGISTRATION_MODE_ON_DEMAND,	//registers users on demand only
            };

            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public class RecognitionStorageDesc
            {
                [MarshalAs(UnmanagedType.Bool)]
                public Boolean isPersistent;
                public Int32 maxUsers;
                internal Reserved reserved;
            };

            public RecognitionStorageDesc storageDesc
            {
                get
                {
                    return instance.configs.storageDesc;
                }
                set
                {
                    instance.configs.storageDesc = value;
                }
            }

            public String storageName
            {
                get
                {
                    return instance.configs.storageName;
                }
                set
                {
                    instance.configs.storageName = value;
                }
            }

            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public class RecognitionProperties
            {
                [MarshalAs(UnmanagedType.Bool)]
                public Boolean isEnabled;
                public Int32 accuracyThreshold;
                public RecognitionRegistrationMode registrationMode;
                internal Reserved reserved;
            };

            public RecognitionProperties properties
            {
                get
                {
                    return instance.configs.recognitionProperties;
                }
                set
                {
                    instance.configs.recognitionProperties = value;
                }
            }

            public void Enable()
            {
                properties.isEnabled = true;
            }

            public void Disable()
            {
                properties.isEnabled = false;
            }

            public void SetAccuracyThreshold(Int32 threshold)
            {
                properties.accuracyThreshold = threshold;
            }

            public Int32 GetAccuracyThreshold()
            {
                return properties.accuracyThreshold;
            }

            public void SetRegistrationMode(RecognitionRegistrationMode mode)
            {
                properties.registrationMode = mode;
            }

            public RecognitionRegistrationMode GetRegistrationMode()
            {
                return properties.registrationMode;
            }


            public void SetDatabaseBuffer(Byte[] buffer)
            {
                PXCMFaceConfiguration_RecognitionConfiguration_SetDatabaseBuffer(instance.configs.recognitionInstance, buffer, buffer.Length);
            }

            /** 
                @brief Sets the active Recognition database.
                @param[in] storageName - The name of the database to be loaded by the Recognition module.
                @param[in] storage - A pointer to the Recognition database, or NULL for an existing database.
                @return PXCM_STATUS_HANDLE_INVALID - if the module wasn't initialized properly.
                PXCM_STATUS_DATA_UNAVAILABLE - if the registration failed.
                PXCM_STATUS_NO_ERROR - if registration was successful.
            */
            public pxcmStatus UseStorage(String storageName)
            {
                this.storageName = storageName;
                return PXCMFaceConfiguration_RecognitionConfiguration_UseStorage(instance.configs.recognitionInstance, this.storageName);
            }

            public pxcmStatus QueryActiveStorage(out RecognitionStorageDesc outStorage)
            {
                return QueryActiveStorageINT(instance.configs.recognitionInstance, out outStorage);
            }

            /** 
                @brief Create a new Recognition database.
                @param[in] storageName The name of the new database.
                @return The new database description.
            */
            public pxcmStatus CreateStorage(String storageName, out RecognitionStorageDesc storageDesc)
            {
                return CreateStorageINT(instance.configs.recognitionInstance, storageName, out storageDesc);
            }

            public pxcmStatus SetStorageDesc(String storageName, RecognitionStorageDesc storageDesc)
            {
                return PXCMFaceConfiguration_RecognitionConfiguration_SetStorageDesc(instance.configs.recognitionInstance, storageName, storageDesc);
            }

            public pxcmStatus DeleteStorage(String storageName)
            {
                return PXCMFaceConfiguration_RecognitionConfiguration_DeleteStorage(instance.configs.recognitionInstance, storageName);
            }

            internal PXCMFaceConfiguration instance;
            internal RecognitionConfiguration(PXCMFaceConfiguration instance)
            {
                this.instance = instance;
            }
        };

        public RecognitionConfiguration QueryRecognition()
        {
            if (configs.recognitionInstance == IntPtr.Zero) return null;
            return new RecognitionConfiguration(this);
        }

        public class PulseConfiguration
        {
            [Serializable]
            [StructLayout(LayoutKind.Sequential)]
            public class PulseProperties
            {
                [MarshalAs(UnmanagedType.Bool)]
                public Boolean isEnabled;
                public Int32 maxTrackedFaces;
                internal Reserved reserved;
            }

            public PulseProperties properties
            {
                get
                {
                    return instance.configs.pulseProperties;
                }
                set
                {
                    instance.configs.pulseProperties = value;
                }
            }

            /*
                @brief Enables pulse module.
            */
            public void Enable()
            {
                properties.isEnabled = true;
            }

            /*
                @brief Disables pulse module.
            */
            public void Disable()
            {
                properties.isEnabled = false;
            }

            public Boolean IsEnabled()
            {
                return properties.isEnabled;
            }

            private PXCMFaceConfiguration instance;

            internal PulseConfiguration(PXCMFaceConfiguration instance)
            {
                this.instance = instance;
            }
        }

        public PulseConfiguration QueryPulse()
        {
            if (configs.pulseInsance == IntPtr.Zero) return null;
            return new PulseConfiguration(this);
        }

        public enum TrackingModeType
        {
            FACE_MODE_COLOR,
            FACE_MODE_COLOR_PLUS_DEPTH,
            FACE_MODE_COLOR_STILL,
        };

        public DetectionConfiguration detection
        {
            get
            {
                return configs.detection;
            }
            set
            {
                configs.detection = value;
            }
        }

        public LandmarksConfiguration landmarks
        {
            get
            {
                return configs.landmarks;
            }
            set
            {
                configs.landmarks = value;
            }
        }

        public PoseConfiguration pose
        {
            get
            {
                return configs.pose;
            }
            set
            {
                configs.pose = value;
            }
        }

        public TrackingStrategyType strategy
        {
            get
            {
                return configs.strategy;
            }
            set
            {
                configs.strategy = value;
            }
        }

        public pxcmStatus SetTrackingMode(TrackingModeType trackingMode)
        {
            return PXCMFaceConfiguration_SetTrackingMode(instance, trackingMode);
        }

        public TrackingModeType GetTrackingMode()
        {
            return PXCMFaceConfiguration_GetTrackingMode(instance);
        }

        /** 
            @brief Enable alert, so that events are fired when the alert is identified.			
            @param[in] alertEvent the label of the alert to enabled. 
            @return PXCM_STATUS_NO_ERROR if the alert was enabled successfully; otherwise, return one of the following errors:
            PXCM_STATUS_PARAM_UNSUPPORTED - Unsupported parameter.
            PXCM_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.
        */
        public pxcmStatus EnableAlert(PXCMFaceData.AlertData.AlertType alertEvent)
        {
            return PXCMFaceConfiguration_EnableAlert(instance, alertEvent);
        }

        /** 
            @brief Enable all alert messaging events.            
            @return PXC_STATUS_NO_ERROR if enabling all alerts was successful; otherwise, return one of the following errors:
            PXCM_STATUS_PROCESS_FAILED - Module failure during processing.
            PXCM_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.
        */
        public void EnableAllAlerts()
        {
            PXCMFaceConfiguration_EnableAllAlerts(instance);
        }

        /** 
            @brief Check if an alert is enabled.    
            @param[in] alertEvent the ID of the event.            
            @return true if the alert is enabled; otherwise, return false
        */
        public Boolean IsAlertEnabled(PXCMFaceData.AlertData.AlertType alertEvent)
        {
            return PXCMFaceConfiguration_IsAlertEnabled(instance, alertEvent);
        }

        /** 
            @brief Disable alert messaging for a specific event.            
            @param[in] alertEvent the ID of the event to be disabled.
            @return PXC_STATUS_NO_ERROR if disabling the alert was successful; otherwise, return one of the following errors:
            PXCM_STATUS_PARAM_UNSUPPORTED - Unsupported parameter.
            PXCM_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.
        */
        public Boolean DisableAlert(PXCMFaceData.AlertData.AlertType alertEvent)
        {
            return PXCMFaceConfiguration_DisableAlert(instance, alertEvent);
        }

        /** 
            @brief Disable all alerts messaging for all events.                        
            @return PXC_STATUS_NO_ERROR if disabling all alerts was successful; otherwise, return one of the following errors:
            PXCM_STATUS_PROCESS_FAILED - Module failure during processing.
            PXCM_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.
        */
        public void DisableAllAlerts()
        {
            PXCMFaceConfiguration_DisableAllAlerts(instance);
        }

        public delegate void OnFiredAlertDelegate(PXCMFaceData.AlertData alertData);

        /** 
            @brief Register an event handler object for the alerts. The event handler's OnFiredAlert method will be called each time an alert is identified.
            @param[in] alertHandler a pointer to the event handler.
            @see AlertHandler::OnFiredAlert
            @return PXCM_STATUS_NO_ERROR if the registering an event handler was successful; otherwise, return the following error:
            PXCM_STATUS_DATA_NOT_INITIALIZED - Data failed to initialize.        
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
            @brief Unsubscribe an event handler object for the alerts.
            @param[in] alertHandler a pointer to the event handler that should be removed.
            @return PXCM_STATUS_NO_ERROR if the unregistering the event handler was successful, an error otherwise.
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

        /* apply/commit configuration changes to MW*/
        public pxcmStatus ApplyChanges()
        {
            return PXCMFaceConfiguration_ApplyChanges(instance, configs);
        }

        /*  restore settings to default values */
        public void RestoreDefaults()
        {
            RestoreDefaultsINT(instance, out configs);
        }

        /**
        * @brief Updates data to latest available configuration.
        */
        public pxcmStatus Update()
        {
            return UpdateINT(instance, out configs);
        }

        public partial class GazeConfiguration
        {
            public Boolean isEnabled
            {
                get
                {
                    return face.configs.isGazeEnabled;
                }
                set
                {
                    face.configs.isGazeEnabled = value;
                }
            }

			/**
				loads previously calculated calib data
			*/
            public pxcmStatus LoadCalibration(Byte[] buffer) {
                if (buffer == null) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
                if (buffer.Length < QueryCalibDataSize()) return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
                return PXCMFaceConfiguration_GazeConfiguration_LoadCalibration(instance, buffer, buffer.Length);
            }

			/**
				retrieves calibration data size
			*/
			public Int32 QueryCalibDataSize() {
                return PXCMFaceConfiguration_GazeConfiguration_QueryCalibDataSize(instance);
            }

   			/**
				The actual dominant eye as entered by the user, modifying the optimal eye suggested by the calibration.
				An alternative option to setting the dominant eye would be to repeat calibration, QueryCalibDominantEye until desired result is reached.
				The dominant eye is a preference of visual input from one eye over the other.
				This is the eye relied on in the gaze inference algorithm.
			*/
			public void SetDominantEye(PXCMFaceData.GazeCalibData.DominantEye eye) {
                PXCMFaceConfiguration_GazeConfiguration_SetDominantEye(instance, eye);
            }

            private IntPtr instance;
            private PXCMFaceConfiguration face;

            internal GazeConfiguration(IntPtr instance, PXCMFaceConfiguration face)
            {
                this.instance = instance;
                this.face = face;
            }
        }

    	public GazeConfiguration QueryGaze() {
            IntPtr gaze = PXCMFaceConfiguration_QueryGaze(instance);
            return gaze != IntPtr.Zero ? new GazeConfiguration(gaze, this) : null;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ExpressionsPropertiesINT {
            public ExpressionsConfiguration.ExpressionsProperties properties;
        } 

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class AllFaceConfigurations
        {
            public IntPtr recognitionInstance;
            public IntPtr expressionInstance;
            public IntPtr pulseInsance;
            public DetectionConfiguration detection;
            public LandmarksConfiguration landmarks;
            public PoseConfiguration pose;
            public ExpressionsPropertiesINT expressions;
            public RecognitionConfiguration.RecognitionProperties recognitionProperties;
            public PulseConfiguration.PulseProperties pulseProperties;
            public RecognitionConfiguration.RecognitionStorageDesc storageDesc;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RecognitionConfiguration.STORAGE_NAME_SIZE)]
            public String storageName;
            public TrackingStrategyType strategy;
            [MarshalAs(UnmanagedType.Bool)]
            public Boolean isGazeEnabled; 

            public AllFaceConfigurations()
            {
                storageName = "";
                detection = new DetectionConfiguration();
                landmarks = new LandmarksConfiguration();
                pose = new PoseConfiguration();
                expressions = new ExpressionsPropertiesINT();
                recognitionProperties = new RecognitionConfiguration.RecognitionProperties();
                pulseProperties = new PulseConfiguration.PulseProperties();
                storageDesc = new RecognitionConfiguration.RecognitionStorageDesc();
            }
        };

        internal AllFaceConfigurations configs;

        internal class EventMaps
        {
            internal Dictionary<OnFiredAlertDelegate, Object> alert = new Dictionary<OnFiredAlertDelegate, Object>();
            internal Object cs = new Object();
        };

        internal EventMaps maps;

        internal PXCMFaceConfiguration(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            GetConfigurationsINT(instance, out configs);
            maps = new EventMaps();
        }

        internal PXCMFaceConfiguration(EventMaps maps, IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
            GetConfigurationsINT(instance, out configs);
            this.maps = maps;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
