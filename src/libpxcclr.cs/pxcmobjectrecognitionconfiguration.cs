/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2015-2016 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/**
@class PXCMObjectRecognitionConfiguration
@brief Retrieve the current configuration of the ObjectRecognition module and set new configuration values.
@note Changes to PXCMObjectRecognitionConfiguration are applied only when ApplyChanges() is called.
*/
public partial class PXCMObjectRecognitionConfiguration : PXCMBase
    {
        new public const Int32 CUID = 0x434a424f;
        public const Int32 MAX_PATH_NAME = 256;

        public enum RecognitionConfidence
        {
            HIGH,
            MEDIUM,
            LOW
        };

        public enum RecognitionMode
        {
            SINGLE_RECOGNITION,
            LOCALIZATION,
            LOCALIZATION_AND_TRACKING,
            TRACKING_ONLY,
            PROPOSAL_ONLY
        };

        public enum LocalizationMechanism
        {
            LM_EDGE_BOXES,
            LM_OBJECTS_ON_PLANE,
            LM_CNN
        };

        [StructLayout(LayoutKind.Sequential)]
        public class RecognitionConfiguration
        {
            public RecognitionMode mode;
            public RecognitionConfidence confidence;
        };

        /**
        @brief Apply the configuration changes to the module.
        This method must be called in order for any configuration changes to apply.
        @return PXCM_STATUS_NO_ERROR - successful operation.
        @return PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                        
        */
        public pxcmStatus ApplyChanges()
        {
            return PXCMObjectRecognitionConfiguration_ApplyChanges(instance);
        }

        /**  
        @brief Restore configuration settings to the default values.
        @return PXCM_STATUS_NO_ERROR - successful operation.
        @return PXCM_STATUS_DATA_NOT_INITIALIZED - the configuration was not initialized.                  
        */
        public pxcmStatus RestoreDefaults()
        {
            return PXCMObjectRecognitionConfiguration_RestoreDefaults(instance);
        }

        /**
        @brief Return the current active recognition configuration.
        @return RecognitionConfig - the current configuration.
        */
        public RecognitionConfiguration QueryRecognitionConfiguration() {
            RecognitionConfiguration rc = new RecognitionConfiguration();
             PXCMObjectRecognitionConfiguration_QueryRecognitionConfiguration(instance, rc);
             return rc;
        }

        /**
        @brief Return the number of classes which supported by the recognition configuration.
        */
        public Int32 QueryNumberOfClasses() {
            return PXCMObjectRecognitionConfiguration_QueryNumberOfClasses(instance);
        }

        /**
        @brief Set the active recognition configuration.
        @param[in] rConfig - struct with the desired confidence and mode of recognition.
        @return PXCM_STATUS_NO_ERROR - on operation succeeded.
        @see PXCRecognitionConfig     
        */
        public pxcmStatus SetRecognitionConfiguration(RecognitionConfiguration config) {
            return PXCMObjectRecognitionConfiguration_SetRecognitionConfiguration(instance, config);
        }

        /** 
        @brief Return the active classifier model.
        @return the active classifier name
        */    
        public String QueryActiveClassifier() {
            return QueryActiveClassifierINT(instance);
        }


        /** 
        @brief Set the active classifier model.
        @The number of outputs must be corresponds to the correct number of outputs supported by the specified model
        @param[in] configFilePath - relative path to the classifier model file from RSSDK data path.
        @return PXCM_STATUS_NO_ERROR - operation succeeded. 
        */
        public pxcmStatus SetActiveClassifier(String configFilePath) {
            return PXCMObjectRecognitionConfiguration_SetActiveClassifier(instance, configFilePath);
        }
    
        /** 
        @brief Enable or disable the segmented image feature.
        @param[in] enable - boolean value to enable/disable the segmentation.
        @return PXCM_STATUS_NO_ERROR - operation succeeded.
        */    
        public pxcmStatus EnableSegmentation(Boolean enable) {
             return PXCMObjectRecognitionConfiguration_EnableSegmentation(instance, enable);
        }

        /** 
        @brief get the current state of segmented image feature.
        */   
        public Boolean  IsSegmentationEnabled() {
            return PXCMObjectRecognitionConfiguration_IsSegmentationEnabled(instance);
        }

        /** 
        @brief get the current state of absolute roi feature.    
        */
        public Boolean IsAbsoluteRoiEnabled()
        {
            return PXCMObjectRecognitionConfiguration_IsAbsoluteRoiEnabled(instance);
        }

        /** 
        @brief Add ROI to the classification. This roi is absolute and the image will croped by this roi
        @param[in] roi - ROI rectangle to be add.
        @return PXCM_STATUS_NO_ERROR - operation succeeded.
        */
        public pxcmStatus AddROI(PXCMRectF32 roi) {
            return PXCMObjectRecognitionConfiguration_AddROI(instance, roi);
        }


        /** 
        @brief Add ROI to the classification. this roi is absolute and the image will croped by this roi
        @param[in] roi - ROI rectangle to be add.  
        @return PXCM_STATUS_NO_ERROR - operation succeeded.
        */
        public pxcmStatus AddAbsoluteROI(PXCMRectF32 roi)
        {
            return PXCMObjectRecognitionConfiguration_AddAbsoluteROI(instance, roi);
        }


        /** 
        @brief Return the ROI.    
        @return the current ROI.         
        */
        public PXCMRectF32 QueryROI()
        {
            PXCMRectF32 roi = new PXCMRectF32();
            PXCMObjectRecognitionConfiguration_QueryROI(instance,ref  roi);
            return roi;
        }

    
        /** 
        @brief Return the absolute ROI.
        @return the current absolute ROI.
        */    
        public PXCMRectI32 QueryAbsoluteROI()
        {
            PXCMRectI32 roi = new PXCMRectI32();
            PXCMObjectRecognitionConfiguration_QueryAbsoluteROI(instance,ref  roi);
            return roi;
        }
        
        /** 
        @brief Query the current localization mechanism.
        @return the current localization mechanism.
        */    
        public LocalizationMechanism  QueryLocalizationMechanism()
        {
            LocalizationMechanism lm = new LocalizationMechanism();
            PXCMObjectRecognitionConfiguration_QueryLocalizationMechanism(instance,ref lm);
            return lm;
        }

        /** 
        @brief Set the active localization mechanism.
        @brief The localization mechanism is activated only if LOCALIZATION or LOCALIZATION_AND_TRACKING or PROPOSAL_ONLY  mode have been selected. 
        @param[in] lm - the selected localization method.
        @see LocalizationMechanism enum.
        @return PXC_STATUS_NO_ERROR - operation succeeded.
        */
        public pxcmStatus  SetLocalizationMechanism(LocalizationMechanism lm)
         {
             return PXCMObjectRecognitionConfiguration_SetLocalizationMechanism(instance, lm);
         }

        /** 
        @brief Remove all ROIs from the classification and set the ROI to the whole image.
        @return PXCM_STATUS_NO_ERROR - operation succeeded.      
        */
        public void ClearAllROIs()
        {
            PXCMObjectRecognitionConfiguration_ClearAllROIs(instance);
        }

        /** 
        @brief Set or unset the absolute ROI feature.
        @param[in] enable - boolean value to enable/disable the absolute ROI.
        */
        public void EnableAbsoluteROI(Boolean enable)
        {
            PXCMObjectRecognitionConfiguration_EnableAbsoluteROI(instance, enable);
        }

        /** 
        @brief Set the initial rois to track by tracking module .
        @brief Enables only when tracking bool is on and all other are off.
        @brief The number of rectangles will be between 1 and 5.
        @param[in] rois - pointer to the rectangles tracked by tracker module.
        @param[in] nRois - number the rectangles tracked by tracker module.
        @return the status of the operation.
        */
        public pxcmStatus SetTrackingROIs(PXCMRectF32[] rois)
        {
            if (rois.Length < 1 || rois.Length > 5 || rois == null)
                return pxcmStatus.PXCM_STATUS_PARAM_UNSUPPORTED;

            return PXCMObjectRecognitionConfiguration_SetTrackingROIs(instance, rois, rois.Length);
        }


        internal PXCMObjectRecognitionConfiguration(IntPtr instance, Boolean delete)
            : base(instance, delete) { }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
