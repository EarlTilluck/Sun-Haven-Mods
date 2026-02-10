using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish;

namespace FlatFoodStats
{
    [Harmony]
    class PatchFoodData
    {

        // this method is used to calculate the value for a stat based all how much food 
        // has been eaten so far (when the game loads, it uses this to find the current stat for player)
        [HarmonyPrefix]
        [HarmonyPatch(typeof(FoodData), nameof(FoodData.GetStat))]
        public static bool Pre_Wish_GetStat(ref StatType statType, ref int numEaten,
            ref StatIncrease statIncrease, ref float __result)
        {
            bool runOriginalMethod = !Plugin.ConfigEnableFlatFood.Value;

            if (statIncrease == StatIncrease.None)
            {
                __result = 0f;
                return runOriginalMethod;
            }

            float num = FoodData.GetStatIncreaseByNumEaten(statType, (int)statIncrease) * Plugin.ConfigFlatFoodModifier.Value;
            num = num * numEaten;

            if (GameSave.Farming.GetNode("Farming10b", true))
            {
                num = num * (1.05f + (float)GameSave.Farming.GetNodeAmount("Farming10b", 3, true) * 0.05f);
            }
            __result = num;
            return runOriginalMethod;
        }


        // this method calculates how much value to add to a stat when a food is eaten.
        // (this is used when the player eats a food.)
        [HarmonyPrefix]
        [HarmonyPatch(typeof(FoodData), nameof(FoodData.GetStatAddition))]
        public static bool Pre_Wish_GetStatAddition(ref StatType statType, ref int numEaten,
            ref StatIncrease statIncrease, ref float __result)
        {
            bool runOriginalMethod = !Plugin.ConfigEnableFlatFood.Value;

            if (statIncrease == StatIncrease.None)
            {
                __result = 0f;
                return runOriginalMethod;
            }

            float num = FoodData.GetStatIncreaseByNumEaten(statType, (int)statIncrease) * Plugin.ConfigFlatFoodModifier.Value;

            if (GameSave.Farming.GetNode("Farming10b", true))
            {
                num = num * (1.05f + (float)GameSave.Farming.GetNodeAmount("Farming10b", 3, true) * 0.05f);
            }

            __result = num;
            return runOriginalMethod;
        }

    }
}
