namespace tmxparser.model {

    public class TileSetLayer : Layer {

        public const int EMPTY_TILE = 0;

        private readonly int[,] tiles;

        public TileSetLayer(int width, int height, string name, float offsetX, float offsetY, float opacity, bool visible) : base(name, offsetX, offsetY, opacity, visible) {
            tiles = new int[width, height];
        }

        public int[,] Tiles
        {
            get
            {
                return tiles;
            }
        }

    }
}
