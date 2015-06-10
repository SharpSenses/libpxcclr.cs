using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

public class PXCMBase : IDisposable {
    internal const string DLLNAME = "libpxccpp2c";
    public const int CUID = 0;
    public const int WORKING_PROFILE = -1;
    protected static Dictionary<Type, int> Type2CUID = new Dictionary<Type, int>();
    private static Dictionary<int, ConstructorInfo> CUID2CTOR = new Dictionary<int, ConstructorInfo>();
    public IntPtr instance;
    internal PXCMBase orig;
    protected int refcount;

    static PXCMBase() {
        foreach (var index1 in Assembly.GetAssembly(typeof (PXCMBase)).GetExportedTypes()) {
            if (index1.IsSubclassOf(typeof (PXCMBase))) {
                var field = index1.GetField("CUID");
                if (!(field == null) && field.IsLiteral && field.IsPublic) {
                    var index2 = (int) field.GetValue(null);
                    Type2CUID[index1] = index2;
                    ConstructorInfo constructorInfo1 = null;
                    foreach (
                        var constructorInfo2 in index1.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)) {
                        var parameters = constructorInfo2.GetParameters();
                        if (parameters.Length == 2 && !(parameters[0].ParameterType != typeof (IntPtr)) &&
                            !(parameters[1].ParameterType != typeof (bool))) {
                            constructorInfo1 = constructorInfo2;
                            break;
                        }
                    }
                    if (!(constructorInfo1 == null)) {
                        CUID2CTOR[index2] = constructorInfo1;
                    }
                }
            }
        }
    }

    internal PXCMBase(IntPtr instance, bool delete) {
        this.instance = instance;
        refcount = delete ? 1 : 0;
        orig = this;
    }

    public virtual void Dispose() {
        int num;
        lock (this) {
            num = refcount;
            if (refcount > 0) {
                --refcount;
            }
        }
        if (num > 0 && instance != IntPtr.Zero) {
            PXCMBase_Release(instance);
        }
        else {
            orig = this;
        }
    }

    ~PXCMBase() {
        Dispose();
    }

    [DllImport("libpxccpp2c")]
    internal static extern void PXCMBase_Release(IntPtr pbase);

    [DllImport("libpxccpp2c")]
    internal static extern IntPtr PXCMBase_QueryInstance(IntPtr pbase, int cuid);

    internal int AddRef() {
        lock (this)
            return ++refcount;
    }

    public PXCMBase QueryInstance(int cuid) {
        var num = PXCMBase_QueryInstance(instance, cuid);
        if (num == IntPtr.Zero) {
            return null;
        }
        var pxcmBase = CUID2CTOR[cuid].Invoke(new object[2] {
            num,
            false
        }) as PXCMBase;
        if (pxcmBase == null) {
            return null;
        }
        pxcmBase.orig = orig;
        return pxcmBase;
    }

    public TT QueryInstance<TT>() where TT : PXCMBase {
        return (TT) QueryInstance(Type2CUID[typeof (TT)]);
    }

    internal static PXCMBase IntPtr2PXCMBase(IntPtr instance, int cuid) {
        var pxcmBase = CUID2CTOR[cuid].Invoke(new object[2] {
            instance,
            false
        }) as PXCMBase;
        if (pxcmBase == null) {
            return null;
        }
        pxcmBase.orig = pxcmBase;
        return pxcmBase;
    }

    internal static string CUID2String(int cuid) {
        ConstructorInfo constructorInfo;
        if (CUID2CTOR.TryGetValue(cuid, out constructorInfo)) {
            return constructorInfo.Name;
        }
        return "UnknownCUID";
    }

    internal class ObjectPair : IDisposable {
        public object managed;
        public IntPtr unmanaged;

        public ObjectPair(object tt) {
            managed = tt;
            unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf(managed));
            Marshal.StructureToPtr(managed, unmanaged, false);
        }

        public ObjectPair(object tt, Type type) {
            managed = tt;
            unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf(type));
        }

        public void Dispose() {
            if (!(unmanaged != IntPtr.Zero)) {
                return;
            }
            Marshal.FreeHGlobal(unmanaged);
            unmanaged = IntPtr.Zero;
        }

        ~ObjectPair() {
            Dispose();
        }
    }
}