namespace TmxParserUnity.Model {

    public class ImageLayer : Layer {

        private readonly Image image;

        public ImageLayer(Image image, string name, float offsetX, float offsetY, float opacity, bool visible) : base(name, offsetX, offsetY, opacity, visible) {
            this.image = image;
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
