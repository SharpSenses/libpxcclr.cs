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

public partial class PXCMRotation : PXCMBase
{

    [DllImport(DLLNAME)]
    internal extern static PXCMPoint3DF32 PXCMRotation_QueryEulerAngles(IntPtr instance, EulerOrder order);

    [DllImport(DLLNAME)]
    internal extern static PXCMPoint4DF32 PXCMRotation_QueryQuaternion(IntPtr instance);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_QueryRotationMatrix(IntPtr instance, [Out] Single[,] rotationMatrix);

    [DllImport(DLLNAME)]
    internal extern static AngleAxis PXCMRotation_QueryAngleAxis(IntPtr instance);

    [DllImport(DLLNAME)]
    internal extern static Single PXCMRotation_QueryRoll(IntPtr instance);

    [DllImport(DLLNAME)]
    internal extern static Single PXCMRotation_QueryYaw(IntPtr instance);

    [DllImport(DLLNAME)]
    internal extern static Single PXCMRotation_QueryPitch(IntPtr instance);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_Rotate(IntPtr instance, IntPtr rotation_instance);

    [DllImport(DLLNAME)]
    internal extern static PXCMPoint3DF32 PXCMRotation_RotateVector(IntPtr instance, PXCMPoint3DF32 vec);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_SetFromQuaternion(IntPtr instance, PXCMPoint4DF32 quaternion);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_SetFromEulerAngles(IntPtr instance, PXCMPoint3DF32 eulerAngles, PXCMRotation.EulerOrder order);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_SetFromRotationMatrix(IntPtr instance, Single[,] rotationMatrix);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_SetFromAngleAxis(IntPtr instance, Single angle, PXCMPoint3DF32 axis);

    [DllImport(DLLNAME)]
    internal extern static void PXCMRotation_SetFromSlerp(IntPtr instance, Single factor, IntPtr startRotation_instance, IntPtr endRotation_instance);
};

#if RSSDK_IN_NAMESPACE
}
#endif
