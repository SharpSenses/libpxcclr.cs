using System;
using System.Runtime.InteropServices;

public class PXCMBlockMeshingData : PXCMBase {
    public new const int CUID = 1296191571;

    internal PXCMBlockMeshingData(IntPtr instance, bool delete)
        : base(instance, delete) {}

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryNumberOfBlockMeshes(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryNumberOfVertices(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryNumberOfFaces(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryMaxNumberOfBlockMeshes(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryMaxNumberOfVertices(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    internal static extern int PXCMBlockMeshingData_QueryMaxNumberOfFaces(IntPtr mesh);

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMBlockMeshingData_QueryBlockMeshes(IntPtr mesh);

    internal static void QueryBlockMeshesINT(IntPtr mesh, PXCMBlockMesh[] meshes, int nmeshes) {
        var ptr = PXCMBlockMeshingData_QueryBlockMeshes(mesh);
        for (var index = 0; index < nmeshes; ++index) {
            if (meshes[index] == null) {
                meshes[index] = new PXCMBlockMesh();
            }
            Marshal.PtrToStructure(ptr, meshes[index]);
            ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof (PXCMBlockMesh)));
        }
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMBlockMeshingData_QueryVertices(IntPtr mesh);

    internal static void QueryVerticesINT(IntPtr mesh, float[] vertices, int nvertices) {
        Marshal.Copy(PXCMBlockMeshingData_QueryVertices(mesh), vertices, 0, nvertices*4);
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMBlockMeshingData_QueryVerticesColor(IntPtr mesh);

    internal static void QueryVerticesColorINT(IntPtr mesh, byte[] colors, int nvertices) {
        Marshal.Copy(PXCMBlockMeshingData_QueryVerticesColor(mesh), colors, 0, nvertices*3);
    }

    [DllImport("libpxccpp2c")]
    private static extern IntPtr PXCMBlockMeshingData_QueryFaces(IntPtr mesh);

    internal static void QueryFacesINT(IntPtr mesh, int[] faces, int nfaces) {
        Marshal.Copy(PXCMBlockMeshingData_QueryFaces(mesh), faces, 0, nfaces*3);
    }

    [DllImport("libpxccpp2c")]
    internal static extern pxcmStatus PXCMBlockMeshingData_Reset(IntPtr mesh);

    public int QueryNumberOfBlockMeshes() {
        return PXCMBlockMeshingData_QueryNumberOfBlockMeshes(instance);
    }

    public int QueryNumberOfVertices() {
        return PXCMBlockMeshingData_QueryNumberOfVertices(instance);
    }

    public int QueryNumberOfFaces() {
        return PXCMBlockMeshingData_QueryNumberOfFaces(instance);
    }

    public int QueryMaxNumberOfBlockMeshes() {
        return PXCMBlockMeshingData_QueryMaxNumberOfBlockMeshes(instance);
    }

    public int QueryMaxNumberOfVertices() {
        return PXCMBlockMeshingData_QueryMaxNumberOfVertices(instance);
    }

    public int QueryMaxNumberOfFaces() {
        return PXCMBlockMeshingData_QueryMaxNumberOfFaces(instance);
    }

    public PXCMBlockMesh[] QueryBlockMeshes(PXCMBlockMesh[] meshes) {
        var nmeshes = QueryNumberOfBlockMeshes();
        if (nmeshes <= 0) {
            return null;
        }
        if (meshes == null) {
            meshes = new PXCMBlockMesh[nmeshes];
        }
        QueryBlockMeshesINT(instance, meshes, nmeshes);
        return meshes;
    }

    public PXCMBlockMesh[] QueryBlockMeshes() {
        return QueryBlockMeshes(null);
    }

    public float[] QueryVertices(float[] vertices) {
        var nvertices = QueryNumberOfVertices();
        if (nvertices <= 0) {
            return null;
        }
        if (vertices == null) {
            vertices = new float[4*nvertices];
        }
        QueryVerticesINT(instance, vertices, nvertices);
        return vertices;
    }

    public float[] QueryVertices() {
        return QueryVertices(null);
    }

    public byte[] QueryVerticesColor(byte[] colors) {
        var nvertices = QueryNumberOfVertices();
        if (nvertices <= 0) {
            return null;
        }
        if (colors == null) {
            colors = new byte[3*nvertices];
        }
        QueryVerticesColorINT(instance, colors, nvertices);
        return colors;
    }

    public byte[] QueryVerticesColor() {
        return QueryVerticesColor(null);
    }

    public int[] QueryFaces(int[] faces) {
        var nfaces = QueryNumberOfFaces();
        if (nfaces <= 0) {
            return null;
        }
        if (faces == null) {
            faces = new int[3*nfaces];
        }
        QueryFacesINT(instance, faces, nfaces);
        return faces;
    }

    public int[] QueryFaces() {
        return QueryFaces(null);
    }

    public pxcmStatus Reset() {
        return PXCMBlockMeshingData_Reset(instance);
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class PXCMBlockMesh {
        public int meshId;
        public int vertexStartIndex;
        public int numVertices;
        public int faceStartIndex;
        public int numFaces;
        public PXCMPoint3DF32 min3dPoint;
        public PXCMPoint3DF32 max3dPoint;
        public float maxDistanceChange;
        public float avgDistanceChange;
    }
}