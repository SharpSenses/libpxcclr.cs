/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text; 

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMSpeechRecognition : PXCMBase
    {
        internal partial class HandlerDIR : IDisposable
        {
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern IntPtr PXCMSpeechRecognition_AllocHandlerDIR(HandlerSet hs);
            [DllImport(PXCMBase.DLLNAME)]
            internal static extern void PXCMSpeechRecognition_FreeHandlerDIR(IntPtr hdir);

            internal delegate void OnRecognitionDIRDelegate(IntPtr data);
            internal delegate void OnAlertDIRDelegate(AlertData data);

            [StructLayout(LayoutKind.Sequential)]
            internal class HandlerSet
            {
                internal IntPtr onRecognition;
                internal IntPtr onAlert;
            };

            private Handler handler;
            private List<GCHandle> gchandles;
            internal IntPtr dirUnmanaged;

            private void OnRecognition(IntPtr data)
            {
                /* Custom Marshalling */
                RecognitionData data2 = new RecognitionData();
                data2.timeStamp = Marshal.ReadInt64(data, 0);
                data2.grammar = Marshal.ReadInt32(data, 8);
                data2.duration = Marshal.ReadInt32(data, 12);

                Int64 addr = data.ToInt64() + 16;
                for (int i = 0; i < NBEST_SIZE; i++)
                {
                    Marshal.PtrToStructure(new IntPtr(addr), data2.scores[i]);
                    addr += Marshal.SizeOf(typeof(NBest));
                }
                handler.onRecognition(data2);
            }

            private void OnAlert(AlertData data)
            {
                handler.onAlert(data);
            }

            public void Dispose()
            {
                if (dirUnmanaged == IntPtr.Zero) return;

                PXCMSpeechRecognition_FreeHandlerDIR(dirUnmanaged);
                dirUnmanaged = IntPtr.Zero;

                foreach (GCHandle gch in gchandles)
                {
                    if (gch.IsAllocated) gch.Free();
                }
                gchandles.Clear();
            }

            public HandlerDIR(Handler handler)
            {
                this.handler = handler;
                gchandles = new List<GCHandle>();
                if (handler != null)
                {
                    HandlerSet handlerSet = new HandlerSet();
                    if (handler.onRecognition != null)
                    {
                        OnRecognitionDIRDelegate dir = new OnRecognitionDIRDelegate(OnRecognition);
                        gchandles.Add(GCHandle.Alloc(dir));
                        handlerSet.onRecognition = Marshal.GetFunctionPointerForDelegate(dir);
                    }
                    if (handler.onAlert != null)
                    {
                        OnAlertDIRDelegate dir = new OnAlertDIRDelegate(OnAlert);
                        gchandles.Add(GCHandle.Alloc(dir));
                        handlerSet.onAlert = Marshal.GetFunctionPointerForDelegate(dir);
                    }
                    dirUnmanaged = PXCMSpeechRecognition_AllocHandlerDIR(handlerSet);
                }
            }

            ~HandlerDIR()
            {
                Dispose();
            }
        };

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_QueryProfile(IntPtr voice, Int32 pidx, [Out] ProfileInfo pinfo);

        private pxcmStatus QueryProfileINT(Int32 pidx, out ProfileInfo pinfo)
        {
            pinfo = new ProfileInfo();
            return PXCMSpeechRecognition_QueryProfile(instance, pidx, pinfo);
        }

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_SetProfile(IntPtr voice, ProfileInfo pinfo);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromStringList(IntPtr voice, Int32 gid, String[] cmds, Int32[] labels, Int32 ncmds);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_ReleaseGrammar(IntPtr voice, Int32 gid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_SetGrammar(IntPtr voice, Int32 gid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_DeleteGrammar(IntPtr voice, Int32 gid);

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_StartRec(IntPtr voice, IntPtr source, IntPtr handler);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromFile(IntPtr voice, Int32 gid, GrammarFileType fileType, String grammarFilename);

        [DllImport(DLLNAME)]
        internal static extern pxcmStatus PXCMSpeechRecognition_BuildGrammarFromMemory(IntPtr voice, Int32 gid, GrammarFileType fileType, Byte[] grammaryMemory, Int32 memSize);

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        private static extern void PXCMSpeechRecognition_GetGrammarCompileErrors(IntPtr voice, Int32 gid, StringBuilder error, Int32 nchars);

        internal static String GetGrammarCompileErrorsINT(IntPtr voice, Int32 gid)
        {
            StringBuilder error = new StringBuilder(1024);
            PXCMSpeechRecognition_GetGrammarCompileErrors(voice, gid, error, error.Capacity);
            return error.ToString();
        }

        [DllImport(DLLNAME, CharSet = CharSet.Unicode)]
        internal static extern pxcmStatus PXCMSpeechRecognition_AddVocabToDictation(IntPtr voice, VocabFileType fileType, String vocabFileName);

        public pxcmStatus StartRecINT(IntPtr source, Handler handler)
        {
            if (handlerDIR != null) handlerDIR.Dispose();
            handlerDIR = new HandlerDIR(handler);
            return PXCMSpeechRecognition_StartRec(instance, source, handlerDIR.dirUnmanaged);
        }

        [DllImport(PXCMBase.DLLNAME)]
        internal static extern void PXCMSpeechRecognition_StopRec(IntPtr voice);

        private void StopRecINT()
        {
            PXCMSpeechRecognition_StopRec(instance);
            if (handlerDIR != null) handlerDIR.Dispose();
        }

        private HandlerDIR handlerDIR = null;
    };

#if RSSDK_IN_NAMESPACE
}
#endif
