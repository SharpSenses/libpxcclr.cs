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

    /**
   This enumeration defines various return codes that SDK interfaces
   use.  Negative values indicate errors, a zero value indicates success,
   and positive values indicate warnings.
 */
    public enum pxcmStatus
    {
        PXCM_STATUS_NO_ERROR = 0,

        /* errors */
        PXCM_STATUS_FEATURE_UNSUPPORTED = -1,     /* Unsupported feature */
        PXCM_STATUS_PARAM_UNSUPPORTED = -2,     /* Unsupported parameter(s) */
        PXCM_STATUS_ITEM_UNAVAILABLE = -3,     /* Item not found/not available */

        PXCM_STATUS_HANDLE_INVALID = -101,   /* Invalid session, algorithm instance, or pointer */
        PXCM_STATUS_ALLOC_FAILED = -102,   /* Memory allocation failure */

        PXCM_STATUS_DEVICE_FAILED = -201,   /* Acceleration device failed/lost */
        PXCM_STATUS_DEVICE_LOST = -202,   /* Acceleration device lost */
        PXCM_STATUS_DEVICE_BUSY = -203,   /* Acceleration device busy */

        PXCM_STATUS_EXEC_ABORTED = -301,   /* Execution aborted due to errors in upstream components */
        PXCM_STATUS_EXEC_INPROGRESS = -302,   /* Asynchronous operation is in execution */
        PXCM_STATUS_EXEC_TIMEOUT = -303,   /* Operation time out */

        PXCM_STATUS_FILE_WRITE_FAILED = -401,   /** Failure in open file in WRITE mode */
        PXCM_STATUS_FILE_READ_FAILED = -402,   /** Failure in open file in READ mode */
        PXCM_STATUS_FILE_CLOSE_FAILED = -403,   /** Failure in close a file handle */

        PXCM_STATUS_DATA_UNAVAILABLE = -501,   /** Data not available for MW model or processing */
        PXCM_STATUS_DATA_NOT_INITIALIZED = -502,	/** Data failed to initialize */
        PXCM_STATUS_INIT_FAILED = -503,   /** Module failure during initialization */


        PXCM_STATUS_STREAM_CONFIG_CHANGED = -601,   /** Configuration for the stream has changed */
        PXCM_STATUS_POWER_UID_ALREADY_REGISTERED = -701,
        PXCM_STATUS_POWER_UID_NOT_REGISTERED = -702,
        PXCM_STATUS_POWER_ILLEGAL_STATE = -703,
        PXCM_STATUS_POWER_PROVIDER_NOT_EXISTS = -704,

        PXCM_STATUS_CAPTURE_CONFIG_ALREADY_SET = -801, /** parameter cannot be changed since configuration for capturing has been already set */
        PXCM_STATUS_COORDINATE_SYSTEM_CONFLICT = -802, /** Mismatched coordinate system between modules */
        PXCM_STATUS_NOT_MATCHING_CALIBRATION = -803,   /** calibration values not matching*/

        /* warnings */
        PXCM_STATUS_TIME_GAP = 101,    /* time gap in time stamps */
        PXCM_STATUS_PARAM_INPLACE = 102,    /* the same parameters already defined */
        PXCM_STATUS_DATA_NOT_CHANGED = 103,	 /* Data not changed (no new data available)*/
        PXCM_STATUS_PROCESS_FAILED = 104,     /* Module failure during processing */
        PXCM_STATUS_VALUE_OUT_OF_RANGE = 105,   /** Data value(s) out of range*/
        PXCM_STATUS_DATA_PENDING = 106         /* Not all data was copied, more data is available for fetching*/
    };

#if RSSDK_IN_NAMESPACE
}
#endif