using UnityEngine;
using TmxParserUnity.Model;

public class ObjectGroupRenderer : MonoBehaviour {

    public SpriteRenderer SpritePrefab;
    [HideInInspector]
    public ObjectGroupLayer ObjectGroupLayer;
    [HideInInspector]
    public Color[][] Tiles;
    [HideInInspector]
    public FilterMode FilterMode;
    [HideInInspector]
    public TextureWrapMode WrapMode;
    [HideInInspector]
    public int MapHeightInPixels;

	void Start () {

        for (int i = 0; i < ObjectGroupLayer.Objects.Count; i++) {

            TileObject spriteObject = ObjectGroupLayer.Objects[i];

            Texture2D spriteTexture = new Texture2D(spriteObject.Width, spriteObject.Height);
            spriteTexture.SetPixels(0, 0, spriteObject.Width, spriteObject.Height, Tiles[spriteObject.TileId - 1]);
            spriteTexture.alphaIsTransparency = true;
            spriteTexture.filterMode = FilterMode;
            spriteTexture.wrapMode = WrapMode;
            spriteTexture.Apply();

            Sprite sprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteObject.Width, spriteObject.Height), new Vector2(0, 0));

            SpriteRenderer spriteRenderer = Instantiate(SpritePrefab);
            spriteRenderer.sprite = sprite;
            // Precisamos posicionar o sprite no topo e depois transladar negativamente com seu y, pois o Tiled coloca o y contrário
            spriteRenderer.transform.position = new Vector3(transform.position.x, transform.position.y + MapHeightInPixels, transform.position.z);
            spriteRenderer.transform.Translate(new Vector3(spriteObject.X, -spriteObject.Y, 0));
            spriteRenderer.transform.parent = transform;

            BoxCollider2D collider = spriteRenderer.gameObject.GetComponent<BoxCollider2D>();

            float colliderWidth = spriteObject.Width / (collider.bounds.max - collider.bounds.min).x;
            float colliderHeight = spriteObject.Height / (collider.bounds.max - collider.bounds.min).y;

            float colliderOffsetX = colliderWidth / 2;
            float colliderOffsetY = colliderHeight / 2;

            collider.offset = new Vector2(colliderOffsetX, colliderOffsetY);
            collider.size = new Vector2(colliderWidth, colliderHeight);
        }

	}
	
}
