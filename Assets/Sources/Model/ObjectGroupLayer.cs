using System.Collections.Generic;

namespace tmxparser.model {

    public class ObjectGroupLayer : Layer {

        private readonly List<Object> objects;

        public ObjectGroupLayer(List<Object> objects, string name, float offsetX, float offsetY, float opacity, bool visible) : base(name, offsetX, offsetY, opacity, visible) {
            this.objects = objects;
        }

        public List<Object> Objects
        {
            get
            {
                return objects;
            }
        }

    }

}
