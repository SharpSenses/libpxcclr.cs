/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMImage : PXCMBase
    {
        public partial class ImageData
        {
            public Bitmap ToBitmap(Int32 index, Int32 width, Int32 height)
            {
                Bitmap bitmap = null;
                switch (format)
                {
                    case PixelFormat.PIXEL_FORMAT_RGB32:
                        bitmap = new Bitmap(width, height, pitches[index], System.Drawing.Imaging.PixelFormat.Format32bppArgb, planes[0]);
                        break;
                    case PixelFormat.PIXEL_FORMAT_RGB24:
                        bitmap = new Bitmap(width, height, pitches[index], System.Drawing.Imaging.PixelFormat.Format24bppRgb, planes[0]);
                        break;
                    case PixelFormat.PIXEL_FORMAT_DEPTH:
                    case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                        bitmap = new Bitmap(width, height, pitches[index], System.Drawing.Imaging.PixelFormat.Format16bppGrayScale, planes[index]);
                        break;
                    default:
                        return null;
                }
                return bitmap;
            }

            public Bitmap ToBitmap(Int32 index, Bitmap bitmap)
            {
                BitmapData bdata;
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                switch (format)
                {
                    case PixelFormat.PIXEL_FORMAT_RGB32:
                        bdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        PXCMImage_ImageData_Copy(planes[index], pitches[index], bdata.Scan0, bdata.Stride, (Int32)bitmap.Width * 4, (Int32)bitmap.Height);
                        bitmap.UnlockBits(bdata);
                        break;
                    case PixelFormat.PIXEL_FORMAT_RGB24:
                        bdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        PXCMImage_ImageData_Copy(planes[index], pitches[index], bdata.Scan0, bdata.Stride, (Int32)bitmap.Width * 3, (Int32)bitmap.Height);
                        bitmap.UnlockBits(bdata);
                        break;
                    case PixelFormat.PIXEL_FORMAT_DEPTH:
                    case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                        bdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                        PXCMImage_ImageData_Copy(planes[index], pitches[index], bdata.Scan0, bdata.Stride, (Int32)bitmap.Width * 2, (Int32)bitmap.Height);
                        bitmap.UnlockBits(bdata);
                        break;
                    default:
                        return null;
                }
                return bitmap;
            }

        };
    };

#if RSSDK_IN_NAMESPACE
}
#endif
