using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace OpenOrShut
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, "OpenOrShut", "1.0.3.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log { get; set; }
        public const string PLUGIN_GUID = "com.mattdeduck.openorshut";

        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Log = this.Logger;

            Log.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Harmony.CreateAndPatchAll(typeof(Utilities));
            Harmony.CreateAndPatchAll(typeof(Shop));
            Harmony.CreateAndPatchAll(typeof(ShopButton));
            Harmony.CreateAndPatchAll(typeof(GameManagerStartPatch));
            Log.LogInfo($"Plugin {PLUGIN_GUID}: Patch Succeeded!");
        }    
    }
}
