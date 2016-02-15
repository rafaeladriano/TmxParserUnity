using TmxParserUnity.Model;
using System.Xml;
using System.Collections.Generic;

namespace TmxParserUnity {

    public class TmxParser {

        private const string TAG_MAP = "map";
        private const string TAG_TILESET = "tileset";
        private const string TAG_TILESET_LAYER = "layer";
        private const string TAG_IMAGE_LAYER = "imagelayer";
        private const string TAG_OBJECT_GROUP_LAYER = "objectgroup";

        private static Dictionary<string, TileMap> cache = new Dictionary<string, TileMap>();

        public static TileMap Parser(string tmxFilePath) {
            TileMap tileMap;

            if (!cache.ContainsKey(tmxFilePath)) {
                XmlDocument tmxDocument = new XmlDocument();
                tmxDocument.Load(tmxFilePath);

                XmlNodeList maps = tmxDocument.GetElementsByTagName(TAG_MAP);

                if (maps.Count == 0) {
                    return null;
                }

                tileMap = ParserMap(maps.Item(0));
                cache[tmxFilePath] = tileMap;
            } else {
                tileMap = cache[tmxFilePath];
            }

            return tileMap;
        }

        public static void ClearCache() {
            cache.Clear();
        }

        public static void RemoveFromCache(string tmxFilePath) {
            cache.Remove(tmxFilePath);
        }

        private static TileMap ParserMap(XmlNode mapNode) {

            List<TileSet> tilesets = new List<TileSet>();
            List<Layer> layers = new List<Layer>();

            XmlNodeList tilesetNodes = mapNode.OwnerDocument.GetElementsByTagName(TAG_TILESET);
            for (int i = 0; i < tilesetNodes.Count; i++) {
                tilesets.Add(ParserTileSet(tilesetNodes.Item(i)));
            }

            ParserLayers(mapNode, layers);

            int width = -1;
            int height = -1;
            int tileWidth = -1;
            int tileHeight = -1;

            for (int i = 0; i < mapNode.Attributes.Count; i++) {
                XmlNode attribute = mapNode.Attributes.Item(i);
                string attributeName = mapNode.Attributes.Item(i).Name;

                switch (attributeName) {
                    case "width":
                        width = int.Parse(attribute.Value);
                        break;
                    case "height":
                        height = int.Parse(attribute.Value);
                        break;
                    case "tilewidth":
                        tileWidth = int.Parse(attribute.Value);
                        break;
                    case "tileheight":
                        tileHeight = int.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }
            }

            return new TileMap(width, height, tileWidth, tileHeight, tilesets, layers);
        }

        private static void ParserLayers(XmlNode node, List<Layer> layers) {

            switch (node.Name) {
                case TAG_TILESET_LAYER:
                    layers.Add(ParserTileSetLayer(node));
                    return;
                case TAG_IMAGE_LAYER:
                    layers.Add(ParserImageLayer(node));
                    return;
                case TAG_OBJECT_GROUP_LAYER:
                    layers.Add(ParserObjectGroup(node));
                    return;
            }

            for (int i = 0; i < node.ChildNodes.Count; i++) {
                ParserLayers(node.ChildNodes[i], layers);
            }

        }

        private static TileSet ParserTileSet(XmlNode tilesetNode) {

            string name = null;
            int firstTileId = -1;
            int tileCount = -1;
            int tileWidth = -1;
            int tileHeight = -1;
            int columns = -1;
            Image image = ParserImage(tilesetNode.ChildNodes.Item(0));

            for (int i = 0; i < tilesetNode.Attributes.Count; i++) {
                XmlNode attribute = tilesetNode.Attributes.Item(i);
                string attributeName = tilesetNode.Attributes.Item(i).Name;

                switch (attributeName) {
                    case "firstgid":
                        firstTileId = int.Parse(attribute.Value);
                        break;
                    case "name":
                        name = attribute.Value;
                        break;
                    case "tilewidth":
                        tileWidth = int.Parse(attribute.Value);
                        break;
                    case "tileheight":
                        tileHeight = int.Parse(attribute.Value);
                        break;
                    case "tilecount":
                        tileCount = int.Parse(attribute.Value);
                        break;
                    case "columns":
                        columns = int.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }
            }

            return new TileSet(name, firstTileId, tileCount, tileWidth, tileHeight, columns, image);
        }

        private static Image ParserImage(XmlNode imageNode) {

            string source = null;
            int width = -1;
            int height = -1;

            for (int i = 0; i < imageNode.Attributes.Count; i++) {
                XmlNode attribute = imageNode.Attributes.Item(i);
                string attributeName = imageNode.Attributes.Item(i).Name;

                switch (attributeName) {
                    case "width":
                        width = int.Parse(attribute.Value);
                        break;
                    case "height":
                        height = int.Parse(attribute.Value);
                        break;
                    case "source":
                        source = attribute.Value;
                        break;
                    default:
                        break;
                }
            }

            return new Image(source, width, height);
        }

