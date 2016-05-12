using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TmxParserUnity {

    public class Utils {

        public static void Destroy(Transform parent) {
            int childCount = parent.childCount;
            for (int i = childCount - 1; i >= 0; i--) {
                DestroyChildren(parent.GetChild(i));
            }
        }

        private static void DestroyChildren(Transform parent) {
            int childCount = parent.childCount;
            for (int i = childCount - 1; i >= 0; i--) {
                DestroyChildren(parent.GetChild(i));
            }
            UnityEngine.Object.DestroyImmediate(parent.gameObject);
        }

    }
}
