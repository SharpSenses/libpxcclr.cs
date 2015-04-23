using System;
using System.Runtime.InteropServices;

public class PXCMAudio : PXCMBase {
    public new const int CUID = 962214344;

    public AudioInfo info {
        get {
            return QueryInfo();
        }
    }

    public long timeStamp {
        get {
            return QueryTimeStamp();
        }
        set {
            SetTimeStamp(value);
        }
    }

    internal PXCMAudio(IntPtr instance, bool delete)
        : base(instance, delete) {
    }

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMAudio_QueryInfo(IntPtr audio, [Out] AudioInfo info);

    public AudioInfo QueryInfo() {
        AudioInfo info = new AudioInfo();
        PXCMAudio_QueryInfo(instance, info);
        return info;
    }

    [DllImport("libpxccpp2c")]
    internal static extern long PXCMAudio_QueryTimeStamp(IntPtr audio);

    [DllImport("libpxccpp2c")]
    internal static extern Option PXCMAudio_QueryOptions(IntPtr audio);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMAudio_SetTimeStamp(IntPtr audio, long ts);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMAudio_SetOptions(IntPtr audio, Option options);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudio_CopyAudio(IntPtr audio, IntPtr src_audio);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudio_AcquireAccess(IntPtr audio, Access access, AudioFormat format, Option options, IntPtr data);

    public pxcmStatus AcquireAccess(Access access, AudioFormat format, Option options, out AudioData data) {
        AudioDataNative audioDataNative = new AudioDataNative();
        audioDataNative.native = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AudioDataNative)));
        data = audioDataNative;
        pxcmStatus pxcmStatus = PXCMAudio_AcquireAccess(instance, access, format, options, audioDataNative.native);
        if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            IntPtr num = audioDataNative.native;
            Marshal.PtrToStructure(audioDataNative.native, audioDataNative);
            audioDataNative.native = num;
        }
        else {
            Marshal.FreeHGlobal(audioDataNative.native);
            audioDataNative.native = IntPtr.Zero;
        }
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMAudio_ReleaseAccess(IntPtr audio, IntPtr data);

    public pxcmStatus ReleaseAccess(AudioData data) {
        AudioDataNative audioDataNative = data as AudioDataNative;
        if (audioDataNative == null || audioDataNative.native == IntPtr.Zero)
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        Marshal.WriteInt32(audioDataNative.native, Marshal.OffsetOf(typeof(AudioData), "dataSize").ToInt32(), data.dataSize);
        pxcmStatus status = PXCMAudio_ReleaseAccess(instance, audioDataNative.native);
        Marshal.FreeHGlobal(audioDataNative.native);
        audioDataNative.native = IntPtr.Zero;
        return status;
    }

    public static string AudioFormatToString(AudioFormat format) {
        switch (format) {
            case AudioFormat.AUDIO_FORMAT_PCM:
                return "PCM";
            case AudioFormat.AUDIO_FORMAT_IEEE_FLOAT:
                return "Float";
            default:
                return "Unknown";
        }
    }

    public static int AudioFormatToSize(AudioFormat format) {
        return (int) (format & (AudioFormat) 255) >> 3;
    }

    public long QueryTimeStamp() {
        return PXCMAudio_QueryTimeStamp(instance);
    }

    public Option QueryOptions() {
        return PXCMAudio_QueryOptions(instance);
    }

    public void SetTimeStamp(long ts) {
        PXCMAudio_SetTimeStamp(instance, ts);
    }

    public void SetOptions(Option options) {
        PXCMAudio_SetOptions(instance, options);
    }

    public pxcmStatus CopyAudio(PXCMAudio src_audio) {
        return PXCMAudio_CopyAudio(instance, src_audio.instance);
    }

    public pxcmStatus AcquireAccess(Access access, AudioFormat format, out AudioData data) {
        return AcquireAccess(access, format, Option.OPTION_ANY, out data);
    }

    public pxcmStatus AcquireAccess(Access access, out AudioData data) {
        return AcquireAccess(access, 0, out data);
    }

    public void AddRef() {
        QueryInstance(1397965122).AddRef();
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AudioData {
        public AudioFormat format;
        public int dataSize;
        public IntPtr dataPtr;

        public byte[] ToByteArray(byte[] dest) {
            Marshal.Copy(dataPtr, dest, 0, dest.Length);
            return dest;
        }

        public short[] ToShortArray(short[] dest) {
            Marshal.Copy(dataPtr, dest, 0, dest.Length);
            return dest;
        }

        public float[] ToFloatArray(float[] dest) {
            Marshal.Copy(dataPtr, dest, 0, dest.Length);
            return dest;
        }

        public void FromByteArray(byte[] src) {
            dataSize = src.Length / ((int) (format & (AudioFormat) 255) / 8);
            Marshal.Copy(src, 0, dataPtr, src.Length);
        }

        public void FromShortArray(short[] src) {
            dataSize = src.Length;
            Marshal.Copy(src, 0, dataPtr, src.Length);
        }

        public void FromFloatArray(float[] src) {
            dataSize = src.Length;
            Marshal.Copy(src, 0, dataPtr, src.Length);
        }

        public byte[] ToByteArray() {
            return ToByteArray(new byte[dataSize * (int) (format & (AudioFormat) 255) / 8]);
        }

        public short[] ToShortArray() {
            return ToShortArray(new short[dataSize]);
        }

        public float[] ToFloatArray() {
            return ToFloatArray(new float[dataSize]);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private class AudioDataNative : AudioData {
        public IntPtr native;
    }

    [Flags]
    public enum AudioFormat {
        AUDIO_FORMAT_PCM = 1296257040,
        AUDIO_FORMAT_IEEE_FLOAT = 1414284832
    }

    [Flags]
    public enum ChannelMask {
        CHANNEL_MASK_FRONT_LEFT = 1,
        CHANNEL_MASK_FRONT_RIGHT = 2,
        CHANNEL_MASK_FRONT_CENTER = 4,
        CHANNEL_MASK_LOW_FREQUENCY = 8,
        CHANNEL_MASK_BACK_LEFT = 16,
        CHANNEL_MASK_BACK_RIGHT = 32,
        CHANNEL_MASK_SIDE_LEFT = 512,
        CHANNEL_MASK_SIDE_RIGHT = 1024
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class AudioInfo {
        public int bufferSize;
        public AudioFormat format;
        public int sampleRate;
        public int nchannels;
        public ChannelMask channelMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        internal int[] reserved;

        public AudioInfo() {
            reserved = new int[3];
        }
    }

    [Flags]
    public enum Option {
        OPTION_ANY = 0
    }

    [Flags]
    public enum Access {
        ACCESS_READ = 1,
        ACCESS_WRITE = 2,
        ACCESS_READ_WRITE = ACCESS_WRITE | ACCESS_READ
    }
}
