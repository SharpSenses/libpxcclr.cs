using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

public class PXCMSenseManager : PXCMBase {
    public new const int CUID = -661306591;
    public const int TIMEOUT_INFINITE = -1;
    internal PXCMFaceConfiguration.EventMaps faceEvents = new PXCMFaceConfiguration.EventMaps();
    internal PXCMHandConfiguration.EventMaps handEvents = new PXCMHandConfiguration.EventMaps();
    private HandlerDIR handlerDIR;
    internal PXCMTouchlessController.EventMap tcEvents = new PXCMTouchlessController.EventMap();

    internal PXCMSenseManager(IntPtr instance, bool delete)
        : base(instance, delete) {}

    public PXCMSession session {
        get { return QuerySession(); }
    }

    public PXCMCaptureManager captureManager {
        get { return QueryCaptureManager(); }
    }

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSenseManager_QuerySession(IntPtr putil);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSenseManager_QueryCaptureManager(IntPtr putil);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSenseManager_QuerySample(IntPtr putil, int mid);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSenseManager_QueryModule(IntPtr putil, int mid);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSenseManager_Init(IntPtr putil, IntPtr hdir);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSenseManager_StreamFrames(IntPtr putil,
        [MarshalAs(UnmanagedType.Bool)] bool blocking);

    [DllImport("libpxccpp2c")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool PXCMSenseManager_IsConnected(IntPtr putil);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSenseManager_AcquireFrame(IntPtr putil,
        [MarshalAs(UnmanagedType.Bool)] bool ifall, int timeout);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMSenseManager_ReleaseFrame(IntPtr putil);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMSenseManager_FlushFrame(IntPtr putil);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMSenseManager_Close(IntPtr putil);

    public void Close() {
        PXCMSenseManager_Close(instance);
        if (handlerDIR == null) {
            return;
        }
        handlerDIR.Dispose();
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSenseManager_EnableStreams(IntPtr putil, PXCMVideoModule.DataDesc sdesc);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMSenseManager_EnableModule(IntPtr putil, int mid, PXCMSession.ImplDesc mdesc);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMSenseManager_PauseModule(IntPtr putil, int cuid,
        [MarshalAs(UnmanagedType.Bool)] bool pause);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMSenseManager_CreateInstance();

    public pxcmStatus Init(Handler handler) {
        if (handler == null) {
            return PXCMSenseManager_Init(instance, IntPtr.Zero);
        }
        if (handlerDIR != null) {
            handlerDIR.Dispose();
        }
        handlerDIR = new HandlerDIR(this, handler);
        return PXCMSenseManager_Init(instance, handlerDIR.dirUnmanaged);
    }

    public pxcmStatus StreamFrames(bool blocking) {
        return PXCMSenseManager_StreamFrames(instance, blocking);
    }

    public PXCMCapture.Sample QuerySample(int mid) {
        var instance = PXCMSenseManager_QuerySample(this.instance, mid);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMCapture.Sample(instance);
    }

    public PXCMSession QuerySession() {
        var instance = PXCMSenseManager_QuerySession(this.instance);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMSession(instance, false);
    }

    public PXCMCaptureManager QueryCaptureManager() {
        var instance = PXCMSenseManager_QueryCaptureManager(this.instance);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMCaptureManager(instance, false);
    }

    public PXCMCapture.Sample QuerySample() {
        return QuerySample(0);
    }

    public PXCMCapture.Sample Query3DSegSample() {
        return QuerySample(826885971);
    }

    public PXCMCapture.Sample QueryScenePerceptionSample() {
        return QuerySample(1347306323);
    }

    public PXCMCapture.Sample QueryTrackerSample() {
        return QuerySample(1380667988);
    }

    public PXCMCapture.Sample QueryFaceSample() {
        return QuerySample(1144209734);
    }

    public PXCMCapture.Sample QueryHandSample() {
        return QuerySample(1313751368);
    }

    public PXCMCapture.Sample QueryEmotionSample() {
        return QuerySample(1314147653);
    }

    public PXCMBase QueryModule(int mid) {
        if (mid == 1313751368) {
            return QueryFace();
        }
        if (mid == 1144209734) {
            return QueryHand();
        }
        if (mid == 1397443654) {
            return QueryTouchlessController();
        }
        var instance = PXCMSenseManager_QueryModule(this.instance, mid);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return IntPtr2PXCMBase(instance, mid);
    }

    public PXCM3DSeg Query3DSeg() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 826885971);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCM3DSeg(instance, false);
    }

