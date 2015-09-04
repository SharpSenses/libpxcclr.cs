/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if NETFX_CORE
using System.Reflection;
#endif

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSession : PXCMBase
    {
        new public const Int32 CUID = unchecked((Int32)0x20534553);

        /** 
            @structure ImplVersion
            Describe the video streams requested by a module implementation.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class ImplVersion
        {
            public Int16 major;        /* The major version number */
            public Int16 minor;        /* The minor version number */
        };

        /** 
            @enum ImplGroup
            The SDK group I/O and algorithm modules into groups and subgroups.
            This is the enumerator for algorithm groups.
        */
        [Flags]
        public enum ImplGroup
        {
            IMPL_GROUP_ANY = 0,             /* Undefine group */
            IMPL_GROUP_OBJECT_RECOGNITION = 0x00000001,    /* Object recognition algorithms */
            IMPL_GROUP_SPEECH_RECOGNITION = 0x00000002,    /* Speech recognition algorithms */
            IMPL_GROUP_SENSOR = 0x00000004,    /* I/O modules */
            IMPL_GROUP_PHOTOGRAPHY = 0x00000008,    /* Photography algorithms */
            IMPL_GROUP_UTILITIES = 0x00000010,  /* Utilities modules */

            IMPL_GROUP_CORE = unchecked((Int32)0x80000000),  /* Core SDK modules */
            IMPL_GROUP_USER = 0x40000000,    /* user defined algorithms */
        };

        /**
            @enum ImplSubgroup
            The SDK group I/O and algorithm modules into groups and subgroups.
            This is the enumerator for algorithm subgroups.
        */
        [Flags]
        public enum ImplSubgroup
        {
            IMPL_SUBGROUP_ANY = 0,            /* Undefine subgroup */

            /* object recognition building blocks */
            IMPL_SUBGROUP_FACE_ANALYSIS = 0x00000001,   /* face analysis subgroup */
            IMPL_SUBGROUP_GESTURE_RECOGNITION = 0x00000010,   /* gesture recognition subgroup */
            IMPL_SUBGROUP_SEGMENTATION = 0x00000020,   /* segmentation subgroup */
            IMPL_SUBGROUP_PULSE_ESTIMATION = 0x00000040,   /* pulse estimation subgroup */
            IMPL_SUBGROUP_EMOTION_RECOGNITION = 0x00000080,   /* emotion recognition subgroup */
            IMPL_SUBGROUP_OBJECT_TRACKING = 0x000000100, /* object tracking subgroup */
            IMPL_SUBGROUP_3DSEG = 0x00000200,   /* 3D segmentation subgroup */
            IMPL_SUBGROUP_3DSCAN = 0x00000400,   /* mesh capture subgroup */
            IMPL_SUBGROUP_SCENE_PERCEPTION = 0x00000800,   /* scene perception subgroup */

            /* Photography building blocks */
            IMPL_SUBGROUP_ENHANCED_PHOTOGRAPHY = 0x00001000,   /* scene perception subgroup */

            /* sensor building blocks */
            IMPL_SUBGROUP_AUDIO_CAPTURE = 0x00000001,   /* audio capture subgroup */
            IMPL_SUBGROUP_VIDEO_CAPTURE = 0x00000002,   /* video capture subgroup */

            /* speech recognition building blocks */
            IMPL_SUBGROUP_SPEECH_RECOGNITION = 0x00000001,   /* speech recognition subgroup */
            IMPL_SUBGROUP_SPEECH_SYNTHESIS = 0x00000002,   /* speech synthesis subgroup */
        };

        /**
            @enum CoordinateSystem
            SDK supports several 3D coordinate systems for front and rear facing cameras.
        */
        [Flags]
        public enum CoordinateSystem
        {
            COORDINATE_SYSTEM_REAR_DEFAULT = 0x100,    /* Right-hand system: X right, Y up, Z to the user */
            COORDINATE_SYSTEM_REAR_OPENCV = 0x200,    /* Right-hand system: X right, Y down, Z to the world */
            COORDINATE_SYSTEM_FRONT_DEFAULT = 0x001,    /* Left-hand system: X left, Y up, Z to the user */
        };

        /** 
            @structure ImplDesc
            The module descriptor lists details about the module implementation.
        */
        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class ImplDesc
        {
            public ImplGroup group;          /* implementation group */
            public ImplSubgroup subgroup;       /* implementation sub-group */
            public Int32 algorithm;      /* algorithm identifier */
            public Int32 iuid;           /* implementation unique id */
            public ImplVersion version;        /* implementation version */
            public Int32 reserved2;
            public Int32 merit;          /* implementation merit */
            public Int32 vendor;         /* vendor */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Int32[] cuids;                          /* interfaces supported by implementation */
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public String friendlyName;                    /* friendly name */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            internal Int32[] reserved;

            public ImplDesc()
            {
                cuids = new Int32[4];
                friendlyName = "";
                reserved = new Int32[12];
            }
        };

        /** 
            @brief Create an instance of the specified module.
            @param[in]	desc					The module descriptor.
            @param[in]	iuid					Optional module implementation identifier.
            @param[in]	cuid					Optional interface identifier.
            @param[out]	instance				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl(ImplDesc desc, Int32 iuid, Int32 cuid, out PXCMBase impl)
        {
            IntPtr impl2;
            pxcmStatus sts = PXCMSession_CreateImpl(instance, desc, iuid, cuid, out impl2);
            impl = null;
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                impl = new PXCMBase(impl2, false).QueryInstance(cuid);
                impl.AddRef();
            }
            return sts;
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	desc					The module descriptor.
            @param[in]	cuid					Optional interface identifier.
            @param[out]	instance				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl(ImplDesc desc, Int32 cuid, out PXCMBase instance)
        {
            return CreateImpl(desc, 0, cuid, out instance);
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	iuid					Optional module implementation identifier.
            @param[in]	cuid					Optional interface identifier.
            @param[out]	instance				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl(Int32 iuid, Int32 cuid, out PXCMBase instance)
        {
            return CreateImpl(null, iuid, cuid, out instance);
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	cuid					Optional interface identifier.
            @param[out]	instance				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl(Int32 cuid, out PXCMBase instance)
        {
            return CreateImpl(null, 0, cuid, out instance);
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	desc					The module descriptor.
            @param[in]	iuid					Optional module implementation identifier.
            @param[out]	tt				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl<TT>(ImplDesc desc, Int32 iuid, out TT tt) where TT : PXCMBase
        {
            PXCMBase tt2;
#if !NETFX_CORE
            pxcmStatus sts = CreateImpl(desc, iuid, Type2CUID[typeof(TT)], out tt2);
#else
        pxcmStatus sts = CreateImpl(desc, iuid, Type2CUID[typeof(TT).GetTypeInfo()], out tt2);
#endif
            tt = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? (TT)tt2 : null;
            return sts;
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	desc					The module descriptor.
            @param[out]	tt				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl<TT>(ImplDesc desc, out TT tt) where TT : PXCMBase
        {
            PXCMBase tt2;
#if !NETFX_CORE
            pxcmStatus sts = CreateImpl(desc, Type2CUID[typeof(TT)], out tt2);
#else
        pxcmStatus sts = CreateImpl(desc, Type2CUID[typeof(TT).GetTypeInfo()], out tt2);
#endif
            tt = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? (TT)tt2 : null;
            return sts;
        }

        /** 
            @brief Create an instance of the specified module.
            @param[in]	iuid					Optional module implementation identifier.
            @param[out]	tt				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl<TT>(Int32 iuid, out TT tt) where TT : PXCMBase
        {
            PXCMBase tt2;
#if !NETFX_CORE
            pxcmStatus sts = CreateImpl(iuid, Type2CUID[typeof(TT)], out tt2);
#else
        pxcmStatus sts = CreateImpl(iuid, Type2CUID[typeof(TT).GetTypeInfo()], out tt2);
#endif
            tt = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? (TT)tt2 : null;
            return sts;
        }

        /** 
            @brief Create an instance of the specified module.
            @param[out]	tt				The created instance, to be returned.
            @return PXCM_STATUS_NO_ERROR			Successful execution.
            @return PXCM_STATUS_ITEM_UNAVAILABLE	No matched module implementation.
        */
        public pxcmStatus CreateImpl<TT>(out TT tt) where TT : PXCMBase
        {
            PXCMBase tt2;
#if !NETFX_CORE
            pxcmStatus sts = CreateImpl(Type2CUID[typeof(TT)], out tt2);
#else
        pxcmStatus sts = CreateImpl(Type2CUID[typeof(TT).GetTypeInfo()], out tt2);
#endif
            tt = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? (TT)tt2 : null;
            return sts;
        }

        /** 
            @brief Create an instance of the PXCMSenseManager interface.
            @return The PXCMSenseManager instance.
        */
        public PXCMSenseManager CreateSenseManager()
        {
            PXCMSenseManager senseManager;
            pxcmStatus sts = CreateImpl<PXCMSenseManager>(out senseManager);
            return (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? senseManager : null;
        }

        /** 
            @brief Create an instance of the PXCMCaptureManager interface.
            @return The PXCMCaptureManager instance.
        */
        public PXCMCaptureManager CreateCaptureManager()
        {
            PXCMCaptureManager captureManager;
            pxcmStatus sts = CreateImpl<PXCMCaptureManager>(out captureManager);
            return (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? captureManager : null;
        }

        /** 
            @brief Create an instance of the PXCMAudioSource interface.
            @return The PXCMAudioSource instance.
        */
        public PXCMAudioSource CreateAudioSource()
        {
            PXCMAudioSource source;
            pxcmStatus sts = CreateImpl<PXCMAudioSource>(out source);
            return (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? source : null;
        }

        /** 
            @brief Create an instance of the PXCMImage interface with data. The application must
            maintain the life cycle of the image data for the PXCMImage instance.
            @param[in]  info	The format and resolution of the image.
            @param[in]	data	Optional image data.
            @return The PXCMImage instance.
        */
        public PXCMImage CreateImage(PXCMImage.ImageInfo info, PXCMImage.ImageData data)
        {
            IntPtr image = PXCMSession_CreateImage(instance, info, data);
            return (image != IntPtr.Zero) ? new PXCMImage(image, true) : null;
        }

        /** 
            @brief Create an instance of the PXCMImage interface.
            @param[in]  info	The format and resolution of the image.
            @return The PXCMImage instance.
        */
        public PXCMImage CreateImage(PXCMImage.ImageInfo info)
        {
            return CreateImage(info, null);
        }

        /** 
            @brief Create an instance of the PXCMPhoto interface.
            @return The PXCMImage instance.
        */
        public PXCMPhoto CreatePhoto()
        {
            PXCMPhoto photo = null;
            pxcmStatus sts=CreateImpl<PXCMPhoto>(out photo);
            return sts<pxcmStatus.PXCM_STATUS_NO_ERROR?null:photo;
        }

        /** 
            @brief Create an instance of the PXCMAudio interface with data. The application must
            maintain the life cycle of the audio data for the PXCMAudio instance.
            @param[in]  info	The audio channel information.
            @param[in]	data	Optional audio data.
            @return The PXCMAudio instance.
        */
        public PXCMAudio CreateAudio(PXCMAudio.AudioInfo info, PXCMAudio.AudioData data)
        {
            IntPtr audio = PXCMSession_CreateAudio(instance, info, data);
            return (audio != IntPtr.Zero) ? new PXCMAudio(audio, true) : null;
        }

        /** 
            @brief Create an instance of the PXCAudio interface.
            @param[in]  info	The audio channel information.
            @return The PXCMAudio instance.
        */
        public PXCMAudio CreateAudio(PXCMAudio.AudioInfo info)
        {
            return CreateAudio(info, null);
        }

        /** 
            @brief Load the module from a file.
            @param[in]  moduleName		The module file name.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus LoadImplFromFile(String moduleName)
        {
            return PXCMSession_LoadImplFromFile(instance, moduleName);
        }

        /** 
            @brief Unload the specified module.
            @param[in]  moduleName		    The module file name.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public pxcmStatus UnloadImplFromFile(String moduleName)
        {
            return PXCMSession_UnloadImplFromFile(instance, moduleName);
        }

        /** 
            @brief Set the camera coordinate system.
            @param[in]  cs          The coordinate system.
            @return PXC_STATUS_NO_ERROR    Successful execution.
        */
        public pxcmStatus SetCoordinateSystem(CoordinateSystem cs)
        {
            return PXCMSession_SetCoordinateSystem(instance, cs);
        }

        /** 
            @brief Return current camera coordinate system (bit-mask of coordinate systems for front and rear cameras) 
            @param[in]  cs          The coordinate system.
            @return PXC_STATUS_NO_ERROR    Successful execution.
        */
        public CoordinateSystem QueryCoordinateSystem()
        {
            return PXCMSession_QueryCoordinateSystem(instance);
        }

        /** 
            @brief Create an instance of the PXCSession interface.
            @param[out]	session			The PXCSession instance, to be returned.
            @return PXCM_STATUS_NO_ERROR	Successful execution.
        */
        public static PXCMSession CreateInstance()
        {
            IntPtr session2 = PXCMSession_CreateInstance();
            PXCMSession session = (session2 != IntPtr.Zero) ? new PXCMSession(session2, true) : null;
            if (session == null) return null;
            try
            {
                PXCMMetadata md = session.QueryInstance<PXCMMetadata>();
                if (md == null) return session;

                string frameworkName = null;
#if PH_CS
                frameworkName = "CSharp";
#elif PH_UNITY
                frameworkName = "Unity";
#endif
                if (!string.IsNullOrEmpty(frameworkName))
                    md.AttachBuffer(1296451664, System.Text.Encoding.Unicode.GetBytes(frameworkName));
            }
            catch (Exception) {}
            return session;
        }

        internal PXCMSession(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif

#if NETFX_CORE
public sealed class SerializableAttribute : Attribute
{
};
#endif