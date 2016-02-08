using System.Collections.Generic;

namespace TmxParserUnity.Model {

    public class ObjectGroupLayer : Layer {

        private readonly List<TileObject> objects;

        public ObjectGroupLayer(List<TileObject> objects, string name, float offsetX, float offsetY, float opacity, bool visible) : base(name, offsetX, offsetY, opacity, visible) {
            this.objects = objects;
        }

        public List<TileObject> Objects
        {
            get
            {
                return objects;
            }
        }

    }

}
