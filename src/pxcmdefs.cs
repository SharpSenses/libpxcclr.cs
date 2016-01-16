/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if UNITY
using UnityEngine;
#endif

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMRectI32
    {
        public Int32 x, y, w, h;

        public PXCMRectI32(Int32 x, Int32 y, Int32 w, Int32 h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMRectF32
    {
        public Single x, y, w, h;

        public PXCMRectF32(Single x, Single y, Single w, Single h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMSizeI32
    {
        public Int32 width, height;

        public PXCMSizeI32(Int32 width, Int32 height)
        {
            this.width = width;
            this.height = height;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMPointF32
    {
        public Single x, y;

#if UNITY
    static public implicit operator Vector2(PXCMPointF32 tmp)
    {
        return new Vector2(tmp.x, tmp.y);
    }

    static public implicit operator PXCMPointF32(Vector2 tmp)
    {
        return new PXCMPointF32(tmp.x, tmp.y);
    }
#endif

        public PXCMPointF32(Single x, Single y)
        {
            this.x = x;
            this.y = y;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMPointI32
    {
        public Int32 x, y;

        public PXCMPointI32(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMRangeF32
    {
        public Single min, max;

        public PXCMRangeF32(Single min, Single max)
        {
            this.min = min;
            this.max = max;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMSize3DF32
    {
        public Single width, height, depth;

#if UNITY
    static public implicit operator Vector3(PXCMSize3DF32 tmp)
    {
        return new Vector3(tmp.width, tmp.height, tmp.depth);
    }

    static public implicit operator PXCMSize3DF32(Vector3 tmp)
    {
        return new PXCMSize3DF32(tmp.x, tmp.y, tmp.z);
    }
#endif

        public PXCMSize3DF32(Single width, Single height, Single depth)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMPoint3DF32
    {
        public Single x, y, z;

#if UNITY
    static public implicit operator Vector3(PXCMPoint3DF32 tmp)
    {
        return new Vector3(tmp.x, tmp.y, tmp.z);
    }

    static public implicit operator PXCMPoint3DF32(Vector3 tmp)
    {
        return new PXCMPoint3DF32(tmp.x, tmp.y, tmp.z);
    }
#endif

        public PXCMPoint3DF32(Single x, Single y, Single z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMPoint4DF32
    {
        public Single x, y, z, w;

#if UNITY
    static public implicit operator Quaternion(PXCMPoint4DF32 tmp)
    {
        return new Quaternion(tmp.x, tmp.y, tmp.z, tmp.w);
    }

    static public implicit operator PXCMPoint4DF32(Quaternion tmp)
    {
        return new PXCMPoint4DF32(tmp.x, tmp.y, tmp.z, tmp.w);
    }
#endif

        public PXCMPoint4DF32(Single x, Single y, Single z, Single w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    };

    /** A type representing a 3d box with pxcF32 values */
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PXCMBox3DF32
    {
       
        public PXCMPoint3DF32 centerOffset, dimension;

        public PXCMBox3DF32(PXCMPoint3DF32 centerOffset, PXCMPoint3DF32 dimension)
        {
            this.centerOffset = centerOffset;
            this.dimension = dimension; 
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
