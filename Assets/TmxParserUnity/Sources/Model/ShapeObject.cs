namespace TmxParserUnity.Model {

    public class ShapeObject {

        public const int NO_TILE = -1;

        private readonly int tileId;
        private readonly string name;
        private readonly float x;
        private readonly float y;
        private readonly int width;
        private readonly int height;

        public ShapeObject(int tileId, string name, float x, float y, int width, int height) {
            this.tileId = tileId;
            this.name = name;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool HasTile() {
            return tileId != NO_TILE;
        }

        public int TileId
        {
            get
            {
                return tileId;
            }
        }

        public float X
        {
            get
            {
                return x;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
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
    }
}
