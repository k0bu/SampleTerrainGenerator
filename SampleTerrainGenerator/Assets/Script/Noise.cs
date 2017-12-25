using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap
        (int mapSize, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        if (scale <= 0) scale = 1f;
        float[,] noiseMap = new float[mapSize, mapSize];

        //seedの値によって決まる疑似的ランダム(pseudo random)
        System.Random pseudoRnd = new System.Random(seed);

        //offsetの位置を変えることによってランダム生成
        offset.x += pseudoRnd.Next(-99999, 99999);
        offset.y += pseudoRnd.Next(-99999, 99999);

        //for文の中で必ず値が置き換わって比較できるようにmaxにMinValue、minにMaxValue
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfSize = mapSize / 2f;

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    //scaleを上げることによって変位を緩やかに、下げると変位が唐突に
                    float coordX = (x - halfSize) / scale * frequency + offset.x;
                    float coordY = (y - halfSize) / scale * frequency + offset.y;

                    float perlinValue = Mathf.PerlinNoise(coordX, coordY);
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;

                }
                //比較して一番低い位置と高い位置を求める
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;

            }
        }

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                //minを0.0f、maxを1.0fとしてnoiseMapを割合で当てはめる
                noiseMap[x, y] = Mathf.InverseLerp
                    (minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
