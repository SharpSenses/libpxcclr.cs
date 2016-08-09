/*******************************************************************************

  INTEL CORPORATION PROPRIETARY INFORMATION
  This software is supplied under the terms of a license agreement or nondisclosure
  agreement with Intel Corporation and may not be copied or disclosed except in
  accordance with the terms of that agreement
  Copyright(c) 2013-2014 Intel Corporation. All Rights Reserved.

 *******************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

	public partial class PXCMVideoModule : PXCMBase
	{
		new public const Int32 CUID = unchecked((Int32)0x69D5B036);
		public const Int32 DEVCAP_LIMIT = 120;

		/// <summary> 
		/// struct
		/// Describes a pair value of device property and its value.
		/// Use the inline functions to access specific device properties.
		/// </summary>
		[Serializable]
			[StructLayout(LayoutKind.Sequential)]
			public struct DeviceCap
			{
				public PXCMCapture.Device.Property label;       /* Property type */
				public Single value;       /* Property value */
			};

		/// <summary> 
		/// struct
		/// Describes the streams requested by a module implementation.
		/// </summary>
		[Serializable]
			[StructLayout(LayoutKind.Sequential)]
			public class StreamDesc
			{
				public PXCMSizeI32 sizeMin;         /* minimum size */
				public PXCMSizeI32 sizeMax;         /* maximum size */
				public PXCMRangeF32 frameRate;      /* frame rate	*/
				public PXCMCapture.Device.StreamOption options; /* options      */
				public Int32 propertySet;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
					internal Int32[] reserved;

				public StreamDesc()
				{
					reserved = new Int32[4];
				}
			};

		/// <summary> 
		/// StreamDescSet
		/// A set of stream descriptors accessed by StreamType.
		/// </summary>
		[Serializable]
			[StructLayout(LayoutKind.Sequential, Size = 384)]
			public class StreamDescSet
			{
				public StreamDesc color;
				public StreamDesc depth;
				public StreamDesc ir;
				public StreamDesc left;
				public StreamDesc right;
				internal StreamDesc reserved1;
				internal StreamDesc reserved2;
				internal StreamDesc reserved3;

				/// <summary>
				/// Access the stream descriptor by the stream type.
                /// </summary>
                /// <param name="type"> The stream type.</param>
				/// <returns> The stream descriptor instance.</returns>
				public StreamDesc this[PXCMCapture.StreamType type]
				{
					get
					{
						if (type == PXCMCapture.StreamType.STREAM_TYPE_COLOR) return color;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_DEPTH) return depth;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_IR) return ir;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_LEFT) return left;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_RIGHT) return right;
						if (type == PXCMCapture.StreamTypeFromIndex(5)) return reserved1;
						if (type == PXCMCapture.StreamTypeFromIndex(6)) return reserved2;
						return reserved3;
					}
					set
					{
						if (type == PXCMCapture.StreamType.STREAM_TYPE_COLOR) color = value;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_DEPTH) depth = value;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_IR) ir = value;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_LEFT) left = value;
						if (type == PXCMCapture.StreamType.STREAM_TYPE_RIGHT) right = value;
						if (type == PXCMCapture.StreamTypeFromIndex(5)) reserved1 = value;
						if (type == PXCMCapture.StreamTypeFromIndex(6)) reserved2 = value;
						if (type == PXCMCapture.StreamTypeFromIndex(7)) reserved3 = value;
					}
				}

				public StreamDescSet()
				{
					color = new StreamDesc();
					depth = new StreamDesc();
					ir = new StreamDesc();
					left = new StreamDesc();
					right = new StreamDesc();
					reserved1 = new StreamDesc();
					reserved2 = new StreamDesc();
					reserved3 = new StreamDesc();
				}

				internal StreamDescSet(StreamDesc[] descs)
				{
					color = descs[0];
					depth = descs[1];
					ir = descs[2];
					left = descs[3];
					right = descs[4];
					reserved1 = descs[5];
					reserved2 = descs[6];
					reserved3 = descs[7];
				}

				internal StreamDesc[] ToStreamDescArray()
				{
					StreamDesc[] descs = new StreamDesc[PXCMCapture.STREAM_LIMIT];
					descs[0] = color;
					descs[1] = depth;
					descs[2] = ir;
					descs[3] = left;
					descs[4] = right;
					descs[5] = reserved1;
					descs[6] = reserved2;
					descs[7] = reserved3;
					return descs;
				}
			};

		internal class DataDescINT
		{
			public StreamDesc[] streams;                   // requested stream characters 
			public DeviceCap[] devCaps;                     // requested device properties 
			public PXCMCapture.DeviceInfo deviceInfo;       // requested device info 
			 };

			/// <summary> 
			/// struct
			/// Data descriptor to describe the module input needs.
			/// </summary>
			[Serializable]
				[StructLayout(LayoutKind.Sequential)]
				public class DataDesc
				{
					public StreamDescSet streams;                   // requested stream characters
					[MarshalAs(UnmanagedType.ByValArray, SizeConst = DEVCAP_LIMIT)]
					public DeviceCap[] devCaps;                     // requested device properties
					public PXCMCapture.DeviceInfo deviceInfo;       // requested device info 
					[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
						internal Int32[] reserved;

					public DataDesc()
					{
						streams = new StreamDescSet();
						devCaps = new DeviceCap[DEVCAP_LIMIT];
						deviceInfo = new PXCMCapture.DeviceInfo();
						reserved = new Int32[8];
					}

					internal DataDescINT ToDataDescINT()
					{
						DataDescINT data = new DataDescINT();
						data.devCaps = devCaps;
						data.deviceInfo = deviceInfo;
						data.streams = streams.ToStreamDescArray();
						return data;
					}

					internal DataDesc(DataDescINT data)
					{
						devCaps = data.devCaps;
						deviceInfo = data.deviceInfo;
						streams = new StreamDescSet(data.streams);
						reserved = new Int32[8];
					}
				};

			/// <summary> 
			/// Return the active input descriptor that the module works on.
            /// </summary>
            /// <param name="inputs"> The module input descriptor, to be returned.</param>
			/// <returns> PXCM_STATUS_NO_ERROR		Successful execution.</returns>
			public pxcmStatus QueryCaptureProfile(out DataDesc inputs)
			{
				return QueryCaptureProfile(WORKING_PROFILE, out inputs);
			}

			/// <summary> 
			/// Set the active input descriptor with the device information from the capture device.
            /// </summary>
            /// <param name="inputs"> The input descriptor with the device information.</param>
			/// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
			public pxcmStatus SetCaptureProfile(DataDesc inputs)
			{
				return PXCMVideoModule_SetCaptureProfile(instance, inputs);
			}

			/// <summary> 
			/// Feed captured samples to module for processing. If the samples are not available 
			/// immediately, the function will register to run the module processing when the samples 
			/// are ready. This is an asynchronous function. The application must synchronize the 
			/// returned SP before retrieving any module data, which is not available during processing.
            /// </summary>
            /// <param name="images"> The samples from the capture device.</param>
			/// <param name="sp"> The SP, to be returned.</param>
			/// <returns> PXCM_STATUS_NO_ERROR	Successful execution.</returns>
			public pxcmStatus ProcessImageAsync(PXCMCapture.Sample sample, out PXCMSyncPoint sp)
			{
				IntPtr sp2;
				pxcmStatus sts = PXCMVideoModule_ProcessImageAsync(instance, sample, out sp2);
				sp = (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR) ? new PXCMSyncPoint(sp2, true) : null;
				return sts;
			}

			internal PXCMVideoModule(IntPtr instance, Boolean delete)
				: base(instance, delete)
			{
			}
	};

#if RSSDK_IN_NAMESPACE
}
#endif
