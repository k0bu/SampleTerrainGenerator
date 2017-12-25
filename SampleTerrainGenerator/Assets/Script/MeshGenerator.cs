using UnityEngine;

public static class MeshGenerator
{
    public static Mesh GenerateTerrainMesh
        (float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int verticesPerLine = heightMap.GetLength(0);

        int triangleIndex = 0;
        Vector3[] vertices = new Vector3[verticesPerLine * verticesPerLine];
        int[] triangles = new int[(verticesPerLine - 1) * (verticesPerLine - 1) * 6];
        Vector2[] uv = new Vector2[verticesPerLine * verticesPerLine];

        Mesh mesh = new Mesh();

        int vertexIndex = 0;

        for (int y = 0; y < verticesPerLine; y++)
        {
            for (int x = 0; x < verticesPerLine; x++)
            {
                vertices[vertexIndex] = new Vector3
                    (x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, -y);
                uv[vertexIndex] = new Vector2
                    (x / (float)verticesPerLine, y / (float)verticesPerLine);

                if (x < verticesPerLine - 1 && y < verticesPerLine - 1)
                {
                    triangles[triangleIndex] = vertexIndex;
                    triangles[triangleIndex + 1] = vertexIndex + verticesPerLine + 1;
                    triangles[triangleIndex + 2] = vertexIndex + verticesPerLine;

                    triangles[triangleIndex + 3] = vertexIndex + verticesPerLine + 1;
                    triangles[triangleIndex + 4] = vertexIndex;
                    triangles[triangleIndex + 5] = vertexIndex + 1;
                    triangleIndex += 6;
                }
                vertexIndex++;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        return mesh;
    }
}
