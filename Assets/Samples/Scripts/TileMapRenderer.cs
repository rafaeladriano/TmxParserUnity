using UnityEngine;
using System.Collections.Generic;
using tmxparser;
using tmxparser.model;

public class TileMapRenderer : MonoBehaviour {

    public TileLayerRenderer Prefab;
    public string TmxFileRelativePath;
    public FilterMode FilterMode;
    public TextureWrapMode WrapMode;
    public Texture2D TileSet;


    public void Build() {
        TileMap tilemap = TmxParser.Parser(Application.dataPath + TmxFileRelativePath);
        List<Layer> layers = tilemap.Layers;

        int index = 0;

        for (int i = 0; i < layers.Count; i++) {
            Layer layer = layers[i];
            if (layer.GetType() == typeof(TileSetLayer)) {
                TileLayerRenderer tilelayer = Instantiate(Prefab);
                tilelayer.transform.position = new Vector3(transform.position.x, transform.position.y, -index);
                tilelayer.TmxFileRelativePath = TmxFileRelativePath;
                tilelayer.FilterMode = FilterMode;
                tilelayer.WrapMode = WrapMode;
                tilelayer.TileSet = TileSet;
                tilelayer.IndexLayer = index;
                tilelayer.transform.parent = transform;
                index++;
            } else if (layer.GetType() == typeof(ObjectGroupLayer)) {
                // TODO
            } else if (layer.GetType() == typeof(ImageLayer)) {
                // TODO 
            }
        }
    }

    void Start () {
        Build();
    }
	
}
