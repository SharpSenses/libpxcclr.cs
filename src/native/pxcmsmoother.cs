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

    public partial class PXCMSmoother : PXCMBase
    {
        public partial class Smoother1D : PXCMBase
        {
            [DllImport(DLLNAME)]
            internal extern static Single PXCMSmoother_Smoother1D_SmoothValue(IntPtr instance, Single value);

            [DllImport(DLLNAME)]
            internal extern static void PXCMSmoother_Smoother1D_Reset(IntPtr instance);
        }

        public partial class Smoother2D : PXCMBase
        {
            [DllImport(DLLNAME)]
            internal extern static PXCMPointF32 PXCMSmoother_Smoother2D_SmoothValue(IntPtr instance, PXCMPointF32 value);

            [DllImport(DLLNAME)]
            internal extern static void PXCMSmoother_Smoother2D_Reset(IntPtr instance);
        }

        public partial class Smoother3D : PXCMBase
        {

            [DllImport(DLLNAME)]
            internal extern static PXCMPoint3DF32 PXCMSmoother_Smoother3D_SmoothValue(IntPtr instance, PXCMPoint3DF32 value);

            [DllImport(DLLNAME)]
            internal extern static void PXCMSmoother_Smoother3D_Reset(IntPtr instance);
        }

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create1DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create1DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create1DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create1DSpring(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create2DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create2DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create2DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create2DSpring(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create3DStabilizer(IntPtr instance, Single stabilizeStrength, Single stabilizeRadius);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create3DWeighted(IntPtr instance, Int32 nweights, Single[] weights);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create3DQuadratic(IntPtr instance, Single smoothStrength);

        [DllImport(DLLNAME)]
        internal extern static IntPtr PXCMSmoother_Create3DSpring(IntPtr instance, Single smoothStrength);
    };

#if RSSDK_IN_NAMESPACE
}
#endif
