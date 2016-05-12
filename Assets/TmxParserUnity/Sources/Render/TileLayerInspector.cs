using UnityEngine;
using UnityEditor;
using TmxParserUnity;

[CustomEditor(typeof(TileMapRenderer))]
public class TileLayerInspector : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) {
            TileMapRenderer tilemap = (TileMapRenderer)target;
            Utils.Destroy(tilemap.transform);
            tilemap.Build();
        }

        if (GUILayout.Button("Destroy")) {
            TileMapRenderer tilemap = (TileMapRenderer)target;
            Utils.Destroy(tilemap.transform);
        }

    }

}