        private static ShapeObject ParserObject(XmlNode objectNode) {

            int tileId = ShapeObject.NO_TILE;
            float x = -1;
            float y = -1;
            float width = -1;
            float height = -1;
            string name = null;

            for (int i = 0; i < objectNode.Attributes.Count; i++) {
                XmlNode attribute = objectNode.Attributes.Item(i);
                string attributeName = objectNode.Attributes.Item(i).Name;

                switch (attributeName) {
                    case "gid":
                        tileId = int.Parse(attribute.Value);
                        break;
                    case "name":
                        name = attribute.Value;
                        break;
                    case "x":
                        x = float.Parse(attribute.Value);
                        break;
                    case "y":
                        y = float.Parse(attribute.Value);
                        break;
                    case "width":
                        width = float.Parse(attribute.Value);
                        break;
                    case "height":
                        height = float.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }
            }

            return new ShapeObject(tileId, name, x, y, (int) width, (int) height);
        }

        private static TileSetLayer ParserTileSetLayer(XmlNode tilesetLayerNode) {

            int width = -1;
            int height = -1;

            for (int i = 0; i < tilesetLayerNode.Attributes.Count; i++) {
                XmlNode attribute = tilesetLayerNode.Attributes.Item(i);
                string attributeName = tilesetLayerNode.Attributes.Item(i).Name;

                switch (attributeName) {
                     case "width":
                        width = int.Parse(attribute.Value);
                        break;
                    case "height":
                        height = int.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }
            }

            string name;
            float offsetX;
            float offsetY;
            bool visible;
            float opacity;

            ParserLayerBaseAttributes(tilesetLayerNode, out name, out offsetX, out offsetY, out visible, out opacity);
            TileSetLayer tileSetLayer = new TileSetLayer(width, height, name, offsetX, offsetY, opacity, visible);

            XmlNode dataNode = tilesetLayerNode.ChildNodes.Item(0);
            string[] data = dataNode.InnerText.Split(',');
            int y = 0;
            int x = 0;
            for (int i = 0; i < data.Length; i++) {
                tileSetLayer.Tiles[x, y] = int.Parse(data[i]);
                if ((i + 1) % width == 0) {
                    x = 0;
                    y++;
                } else {
                    x++;
                }
            }

            return tileSetLayer;
        }

        private static ImageLayer ParserImageLayer(XmlNode imageLayerNode) {

            Image image = null;
            if (imageLayerNode.ChildNodes.Count > 0) {
                image = ParserImage(imageLayerNode.ChildNodes.Item(0));
            }

            string name;
            float offsetX;
            float offsetY;
            bool visible;
            float opacity;

            ParserLayerBaseAttributes(imageLayerNode, out name, out offsetX, out offsetY, out visible, out opacity);

            return new ImageLayer(image, name, offsetX, offsetY, opacity, visible);
        }

        private static ObjectGroupLayer ParserObjectGroup(XmlNode objectGroupLayerNode) {

            List<ShapeObject> objects = new List<ShapeObject>();

            for (int i = 0; i < objectGroupLayerNode.ChildNodes.Count; i++) {
                objects.Add(ParserObject(objectGroupLayerNode.ChildNodes.Item(i)));
            }

            string name;
            float offsetX;
            float offsetY;
            bool visible;
            float opacity;

            ParserLayerBaseAttributes(objectGroupLayerNode, out name, out offsetX, out offsetY, out visible, out opacity);

            return new ObjectGroupLayer(objects, name, offsetX, offsetY, opacity, visible);
        }

        private static void ParserLayerBaseAttributes(XmlNode layerNode, out string name, out float offsetX, out float offsetY, out bool visible, out float opacity) {
            offsetX = Layer.DEFAULT_OFFSET_X;
            offsetY = Layer.DEFAULT_OFFSET_Y;
            visible = Layer.DEFAULT_VISIBLE;
            opacity = Layer.DEFAULT_OPACITY;
            name = null;

            for (int i = 0; i < layerNode.Attributes.Count; i++) {
                XmlNode attribute = layerNode.Attributes.Item(i);
                string attributeName = layerNode.Attributes.Item(i).Name;

                switch (attributeName) {
                    case "name":
                        name = attribute.Value;
                        break;
                    case "offsetx":
                        offsetX = float.Parse(attribute.Value);
                        break;
                    case "offsety":
                        offsetY = float.Parse(attribute.Value);
                        break;
                    case "opacity":
                        opacity = float.Parse(attribute.Value);
                        break;
                    case "visible":
                        visible = int.Parse(attribute.Value) == 1;
                        break;
                    default:
                        break;
                }
            }

        }
    }

}
