using System;
using System.Runtime.InteropServices;

public class PXCMPointConverterFactory : PXCMBase
{
  public new const int CUID = 1380143184;

  internal PXCMPointConverterFactory(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMPointConverterFactory_CreateHandJointConverter(IntPtr instance, IntPtr handData, PXCMHandData.AccessOrderType accessOrder, int index, PXCMHandData.JointType jointType);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMPointConverterFactory_CreateHandExtremityConverter(IntPtr instance, IntPtr handData, PXCMHandData.AccessOrderType accessOrder, int index, PXCMHandData.ExtremityType extremityType);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMPointConverterFactory_CreateBlobPointConverter(IntPtr instance, IntPtr blobData, PXCMBlobData.AccessOrderType accessOrder, int index, PXCMBlobData.ExtremityType extremityType);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMPointConverterFactory_CreateCustomPointConverter(IntPtr instance);

  public PXCMPointConverter CreateHandJointConverter(PXCMHandData handData, PXCMHandData.AccessOrderType accessOrder, int index, PXCMHandData.JointType jointType)
  {
    IntPtr handJointConverter = PXCMPointConverterFactory_CreateHandJointConverter(instance, handData.instance, accessOrder, index, jointType);
    if (!(handJointConverter == IntPtr.Zero))
      return new PXCMPointConverter(handJointConverter, true);
    return null;
  }

  public PXCMPointConverter CreateHandExtremityConverter(PXCMHandData handData, PXCMHandData.AccessOrderType accessOrder, int index, PXCMHandData.ExtremityType extremityType)
  {
    IntPtr extremityConverter = PXCMPointConverterFactory_CreateHandExtremityConverter(instance, handData.instance, accessOrder, index, extremityType);
    if (!(extremityConverter == IntPtr.Zero))
      return new PXCMPointConverter(extremityConverter, true);
    return null;
  }

  public PXCMPointConverter CreateBlobPointConverter(PXCMBlobData blobData, PXCMBlobData.AccessOrderType accessOrder, int index, PXCMBlobData.ExtremityType extremityType)
  {
    IntPtr blobPointConverter = PXCMPointConverterFactory_CreateBlobPointConverter(instance, blobData.instance, accessOrder, index, extremityType);
    if (!(blobPointConverter == IntPtr.Zero))
      return new PXCMPointConverter(blobPointConverter, true);
    return null;
  }

  public PXCMPointConverter CreateCustomPointConverter()
  {
    IntPtr customPointConverter = PXCMPointConverterFactory_CreateCustomPointConverter(instance);
    if (!(customPointConverter == IntPtr.Zero))
      return new PXCMPointConverter(customPointConverter, true);
    return null;
  }
}
