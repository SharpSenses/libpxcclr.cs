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
using UnityEngine;

public partial class PXCMImage:PXCMBase {
    public partial class ImageData
    {
		/* P/Invoke Functions */

        [DllImport(PXCMBase.DLLNAME, EntryPoint = "PXCMImage_ImageData_EndianSwap")]
        private static extern void PXCMImage_ImageData_EndianSwapC(IntPtr src, Int32 spitch, IntPtr dst, Int32 dpitch, Int32 width, Int32 height);

        private static object cs = new object();
        private static Color32[] buffer = new Color32[1];

		public Texture2D ToTexture2D(Int32 index, Int32 width, Int32 height) {
			Texture2D image=new Texture2D((Int32)width, (Int32)height, TextureFormat.RGBA32, false);
            return ToTexture2D(index, image);
		}

		public Texture2D ToTexture2D(Int32 index, Texture2D image) {
            lock (cs)
            {
                if (buffer.Length != image.width*image.height)
                    buffer = new Color32[image.width*image.height];

                GCHandle gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                PXCMImage_ImageData_EndianSwapC(planes[index], pitches[index], gch.AddrOfPinnedObject(), image.width * sizeof(Int32), (Int32)image.width, (Int32)image.height);
                gch.Free();
                image.SetPixels32(buffer);
            }
			return image;
		}
		
		public void FromTexture2D(Int32 index, Texture2D image) {
			Color32[] pixels=image.GetPixels32();
            GCHandle pinnedHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            PXCMImage_ImageData_EndianSwapC(pinnedHandle.AddrOfPinnedObject(), image.width * sizeof(Int32), planes[index], pitches[index], (Int32)image.width, (Int32)image.height);
            pinnedHandle.Free();
		}
    };
};
