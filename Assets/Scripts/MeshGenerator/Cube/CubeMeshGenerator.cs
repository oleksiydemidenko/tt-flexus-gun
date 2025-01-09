using UnityEngine;

public class CubeMeshGenerator : IMeshGenerator
{
    public readonly Vector3[] vertices;
    public readonly int[] triangles;
    public readonly Vector2[] uv;

    public CubeMeshGenerator()
    {
        vertices = GenerateVertices();
        triangles = GenerateTriangles(vertices);
        uv = GenerateUV();
    }

    private Vector2[] GenerateUV()
    {
        return new Vector2[] 
        {
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.right, Vector2.zero, Vector2.up, Vector2.one,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up,
            Vector2.zero, Vector2.right, Vector2.one, Vector2.up
        };
    }
    private int[] GenerateTriangles(Vector3[] vetrtices)
    {
        return new int[] 
        {
            0, 1, 2,
            2, 3, 0,

            7, 5, 4,
            6, 5, 7,

            8, 9,  10,
            8, 10, 11,

            12, 14, 13,
            12, 15, 14,

            16, 18, 17,
            16, 19, 18,

            20, 21, 22,
            20, 22, 23
        };
    }
    private Vector3[] GenerateVertices()
    {
        return new Vector3[] 
        {
            //F
            new (-1, -1,  1),
            new ( 1, -1,  1),
            new ( 1,  1,  1),
            new (-1,  1,  1),

            //B
            new (-1, -1, -1),
            new ( 1, -1, -1),
            new ( 1,  1, -1),
            new (-1,  1, -1),

            //U
            new (-1,  1,  1),
            new ( 1,  1,  1),
            new ( 1,  1, -1),
            new (-1,  1, -1),

            //D
            new (-1, -1,  1),
            new ( 1, -1,  1),
            new ( 1, -1, -1),
            new (-1, -1, -1),

            //R
            new ( 1, -1,  1),
            new ( 1,  1,  1),
            new ( 1,  1, -1),
            new ( 1, -1, -1),

            //L
            new (-1, -1,  1),
            new (-1,  1,  1),
            new (-1,  1, -1),
            new (-1, -1, -1)
        };
    }
    
    public Mesh Generate()
    {
        var mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
}