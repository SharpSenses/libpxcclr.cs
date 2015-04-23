using System;
using System.Runtime.InteropServices;

[Obsolete("Not used anymore. Use PXCMBlob module instead")]
public class PXCMBlobExtractor : PXCMBase
{
  public new const int CUID = -1524431428;

  internal PXCMBlobExtractor(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern void PXCMBlobExtractor_Init(IntPtr instsance, PXCMImage.ImageInfo imageInfo);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_ProcessImage(IntPtr instance, IntPtr depthImage);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMBlobExtractor_QueryBlobData(IntPtr instance, int index, IntPtr segmentationImage, [Out] BlobData blobData);

  internal static pxcmStatus QueryBlobDataINT(IntPtr instance, int index, IntPtr segmentationImage, out BlobData blobData)
  {
    blobData = new BlobData();
    return PXCMBlobExtractor_QueryBlobData(instance, index, segmentationImage, blobData);
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_SetMaxBlobs(IntPtr instance, int maxBlobs);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMBlobExtractor_QueryMaxBlobs(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern int PXCMBlobExtractor_QueryNumberOfBlobs(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_SetMaxDistance(IntPtr instance, float maxDistance);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMBlobExtractor_QueryMaxDistance(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_SetMaxObjectDepth(IntPtr instance, float maxDepth);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMBlobExtractor_QueryMaxObjectDepth(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_SetSmoothing(IntPtr instance, float smoothing);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMBlobExtractor_QuerySmoothing(IntPtr instance);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMBlobExtractor_SetClearMinBlobSize(IntPtr instance, float minBlobSize);

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMBlobExtractor_QueryClearMinBlobSize(IntPtr instance);

  public void Init(PXCMImage.ImageInfo imageInfo)
  {
    PXCMBlobExtractor_Init(instance, imageInfo);
  }

  public pxcmStatus ProcessImage(PXCMImage depthImage)
  {
    return PXCMBlobExtractor_ProcessImage(instance, depthImage.instance);
  }

  public pxcmStatus QueryBlobData(int index, PXCMImage segmentationImage, out BlobData blobData)
  {
    return QueryBlobDataINT(instance, index, segmentationImage.instance, out blobData);
  }

  public pxcmStatus SetMaxBlobs(int maxBlobs)
  {
    return PXCMBlobExtractor_SetMaxBlobs(instance, maxBlobs);
  }

  public int QueryMaxBlobs()
  {
    return PXCMBlobExtractor_QueryMaxBlobs(instance);
  }

  public int QueryNumberOfBlobs()
  {
    return PXCMBlobExtractor_QueryNumberOfBlobs(instance);
  }

  public pxcmStatus SetMaxDistance(float maxDistance)
  {
    return PXCMBlobExtractor_SetMaxDistance(instance, maxDistance);
  }

  public float QueryMaxDistance()
  {
    return PXCMBlobExtractor_QueryMaxDistance(instance);
  }

  public pxcmStatus SetMaxObjectDepth(float maxDepth)
  {
    return PXCMBlobExtractor_SetMaxObjectDepth(instance, maxDepth);
  }

  public float QueryMaxObjectDepth()
  {
    return PXCMBlobExtractor_QueryMaxObjectDepth(instance);
  }

  public pxcmStatus SetSmoothing(float smoothing)
  {
    return PXCMBlobExtractor_SetSmoothing(instance, smoothing);
  }

  public float QuerySmoothing()
  {
    return PXCMBlobExtractor_QuerySmoothing(instance);
  }

  public pxcmStatus SetClearMinBlobSize(float minBlobSize)
  {
    return PXCMBlobExtractor_SetClearMinBlobSize(instance, minBlobSize);
  }

  public float QueryClearMinBlobSize()
  {
    return PXCMBlobExtractor_QueryClearMinBlobSize(instance);
  }

  [StructLayout(LayoutKind.Sequential)]
  public class BlobData
  {
    public PXCMPointI32 closestPoint;
    public PXCMPointI32 leftPoint;
    public PXCMPointI32 rightPoint;
    public PXCMPointI32 topPoint;
    public PXCMPointI32 bottomPoint;
    public PXCMPointF32 centerPoint;
    public int pixelCount;
  }
}
