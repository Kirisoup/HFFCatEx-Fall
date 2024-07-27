using BepInEx;
using HarmonyLib;

namespace fall {

    [BepInPlugin("com.kirisoup.hff.fall", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Human.exe")]
    public class Plugin : BaseUnityPlugin {

        private readonly Harmony harmony = new("com.kirisoup.hff.fall");

        public static void Print() => print("");
        public static void Print(string msg) => print(msg);
        public static void Print(object msg) => print(msg.ToString());

        public void Awake() {
            harmony.PatchAll(typeof(Plugin));
            PluginConfig.Init(Config);
            if (PluginConfig.EnableFall) Fall.PrepareLevel();
        }

        [HarmonyPatch(typeof(Game), "OnSceneLoaded"), HarmonyPostfix]
        private static void OnSceneLoaded() {
            if (PluginConfig.EnableFall) Fall.PrepareLevel();
        }
        
        public static void OnToggle() {
            if (PluginConfig.EnableFall) {
                Fall.PrepareLevel();
            } else {
                Fall.RestoreLevel();
            }
        }

        public void OnDestroy() {
            if (PluginConfig.EnableFall) Fall.RestoreLevel();
            PluginConfig.DestroyCmds();
            harmony.UnpatchSelf();
        }
    }
}

