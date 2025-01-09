using UnityEngine;

public class CubeMeshRandomizer : IMeshRandomizer
{
    private readonly Vector3[] _vertices;

    public CubeMeshRandomizer(Vector3[] vertices)
    {
        _vertices = vertices;
    }
    public void Randomize(Mesh mesh)
    {
        var verts = mesh.vertices;
        var rand = Random.insideUnitSphere * 0.25f;
        verts[0] = _vertices[0] + rand;
        verts[12] = _vertices[12] + rand;
        verts[20] = _vertices[20] + rand;

        rand = Random.insideUnitSphere * 0.25f;
        verts[1] = _vertices[1] + rand;
        verts[13] = _vertices[13] + rand;
        verts[16] = _vertices[16] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[4] = _vertices[4] + rand;
        verts[15] = _vertices[15] + rand;
        verts[23] = _vertices[23] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[5] = _vertices[5] + rand;
        verts[14] = _vertices[14] + rand;
        verts[19] = _vertices[19] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[7] = _vertices[7] + rand;
        verts[11] = _vertices[11] + rand;
        verts[22] = _vertices[22] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[6] = _vertices[6] + rand;
        verts[10] = _vertices[10] + rand;
        verts[18] = _vertices[18] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[3] = _vertices[3] + rand;
        verts[8] = _vertices[8] + rand;
        verts[21] = _vertices[21] + rand;
        
        rand = Random.insideUnitSphere * 0.25f;
        verts[2] = _vertices[2] + rand;
        verts[9] = _vertices[9] + rand;
        verts[17] = _vertices[17] + rand;

        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}