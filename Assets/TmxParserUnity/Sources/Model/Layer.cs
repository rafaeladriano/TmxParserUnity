namespace TmxParserUnity.Model {

    public abstract class Layer {

        public const float DEFAULT_OFFSET_X = 0;
        public const float DEFAULT_OFFSET_Y = 0;
        public const float DEFAULT_OPACITY = 1;
        public const bool DEFAULT_VISIBLE = true;

        private readonly string name;
        private readonly float offsetX;
        private readonly float offsetY;
        private readonly bool visible;
        private readonly float opacity;

        public Layer(string name, float offsetX, float offsetY, float opacity, bool visible) {
            this.name = name;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.opacity = opacity;
            this.visible = visible;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public float OffsetX
        {
            get
            {
                return offsetX;
            }
        }

        public float OffsetY
        {
            get
            {
                return offsetY;
            }
        }

        public bool Visible
        {
            get
            {
                return visible;
            }
        }

        public float Opacity
        {
            get
            {
                return opacity;
            }
        }
    }
}
