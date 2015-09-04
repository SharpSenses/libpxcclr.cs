/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2013 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;
using System.Runtime.InteropServices;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public partial class PXCMPowerState : PXCMBase
    {
        new public const Int32 CUID = 0x474d5750;

        public enum State
        {
            STATE_PERFORMANCE,  /* full feature set/best algorithm */
            STATE_BATTERY,      /* idle */
        };

        /* Member Functions */

        /**
         * Query current power state of the device, returns maximal used state 
         */
        public State QueryState()
        {
            return PXCMPowerState_QueryState(instance);
        }

        /**
         * Try to set power state of all used devices, all streams, application should call 
         * QueryStream to check if the desired state was set 
         */
        public pxcmStatus SetState(State state)
        {
            return PXCMPowerState_SetState(instance, state);
        }

	    /* Sets inactivity interval */ 
	    public pxcmStatus SetInactivityInterval(Int32 timeInSeconds)
        {
            return PXCMPowerState_SetInactivityInterval(instance, timeInSeconds);
        }

	    /* Returns inactivity interval */
        public Int32 QueryInactivityInterval()
        {
            return PXCMPowerState_QueryInactivityInterval(instance);
        }

        /* constructors & misc */
        internal PXCMPowerState(IntPtr instance, Boolean delete)
            : base(instance, delete)
        {
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif