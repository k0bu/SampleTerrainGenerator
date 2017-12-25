using UnityEngine;

public class MapDisplay : MonoBehaviour
{

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(Mesh mesh, Texture2D texture)
    {
        //各頂点のnormal vectorを計算し直してくれる
        //複数のchunkを扱う際はchunkとchunkの間がおかしくなるので自分で処理を書く必要がある
        mesh.RecalculateNormals();
        //run timeじゃなくても表現されるようにsharedの方で
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
