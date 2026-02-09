using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wish;

namespace FlatFoodStats
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {

        // description of this mod
        private const string modGUID = "codesprint.sun_haven.flatfoodstats";
        private const string modName = "Flat Food Stats And More";
        private const string modVersion = "1.0.0";


        // we need harmony to patch our code into the game
        private readonly Harmony harmony = new Harmony(modGUID);

        // BepInEx configurable values 
        // flat food stats
        public static ConfigEntry<bool> ConfigEnableFlatFood;
        public static ConfigEntry<float> ConfigFlatFoodModifier;

        // harvest multiplier
        public static ConfigEntry<int> ConfigHarvestMultiplier;
        public static ConfigEntry<int> ConfigHarvestMultiplier2;

        // experience boost
        public static ConfigEntry<float> ConfigMiningExp;
        public static ConfigEntry<float> ConfigCombatExp;
        public static ConfigEntry<float> ConfigFishingExp;
        public static ConfigEntry<float> ConfigFarmingExp;
        public static ConfigEntry<float> ConfigExplorationExp;

        // log
        public static ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("Flat Food Stats");

        void Awake()
        {
            // init BepInEx configuration value
            ConfigEnableFlatFood = Config.Bind(
                "01. Flat Food Stats",              // Config section
                "Enable Flat Stat Increase.",         // Config key
                true,                     // Default value
                "Food will increase stats at a flat rate. Forever"       // Description
            );

            ConfigFlatFoodModifier = Config.Bind(
                "01. Flat Food Stats",              // Config section
                "Food Increase Multiplier.",         // Config key
                1f,                     // Default value
                "Use 0.5 for half, 2 for double etc."       // Description
            );

            ConfigHarvestMultiplier = Config.Bind(
                "02. Fertilizer",
                "1. Earth or Magic Fertilizer Harvest Addition.",
                1,
                "Gain this number of additional crops. Set to zero to disable"
            );

            ConfigHarvestMultiplier2 = Config.Bind(
                "02. Fertilizer",
                "2. Adv. Earth or Magic Fertilizer Harvest Addition.",
                2,
                "Gain this number of additional crops. Set to zero to disable"
            );

            ConfigExplorationExp = Config.Bind(
                "03. Experience Mulitplier",
                "1. Exploration Exp",
                1f,
                "Experience is mulitiplied by this value"
            );

            ConfigFarmingExp = Config.Bind(
                "03. Experience Mulitplier",
                "2. Farming Exp",
                1f,
                "Experience is mulitiplied by this value"
            );

            ConfigMiningExp = Config.Bind(
                "03. Experience Mulitplier",
                "3. Mining Exp",
                1f,
                "Experience is mulitiplied by this value"
            );

            ConfigCombatExp = Config.Bind(
                "03. Experience Mulitplier",
                "4. Combat Exp",
                1f,
                "Experience is mulitiplied by this value"
            );

            ConfigFishingExp = Config.Bind(
                "03. Experience Mulitplier",
                "5. Fishing Exp",
                1f,
                "Experience is mulitiplied by this value"
            );

            // on init, run patch all to patch our code into the game 
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(PatchFoodData));
            harmony.PatchAll(typeof(PatchCrop));
            harmony.PatchAll(typeof(PatchPlayer));

            Log.LogInfo("Flat Food Stats Loaded.");
        }

    }

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

    [Harmony]
    class PatchCrop
    {

        // this method is used to calculate how many crops drop when harvested
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Crop), "GetDropAmount")]
        public static void Post_Crop_GetDropAmount(ref float __result, ref Crop __instance)
        {
            FertilizerType f = __instance.data.fertilizerType;
            if (f == FertilizerType.Earth1 || f == FertilizerType.Magic1)
            {
                __result = __result + Plugin.ConfigHarvestMultiplier.Value;
                return;
            }

            if (f == FertilizerType.Earth2 || f == FertilizerType.Magic2)
            {
                __result = __result + Plugin.ConfigHarvestMultiplier2.Value;
                return;
            }

        }
    }

    [Harmony]
    class PatchPlayer
    {
        // this method adds experience to the player
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), nameof(Player.AddEXP))]
        public static void Pre_Player_AddEXP(ref ProfessionType profession, ref float amount)
        {
            switch (profession)
            {
                case ProfessionType.Exploration: amount *= Plugin.ConfigExplorationExp.Value; break;
                case ProfessionType.Farming: amount *= Plugin.ConfigFarmingExp.Value; break;
                case ProfessionType.Mining: amount *= Plugin.ConfigMiningExp.Value; break;
                case ProfessionType.Combat: amount *= Plugin.ConfigCombatExp.Value; break;
                case ProfessionType.Fishing: amount *= Plugin.ConfigFishingExp.Value; break;
            }
        }
    }

}
