using System.Linq;
using HumanAPI;
using UnityEngine;

namespace fall {

    public static class Fall {

        private static LevelVoid levelVoids;

        public static void InitLevel() {
            levelVoids = new();
            InitVoidTrigs();
            RemoveOriginalPassTrigs();
            Plugin.Print("ready to fall!");
        }

        public static void InitVoidTrigs() {
            levelVoids = new();
            foreach (var obj in levelVoids.VoidObjts) {
                foreach (var ftrig in obj.GetComponents<FallTrigger>()) {
                    Object.Destroy(ftrig);
                }
                obj.AddComponent<VoidTrigger>();
            }
        }

        public static void RemoveOriginalPassTrigs() {
            foreach (var trig in Object.FindObjectsOfType<LevelPassTrigger>()) {
                if (!levelVoids.VoidTrigs.Contains(trig)) Object.Destroy(trig);
            }
        }

        public static void RestoreVoidTrigs() {
            foreach (var trig in levelVoids.VoidTrigs) {
                trig.gameObject.AddComponent<FallTrigger>();
                Object.Destroy(trig);
            }
        }    
    }
}