using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public class PXCMImage : PXCMBase {
    [Flags]
    public enum Access {
        ACCESS_READ = 1,
        ACCESS_WRITE = 2,
        ACCESS_READ_WRITE = ACCESS_WRITE | ACCESS_READ
    }

    [Flags]
    public enum Option {
        OPTION_ANY = 0
    }

    [Flags]
    public enum PixelFormat {
        PIXEL_FORMAT_ANY = 0,
        PIXEL_FORMAT_YUY2 = 65536,
        PIXEL_FORMAT_NV12 = 65537,
        PIXEL_FORMAT_RGB32 = 65538,
        PIXEL_FORMAT_RGB24 = 65539,
        PIXEL_FORMAT_Y8 = 65540,
        PIXEL_FORMAT_DEPTH = 131072,
        PIXEL_FORMAT_DEPTH_RAW = 131073,
        PIXEL_FORMAT_DEPTH_F32 = 131074,
        PIXEL_FORMAT_Y16 = 262144,
        PIXEL_FORMAT_Y8_IR_RELATIVE = 524288
    }

    public new const int CUID = 611585910;
    public const int NUM_OF_PLANES = 4;
    public const int METADATA_DEVICE_PROPERTIES = 1632724787;
    public const int METADATA_DEVICE_PROJECTION = 893810778;

    internal PXCMImage(IntPtr instance, bool delete)
        : base(instance, delete) {}

    public ImageInfo info {
        get { return QueryInfo(); }
    }

    public PXCMCapture.StreamType streamType {
        get { return QueryStreamType(); }
        set { SetStreamType(value); }
    }

    public long timeStamp {
        get { return QueryTimeStamp(); }
        set { SetTimeStamp(value); }
    }

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMImage_QueryInfo(IntPtr image, [Out] ImageInfo info);

    public ImageInfo QueryInfo() {
        var info = new ImageInfo();
        PXCMImage_QueryInfo(instance, info);
        return info;
    }

    [DllImport("libpxccpp2c")]
    internal static extern PXCMCapture.StreamType PXCMImage_QueryStreamType(IntPtr image);

    [DllImport("libpxccpp2c")]
    internal static extern long PXCMImage_QueryTimeStamp(IntPtr image);

    [DllImport("libpxccpp2c")]
    internal static extern Option PXCMImage_QueryOptions(IntPtr image);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMImage_SetStreamType(IntPtr image, PXCMCapture.StreamType type);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMImage_SetTimeStamp(IntPtr image, long ts);

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMImage_SetOptions(IntPtr image, Option options);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMImage_CopyImage(IntPtr image, IntPtr src_image);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMImage_ExportData(IntPtr image, ImageData data, int flags);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMImage_ImportData(IntPtr image, ImageData data, int flags);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMImage_AcquireAccess(IntPtr image, Access access, PixelFormat format,
        Option options, IntPtr data);

    public pxcmStatus AcquireAccess(Access access, PixelFormat format, Option options, out ImageData data) {
        var imageDataNative = new ImageDataNative();
        imageDataNative.native = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (ImageDataNative)));
        data = imageDataNative;
        var pxcmStatus = PXCMImage_AcquireAccess(instance, access, format, options, imageDataNative.native);
        if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR) {
            var num = imageDataNative.native;
            Marshal.PtrToStructure(imageDataNative.native, imageDataNative);
            imageDataNative.native = num;
        }
        else {
            Marshal.FreeHGlobal(imageDataNative.native);
            imageDataNative.native = IntPtr.Zero;
        }
        return pxcmStatus;
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMImage_ReleaseAccess(IntPtr image, IntPtr data);

    public pxcmStatus ReleaseAccess(ImageData data) {
        var imageDataNative = data as ImageDataNative;
        if (imageDataNative == null || imageDataNative.native == IntPtr.Zero) {
            return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
        }
        var status = PXCMImage_ReleaseAccess(instance, imageDataNative.native);
        Marshal.FreeHGlobal(imageDataNative.native);
        imageDataNative.native = IntPtr.Zero;
        return status;
    }

    public static string PixelFormatToString(PixelFormat format) {
        switch (format) {
            case PixelFormat.PIXEL_FORMAT_YUY2:
                return "YUY2";
            case PixelFormat.PIXEL_FORMAT_NV12:
                return "NV12";
            case PixelFormat.PIXEL_FORMAT_RGB32:
                return "RGB32";
            case PixelFormat.PIXEL_FORMAT_RGB24:
                return "RGB24";
            case PixelFormat.PIXEL_FORMAT_Y8:
                return "Y8";
            case PixelFormat.PIXEL_FORMAT_DEPTH:
                return "DEPTH";
            case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                return "DEPTH(NATIVE)";
            case PixelFormat.PIXEL_FORMAT_DEPTH_F32:
                return "DEPTH(FLOAT)";
            case PixelFormat.PIXEL_FORMAT_Y16:
                return "Y16";
            default:
                return "Unknown";
        }
    }

    public long QueryTimeStamp() {
        return PXCMImage_QueryTimeStamp(instance);
    }

    public PXCMCapture.StreamType QueryStreamType() {
        return PXCMImage_QueryStreamType(instance);
    }

    public Option QueryOptions() {
        return PXCMImage_QueryOptions(instance);
    }

    public void SetTimeStamp(long ts) {
        PXCMImage_SetTimeStamp(instance, ts);
    }

    public void SetStreamType(PXCMCapture.StreamType type) {
        PXCMImage_SetStreamType(instance, type);
    }

    public void SetOptions(Option options) {
        PXCMImage_SetOptions(instance, options);
    }

    public pxcmStatus CopyImage(PXCMImage src_image) {
        return PXCMImage_CopyImage(instance, src_image.instance);
    }

    public pxcmStatus ExportData(ImageData data, int flags) {
        return PXCMImage_ExportData(instance, data, flags);
    }

    public pxcmStatus ImportData(ImageData data, int flags) {
        return PXCMImage_ImportData(instance, data, flags);
    }

    public pxcmStatus AcquireAccess(Access access, out ImageData data) {
        return AcquireAccess(access, PixelFormat.PIXEL_FORMAT_ANY, out data);
    }

    public pxcmStatus AcquireAccess(Access access, PixelFormat format, out ImageData data) {
        return AcquireAccess(access, format, Option.OPTION_ANY, out data);
    }

    public void AddRef() {
        using (var pxcmBase = QueryInstance(1397965122)) {
            if (pxcmBase == null) {
                return;
            }
            pxcmBase.AddRef();
        }
    }

    internal static IntPtr[] Array2IntPtrs(PXCMImage[] images) {
        var numArray = new IntPtr[images.Length];
        for (var index = 0; index < images.Length; ++index) {
            if (images[index] != null) {
                numArray[index] = images[index].instance;
            }
        }
        return numArray;
    }

    [StructLayout(LayoutKind.Sequential, Size = 48)]
    public class ImageData {
        public PixelFormat format;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] internal int[] reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public int[] pitches;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public IntPtr[] planes;

        public ImageData() {
            pitches = new int[4];
            planes = new IntPtr[4];
            reserved = new int[3];
        }

        public Bitmap ToBitmap(int index, int width, int height) {
            Bitmap bitmap = null;
            switch (format) {
                case PixelFormat.PIXEL_FORMAT_RGB32:
                    bitmap = new Bitmap(width, height, pitches[index], System.Drawing.Imaging.PixelFormat.Format32bppRgb,
                        planes[0]);
                    break;
                case PixelFormat.PIXEL_FORMAT_RGB24:
                    bitmap = new Bitmap(width, height, pitches[index], System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                        planes[0]);
                    break;
                case PixelFormat.PIXEL_FORMAT_DEPTH:
                case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                    bitmap = new Bitmap(width, height, pitches[index],
                        System.Drawing.Imaging.PixelFormat.Format16bppGrayScale, planes[index]);
                    break;
            }
            return bitmap;
        }

        public Bitmap ToBitmap(int index, Bitmap bitmap) {
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            switch (format) {
                case PixelFormat.PIXEL_FORMAT_RGB32:
                    var bitmapdata1 = bitmap.LockBits(rect, ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    PXCMImage_ImageData_Copy(planes[index], pitches[index], bitmapdata1.Scan0, bitmapdata1.Stride,
                        bitmap.Width*4, bitmap.Height);
                    bitmap.UnlockBits(bitmapdata1);
                    break;
                case PixelFormat.PIXEL_FORMAT_RGB24:
                    var bitmapdata2 = bitmap.LockBits(rect, ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    PXCMImage_ImageData_Copy(planes[index], pitches[index], bitmapdata2.Scan0, bitmapdata2.Stride,
                        bitmap.Width*3, bitmap.Height);
                    bitmap.UnlockBits(bitmapdata2);
                    break;
                case PixelFormat.PIXEL_FORMAT_DEPTH:
                case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                    var bitmapdata3 = bitmap.LockBits(rect, ImageLockMode.WriteOnly,
                        System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                    PXCMImage_ImageData_Copy(planes[index], pitches[index], bitmapdata3.Scan0, bitmapdata3.Stride,
                        bitmap.Width*2, bitmap.Height);
                    bitmap.UnlockBits(bitmapdata3);
                    break;
            }
            return bitmap;
        }

        [DllImport("libpxccpp2c")]
        private static extern void PXCMImage_ImageData_Copy(IntPtr src, int spitch, IntPtr dst, int dpitch, int width,
            int height);

        public byte[] ToByteArray(int index, byte[] dest) {
            Marshal.Copy(planes[index], dest, 0, dest.Length);
            return dest;
        }

        public short[] ToShortArray(int index, short[] dest) {
            Marshal.Copy(planes[index], dest, 0, dest.Length);
            return dest;
        }

        [CLSCompliant(false)]
        public ushort[] ToUShortArray(int index, ushort[] dest) {
            var gcHandle = GCHandle.Alloc(dest, GCHandleType.Pinned);
            var num = pitches[index]/2;
            PXCMImage_ImageData_Copy(planes[index], pitches[index], gcHandle.AddrOfPinnedObject(), pitches[index], num*2,
                dest.Length/num);
            gcHandle.Free();
            return dest;
        }

        public int[] ToIntArray(int index, int[] dest) {
            Marshal.Copy(planes[index], dest, 0, dest.Length);
            return dest;
        }

        public float[] ToFloatArray(int index, float[] dest) {
            Marshal.Copy(planes[index], dest, 0, dest.Length);
            return dest;
        }

        public void FromByteArray(int index, byte[] src) {
            Marshal.Copy(src, 0, planes[index], src.Length);
        }

        public void FromShortArray(int index, short[] src) {
            Marshal.Copy(src, 0, planes[index], src.Length);
        }

        [CLSCompliant(false)]
        public void FromUShortArray(int index, ushort[] src) {
            var gcHandle = GCHandle.Alloc(src, GCHandleType.Pinned);
            var num = pitches[index]/2;
            PXCMImage_ImageData_Copy(gcHandle.AddrOfPinnedObject(), pitches[index], planes[index], pitches[index], num*2,
                src.Length/num);
        }

        public void FromIntArray(int index, int[] src) {
            Marshal.Copy(src, 0, planes[index], src.Length);
        }

        public void FromFloatArray(int index, float[] src) {
            Marshal.Copy(src, 0, planes[index], src.Length);
        }

        public WriteableBitmap ToWritableBitmap(int index, int width, int height, double dpiX, double dpiY) {
            WriteableBitmap writeableBitmap = null;
            switch (format) {
                case PixelFormat.PIXEL_FORMAT_RGB32:
                    writeableBitmap = new WriteableBitmap(width, height, dpiX, dpiY, PixelFormats.Bgr32, null);
                    writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), planes[index], pitches[index]*height,
                        pitches[index]);
                    break;
                case PixelFormat.PIXEL_FORMAT_RGB24:
                    writeableBitmap = new WriteableBitmap(width, height, dpiX, dpiY, PixelFormats.Bgr24, null);
                    writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), planes[index], pitches[index]*height,
                        pitches[index]);
                    break;
                case PixelFormat.PIXEL_FORMAT_DEPTH:
                case PixelFormat.PIXEL_FORMAT_DEPTH_RAW:
                    writeableBitmap = new WriteableBitmap(width, height, dpiX, dpiY, PixelFormats.Gray16, null);
                    writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), planes[index], pitches[index]*height,
                        pitches[index]);
                    break;
            }
            return writeableBitmap;
        }

        public WriteableBitmap ToWritableBitmap(int width, int height, double dpiX, double dpiY) {
            return ToWritableBitmap(0, width, height, dpiX, dpiY);
        }

        public byte[] ToByteArray(int index, int size) {
            return ToByteArray(index, new byte[size]);
        }

        public short[] ToShortArray(int index, int size) {
            return ToShortArray(index, new short[size]);
        }

        [CLSCompliant(false)]
        public ushort[] ToUShortArray(int index, int size) {
            return ToUShortArray(index, new ushort[size]);
        }

        public int[] ToIntArray(int index, int size) {
            return ToIntArray(index, new int[size]);
        }

        public float[] ToFloatArray(int index, int size) {
            return ToFloatArray(index, new float[size]);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ImageDataNative : ImageData {
        public IntPtr native;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class ImageInfo {
        public int width;
        public int height;
        public PixelFormat format;
        internal int reserved;
    }
}