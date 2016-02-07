namespace tmxparser.model {

    public class Image {

        private readonly string path;
        private readonly int width;
        private readonly int height;

        public Image(string path, int width, int height) {
            this.path = path;
            this.width = width;
            this.height = height;
        }

        public string Path
        {
            get
            {
                return path;
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
