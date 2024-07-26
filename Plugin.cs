using BepInEx;
using HarmonyLib;

namespace fall {

    [BepInPlugin("com.kirisoup.hff.fall", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Human.exe")]
    public class Plugin : BaseUnityPlugin {

        readonly Harmony harmony = new("com.kirisoup.hff.fall");

        public static void Print(string msg) => print(msg);
        public static void Print(object msg) => print(msg.ToString());

        public void Awake() {
            harmony.PatchAll(typeof(Plugin));
            Fall.InitLevel();
        }

        [HarmonyPatch(typeof(Game), "OnSceneLoaded"), HarmonyPostfix]
        static void OnSceneLoaded() {
            Fall.InitLevel();
        }

        public void OnDestroy() {
            Fall.RestoreVoidTrigs();
            harmony.UnpatchSelf();
        }
    }
}

