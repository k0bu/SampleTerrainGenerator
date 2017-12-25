using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Noiseで使われる、起伏の激しさをコントロールできる変数の一つ
    public float noiseScale;

    //NoiseMapの値を分かりやすくするための調整用MeshGeneratorで使われる
    public float heightMultiplier;
    //NoiseMapをもっと綺麗にするための調整用MeshGeneratorで使われる
    public AnimationCurve meshHeightCurve;

    //Noiseで使われる、加算するオクターブの数
    public int octaves;
    //Noiseで使われる、オクターブ数が増えるほど振幅が減衰するように0から1
    [Range(0, 1)]
    public float persistance;
    //Noiseで使われる、周波数をコントロールする変数、波形の変化を急にしていく
    public float lacunarity;

    //Noiseで使われる、疑似ランダム用の変数、同じ数字を入れれば同じNoiseMapが得られる
    public int seed;
    //Noiseで使われる、NoiseMapを移動させることができる
    public Vector2 offset;

    //常にInspectorで値を変えたらアップデートされるようにするかしないか、MapGeneratorEditorで使われる
    public bool autoUpdate;

    //noiseMapの[x,y]においての高さと比べて色を決めるためのパラメータが入った変数、このクラスで使われる
    public TerrainType[] regions;

    //１つのMeshにつき65000までの頂点
    [Range(32, 254)]
    public int mapChunkSize;
    //MapDisplayスクリプトをInspector上で指定
    [SerializeField] MapDisplay display;

    public void DrawMapInEditor()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap
            (mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        //noiseMap[x,y]値と比較してつける色を決めcolourMapに格納する
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        if (display)
        {
            //実際に得たMeshとTextyre2Dを適用する
            display.DrawMesh(
                MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, meshHeightCurve),
                TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize));
        }
    }
}
//ちょっと使いづらいと思います
[System.Serializable]
public struct TerrainType
{
    //名前を付けるだけ
    public string name;
    //noiseMapと比較する値を格納する変数
    public float height;
    //色を指定する
    public Color colour;
}
