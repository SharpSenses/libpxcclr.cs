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

    /** @class PXCMDataSmoothing 
	A utility that allows smoothing data of different types, using a variety of algorithms
*/
    public partial class PXCMDataSmoothing : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0x4f4d5344);

        /** @class Smoother1D
            Handles the smoothing of a stream of floats, using a specific smoothing algorithm
        */
        public partial class Smoother1D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x014d5344);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] newSample the latest data sample 
                @param[in] timestamp
                @return PXCM_STATUS_NO_ERROR		
            */
            public pxcmStatus AddSample(Single newSample, Int64 timestamp)
            {
                return PXCMDataSmoothing_Smoother1D_AddSample(instance, newSample, timestamp);
            }

            /**			
                 @brief Add a new data sample to the smoothing algorithm
                 @param[in] newSample the latest data sample              
                 @return PXCM_STATUS_NO_ERROR		
             */
            public pxcmStatus AddSample(Single newSample)
            {
                return AddSample(newSample, 0);
            }

            /**			
                 @brief Gets a new data sample from the smoothing algorithm
                 @param[in] timestamp
                 @return smoothed value in pxcF32 format	
             */
            public Single GetSample(Int64 timestamp)
            {
                return PXCMDataSmoothing_Smoother1D_GetSample(instance, timestamp);
            }

            /**			
                @brief Gets a new data sample from the smoothing algorithm          
                @return smoothed value in pxcF32 format	
            */
            public Single GetSample()
            {
                return GetSample(0);
            }

            /* constructors & misc */
            internal Smoother1D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
        };

        public partial class Smoother2D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x024d5344);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] newSample the latest data sample 
                @param[in] timestamp
                @return PXCM_STATUS_NO_ERROR		
            */
            public pxcmStatus AddSample(PXCMPointF32 newSample, Int64 timestamp)
            {
                return PXCMDataSmoothing_Smoother2D_AddSample(instance, newSample, timestamp);
            }

            /**			
               @brief Add a new data sample to the smoothing algorithm
               @param[in] newSample the latest data sample 
               @param[in] timestamp
               @return PXCM_STATUS_NO_ERROR		
           */
            public pxcmStatus AddSample(PXCMPointF32 newSample)
            {
                return AddSample(newSample, 0);
            }

            /**			
                 @brief Gets a new data sample from the smoothing algorithm
                 @param[in] timestamp
                 @return smoothed value in PXCPointF32 format		
             */
            public PXCMPointF32 GetSample(Int64 timestamp)
            {
                PXCMPointF32 point = new PXCMPointF32();
                PXCMDataSmoothing_Smoother2D_GetSample(instance, timestamp, out point);
                return point;
            }

            /**			
                @brief Gets a new data sample from the smoothing algorithm			
                @return smoothed value in PXCPointF32 format		
            */
            public PXCMPointF32 GetSample()
            {
                return GetSample(0);
            }

            /* constructors & misc */
            internal Smoother2D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
        };

        public partial class Smoother3D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x034d5344);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] newSample the latest data sample 
                @param[in] timestamp
                @return PXCM_STATUS_NO_ERROR		
            */
            public pxcmStatus AddSample(PXCMPoint3DF32 newSample, Int64 timestamp)
            {
                return PXCMDataSmoothing_Smoother3D_AddSample(instance, newSample, timestamp);
            }

            /**			
                 @brief Add a new data sample to the smoothing algorithm
                 @param[in] newSample the latest data sample 
                 @param[in] timestamp
                 @return PXCM_STATUS_NO_ERROR		
             */
            public pxcmStatus AddSample(PXCMPoint3DF32 newSample)
            {
                return AddSample(newSample, 0);
            }

            /**			
                @brief Gets a new data sample from the smoothing algorithm
                @param[in] timestamp
                @return smoothed value in PXCPoint3DF32 format		
            */
            public PXCMPoint3DF32 GetSample(Int64 timestamp)
            {
                PXCMPoint3DF32 point = new PXCMPoint3DF32();
                PXCMDataSmoothing_Smoother3D_GetSample(instance, timestamp, out point);
                return point;
            }

            /**			
                @brief Gets a new data sample from the smoothing algorithm			
                @return smoothed value in PXCPoint3DF32 format		
            */
            public PXCMPoint3DF32 GetSample()
            {
                return GetSample(0);
            }

            /* constructors & misc */
            internal Smoother3D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
        };

        /** @brief Create Stabilizer smoother instance for single floats
            The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
            @param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
            @param[in] stabilizeRadius The stabilizer smoother radius, default value is 0.03f
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create1DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        /** @brief Create Stabilizer smoother instance for single floats
        The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold    
        @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DStabilizer()
        {
            return Create1DStabilizer(0.5f, 0.03f);
        }

        /** @brief Create the Weighted algorithm for single floats
                The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
                If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
                @param[in] numWeights The Weighted smoother number of weights
                @param[in] weights The Weighted smoother weight values
                @return a pointer to the created Smoother, or NULL in case of illegal arguments
         */
        public Smoother1D Create1DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create1DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        /** @brief Create the Weighted algorithm for single floats
                The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
                If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
                @param[in] numWeights The Weighted smoother number of weights        
                @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DWeighted(Int32 numWeights)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create1DWeighted(instance, numWeights, null);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for single floats
            @param[in] smoothStrength The Quadratic smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create1DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for single floats
                @return a pointer to the created Smoother, or NULL in case of illegal arguments
         */
        public Smoother1D Create1DQuadratic()
        {
            return Create1DQuadratic(0.5f);
        }


        /** @brief Create the Spring algorithm for single floats
            @param[in] smoothStrength The Spring smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create1DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }


        /** @brief Create the Spring algorithm for single floats
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother1D Create1DSpring()
        {
            return Create1DSpring(0.5f);
        }

        /** @brief Create Stabilizer smoother instance for 2-dimensional points
            The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
            @param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
            @param[in] stabilizeRadius The stabilizer smoother radius, default value is 0.03f
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create2DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create Stabilizer smoother instance for 2-dimensional points
            The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DStabilizer()
        {
            return Create2DStabilizer(0.5f, 0.03f);
        }

        /** @brief Create the Weighted algorithm for 2-dimensional points
            The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
            If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)      
            @param[in] weights The Weighted smoother weight values
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create2DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Weighted algorithm for 2-dimensional points
            The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
            If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
            @param[in] numWeights The Weighted smoother number of weights        
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DWeighted(Int32 numWeighted)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create2DWeighted(instance, numWeighted, null);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 2-dimensional points
            @param[in] smoothStrength The Quadratic smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create2DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 2-dimensional points      
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DQuadratic()
        {
            return Create2DQuadratic(0.5f);
        }

        /** @brief Create the Spring algorithm for 2-dimensional points
            @param[in] smoothStrength The Spring smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create2DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Spring algorithm for 2-dimensional points        
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother2D Create2DSpring()
        {
            return Create2DSpring(0.5f);
        }

        /** @brief Create Stabilizer smoother instance for 3-dimensional points
                The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
                @param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
                @param[in] stabilizeRadius The stabilizer smoother radius, default value is 0.03f
                @return a pointer to the created Smoother, or NULL in case of illegal arguments
            */
        public Smoother3D Create3DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create3DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /**     @brief Create Stabilizer smoother instance for 3-dimensional points
                The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
                @return a pointer to the created Smoother, or NULL in case of illegal arguments
         */
        public Smoother3D Create3DStabilizer()
        {
            return Create3DStabilizer(0.5f, 0.03f);
        }

        /** @brief Create the Weighted algorithm for 3-dimensional points
            The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
            If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)    
            @param[in] weights The Weighted smoother weight values
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create3DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Weighted algorithm for 3-dimensional points
            The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
            If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
            @param[in] numWeights The Weighted smoother number of weights       
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DWeighted(Int32 numWeights)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create3DWeighted(instance, numWeights, null);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 3-dimensional points
            @param[in] smoothStrength The Quadratic smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create3DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 3-dimensional points   
             @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DQuadratic()
        {
            return Create3DQuadratic(0.5f);
        }

        /** @brief Create the Spring algorithm for 3-dimensional points
            @param[in] smoothStrength The Spring smoother smooth strength
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMDataSmoothing_Create3DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Spring algorithm for 3-dimensional points       
            @return a pointer to the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DSpring()
        {
            return Create3DQuadratic(0.5f);
        }

        /* constructors & misc */
        internal PXCMDataSmoothing(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
