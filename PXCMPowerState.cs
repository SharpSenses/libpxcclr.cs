using System;
using System.Runtime.InteropServices;

public class PXCMPowerState : PXCMBase {
    public enum State {
        STATE_PERFORMANCE,
        STATE_BATTERY
    }

    public new const int CUID = 1196250960;

    internal PXCMPowerState(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern State PXCMPowerState_QueryState(IntPtr ps);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMPowerState_SetState(IntPtr ps, State state);

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMPowerState_SetInactivityInterval(IntPtr ps, int timeInSeconds);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMPowerState_QueryInactivityInterval(IntPtr ps);

    public State QueryState() {
        return PXCMPowerState_QueryState(instance);
    }

    public pxcmStatus SetState(State state) {
        return PXCMPowerState_SetState(instance, state);
    }

    public pxcmStatus SetInactivityInterval(int timeInSeconds) {
        return PXCMPowerState_SetInactivityInterval(instance, timeInSeconds);
    }

    public int QueryInactivityInterval() {
        return PXCMPowerState_QueryInactivityInterval(instance);
    }
}