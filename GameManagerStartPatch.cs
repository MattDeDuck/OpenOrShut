using HarmonyLib;
using PotionCraft.ManagersSystem.Game;
using PotionCraft.SceneLoader;

namespace OpenOrShut
{
    class GameManagerStartPatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(GameManager), "Start")]
        public static void Start_Postfix()
        {
            ObjectsLoader.AddLast("SaveLoadManager.SaveNewGameState", () => Utilities.TextureService.LoadTextures());
            ObjectsLoader.AddLast("SaveLoadManager.SaveNewGameState", () => Shop.Button.Create());
        }
    }
}
