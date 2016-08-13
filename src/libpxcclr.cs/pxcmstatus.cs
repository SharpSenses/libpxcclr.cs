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

/// <summary>
///   This enumeration defines various return codes that SDK interfaces
///   use.  Negative values indicate errors, a zero value indicates success,
///   and positive values indicate warnings.
/// </summary>
public enum pxcmStatus
{
    PXCM_STATUS_NO_ERROR = 0,

    /* errors */

    /// <summary>
    /// Unsupported feature
    /// </summary>
    PXCM_STATUS_FEATURE_UNSUPPORTED = -1,
    /// <summary>
    /// Unsupported parameter(s)
    /// </summary>
    PXCM_STATUS_PARAM_UNSUPPORTED = -2,
    /// <summary>
    /// Item not found/not available
    /// </summary>
    PXCM_STATUS_ITEM_UNAVAILABLE = -3,


    /// <summary>
    /// Invalid session, algorithm instance, or pointer
    /// </summary>
    PXCM_STATUS_HANDLE_INVALID = -101,
    /// <summary>
    /// Memory allocation failure 
    /// </summary>
    PXCM_STATUS_ALLOC_FAILED = -102,


    /// <summary>
    /// Acceleration device failed/lost
    /// </summary>
    PXCM_STATUS_DEVICE_FAILED = -201,
    /// <summary>
    /// Acceleration device lost
    /// </summary>
    PXCM_STATUS_DEVICE_LOST = -202,
    /// <summary>
    /// Acceleration device busy
    /// </summary>
    PXCM_STATUS_DEVICE_BUSY = -203,


    /// <summary>
    /// Execution aborted due to errors in upstream components
    /// </summary>
    PXCM_STATUS_EXEC_ABORTED = -301,
    /// <summary>
    /// Asynchronous operation is in execution
    /// </summary>
    PXCM_STATUS_EXEC_INPROGRESS = -302,
    /// <summary>
    /// Operation time out 
    /// </summary>
    PXCM_STATUS_EXEC_TIMEOUT = -303,


    /// <summary>
    /// Failure in open file in WRITE mode
    /// </summary>
    PXCM_STATUS_FILE_WRITE_FAILED = -401,
    /// <summary>
    /// Failure in open file in READ mode
    /// </summary>
    PXCM_STATUS_FILE_READ_FAILED = -402,
    /// <summary>
    /// Failure in close a file handle
    /// </summary>
    PXCM_STATUS_FILE_CLOSE_FAILED = -403,


    /// <summary>
    /// Data not available for MW model or processing 
    /// </summary>
    PXCM_STATUS_DATA_UNAVAILABLE = -501,
    /// <summary>
    /// Data failed to initialize
    /// </summary>
    PXCM_STATUS_DATA_NOT_INITIALIZED = -502,
    /// <summary>
    /// Module failure during initialization
    /// </summary>
    PXCM_STATUS_INIT_FAILED = -503,


    /// <summary>
    /// Configuration for the stream has changed 
    /// </summary>
    PXCM_STATUS_STREAM_CONFIG_CHANGED = -601,
    PXCM_STATUS_POWER_UID_ALREADY_REGISTERED = -701,
    PXCM_STATUS_POWER_UID_NOT_REGISTERED = -702,
    PXCM_STATUS_POWER_ILLEGAL_STATE = -703,
    PXCM_STATUS_POWER_PROVIDER_NOT_EXISTS = -704,


    /// <summary>
    /// parameter cannot be changed since configuration for capturing has been already set
    /// </summary>
    PXCM_STATUS_CAPTURE_CONFIG_ALREADY_SET = -801,
    /// <summary>
    /// Mismatched coordinate system between modules
    /// </summary>
    PXCM_STATUS_COORDINATE_SYSTEM_CONFLICT = -802,
    /// <summary>
    /// calibration values not matching
    /// </summary>
    PXCM_STATUS_NOT_MATCHING_CALIBRATION = -803,


    /// <summary>
    /// Acceleration unsupported or unavailable
    /// </summary>
    PXCM_STATUS_ACCELERATION_UNAVAILABLE = -901,

    /* warnings */

    /// <summary>
    /// time gap in time stamps
    /// </summary>
    PXCM_STATUS_TIME_GAP = 101,
    /// <summary>
    /// the same parameters already defined
    /// </summary>
    PXCM_STATUS_PARAM_INPLACE = 102,
    /// <summary>
    /// Data not changed (no new data available)
    /// </summary>
    PXCM_STATUS_DATA_NOT_CHANGED = 103,
    /// <summary>
    /// Module failure during processing
    /// </summary>
    PXCM_STATUS_PROCESS_FAILED = 104,
    /// <summary>
    /// Data value(s) out of range
    /// </summary>
    PXCM_STATUS_VALUE_OUT_OF_RANGE = 105,
    /// <summary>
    /// Not all data was copied, more data is available for fetching
    /// </summary>
    PXCM_STATUS_DATA_PENDING = 106
};

#if RSSDK_IN_NAMESPACE
}
#endif