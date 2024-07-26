using HumanAPI;
using UnityEngine;

namespace fall {

    public static class Fall {

        private static LevelVoid levelVoid;

        public static void PrepareLevel() {
            levelVoid = new();
            InitVoidTrigs();
            RemovePassTrigs();
            Plugin.Print("ready to fall!");
        }

        public static void InitVoidTrigs() {
            levelVoid = new();
            foreach (var vobj in levelVoid.VoidObjts) {
                foreach (var ftrig in levelVoid.FallTrigs) {
                    ftrig.enabled = false;
                }
                vobj.AddComponent<VoidTrigger>();
            }
        }

        public static void RemovePassTrigs() {
            foreach (var ptrig in levelVoid.PassTrigs) {
                ptrig.enabled = false;
            }
        }

        public static void RestoreLevel() {
            foreach (var vtrig in levelVoid.VoidTrigs) {
                foreach (var ftrig in levelVoid.FallTrigs) {
                    ftrig.enabled = true;
                }
                Object.Destroy(vtrig);
            }
            foreach (var ptrig in levelVoid.PassTrigs) {
                ptrig.enabled = true;
            }
            Plugin.Print("Stopped falling.");
        }
    }
}