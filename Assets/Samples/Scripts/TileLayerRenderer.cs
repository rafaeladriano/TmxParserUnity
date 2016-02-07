using UnityEngine;
using tmxparser;
using tmxparser.model;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileLayerRenderer : MonoBehaviour {

    public string TmxFileRelativePath;
    public FilterMode FilterMode;
    public TextureWrapMode WrapMode;
    public Texture2D TileSet;
    public int IndexLayer;

    private TileMap tileMap;
    private int numberTilesX;
    private int numberTilesY;
    private int tileResolution;
    private Color[] transparentColor;

    private const int NUMBER_VERTICES_SQUARE = 6; // 6 é número de vértices que tem um quadrado (= 2 triangulos)
    private const float ALPHA_OPAQUE = 1.0f;   

    void Start () {
        ReadTmxFile();

        transparentColor = new Color[tileMap.TileHeight * tileMap.TileWidth];
        for (int i = 0; i < transparentColor.Length; i++) {
            transparentColor[i] = new Color(0f, 0f, 0f, 0f);
        }

        Build();
	}

    private void Build() {
        Debug.Log("> Build Tilemap");
        Debug.Log("  # Tilemap Size: " + numberTilesX * tileResolution + " x " + numberTilesY * tileResolution);
        Debug.Log("  # Tilemap Columns/Rowns: " + numberTilesX + " x " + numberTilesY);
        Debug.Log("  # Tile Resolution: " + tileResolution);
        Debug.Log("  # Screen Resolution: " + Camera.main.pixelWidth + " x " + Camera.main.pixelHeight);
        BuildPlaneMesh();
        BuildTexture((TileSetLayer) tileMap.Layers[IndexLayer]);
    }

    private void ReadTmxFile() {
        Debug.Log("> Read Tmx File [" + Application.dataPath + TmxFileRelativePath + "]");
        tileMap = TmxParser.Parser(Application.dataPath + TmxFileRelativePath);
        numberTilesX = tileMap.Width;
        numberTilesY = tileMap.Height;
        tileResolution = tileMap.TileWidth;
    }

    private Color[][] SplitTileset() {
        
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

    private void BuildTexture(TileSetLayer tileSetLayer) {

        int textureWidth = numberTilesX * tileResolution;
        int textureHeight = numberTilesY * tileResolution;

        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        texture.alphaIsTransparency = true;

        Color[][] tiles = SplitTileset();

        Debug.Log(">> Build Texture");
        Debug.Log("   # Texture Resolution: " + texture.width + " x " + texture.width);

        int tileRowIndex = 0;
        for (int y = numberTilesY - 1; y > -1; y--) {
            int lineInPixels = y * tileResolution;
            for (int x = 0; x < numberTilesX; x++) {
                Color[] tile = transparentColor;

                int tileID = tileSetLayer.Tiles[x, tileRowIndex];
                if (tileID != TileSetLayer.EMPTY_TILE) {
                    tile = tiles[tileID - 1];
                    if (tileSetLayer.Opacity < ALPHA_OPAQUE) {
                        SetOpacity(tile, tileSetLayer.Opacity);
                    }
                }

                texture.SetPixels(x * tileResolution, lineInPixels, tileResolution, tileResolution, tile);
            }
            tileRowIndex++;
        }

        texture.filterMode = FilterMode;
        texture.wrapMode = WrapMode;
        texture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = texture;
    }

    private void BuildPlaneMesh() {

        int numberTiles = numberTilesX * numberTilesY;

        int vNumberTilesX = numberTilesX + 1;
        int vNumberTilesY = numberTilesY + 1;
        int numberVertices = vNumberTilesX * vNumberTilesY;

        Debug.Log(">> Build Mesh");
        Debug.Log("   # Number Vertices: " + numberVertices);

        // Criar uma quadrado com a face para cima, mapeando o material
        Vector3[] vertices = new Vector3[numberVertices];
        Vector3[] normals = new Vector3[numberVertices];
        Vector2[] uv = new Vector2[numberVertices];
        int[] triangles = new int[numberTiles * NUMBER_VERTICES_SQUARE];

        for (int indexTileY = 0; indexTileY < vNumberTilesY; indexTileY++) {
            for (int indexTileX = 0; indexTileX < vNumberTilesX; indexTileX++) {
                int verticeIndex = indexTileY * vNumberTilesX + indexTileX;
                vertices[verticeIndex] = new Vector3(indexTileX * tileResolution, indexTileY * tileResolution);
                normals[verticeIndex] = Vector3.up;
                uv[verticeIndex] = new Vector2((float) indexTileX / numberTilesX, (float) indexTileY / numberTilesY);
            }
        }

        for (int indexTileY = 0; indexTileY < numberTilesY; indexTileY++) {
            for (int indexTileX = 0; indexTileX < numberTilesX; indexTileX++) {
                int squareIndex = indexTileY * numberTilesX + indexTileX;
                int triangleIndexOffset = squareIndex * NUMBER_VERTICES_SQUARE;

                int triangleVerticeIndexA = indexTileY * vNumberTilesX + indexTileX;
                int triangleVerticeIndexB = triangleVerticeIndexA + vNumberTilesX;

                triangles[triangleIndexOffset + 0] = triangleVerticeIndexA + 0;
                triangles[triangleIndexOffset + 1] = triangleVerticeIndexB + 0;
                triangles[triangleIndexOffset + 2] = triangleVerticeIndexB + 1;

                triangles[triangleIndexOffset + 3] = triangleVerticeIndexA + 0;
                triangles[triangleIndexOffset + 4] = triangleVerticeIndexB + 1;
                triangles[triangleIndexOffset + 5] = triangleVerticeIndexA + 1;
            }
        }

        // Cria um novo mesh e popula com os dados
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        // Define o criado mesh para nosso filter/renderer/coliider
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private void SetOpacity(Color[] pixels, float opacity) {
        for (int indexPixel = 0; indexPixel < pixels.Length; indexPixel++) {
            if (pixels[indexPixel].a == ALPHA_OPAQUE) {
                pixels[indexPixel].a = opacity;
            }
        }
    }
	
}
