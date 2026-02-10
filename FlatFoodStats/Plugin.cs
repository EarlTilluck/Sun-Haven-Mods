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

        // fishing mini game
        public static ConfigEntry<bool> ConfigEnableFishingMod;
        public static ConfigEntry<float> ConfigFishSpeed;


        // log
        public static ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("Flat Food Stats");

        void Awake()
        {
            // init BepInEx configuration value
            ConfigEnableFlatFood = Config.Bind(
                "01. Flat Food Stats",              // Config section
                "Enable",         // Config key
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
                0,
                "Gain this number of additional crops. Set to zero to disable"
            );

            ConfigHarvestMultiplier2 = Config.Bind(
                "02. Fertilizer",
                "2. Adv. Earth or Magic Fertilizer Harvest Addition.",
                0,
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

            ConfigEnableFishingMod = Config.Bind(
               "04. Fishing Mini Game",
               "1. Enable",
               true,
               "Enable fixed speed for fishing minigame"
           );

            ConfigFishSpeed = Config.Bind(
               "04. Fishing Mini Game",
               "2. Speed (value should be between 0.1 - 1)",
               0.5f,
               "How fast should the bar move. 0.5 is default difficulty"
           );

            // on init, run patch all to patch our code into the game 
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(PatchFoodData));
            harmony.PatchAll(typeof(PatchCrop));
            harmony.PatchAll(typeof(PatchPlayer));
            harmony.PatchAll(typeof(PatchBobber));

            Log.LogInfo("Flat Food Stats Loaded.");
        }

    }

    /**
    
    Flat Food Stats: Enable this to receive flat stat increases when eating food. 
    The Multiplier value is used to increase stats faster if you want to. Every food eaten will give its base value for the stat increase multiplied by this value. 
    For example: 0.5 will half the value and 2 will double it. Leave the value at 1 to use the base value as is.
    The values will continue to increase at a flat rate regardless of how much you have eaten.
    This applies to food you have already eaten as well.
    
    Fertilizer: Gain addition crops on fertilized plants.
    This is an added bonus and not a multiplier, so setting the value to 1 will net you one additional crop to what you would normally get.
    Likewise, setting the value to 2 will net you 2 additional crops.
    Leave the values at zero if you don't want use this feature.
    This applies to earth and magic fertilizers only, other fertilizers are not affected. 

    Experience Multiplier: Boost the value of experience gained for professions.
    The experienced gained is multiplied by this value, so 0.5 will half value and 2 will double it.
    Leave the values at 1 if you don't want to use this feature

    Fishing: Adjust the speed of the minigame slider:
    The value should be between 0.1 and 1, 0.5 is the default. 
    If a value is entered below 0.1 or above 1, the speed will be set to default 0.5
    

    */

}
