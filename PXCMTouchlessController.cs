using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PXCMTouchlessController : PXCMBase
{
  public new const int CUID = 1397443654;
  internal EventMap maps;

  internal PXCMTouchlessController(IntPtr instance, bool delete)
    : base(instance, delete)
  {
    maps = new EventMap();
  }

  internal PXCMTouchlessController(EventMap maps, IntPtr instance, bool delete)
    : base(instance, delete)
  {
    this.maps = maps;
  }

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTouchlessController_QueryProfile(IntPtr instance, [Out] ProfileInfo pinfo);

  [DllImport("libpxccpp2c")]
  internal static extern pxcmStatus PXCMTouchlessController_SetProfile(IntPtr instance, ProfileInfo pinfo);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_SubscribeEvent(IntPtr instance, IntPtr handler);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_UnsubscribeEvent(IntPtr instance, IntPtr handler);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_SubscribeAlert(IntPtr instance, IntPtr handler);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_UnsubscribeAlert(IntPtr instance, IntPtr handler);

  [DllImport("libpxccpp2c", CharSet = CharSet.Unicode)]
  private static extern pxcmStatus PXCMTouchlessController_AddGestureActionMapping(IntPtr instance, string gestureName, Action action, IntPtr actionHandler);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_ClearAllGestureActionMappings(IntPtr instance);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_SetScrollSensitivity(IntPtr instance, float sensitivity);

  [DllImport("libpxccpp2c")]
  private static extern pxcmStatus PXCMTouchlessController_SetPointerSensitivity(IntPtr instance, PointerSensitivity sensitivity);

  public pxcmStatus QueryProfile(out ProfileInfo pinfo)
  {
    pinfo = new ProfileInfo();
    return PXCMTouchlessController_QueryProfile(instance, pinfo);
  }

  internal static pxcmStatus SubscribeEventINT(IntPtr instance, OnFiredUXEventDelegate uxEventHandler, out object proxy)
  {
    UXEventHandlerDIR uxEventHandlerDir = new UXEventHandlerDIR(uxEventHandler);
    pxcmStatus pxcmStatus = PXCMTouchlessController_SubscribeEvent(instance, uxEventHandlerDir.uDIR);
    if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
      uxEventHandlerDir.Dispose();
    proxy = uxEventHandlerDir;
    return pxcmStatus;
  }

  internal static pxcmStatus UnsubscribeEventINT(IntPtr instance, object proxy)
  {
    UXEventHandlerDIR uxEventHandlerDir = (UXEventHandlerDIR) proxy;
    pxcmStatus pxcmStatus = PXCMTouchlessController_UnsubscribeEvent(instance, uxEventHandlerDir.uDIR);
    uxEventHandlerDir.Dispose();
    return pxcmStatus;
  }

  internal static pxcmStatus SubscribeAlertINT(IntPtr instance, OnFiredAlertDelegate alertHandler, out object proxy)
  {
    AlertHandlerDIR alertHandlerDir = new AlertHandlerDIR(alertHandler);
    pxcmStatus pxcmStatus = PXCMTouchlessController_SubscribeAlert(instance, alertHandlerDir.uDIR);
    if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
      alertHandlerDir.Dispose();
    proxy = alertHandlerDir;
    return pxcmStatus;
  }

  internal static pxcmStatus UnsubscribeAlertINT(IntPtr instance, object proxy)
  {
    AlertHandlerDIR alertHandlerDir = (AlertHandlerDIR) proxy;
    pxcmStatus pxcmStatus = PXCMTouchlessController_UnsubscribeAlert(instance, alertHandlerDir.uDIR);
    alertHandlerDir.Dispose();
    return pxcmStatus;
  }

  internal static pxcmStatus AddGestureActionMappingINT(IntPtr instance, string gestureName, Action action, OnFiredActionDelegate actionHandler, out object proxy)
  {
    ActionHandlerDIR actionHandlerDir = new ActionHandlerDIR(actionHandler);
    pxcmStatus pxcmStatus = PXCMTouchlessController_AddGestureActionMapping(instance, gestureName, action, actionHandlerDir.uDIR);
    if (pxcmStatus < pxcmStatus.PXCM_STATUS_NO_ERROR)
      actionHandlerDir.Dispose();
    proxy = actionHandlerDir;
    return pxcmStatus;
  }

  public pxcmStatus SetScrollSensitivity(float sensitivity)
  {
    return PXCMTouchlessController_SetScrollSensitivity(instance, sensitivity);
  }

  public pxcmStatus SetPointerSensitivity(PointerSensitivity sensitivity)
  {
    return PXCMTouchlessController_SetPointerSensitivity(instance, sensitivity);
  }

  public pxcmStatus SetProfile(ProfileInfo pinfo)
  {
    return PXCMTouchlessController_SetProfile(instance, pinfo);
  }

  public pxcmStatus AddGestureActionMapping(string gestureName, Action action)
  {
    return AddGestureActionMapping(gestureName, action, null);
  }

  public pxcmStatus SubscribeEvent(OnFiredUXEventDelegate uxEventHandler)
  {
    if (uxEventHandler == null)
      return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
    object proxy;
    pxcmStatus status = SubscribeEventINT(instance, uxEventHandler, out proxy);
    if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      lock (maps.cs)
        maps.uxEvents[uxEventHandler] = proxy;
    }
    return status;
  }

  public pxcmStatus UnsubscribeEvent(OnFiredUXEventDelegate uxEventHandler)
  {
    if (uxEventHandler == null)
      return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
    lock (maps.cs)
    {
      object local_0;
      if (!maps.uxEvents.TryGetValue(uxEventHandler, out local_0))
        return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
      pxcmStatus local_1 = UnsubscribeEventINT(instance, local_0);
      maps.uxEvents.Remove(uxEventHandler);
      return local_1;
    }
  }

  public pxcmStatus SubscribeAlert(OnFiredAlertDelegate alertHandler)
  {
    if (alertHandler == null)
      return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
    object proxy;
    pxcmStatus status = SubscribeAlertINT(instance, alertHandler, out proxy);
    if (status >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      lock (maps.cs)
        maps.alerts[alertHandler] = proxy;
    }
    return status;
  }

  public pxcmStatus UnsubscribeAlert(OnFiredAlertDelegate alertHandler)
  {
    if (alertHandler == null)
      return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
    lock (maps.cs)
    {
      object local_0;
      if (!maps.alerts.TryGetValue(alertHandler, out local_0))
        return pxcmStatus.PXCM_STATUS_HANDLE_INVALID;
      pxcmStatus local_1 = UnsubscribeAlertINT(instance, local_0);
      maps.alerts.Remove(alertHandler);
      return local_1;
    }
  }

  public pxcmStatus AddGestureActionMapping(string gestureName, Action action, OnFiredActionDelegate actionHandler)
  {
    if (actionHandler == null)
      return PXCMTouchlessController_AddGestureActionMapping(instance, gestureName, action, IntPtr.Zero);
    object proxy;
    pxcmStatus pxcmStatus = AddGestureActionMappingINT(instance, gestureName, action, actionHandler, out proxy);
    if (pxcmStatus >= pxcmStatus.PXCM_STATUS_NO_ERROR)
    {
      lock (maps.cs)
        maps.actions.Add(proxy);
    }
    return pxcmStatus;
  }

  public pxcmStatus ClearAllGestureActionMappings()
  {
    pxcmStatus pxcmStatus = PXCMTouchlessController_ClearAllGestureActionMappings(instance);
    lock (maps.cs)
      maps.actions.Clear();
    return pxcmStatus;
  }

  internal class UXEventHandlerDIR : IDisposable
  {
    private GCHandle gch;
    internal OnFiredUXEventDelegate mfunc;
    internal IntPtr uDIR;

    public UXEventHandlerDIR(OnFiredUXEventDelegate handler)
    {
      mfunc = handler;
      gch = GCHandle.Alloc(handler);
      uDIR = PXCMTouchlessController_AllocUXEventHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
    }

    ~UXEventHandlerDIR()
    {
      Dispose();
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMTouchlessController_AllocUXEventHandlerDIR(IntPtr handlerFunc);

    [DllImport("libpxccpp2c")]
    private static extern void PXCMTouchlessController_FreeUXEventHandlerDIR(IntPtr hdir);

    public void Dispose()
    {
      if (uDIR == IntPtr.Zero)
        return;
      PXCMTouchlessController_FreeUXEventHandlerDIR(uDIR);
      uDIR = IntPtr.Zero;
      if (!gch.IsAllocated)
        return;
      gch.Free();
    }
  }

  internal class AlertHandlerDIR : IDisposable
  {
    private GCHandle gch;
    internal OnFiredAlertDelegate mfunc;
    internal IntPtr uDIR;

    public AlertHandlerDIR(OnFiredAlertDelegate handler)
    {
      mfunc = handler;
      gch = GCHandle.Alloc(handler);
      uDIR = PXCMTouchlessController_AllocAlertHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
    }

    ~AlertHandlerDIR()
    {
      Dispose();
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMTouchlessController_AllocAlertHandlerDIR(IntPtr handlerFunc);

    [DllImport("libpxccpp2c")]
    private static extern void PXCMTouchlessController_FreeAlertHandlerDIR(IntPtr hdir);

    public void Dispose()
    {
      if (uDIR == IntPtr.Zero)
        return;
      PXCMTouchlessController_FreeAlertHandlerDIR(uDIR);
      uDIR = IntPtr.Zero;
      if (!gch.IsAllocated)
        return;
      gch.Free();
    }
  }

  internal class ActionHandlerDIR : IDisposable
  {
    private GCHandle gch;
    internal OnFiredActionDelegate mfunc;
    internal IntPtr uDIR;

    public ActionHandlerDIR(OnFiredActionDelegate handler)
    {
      mfunc = handler;
      gch = GCHandle.Alloc(handler);
      uDIR = PXCMTouchlessController_AllocActionHandlerDIR(Marshal.GetFunctionPointerForDelegate(handler));
    }

    ~ActionHandlerDIR()
    {
      Dispose();
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMTouchlessController_AllocActionHandlerDIR(IntPtr handlerFunc);

    [DllImport("libpxccpp2c")]
    private static extern void PXCMTouchlessController_FreeActionHandlerDIR(IntPtr hdir);

    public void Dispose()
    {
      if (uDIR == IntPtr.Zero)
        return;
      PXCMTouchlessController_FreeActionHandlerDIR(uDIR);
      uDIR = IntPtr.Zero;
      if (!gch.IsAllocated)
        return;
      gch.Free();
    }
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class ProfileInfo
  {
    private IntPtr handInstance;
    public Configuration config;

    public PXCMHandModule handAnalysis
    {
      get
      {
        if (!(handInstance != IntPtr.Zero))
          return null;
        return new PXCMHandModule(handInstance, false);
      }
      private set
      {
        handInstance = value.instance;
      }
    }

    [Flags]
    public enum Configuration
    {
      Configuration_None = 0,
      Configuration_Allow_Zoom = 1,
      Configuration_Use_Draw_Mode = 2,
      Configuration_Scroll_Horizontally = 4,
      Configuration_Scroll_Vertically = 8,
      Configuration_Meta_Context_Menu = 16,
      Configuration_Enable_Injection = 32,
      Configuration_Edge_Scroll_Horizontally = 64,
      Configuration_Edge_Scroll_Vertically = 128,
      Configuration_Allow_Back = 512,
      Configuration_Allow_Selection = 1024
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public class UXEventData
  {
    public UXEventType type;
    public PXCMPoint3DF32 position;
    public PXCMHandData.BodySideType bodySide;

    public enum UXEventType
    {
      UXEvent_StartZoom,
      UXEvent_Zoom,
      UXEvent_EndZoom,
      UXEvent_StartScroll,
      UXEvent_Scroll,
      UXEvent_EndScroll,
      UXEvent_StartDraw,
      UXEvent_Draw,
      UXEvent_EndDraw,
      UXEvent_CursorMove,
      UXEvent_Select,
      UXEvent_GotoStart,
      UXEvent_CursorVisible,
      UXEvent_CursorNotVisible,
      UXEvent_ReadyForAction,
      UXEvent_StartMetaCounter,
      UXEvent_StopMetaCounter,
      UXEvent_ShowMetaMenu,
      UXEvent_HideMetaMenu,
      UXEvent_MetaPinch,
      UXEvent_MetaOpenHand,
      UXEvent_Back,
      UXEvent_ScrollUp,
      UXEvent_ScrollDown,
      UXEvent_ScrollLeft,
      UXEvent_ScrollRight
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public class AlertData
  {
    public AlertType type;

    public enum AlertType
    {
      Alert_TooClose,
      Alert_TooFar,
      Alert_NoAlerts
    }
  }

  public enum Action
  {
    Action_None,
    Action_LeftKeyPress,
    Action_RightKeyPress,
    Action_BackKeyPress,
    Action_PgUpKeyPress,
    Action_PgDnKeyPress,
    Action_VolumeUp,
    Action_VolumeDown,
    Action_Mute,
    Action_NextTrack,
    Action_PrevTrack,
    Action_PlayPause,
    Action_Stop,
    Action_ToggleTabs
  }

  public enum PointerSensitivity
  {
    PointerSensitivity_Smoothed,
    PointerSensitivity_Balanced,
    PointerSensitivity_Sensitive
  }

  public delegate void OnFiredUXEventDelegate(UXEventData data);

  public delegate void OnFiredAlertDelegate(AlertData data);

  public delegate void OnFiredActionDelegate(Action data);

  internal class EventMap
  {
    public Dictionary<OnFiredUXEventDelegate, object> uxEvents = new Dictionary<OnFiredUXEventDelegate, object>();
    public Dictionary<OnFiredAlertDelegate, object> alerts = new Dictionary<OnFiredAlertDelegate, object>();
    public List<object> actions = new List<object>();
    public object cs = new object();
  }
}
