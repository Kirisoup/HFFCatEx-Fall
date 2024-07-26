﻿using BepInEx;
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
            Fall.PrepareLevel();
        }

        [HarmonyPatch(typeof(Game), "OnSceneLoaded"), HarmonyPostfix]
        static void OnSceneLoaded() {
            Fall.PrepareLevel();
        }

        public void OnDestroy() {
            Fall.RestoreLevel();
            harmony.UnpatchSelf();
        }
    }
}

