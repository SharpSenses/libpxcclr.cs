using System;
using System.Runtime.InteropServices;

public class PXCMBlobData : PXCMBase {
    public enum AccessOrderType {
        ACCESS_ORDER_NEAR_TO_FAR,
        ACCESS_ORDER_LARGE_TO_SMALL,
        ACCESS_ORDER_RIGHT_TO_LEFT
    }

    public enum ExtremityType {
        EXTREMITY_CLOSEST,
        EXTREMITY_LEFT_MOST,
        EXTREMITY_RIGHT_MOST,
        EXTREMITY_TOP_MOST,
        EXTREMITY_BOTTOM_MOST,
        EXTREMITY_CENTER
    }

    public new const int CUID = 1413762370;
    public const int NUMBER_OF_EXTREMITIES = 6;

    internal PXCMBlobData(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobData_Update(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlobData_QueryNumberOfBlobs(IntPtr instance);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlobData_QueryBlobByAccessOrder(IntPtr instance, int index,
        AccessOrderType accessOrder, out IntPtr blobData);

    public pxcmStatus Update() {
        return PXCMBlobData_Update(instance);
    }

    public int QueryNumberOfBlobs() {
        return PXCMBlobData_QueryNumberOfBlobs(instance);
    }

    public pxcmStatus QueryBlobByAccessOrder(int index, AccessOrderType accessOrderType, out IBlob blobData) {
        IntPtr blobData1;
        var pxcmStatus = PXCMBlobData_QueryBlobByAccessOrder(instance, index, accessOrderType, out blobData1);
        blobData = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new IBlob(blobData1) : null;
        return pxcmStatus;
    }

    public class IBlob {
        private IntPtr instance;

        internal IBlob(IntPtr instance) {
            this.instance = instance;
        }

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMBlobData_IBlob_QuerySegmentationImage(IntPtr instance, out IntPtr image);

        [DllImport("libpxccpp2c")]
        internal static extern PXCMPoint3DF32 PXCMBlobData_IBlob_QueryExtremityPoint(IntPtr instance,
            ExtremityType extremityLabel);

        internal static PXCMPoint3DF32 QueryExtremityPointINT(IntPtr instance, ExtremityType extremityLabel) {
            return PXCMBlobData_IBlob_QueryExtremityPoint(instance, extremityLabel);
        }

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMBlobData_IBlob_QueryPixelCount(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMBlobData_IBlob_QueryNumberOfContours(IntPtr instance);

        [DllImport("libpxccpp2c")]
        internal static extern pxcmStatus PXCMBlobData_IBlob_QueryContourPoints(IntPtr instsance, int index, int max,
            [Out] PXCMPointI32[] contour);

        internal static pxcmStatus QueryContourDataINT(IntPtr instance, int index, out PXCMPointI32[] contour) {
            var max = PXCMBlobData_IBlob_QueryContourSize(instance, index);
            if (max > 0) {
                contour = new PXCMPointI32[max];
                return PXCMBlobData_IBlob_QueryContourPoints(instance, index, max, contour);
            }
            contour = null;
            return pxcmStatus.PXCM_STATUS_DATA_UNAVAILABLE;
        }

        [DllImport("libpxccpp2c")]
        internal static extern int PXCMBlobData_IBlob_QueryContourSize(IntPtr instance, int index);

        [DllImport("libpxccpp2c")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PXCMBlobData_IBlob_IsContourOuter(IntPtr instance, int index);

        public pxcmStatus QuerySegmentationImage(out PXCMImage image) {
            IntPtr image1;
            var pxcmStatus = PXCMBlobData_IBlob_QuerySegmentationImage(instance, out image1);
            image = pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR ? new PXCMImage(image1, false) : null;
            return pxcmStatus;
        }

        public PXCMPoint3DF32 QueryExtremityPoint(ExtremityType extremityLabel) {
            return QueryExtremityPointINT(instance, extremityLabel);
        }

        public int QueryPixelCount() {
            return PXCMBlobData_IBlob_QueryPixelCount(instance);
        }

        public int QueryNumberOfContours() {
            return PXCMBlobData_IBlob_QueryNumberOfContours(instance);
        }

        public pxcmStatus QueryContourPoints(int index, out PXCMPointI32[] contour) {
            return QueryContourDataINT(instance, index, out contour);
        }

        public bool IsContourOuter(int index) {
            return PXCMBlobData_IBlob_IsContourOuter(instance, index);
        }

        public int QueryContourSize(int index) {
            return PXCMBlobData_IBlob_QueryContourSize(instance, index);
        }
    }
}