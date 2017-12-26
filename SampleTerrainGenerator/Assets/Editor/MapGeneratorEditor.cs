using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{

    //Editor拡張で大抵する決まり文句
    public override void OnInspectorGUI()
    {
        MapGenerator MapGen = (MapGenerator)target;
        if (DrawDefaultInspector())
        {
            //Inspector上でautoUpdateがtrue、falseと切り替えれるのを使う
            if (MapGen.autoUpdate)
            {
                MapGen.DrawMapInEditor();
            }
        }
    }
}
