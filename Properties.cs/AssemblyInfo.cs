using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Intel(R) RealSense(TM) SDK C# Bridge Library")]
[assembly: AssemblyDescription("libpxcclr.cs")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("Intel(R) RealSense(TM) SDK")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b51e736a-bcfc-4d3d-b2a6-9abc50c783c4")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("6.0.21")]
[assembly: InternalsVisibleTo("Capability.Servicer.RealSense, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b37ce310da0809c04567a80b501a0f93b1f3d78eb4e9d2d480e8ef38b2cdde824909a73ebe49f2c3bea203bb2724c5cbdf6fe5c0d1b267e8240aaf2e9b1bce2fbf05f6700fdcf843041c04e489047b0327ebc366d706abf96bc24adbf695b2a3e07b96cb98b6dade08987bd70a349e220b377df961b1ed99407e4f258a560fbf")]
[assembly: InternalsVisibleTo("verifyintegrity, PublicKey=00240000048000009400000006020000002400005253413100040000010001008764b8cf58971f6fa6561541cea3de4bd766d77dd15d74b061f246cf22910c135c00a3a2b6fab35ced1f451bed394c395cd03b9ea10d70a026c317eb16de831ea97998655e80a8d8aa31edff9e9040bb7fa6cca54b98889036e85f273c703651c09a97f5f0e6581c64daa14a27d6d7e8ef63d696928daac0c6cea554acb210c1")]
[assembly: InternalsVisibleTo("libpxciutils.cs, PublicKey=002400000480000094000000060200000024000052534131000400000100010015ef0f599ee35d59a1d02a4ed5ad94e3f15808badd2a7e4599cca16acacc6d6b4772540e77ff813531a90eb2aee8e37af339bbb8f6122d68d537f7e106cead1221e3d09f73959b3b7a3f204f9417ee472f8989acc531cabe5542a8ab6edf725b553af2087160b8f7018b51c1a48d3838d4301ee059dc8902434690658a9e96b2")]
#if SAMPLEDX_CUSTOMIZATION
[assembly: InternalsVisibleTo("libsampledx")]
#else
[assembly: InternalsVisibleTo("libsampledx, PublicKey=002400000480000094000000060200000024000052534131000400000100010089f521d4f3c39d7d091255d7233a71ff7e473054daa313b824dae99390214d541a9394e689ecd0fb6f8682430f8931947951f46364e5f50d6d06231c242bde477781830f8ef4dda2f9f1b601fe0cac4cc8369ac97e8e3e099e1c2316d80cab9c66a59db203817f1d916451df67fc80cc9df017f603f3a554b5f1714c5ea173bb")]
#endif
[assembly: CLSCompliant(true)]
