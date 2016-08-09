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
using System.Reflection;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

/// <summary>
/// This interface forms the base of all SDK interface definitions.
/// The interface overrides the class delete operator to work with the SDK
/// dispatching mechanism; and exposes a custom (RTTI-free) dynaimc casting
/// mechanism with the QueryInstance method.  The application that implements
/// any PXCMBase derived interface must derive from one of the PXCMBaseImpl
/// class templates.
/// </summary>
public partial class PXCMBase : IDisposable
{
    internal const String DLLNAME = "libpxccpp2c";
    public const Int32 CUID = 0;
    public const Int32 WORKING_PROFILE = -1;

    /* Class Body */
    internal IntPtr instance;
    protected Int32 refcount;
    internal PXCMBase orig;

    public IntPtr QueryNativePointer() { return instance; }

    internal PXCMBase(IntPtr instance, Boolean delete)
    {
        this.instance = instance;
        refcount = (delete ? 1 : 0);
        orig = this;
    }

    ~PXCMBase()
    {
        Dispose();
    }

    /// <summary>
    /// The function disposes the object instance. Certain objects are reference
    /// counted, in which case the function reduces the reference count by 1.
    /// </summary>
    public virtual void Dispose()
    {
        Int32 refcount2;
        lock (this)
        {
            refcount2 = refcount;
            if (refcount > 0) refcount--;
        }
        if (refcount2 > 0 && instance != IntPtr.Zero)
        {
            PXCMBase_Release(instance);
        }
        else
        {
            /* If disposed, remove reference to orig so that orig can be GC'ed */
            orig = this;
        }
    }

    internal Int32 AddRef()
    {
        lock (this)
        {
            return ++refcount;
        }
    }


    /// <summary>
    /// The function checks if the object instance supports a specific interface.
    /// If so, return the instance of the interface. Otherwise, returns NULL.
    /// </summary>
    /// <param name="cuid"> The interface identifier.</param>
    /// <returns> The interface instance, or NULL.</returns>
    public PXCMBase QueryInstance(Int32 cuid)
    {
        IntPtr instance2 = PXCMBase_QueryInstance(instance, cuid);
        if (instance2 == IntPtr.Zero) return null;
        PXCMBase base1 = CUID2CTOR[cuid].Invoke(new object[] { instance2, false }) as PXCMBase;
        if (base1 == null)
            return null;
        /* Reference to orig so not to GC it. */
        base1.orig = orig;
        return base1;
    }

    /// <summary>
    /// The function checks if the object instance supports a specific interface.
    /// If so, return the instance of the interface. Otherwise, returns null.
    /// </summary>
    /// <returns> The interface instance, or null.</returns>
    public TT QueryInstance<TT>() where TT : PXCMBase
    {
#if !NETFX_CORE
        return (TT)QueryInstance(Type2CUID[typeof(TT)]);
#else
			return (TT)QueryInstance(Type2CUID[typeof(TT).GetTypeInfo()]);
#endif
    }

    internal static PXCMBase IntPtr2PXCMBase(IntPtr instance, Int32 cuid)
    {
        PXCMBase base1 = CUID2CTOR[cuid].Invoke(new object[] { instance, false }) as PXCMBase;
        if (base1 == null)
            return null;
        /* Reference to orig so not to GC it. */
        base1.orig = base1;
        return base1;
    }

#if !NETFX_CORE
    protected static Dictionary<Type, Int32> Type2CUID = new Dictionary<Type, Int32>();
#else
		protected static Dictionary<TypeInfo, Int32> Type2CUID = new Dictionary<TypeInfo, Int32>();
#endif
    private static Dictionary<Int32, ConstructorInfo> CUID2CTOR = new Dictionary<Int32, ConstructorInfo>();

    internal static string CUID2String(Int32 cuid)
    {
        ConstructorInfo ctor;
        if (PXCMBase.CUID2CTOR.TryGetValue(cuid, out ctor)) return ctor.Name;
        return "UnknownCUID";
    }

    static PXCMBase()
    {
#if !NETFX_CORE
        foreach (Type type in Assembly.GetAssembly(typeof(PXCMBase)).GetExportedTypes())
        {
            if (!type.IsSubclassOf(typeof(PXCMBase))) continue;
            FieldInfo info = type.GetField("CUID");
            if (info == null) continue;
            if (!info.IsLiteral || !info.IsPublic) continue;
            Int32 cuid = (Int32)info.GetValue(null);
            Type2CUID[type] = cuid;

            ConstructorInfo ctor2 = null;
            foreach (ConstructorInfo ctor in type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                ParameterInfo[] args = ctor.GetParameters();
                if (args.Length != 2) continue;
                if (args[0].ParameterType != typeof(IntPtr)) continue;
                if (args[1].ParameterType != typeof(Boolean)) continue;
                ctor2 = ctor;
                break;
            }
            if (ctor2 == null) continue;
            CUID2CTOR[cuid] = ctor2;
        }
#else
			foreach (Type t in typeof(PXCMBase).GetTypeInfo().Assembly.ExportedTypes)
			{
				TypeInfo type = t.GetTypeInfo();
				if (!type.IsSubclassOf(typeof(PXCMBase))) continue;
				FieldInfo info = type.GetDeclaredField("CUID");
				if (info == null) continue;
				if (!info.IsLiteral || !info.IsPublic) continue;
				Int32 cuid = (Int32)info.GetValue(null);
				Type2CUID[type] = cuid;

				ConstructorInfo ctor2 = null;
				foreach (ConstructorInfo ctor in type.DeclaredConstructors)
				{
					ParameterInfo[] args=ctor.GetParameters();
					if (args.Length!=2) continue;
					if (args[0].ParameterType != typeof(IntPtr)) continue;
					if (args[1].ParameterType != typeof(Boolean)) continue;
					ctor2=ctor;
					break;
				}
				if (ctor2==null) continue;
				CUID2CTOR[cuid] = ctor2;
			}
#endif
    }

    internal partial class ObjectPair
    {
        public object managed;
        public IntPtr unmanaged;
    };
};

#if RSSDK_IN_NAMESPACE
}
#endif
