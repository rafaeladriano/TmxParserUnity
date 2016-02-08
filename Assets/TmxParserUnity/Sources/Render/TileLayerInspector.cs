using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileMapRenderer))]
public class TileLayerInspector : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) {
            TileMapRenderer tilemap = (TileMapRenderer)target;
            tilemap.Build();
        }

    }
}
