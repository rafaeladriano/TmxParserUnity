using System.Collections.Generic;

namespace TmxParserUnity.Model {

    public class TileMap {

        private readonly int width;
        private readonly int height;
        private readonly int tileWidth;
        private readonly int tileHeight;
        private readonly List<TileSet> tileSets;
        private readonly List<Layer> layers;

        public TileMap(int width, int height, int tileWidth, int tileHeight, List<TileSet> tileSets, List<Layer> layers) {
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileSets = tileSets;
            this.layers = layers;
        }

        public int Width
        {
            get
            {
                return width;
            }

        }

        public int Height
        {
            get
            {
                return height;
            }

        }

        public int TileWidth
        {
            get
            {
                return tileWidth;
            }

        }

        public int TileHeight
        {
            get
            {
                return tileHeight;
            }

        }

        public List<Layer> Layers
        {
            get
            {
                return layers;
            }

        }

        public List<TileSet> TileSets
        {
            get
            {
                return tileSets;
            }
        }

    }
}
