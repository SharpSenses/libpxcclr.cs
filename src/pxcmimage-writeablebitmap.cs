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
using System.Windows.Media.Imaging;
using System.Windows.Media;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMImage : PXCMBase
    {
        public partial class ImageData
        {
            public WriteableBitmap ToWritableBitmap(Int32 index, Int32 width, Int32 height, Double dpiX, Double dpiY)
            {
                WriteableBitmap bitmap = null;
                switch (format)
                {
                    case PixelFormat.PIXEL_FORMAT_RGB32:
                        bitmap = new WriteableBitmap((Int32)width, (Int32)height, dpiX, dpiY, PixelFormats.Bgr32, null);
                        bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, (Int32)width, (Int32)height), planes[index], pitches[index] * (Int32)height, pitches[index]);
                        break;
                    case PixelFormat.PIXEL_FORMAT_RGB24:
                        bitmap = new WriteableBitmap((Int32)width, (Int32)height, dpiX, dpiY, PixelFormats.Bgr24, null);
                        bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, (Int32)width, (Int32)height), planes[index], pitches[index] * (Int32)height, pitches[index]);
                        break;
                    case PixelFormat.PIXEL_FORMAT_DEPTH:
                    case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                        bitmap = new WriteableBitmap((Int32)width, (Int32)height, dpiX, dpiY, PixelFormats.Gray16, null);
                        bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, (Int32)width, (Int32)height), planes[index], pitches[index] * (Int32)height, pitches[index]);
                        break;
                }
                return bitmap;
            }

            public WriteableBitmap ToWritableBitmap(Int32 width, Int32 height, Double dpiX, Double dpiY)
            {
                return ToWritableBitmap(0, width, height, dpiX, dpiY);
            }
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif