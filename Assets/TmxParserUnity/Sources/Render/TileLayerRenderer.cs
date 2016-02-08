using UnityEngine;
using TmxParserUnity;
using TmxParserUnity.Model;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileLayerRenderer : MonoBehaviour {

    [HideInInspector]
    public TileSetLayer TileSetLayer;
    [HideInInspector]
    public FilterMode FilterMode;
    [HideInInspector]
    public TextureWrapMode WrapMode;
    [HideInInspector]
    public int NumberTilesX;
    [HideInInspector]
    public int NumberTilesY;
    [HideInInspector]
    public int TileResolution;
    [HideInInspector]
    public Color[][] Tiles;

    private Color[] transparentColor;

    private const int NUMBER_VERTICES_SQUARE = 6; // 6 é número de vértices que tem um quadrado (= 2 triangulos)
    private const float ALPHA_OPAQUE = 1.0f;   

    void Start () {
        transparentColor = new Color[TileResolution * TileResolution];
        for (int i = 0; i < transparentColor.Length; i++) {
            transparentColor[i] = new Color(0f, 0f, 0f, 0f);
        }

        Debug.Log("> Build Tilemap");
        Debug.Log("  # Tilemap Size: " + NumberTilesX * TileResolution + " x " + NumberTilesY * TileResolution);
        Debug.Log("  # Tilemap Columns/Rowns: " + NumberTilesX + " x " + NumberTilesY);
        Debug.Log("  # Tile Resolution: " + TileResolution);
        Debug.Log("  # Screen Resolution: " + Camera.main.pixelWidth + " x " + Camera.main.pixelHeight);

        BuildPlaneMesh();
        BuildTexture();
    }
    
    private void BuildTexture() {

        int textureWidth = NumberTilesX * TileResolution;
        int textureHeight = NumberTilesY * TileResolution;

        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        Debug.Log(">> Build Texture");
        Debug.Log("   # Texture Resolution: " + texture.width + " x " + texture.width);

        int tileRowIndex = 0;
        for (int y = NumberTilesY - 1; y > -1; y--) {
            int lineInPixels = y * TileResolution;
            for (int x = 0; x < NumberTilesX; x++) {
                Color[] tile = transparentColor;

                int tileID = TileSetLayer.Tiles[x, tileRowIndex];
                if (tileID != TileSetLayer.EMPTY_TILE) {
                    tile = Tiles[tileID - 1];
                    if (TileSetLayer.Opacity < ALPHA_OPAQUE) {
                        tile = CopyWithOpacity(tile, TileSetLayer.Opacity);
                    }
                }

                texture.SetPixels(x * TileResolution, lineInPixels, TileResolution, TileResolution, tile);
            }
            tileRowIndex++;
        }

        texture.alphaIsTransparency = true;
        texture.filterMode = FilterMode;
        texture.wrapMode = WrapMode;
        texture.Apply();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = texture;
    }

    private void BuildPlaneMesh() {

        int numberTiles = NumberTilesX * NumberTilesY;

        int vNumberTilesX = NumberTilesX + 1;
        int vNumberTilesY = NumberTilesY + 1;
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
                vertices[verticeIndex] = new Vector3(indexTileX * TileResolution, indexTileY * TileResolution);
                normals[verticeIndex] = Vector3.up;
                uv[verticeIndex] = new Vector2((float) indexTileX / NumberTilesX, (float) indexTileY / NumberTilesY);
            }
        }

        for (int indexTileY = 0; indexTileY < NumberTilesY; indexTileY++) {
            for (int indexTileX = 0; indexTileX < NumberTilesX; indexTileX++) {
                int squareIndex = indexTileY * NumberTilesX + indexTileX;
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

    private Color[] CopyWithOpacity(Color[] pixels, float opacity) {
        Color[] copyPixels = new Color[pixels.Length];
        for (int indexPixel = 0; indexPixel < pixels.Length; indexPixel++) {
            copyPixels[indexPixel] = pixels[indexPixel];
            if (copyPixels[indexPixel].a == ALPHA_OPAQUE) {
                copyPixels[indexPixel].a = opacity;
            }
        }
        return copyPixels;
    }
	
}
