using System;
using System.Runtime.InteropServices;

public class PXCMEnhancedPhotography : PXCMBase
{
  public new const int CUID = 1313427525;

  internal PXCMEnhancedPhotography(IntPtr instance, bool delete)
    : base(instance, delete)
  {
  }

  [DllImport("libpxccpp2c")]
  internal static extern float PXCMEnhancedPhotography_MeasureDistance(IntPtr ep, PXCMCapture.Sample sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint, int radius1, int radius2);

  [DllImport("libpxccpp2c")]
  internal static extern IntPtr PXCMEnhancedPhotography_DepthRefocus(IntPtr ep, PXCMCapture.Sample sample, PXCMPointI32 focusPoint, float aperture, BlurType blurtype);

  public float MeasureDistance(PXCMCapture.Sample sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint, int radius1, int radius2)
  {
    return PXCMEnhancedPhotography_MeasureDistance(instance, sample, startPoint, endPoint, radius1, radius2);
  }

  public float MeasureDistance(PXCMCapture.Sample sample, PXCMPointI32 startPoint, PXCMPointI32 endPoint)
  {
    return MeasureDistance(sample, startPoint, endPoint, 0, 0);
  }

  public PXCMImage DepthRefocus(PXCMCapture.Sample sample, PXCMPointI32 focusPoint, float aperture, BlurType blurtype)
  {
    IntPtr instance = PXCMEnhancedPhotography_DepthRefocus(this.instance, sample, focusPoint, aperture, blurtype);
    if (!(instance != IntPtr.Zero))
      return null;
    return new PXCMImage(instance, true);
  }

  public PXCMImage DepthRefocus(PXCMCapture.Sample sample, PXCMPointI32 focusPoint, float aperture)
  {
    return DepthRefocus(sample, focusPoint, aperture, BlurType.BLUR_EXPONENTIAL);
  }

  public PXCMImage DepthRefocus(PXCMCapture.Sample sample, PXCMPointI32 focusPoint)
  {
    return DepthRefocus(sample, focusPoint, 50f, BlurType.BLUR_EXPONENTIAL);
  }

  public enum BlurType
  {
    BLUR_EXPONENTIAL,
    BLUR_CYLINDER_BLUR,
    BLUR_PSEUDO_CYLINDER_SYNC,
    BLUR_GAUSSIAN
  }
}
