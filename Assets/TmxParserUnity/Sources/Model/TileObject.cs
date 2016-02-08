namespace TmxParserUnity.Model {

    public class TileObject {

        private readonly int tileId;
        private readonly float x;
        private readonly float y;
        private readonly int width;
        private readonly int height;

        public TileObject(int tileId, float x, float y, int width, int height) {
            this.tileId = tileId;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
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
