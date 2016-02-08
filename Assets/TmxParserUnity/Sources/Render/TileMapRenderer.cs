using UnityEngine;
using System.Collections.Generic;
using TmxParserUnity;
using TmxParserUnity.Model;

public class TileMapRenderer : MonoBehaviour {

    public TileLayerRenderer TileLayerPrefab;
    public ObjectGroupRenderer ObjectGroupPrefab;
    public string TmxFileRelativePath;
    public FilterMode FilterMode;
    public TextureWrapMode WrapMode;
    public Texture2D TileSet;

    public void Build() {
        TileMap tileMap = TmxParser.Parser(Application.dataPath + TmxFileRelativePath);
        List<Layer> layers = tileMap.Layers;

        Color[][] tiles = SplitTileset(tileMap.TileWidth);

        for (int i = 0; i < layers.Count; i++) {
            Layer layer = layers[i];

            if (layer.GetType() == typeof(TileSetLayer)) {

                TileLayerRenderer tileLayer = Instantiate(TileLayerPrefab);

                tileLayer.transform.position = new Vector3(transform.position.x, transform.position.y, -i);
                tileLayer.TileSetLayer = (TileSetLayer) layer;
                tileLayer.FilterMode = FilterMode;
                tileLayer.WrapMode = WrapMode;
                tileLayer.NumberTilesX = tileMap.Width;
                tileLayer.NumberTilesY = tileMap.Height;
                tileLayer.TileResolution = tileMap.TileWidth;
                tileLayer.Tiles = tiles;

                tileLayer.transform.parent = transform;
                tileLayer.gameObject.name = "[" + i + "]TileLayer";

            } else if (layer.GetType() == typeof(ObjectGroupLayer)) {
                ObjectGroupRenderer objectLayer = Instantiate(ObjectGroupPrefab);

                objectLayer.transform.position = new Vector3(transform.position.x, transform.position.y, -i);
                objectLayer.ObjectGroupLayer = (ObjectGroupLayer) layer;
                objectLayer.Tiles = tiles;
                objectLayer.MapHeightInPixels = tileMap.Height * tileMap.TileHeight;
                objectLayer.FilterMode = FilterMode;
                objectLayer.WrapMode = WrapMode;

                objectLayer.transform.parent = transform;
                objectLayer.gameObject.name = "[" + i + "]ObjectGroupLayer";

            } else if (layer.GetType() == typeof(ImageLayer)) {
                // TODO 
            }

        }

        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10));

    }

    private Color[][] SplitTileset(int tileResolution) {

        int numberColumns = TileSet.width / tileResolution;
        int numberRows = TileSet.height / tileResolution;

        Color[][] tiles = new Color[numberColumns * numberRows][];

        Debug.Log(">> Split Tileset");
        Debug.Log("   # Tileset Size: " + TileSet.width + " x " + TileSet.height);
        Debug.Log("   # Tileset ColumnsxRowns: " + numberColumns + " x " + numberRows);
        Debug.Log("   # Tileset Number Tiles: " + tiles.Length);

        int rowIndex = 0;
        for (int y = numberRows - 1; y > -1; y--) {
            for (int x = 0; x < numberColumns; x++) {
                tiles[rowIndex * numberColumns + x] = TileSet.GetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution);
            }
            rowIndex++;
        }

        return tiles;
    }

    void Start () {
        Build();
    }
	
}
