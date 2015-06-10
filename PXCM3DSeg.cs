using System;
using System.Runtime.InteropServices;

public class PXCM3DSeg : PXCMBase {
    public delegate void OnAlertDelegate(AlertData data);

    [Flags]
    public enum AlertEvent {
        ALERT_USER_IN_RANGE = 0,
        ALERT_USER_TOO_CLOSE = 1,
        ALERT_USER_TOO_FAR = 2
    }

    public new const int CUID = 826885971;
    private AlertHandlerDIR handler;

    internal PXCM3DSeg(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCM3DSeg_AcquireSegmentedImage(IntPtr seg);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCM3DSeg_Subscribe(IntPtr seg, IntPtr handler);

    private void SubscribeINT(OnAlertDelegate d) {
        if (handler != null) {
            handler.Dispose();
            handler = null;
        }
        if (d != null) {
            handler = new AlertHandlerDIR(d);
            PXCM3DSeg_Subscribe(instance, handler.dirUnmanaged);
        }
        else {
            PXCM3DSeg_Subscribe(instance, IntPtr.Zero);
        }
    }

    public PXCMImage AcquireSegmentedImage() {
        var instance = PXCM3DSeg_AcquireSegmentedImage(this.instance);
        if (!(instance != IntPtr.Zero)) {
            return null;
        }
        return new PXCMImage(instance, true);
    }

    public void Subscribe(OnAlertDelegate d) {
        SubscribeINT(d);
    }

    internal class AlertHandlerDIR : IDisposable {
        internal IntPtr dirUnmanaged;
        private GCHandle gch;
        internal OnAlertDelegate handler;

        public AlertHandlerDIR(OnAlertDelegate d) {
            handler = d;
            gch = GCHandle.Alloc(handler);
            dirUnmanaged = PXCM3DSeg_AllocHandlerDIR(Marshal.GetFunctionPointerForDelegate(d));
        }

        public void Dispose() {
            if (dirUnmanaged == IntPtr.Zero) {
                return;
            }
            PXCM3DSeg_FreeHandlerDIR(dirUnmanaged);
            dirUnmanaged = IntPtr.Zero;
            if (!gch.IsAllocated) {
                return;
            }
            gch.Free();
        }

        ~AlertHandlerDIR() {
            Dispose();
        }

        [DllImport("libpxccpp2c")]
        internal static extern IntPtr PXCM3DSeg_AllocHandlerDIR(IntPtr d);

        [DllImport("libpxccpp2c")]
        internal static extern void PXCM3DSeg_FreeHandlerDIR(IntPtr hdir);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AlertData {
        public long timeStamp;
        public AlertEvent label;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] internal int[] reserved;

        public AlertData() {
            reserved = new int[5];
        }
    }
}