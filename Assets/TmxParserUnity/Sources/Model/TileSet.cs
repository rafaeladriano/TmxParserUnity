namespace TmxParserUnity.Model {

    public class TileSet {

        private readonly string name;
        private readonly int firstTileId;
        private readonly int tileCount;
        private readonly int tileWidth;
        private readonly int tileHeight;
        private readonly int columns;
        private readonly Image image;

        public TileSet(string name, int firstTileId, int tileCount, int tileWidth, int tileHeight, int columns, Image image) {
            this.name = name;
            this.firstTileId = firstTileId;
            this.tileCount = tileCount;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.columns = columns;
            this.image = image;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int FirstTileId
        {
            get
            {
                return firstTileId;
            }
        }

        public int TileCount
        {
            get
            {
                return tileCount;
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

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
        }

    }
}