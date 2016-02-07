using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TileMapRenderer))]
public class TileLayerInspector : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate")) {
            TileMapRenderer tilemap = (TileMapRenderer)target;
            tilemap.Build();
        }

    }
}
