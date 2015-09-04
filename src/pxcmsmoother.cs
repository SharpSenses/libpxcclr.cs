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

    /** @class PXCMSmoother 
	    A utility that allows smoothing data of different types, using a variety of algorithms
	    Stabilizer Smoother – The stabilizer smoother keeps the smoothed data point stable as long as it has not moved more than a given threshold. 
	    Weighted Smoother – The weighted smoother applies a (possibly) different weight to each of the previous data samples. 
	    Quadratic Smoother – The quadratic smoother is a time based smoother ideal for UI (User Interface) purposes. 
	    Spring Smoother – The spring smoother is a time based smoother ideal for gaming purposes. 
    */
    public partial class PXCMSmoother : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0x4f4d5553);

        /** @class Smoother1D
            Handles the smoothing of a stream of floats, using a specific smoothing algorithm
        */
        public partial class Smoother1D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x014d5553);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] value the latest data sample 
                @return smoothed value of Single type 		
            */
            public Single SmoothValue(Single value)
            {
                return PXCMSmoother_Smoother1D_SmoothValue(instance, value);
            }

            /* constructors & misc */
            internal Smoother1D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
            /**			
                @brief Reset smoother algorithm data
            */	
            public void Reset()
            {
                PXCMSmoother_Smoother1D_Reset(instance);
            }
        };

        public partial class Smoother2D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x024d5553);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] value the latest data sample 
                @return smoothed value in PXCMPointF32 format		
            */
            public PXCMPointF32 SmoothValue(PXCMPointF32 value)
            {
                return PXCMSmoother_Smoother2D_SmoothValue(instance, value);
            }

            /* constructors & misc */
            internal Smoother2D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
            /**			
                @brief Reset smoother algorithm data
            */	
            public void Reset()
            {
                PXCMSmoother_Smoother2D_Reset(instance);
            }
        };

        public partial class Smoother3D : PXCMBase
        {
            new public const Int32 CUID = unchecked((Int32)0x034d5553);

            /**			
                @brief Add a new data sample to the smoothing algorithm
                @param[in] value the latest data sample 
                @return smoothed value in PXCMPoint3DF32 format		
            */
            public PXCMPoint3DF32 SmoothValue(PXCMPoint3DF32 value)
            {
                return PXCMSmoother_Smoother3D_SmoothValue(instance, value);
            }

            /* constructors & misc */
            internal Smoother3D(IntPtr instance, Boolean delete)
                : base(instance, delete)
            {
            }
            /**			
                @brief Reset smoother algorithm data
            */	
            public void Reset()
            {
                PXCMSmoother_Smoother3D_Reset(instance);
            }
        };

        /** @brief Create Stabilizer smoother instance for single floats
            The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
            @param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
            @param[in] stabilizeRadius The stabilizer smoother radius in correlation to the input unit value
            @return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother1D Create1DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMSmoother_Create1DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        public Smoother1D Create1DStabilizer(Single stabilizeRadius)
        {
            return Create1DStabilizer(0.5f, stabilizeRadius);
        }

        /** @brief Create the Weighted algorithm for single floats
			The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
			If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
			@param[in] weights The Weighted smoother weight values
			@return an object of the created Smoother, or null in case of illegal arguments
         */
        public Smoother1D Create1DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMSmoother_Create1DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        public Smoother1D Create1DWeighted(Int32 numWeights)
        {
            IntPtr inst2 = PXCMSmoother_Create1DWeighted(instance, numWeights, null);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for single floats
            @param[in] smoothStrength The Quadratic smoother smooth strength, default value is 0.5f
            @return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother1D Create1DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create1DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        public Smoother1D Create1DQuadratic()
        {
            return Create1DQuadratic(0.5f);
        }


        /** @brief Create the Spring algorithm for single floats
			@param[in] smoothStrength The Spring smoother smooth strength, default value is 0.5f
			@return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother1D Create1DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create1DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother1D(inst2, true);
        }

        public Smoother1D Create1DSpring()
        {
            return Create1DSpring(0.5f);
        }

        /** @brief Create Stabilizer smoother instance for 2-dimensional points
			The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
			@param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
			@param[in] stabilizeRadius The stabilizer smoother radius in correlation to the input unit value
			@return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother2D Create2DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMSmoother_Create2DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        public Smoother2D Create2DStabilizer(Single stabilizeRadius)
        {
            return Create2DStabilizer(0.5f, stabilizeRadius);
        }

        /** @brief Create the Weighted algorithm for 2-dimensional points
			The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
			If the weights vector is not assigned (null) all the weights will be equal (1/numWeights)
			@param[in] weights The Weighted smoother weight values
			@return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother2D Create2DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMSmoother_Create2DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        public Smoother2D Create2DWeighted(Int32 numWeights)
        {
            IntPtr inst2 = PXCMSmoother_Create2DWeighted(instance, numWeights, null);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 2-dimensional points
            @param[in] smoothStrength The Quadratic smoother smooth strength, default value is 0.5f
            @return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother2D Create2DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create2DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 2-dimensional points      
            @return an object of the created Smoother
        */
        public Smoother2D Create2DQuadratic()
        {
            return Create2DQuadratic(0.5f);
        }

        /** @brief Create the Spring algorithm for 2-dimensional points
            @param[in] smoothStrength The Spring smoother smooth strength
            @return a an object of pointer to the created Smoother, or null in case of illegal arguments
        */
        public Smoother2D Create2DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create2DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother2D(inst2, true);
        }

        /** @brief Create the Spring algorithm for 2-dimensional points        
            @return an object of the created Smoother
        */
        public Smoother2D Create2DSpring()
        {
            return Create2DSpring(0.5f);
        }

        /** @brief Create Stabilizer smoother instance for 3-dimensional points
			The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
			@param[in] stabilizeStrength The stabilizer smoother strength, default value is 0.5f
			@param[in] stabilizeRadius The stabilizer smoother radius in correlation to the input unit value
			@return an object of the created Smoother, or null in case of illegal arguments
            */
        public Smoother3D Create3DStabilizer(Single stabilizeStrength, Single stabilizeRadius)
        {
            IntPtr inst2 = PXCMSmoother_Create3DStabilizer(instance, stabilizeStrength, stabilizeRadius);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create Stabilizer smoother instance for 3-dimensional points
            The stabilizer keeps the smoothed data point stable as long as it has not moved more than a given threshold
            @param[in] stabilizeRadius The stabilizer smoother radius in correlation to the input unit value
            @return an object of the created Smoother
         */
        public Smoother3D Create3DStabilizer(Single stabilizeRadius)
        {
            return Create3DStabilizer(0.5f, stabilizeRadius);
        }

        /** @brief Create the Weighted algorithm for 3-dimensional points
			The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
			If the weights vector is not assigned (null) all the weights will be equal (1/numWeights)
			@param[in] weights The Weighted smoother weight values
			@return an object of the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DWeighted(Single[] weights)
        {
            IntPtr inst2 = PXCMSmoother_Create3DWeighted(instance, weights.Length, weights);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Weighted algorithm for 3-dimensional points
            The Weighted algorithm applies a (possibly) different weight to each of the previous data samples
            If the weights vector is not assigned (NULL) all the weights will be equal (1/numWeights)
            @param[in] numWeights The Weighted smoother number of weights       
            @return a pointer to thean object of the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DWeighted(Int32 numWeights)
        {
            IntPtr inst2 = PXCMSmoother_Create3DWeighted(instance, numWeights, null);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 3-dimensional points
            @param[in] smoothStrength The Quadratic smoother smooth strength, default value is 0.5f
            @return an object of the created Smoother, or null in case of illegal arguments
        */
        public Smoother3D Create3DQuadratic(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create3DQuadratic(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Quadratic algorithm for 3-dimensional points   
            @return an object of the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DQuadratic()
        {
            return Create3DQuadratic(0.5f);
        }

        /** @brief Create the Spring algorithm for 3-dimensional points
            @param[in] smoothStrength The Spring smoother smooth strength, default value is 0.5f
            @return an object of the created Smoother, or NULL in case of illegal arguments
        */
        public Smoother3D Create3DSpring(Single smoothStrength)
        {
            IntPtr inst2 = PXCMSmoother_Create3DSpring(instance, smoothStrength);
            return inst2 == IntPtr.Zero ? null : new Smoother3D(inst2, true);
        }

        /** @brief Create the Spring algorithm for 3-dimensional points       
            @return an object of the created Smoother
        */
        public Smoother3D Create3DSpring()
        {
            return Create3DQuadratic(0.5f);
        }

        /* constructors & misc */
        internal PXCMSmoother(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
