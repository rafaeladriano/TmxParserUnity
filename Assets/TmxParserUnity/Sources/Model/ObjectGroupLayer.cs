using System.Collections.Generic;

namespace TmxParserUnity.Model {

    public class ObjectGroupLayer : Layer {

        private readonly List<ShapeObject> objects;

        public ObjectGroupLayer(List<ShapeObject> objects, string name, float offsetX, float offsetY, float opacity, bool visible) : base(name, offsetX, offsetY, opacity, visible) {
            this.objects = objects;
        }

        public List<ShapeObject> Objects
        {
            get
            {
                return objects;
            }
        }

    }

}
