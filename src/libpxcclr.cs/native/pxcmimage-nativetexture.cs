/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class NativeTexturePlugin : MonoBehaviour
{
    /* P/Invoke Functions */
    [DllImport(PXCMBase.DLLNAME)]
    internal static extern IntPtr GetRenderEventFunc();

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern void CopyTextureData(System.IntPtr srcPXCImage, System.IntPtr dstTexture);

    [DllImport(PXCMBase.DLLNAME)]
    internal static extern void ClearQueue();

#if PH_UNITY
    /// <summary>
    /// Enable and Query the NativeTexturePlugin instance.
    /// Texture2Ds must be created with correct PXCMImage.info.width, PXCMImage.info.hight and TextureFormat.BGRA32.
    /// Only DX9, DX11, OpenGL2 supported.
    /// Color and Depth supported.
    /// </summary>
    /// <returns>NativeTexturePlugin instance.</returns>
    public static NativeTexturePlugin Activate()
    {
        UnityEngine.GameObject obj = new UnityEngine.GameObject("NativeTextureHandler");
        obj.AddComponent<NativeTexturePlugin>();
        return obj.GetComponent<NativeTexturePlugin>();
    }
#endif

    void Start()
    {
        StartCoroutine("CallPluginAtEndOfFrames");
    }

    private IEnumerator CallPluginAtEndOfFrames()
    {
        while (true)
        {
            // Wait until all frame rendering is done
            yield return new WaitForEndOfFrame();

            // Issue a plugin event with arbitrary integer identifier.
            GL.IssuePluginEvent(GetRenderEventFunc(), 1);
        }
    }

    /// <summary>
    /// Texture2Ds passed need to be of correct PXCMImage.info.width, PXCMImage.info.height and TextureFormat.BGRA.
    /// Supports DX9, DX11 and OpenGL2.
    /// </summary>
    /// <param name="srcimage">color or depth PXCMImage</param>
    /// <param name="texture2DPtr">Retrieve using Texture2D.GetNativeTexturePtr()</param>
    public void UpdateTextureNative(PXCMImage srcimage, System.IntPtr texture2DPtr)
    {
        if (srcimage != null && texture2DPtr != System.IntPtr.Zero)
            CopyTextureData(srcimage.QueryNativePointer(), texture2DPtr);
    }

    void OnDisable()
    {
        StopCoroutine("CallPluginAtEndOfFrames");
        ClearQueue();
    }

};
