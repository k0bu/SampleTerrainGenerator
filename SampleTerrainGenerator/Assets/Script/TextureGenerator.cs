using UnityEngine;

public static class TextureGenerator
{

    public static Texture2D TextureFromColourMap(Color[] colourMap, int size)
    {
        Texture2D texture = new Texture2D(size, size);
        //補間なしで色がつく
        texture.filterMode = FilterMode.Point;
        //Repeatで繰り返し、Clampで単一
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }
}