    public PXCM3DScan Query3DScan() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 826884947);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCM3DScan(instance, false);
    }

    public PXCMScenePerception QueryScenePerception() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1347306323);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMScenePerception(instance, false);
    }

    public PXCMTracker QueryTracker() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1380667988);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMTracker(instance, false);
    }

    public PXCMFaceModule QueryFace() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1144209734);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMFaceModule(faceEvents, instance, false);
    }

    public PXCMEmotion QueryEmotion() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1314147653);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMEmotion(instance, false);
    }

    public PXCMTouchlessController QueryTouchlessController() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1397443654);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMTouchlessController(tcEvents, instance, false);
    }

    public PXCMHandModule QueryHand() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1313751368);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMHandModule(handEvents, instance, false);
    }

    public PXCMBlobModule QueryBlob() {
        var instance = PXCMSenseManager_QueryModule(this.instance, 1145916738);
        if (instance == IntPtr.Zero) {
            return null;
        }
        return new PXCMBlobModule(instance, false);
    }

    public pxcmStatus Init() {
        return Init(null);
    }

    public bool IsConnected() {
        return PXCMSenseManager_IsConnected(instance);
    }

    public pxcmStatus AcquireFrame(bool ifall, int timeout) {
        return PXCMSenseManager_AcquireFrame(instance, ifall, timeout);
    }

    public pxcmStatus AcquireFrame(bool ifall) {
        return AcquireFrame(ifall, -1);
    }

    public pxcmStatus AcquireFrame() {
        return AcquireFrame(true);
    }

    public void ReleaseFrame() {
        PXCMSenseManager_ReleaseFrame(instance);
    }

    public void FlushFrame() {
        PXCMSenseManager_FlushFrame(instance);
    }

    public pxcmStatus EnableStreams(PXCMVideoModule.DataDesc ddesc) {
        return PXCMSenseManager_EnableStreams(instance, ddesc);
    }

    public pxcmStatus EnableStream(PXCMCapture.StreamType type, int width, int height) {
        return EnableStream(type, width, height, 0.0f);
    }

    public pxcmStatus EnableStream(PXCMCapture.StreamType type, int width, int height, float fps) {
        var ddesc = new PXCMVideoModule.DataDesc();
        ddesc.deviceInfo.streams = type;
        var streamDesc = ddesc.streams[type];
        streamDesc.sizeMin.width = width;
        streamDesc.sizeMax.width = width;
        streamDesc.sizeMin.height = height;
        streamDesc.sizeMax.height = height;
        streamDesc.frameRate.min = fps;
        streamDesc.frameRate.max = fps;
        return EnableStreams(ddesc);
    }

    public pxcmStatus EnableModule(int mid, PXCMSession.ImplDesc mdesc) {
        return PXCMSenseManager_EnableModule(instance, mid, mdesc);
    }

    public pxcmStatus EnableModule(int mid, string name) {
        PXCMSession.ImplDesc mdesc = null;
        if (name != null) {
            mdesc = new PXCMSession.ImplDesc();
            mdesc.cuids[0] = mid;
            mdesc.friendlyName = name;
        }
        return EnableModule(mid, mdesc);
    }

    public pxcmStatus Enable3DSeg(string name) {
        return EnableModule(826885971, name);
    }

    public pxcmStatus Enable3DSeg() {
        return Enable3DSeg(null);
    }

    public pxcmStatus Enable3DScan(string name) {
        return EnableModule(826884947, name);
    }

    public pxcmStatus EnableScenePerception(string name) {
        return EnableModule(1347306323, name);
    }

    public pxcmStatus EnableScenePerception() {
        return EnableScenePerception(null);
    }

    public pxcmStatus Enable3DScan() {
        return Enable3DScan(null);
    }

    public pxcmStatus EnableTracker(string name) {
        return EnableModule(1380667988, name);
    }

    public pxcmStatus EnableTracker() {
        return EnableTracker(null);
    }

    public pxcmStatus EnableFace(string name) {
        return EnableModule(1144209734, name);
    }

    public pxcmStatus EnableFace() {
        return EnableFace(null);
    }

    public pxcmStatus EnableEmotion(string name) {
        return EnableModule(1314147653, name);
    }

    public pxcmStatus EnableEmotion() {
        return EnableEmotion(null);
    }

    public pxcmStatus EnableTouchlessController(string name) {
        return EnableModule(1397443654, name);
    }

    public pxcmStatus EnableTouchlessController() {
        return EnableTouchlessController(null);
    }

    public pxcmStatus EnableHand(string name) {
        return EnableModule(1313751368, name);
    }

    public pxcmStatus EnableHand() {
        return EnableHand(null);
    }

    public pxcmStatus EnableBlob(string name) {
        return EnableModule(1145916738, name);
    }

    public pxcmStatus EnableBlob() {
        return EnableBlob(null);
    }

    public void PauseModule(int cuid, bool pause) {
        PXCMSenseManager_PauseModule(instance, cuid, pause);
    }

    public void Pause3DSeg(bool pause) {
        PauseModule(826885971, pause);
    }

    public void PauseScenePerception(bool pause) {
        PauseModule(1347306323, pause);
    }

    public void PauseTracker(bool pause) {
        PauseModule(1380667988, pause);
    }

    public void PauseFace(bool pause) {
        PauseModule(1144209734, pause);
    }

    public void PauseEmotion(bool pause) {
        PauseModule(1314147653, pause);
    }

    public void PauseTouchlessController(bool pause) {
        PauseModule(1397443654, pause);
    }

    public void PauseHand(bool pause) {
        PauseModule(1313751368, pause);
    }

    public void PauseBlob(bool pause) {
        PauseModule(1145916738, pause);
    }

    public static PXCMSenseManager CreateInstance() {
        var instance = PXCMSenseManager_CreateInstance();
        var pxcmSenseManager = instance != IntPtr.Zero ? new PXCMSenseManager(instance, true) : null;
        if (pxcmSenseManager == null) {
            return pxcmSenseManager;
        }
        try {
            var pxcmSession = pxcmSenseManager.QuerySession();
            if (pxcmSession == null) {
                return pxcmSenseManager;
            }
            try {
                var pxcmMetadata = pxcmSession.QueryInstance<PXCMMetadata>();
                if (pxcmMetadata == null) {
                    return pxcmSenseManager;
                }
                var s = "CSharp";
                if (!string.IsNullOrEmpty(s)) {
                    var num = (int) pxcmMetadata.AttachBuffer(1296451664, Encoding.Unicode.GetBytes(s));
                }
            }
            catch (Exception ex) {
                pxcmSession.Dispose();
            }
        }
        catch {}
        return pxcmSenseManager;
    }

    internal class HandlerDIR : IDisposable {
        internal IntPtr dirUnmanaged;
        private List<GCHandle> gchandles;
        private Handler handler;
        private PXCMSenseManager sm;

        public HandlerDIR(PXCMSenseManager sm, Handler handler) {
            this.sm = sm;
            this.handler = handler;
            var hs = new HandlerSet();
            gchandles = new List<GCHandle>();
            if (handler.onConnect != null) {
                OnConnectDIRDelegate connectDirDelegate = OnConnect;
                gchandles.Add(GCHandle.Alloc(connectDirDelegate));
                hs.onConnect = Marshal.GetFunctionPointerForDelegate(connectDirDelegate);
            }
            if (handler.onModuleSetProfile != null) {
                OnModuleSetProfileDIRDelegate profileDirDelegate = OnModuleSetProfile;
                gchandles.Add(GCHandle.Alloc(profileDirDelegate));
                hs.onModuleSetProfile = Marshal.GetFunctionPointerForDelegate(profileDirDelegate);
            }
            if (handler.onModuleProcessedFrame != null) {
                OnModuleProcessedFrameDIRDelegate frameDirDelegate = OnModuleProcessedFrame;
                gchandles.Add(GCHandle.Alloc(frameDirDelegate));
                hs.onModuleProcessedFrame = Marshal.GetFunctionPointerForDelegate(frameDirDelegate);
            }
            if (handler.onNewSample != null) {
                OnNewSampleDIRDelegate sampleDirDelegate = OnNewSample;
                gchandles.Add(GCHandle.Alloc(sampleDirDelegate));
                hs.onNewSample = Marshal.GetFunctionPointerForDelegate(sampleDirDelegate);
            }
            if (handler.onStatus != null) {
                OnStatusDIRDelegate statusDirDelegate = OnStatus;
                gchandles.Add(GCHandle.Alloc(statusDirDelegate));
                hs.onStatus = Marshal.GetFunctionPointerForDelegate(statusDirDelegate);
            }
            dirUnmanaged = PXCMSenseManager_AllocHandlerDIR(hs);
        }

        public void Dispose() {
            if (dirUnmanaged == IntPtr.Zero) {
                return;
            }
            PXCMSenseManager_FreeHandlerDIR(dirUnmanaged);
            dirUnmanaged = IntPtr.Zero;
            foreach (var gcHandle in gchandles) {
                if (gcHandle.IsAllocated) {
                    gcHandle.Free();
                }
            }
            gchandles.Clear();
        }

        ~HandlerDIR() {
            Dispose();
        }

        [DllImport("libpxccpp2c")]
        internal static extern IntPtr PXCMSenseManager_AllocHandlerDIR(HandlerSet hs);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCMSenseManager_FreeHandlerDIR(IntPtr hdir);

        private pxcmStatus OnConnect(IntPtr device, bool connected) {
            return handler.onConnect(new PXCMCapture.Device(device, false), connected);
        }

        private pxcmStatus OnModuleSetProfile(int mid, IntPtr module) {
            if (mid == 1144209734) {
                return handler.onModuleSetProfile(mid, new PXCMFaceModule(sm.faceEvents, module, false));
            }
            if (mid == 1313751368) {
                return handler.onModuleSetProfile(mid, new PXCMHandModule(sm.handEvents, module, false));
            }
            return handler.onModuleSetProfile(mid, IntPtr2PXCMBase(module, mid));
        }

        private pxcmStatus OnModuleProcessedFrame(int mid, IntPtr module, IntPtr images) {
            var sample = new PXCMCapture.Sample(images);
            if (mid == 1144209734) {
                return handler.onModuleProcessedFrame(mid, new PXCMFaceModule(sm.faceEvents, module, false), sample);
            }
            if (mid == 1313751368) {
                return handler.onModuleProcessedFrame(mid, new PXCMHandModule(sm.handEvents, module, false), sample);
            }
            return handler.onModuleProcessedFrame(mid, IntPtr2PXCMBase(module, mid), sample);
        }

        private pxcmStatus OnNewSample(int mid, IntPtr images) {
            var sample = new PXCMCapture.Sample(images);
            return handler.onNewSample(mid, sample);
        }

        private void OnStatus(int mid, pxcmStatus sts) {
            handler.onStatus(mid, sts);
        }

        internal delegate pxcmStatus OnConnectDIRDelegate(IntPtr device, bool connected);

        internal delegate pxcmStatus OnModuleSetProfileDIRDelegate(int mid, IntPtr module);

        internal delegate pxcmStatus OnModuleProcessedFrameDIRDelegate(int mid, IntPtr module, IntPtr images);

        internal delegate pxcmStatus OnNewSampleDIRDelegate(int mid, IntPtr images);

        internal delegate void OnStatusDIRDelegate(int mid, pxcmStatus sts);

        [StructLayout(LayoutKind.Sequential)]
        internal class HandlerSet {
            internal IntPtr onConnect;
            internal IntPtr onModuleSetProfile;
            internal IntPtr onModuleProcessedFrame;
            internal IntPtr onNewSample;
            internal IntPtr onStatus;
        }
    }

    public class Handler {
        public delegate pxcmStatus OnConnectDelegate(PXCMCapture.Device device, bool connected);

        public delegate pxcmStatus OnModuleProcessedFrameDelegate(int mid, PXCMBase module, PXCMCapture.Sample sample);

        public delegate pxcmStatus OnModuleSetProfileDelegate(int mid, PXCMBase module);

        public delegate pxcmStatus OnNewSampleDelegate(int mid, PXCMCapture.Sample sample);

        public delegate void OnStatusDelegate(int mid, pxcmStatus sts);

        public OnConnectDelegate onConnect;
        public OnModuleProcessedFrameDelegate onModuleProcessedFrame;
        public OnModuleSetProfileDelegate onModuleSetProfile;
        public OnNewSampleDelegate onNewSample;
        public OnStatusDelegate onStatus;
    }
}