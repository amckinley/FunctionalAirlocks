using Harmony;
using KSerialization;
using UnityEngine;

namespace FunctionalAirlocks
{
    public class Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                Debug.Log("Hello dingus!");
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                Debug.Log("I execute before Db.Initialize!");
            }

            public static void Postfix()
            {
                Debug.Log("I execute after Db.Initialize!");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                StringUtils.AddBuildingStrings(
                    FunctionalAirlockConfig.Id, 
                    FunctionalAirlockConfig.DisplayName, 
                    FunctionalAirlockConfig.Description, 
                    FunctionalAirlockConfig.Effect);

                BuildingUtils.AddBuildingToPlanScreen(
                    GameStrings.PlanMenuCategory.Base, 
                    FunctionalAirlockConfig.Id);
                /*
                BuildingUtils.AddBuildingToTechnology(
                    GameStrings.Technology.Gases.PressureManagement, 
                    FunctionalAirlockConfig.Id);
                */
            }
        }
    }
}
