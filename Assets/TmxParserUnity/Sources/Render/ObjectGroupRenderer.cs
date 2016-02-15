using UnityEngine;
using TmxParserUnity.Model;

public class ObjectGroupRenderer : MonoBehaviour {

    public SpriteRenderer SpritePrefab;
    public BoxCollider2D ShapePrefab;
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

            ShapeObject shapeObject = ObjectGroupLayer.Objects[i];

            BoxCollider2D collider;

            if (shapeObject.HasTile()) {
                Texture2D spriteTexture = new Texture2D(shapeObject.Width, shapeObject.Height);
                spriteTexture.SetPixels(0, 0, shapeObject.Width, shapeObject.Height, Tiles[shapeObject.TileId - 1]);
                spriteTexture.alphaIsTransparency = true;
                spriteTexture.filterMode = FilterMode;
                spriteTexture.wrapMode = WrapMode;
                spriteTexture.Apply();

                Sprite sprite = Sprite.Create(spriteTexture, new Rect(0, 0, shapeObject.Width, shapeObject.Height), new Vector2(0, 0));

                SpriteRenderer spriteRenderer = Instantiate(SpritePrefab);
                spriteRenderer.sprite = sprite;


                collider = spriteRenderer.gameObject.GetComponent<BoxCollider2D>();
                float colliderWidth = shapeObject.Width / (collider.bounds.max - collider.bounds.min).x;
                float colliderHeight = shapeObject.Height / (collider.bounds.max - collider.bounds.min).y;

                float colliderOffsetX = colliderWidth / 2;
                float colliderOffsetY = colliderHeight / 2;

                collider.size = new Vector2(colliderWidth, colliderHeight);
                collider.offset = new Vector2(colliderOffsetX, colliderOffsetY);
            } else {
                collider = Instantiate(ShapePrefab);
                collider.size = new Vector2(shapeObject.Width, shapeObject.Height);
                float colliderOffsetX = collider.size.x / 2;
                float colliderOffsetY = collider.size.y / 2;
                collider.offset = new Vector2(colliderOffsetX, -colliderOffsetY);
            }



            // Precisamos posicionar o sprite no topo e depois transladar negativamente com seu y, pois o Tiled coloca o y contrário
            collider.transform.position = new Vector3(transform.position.x, transform.position.y + MapHeightInPixels, transform.position.z);
            collider.transform.Translate(new Vector3(shapeObject.X, -shapeObject.Y, 0));
            collider.transform.parent = transform;
            if (shapeObject.Name != null) {
                collider.transform.name = shapeObject.Name;
            }
        }

	}
	
}
