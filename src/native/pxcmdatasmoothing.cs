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

    public partial class PXCMDataSmoothing : PXCMBase
    {
        public partial class Smoother1D : PXCMBase
        {
            [DllImport(DLLNAME)]
            internal extern static pxcmStatus PXCMDataSmoothing_Smoother1D_AddSample(IntPtr instance, Single newSample, Int64 timestamp);

            [DllImport(DLLNAME)]
            internal extern static Single PXCMDataSmoothing_Smoother1D_GetSample(IntPtr instance, Int64 timestamp);
        }

        public partial class Smoother2D : PXCMBase
        {
            [DllImport(DLLNAME)]
            internal extern static pxcmStatus PXCMDataSmoothing_Smoother2D_AddSample(IntPtr instance, PXCMPointF32 newSample, Int64 timestamp);

            [DllImport(DLLNAME)]
            internal extern static void PXCMDataSmoothing_Smoother2D_GetSample(IntPtr instance, Int64 timestamp, out PXCMPointF32 sample);
        }

        public partial class Smoother3D : PXCMBase
        {
            [DllImport(DLLNAME)]
            internal extern static pxcmStatus PXCMDataSmoothing_Smoother3D_AddSample(IntPtr instance, PXCMPoint3DF32 newSample, Int64 timestamp);

            [DllImport(DLLNAME)]
            internal extern static void PXCMDataSmoothing_Smoother3D_GetSample(IntPtr instance, Int64 timestamp, out PXCMPoint3DF32 sample);
        }

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create1DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create1DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create1DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create1DSpring(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create2DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create2DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create2DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create2DSpring(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create3DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create3DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create3DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMDataSmoothing_Create3DSpring(IntPtr instance, Single smoothStrength);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
