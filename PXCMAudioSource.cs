using System;
using System.Runtime.InteropServices;

public class PXCMAudioSource : PXCMBase {
    public new const int CUID = -666790621;

    internal PXCMAudioSource(IntPtr instance, bool delete)
        : base(instance, delete) {
    }

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMAudioSource_ScanDevices(IntPtr source);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMAudioSource_QueryDeviceNum(IntPtr source);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudioSource_QueryDeviceInfo(IntPtr source, int didx, [Out] DeviceInfo dinfo);

    internal static pxcmStatus QueryDeviceInfoINT(IntPtr source, int didx, out DeviceInfo dinfo) {
        dinfo = new DeviceInfo();
        return PXCMAudioSource_QueryDeviceInfo(source, didx, dinfo);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudioSource_SetDevice(IntPtr source, DeviceInfo dinfo);

    [DllImport("libpxccpp2c")]
    internal static extern float PXCMAudioSource_QueryVolume(IntPtr source);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudioSource_SetVolume(IntPtr source, float volume);

    public void ScanDevices() {
        PXCMAudioSource_ScanDevices(instance);
    }

    public int QueryDeviceNum() {
        return PXCMAudioSource_QueryDeviceNum(instance);
    }

    public pxcmStatus QueryDeviceInfo(int didx, out DeviceInfo dinfo) {
        return QueryDeviceInfoINT(instance, didx, out dinfo);
    }

    public pxcmStatus QueryDeviceInfo(out DeviceInfo dinfo) {
        return QueryDeviceInfo(-1, out dinfo);
    }

    public pxcmStatus SetDevice(DeviceInfo dinfo) {
        return PXCMAudioSource_SetDevice(instance, dinfo);
    }

    public float QueryVolume() {
        return PXCMAudioSource_QueryVolume(instance);
    }

    public pxcmStatus SetVolume(float volume) {
        return PXCMAudioSource_SetVolume(instance, volume);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class DeviceInfo {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string did;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        internal int[] reserved;

        public DeviceInfo() {
            name = "";
            did = "";
            reserved = new int[16];
        }
    }
}
