using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFoodStats
{
    class Options
    {

        // flat food stats
        public static ConfigEntry<bool> EnableFlatFood;
        public static ConfigEntry<bool> EnableFlatFoodBoost;
        public static ConfigEntry<float> FlatFoodBoost;

        // harvest multiplier
        public static ConfigEntry<bool> EnableFertilizerBoost;
        public static ConfigEntry<int> SimpleBoost;
        public static ConfigEntry<int> AdvancedBoost;
        public static ConfigEntry<int> SlimeBoost;

        // experience boost
        public static ConfigEntry<bool> EnableMiningExpBoost;
        public static ConfigEntry<float> MiningExp;
        public static ConfigEntry<bool> EnableCombatExpBoost;
        public static ConfigEntry<float> CombatExp;
        public static ConfigEntry<bool> EnableFishingExpBoost;
        public static ConfigEntry<float> FishingExp;
        public static ConfigEntry<bool> EnableFarmingExpBoost;
        public static ConfigEntry<float> FarmingExp;
        public static ConfigEntry<bool> EnableExplorationExpBoost;
        public static ConfigEntry<float> ExplorationExp;

        // fishing mini game
        public static ConfigEntry<bool> EnableFishingGame;
        public static ConfigEntry<float> FishingSliderSpeed;

        // tree seeds
        public static ConfigEntry<bool> EnableTreeSeedDrop;
        public static ConfigEntry<int> TreeSeedDropValue;



        public static void BindConfigs(ConfigFile Config)
        {

            /* Flat Food */

            EnableFlatFood = Config.Bind(
                "01. Flat Food Stats",              // Config section
                "01. Enable",                           // Config key
                false,                               // Default value
                "Stats will increase at a flat rate on food consumption forever"       // Description
            );

            EnableFlatFoodBoost = Config.Bind(
                "01. Flat Food Stats",
                "02. Enable Stat Increase Multiplier",
                false,
                "Further boost the stat increases gained from eating food"
            );

            FlatFoodBoost = Config.Bind(
                "01. Flat Food Stats",
                "03. Multiplier Value",
                1f,
                "Use 0.5 for half, 2 for double etc."
            );


            /* Harvest Boost */


            EnableFertilizerBoost = Config.Bind(
                "02. Fertilizer",
                "01. Enable",
                false,
                "Enable to gain additional crops on harvest with fertilizer."
            );

            SimpleBoost = Config.Bind(
                "02. Fertilizer",
                "02. Earth or Magic Fertilizer Harvest Addition.",
                1,
                "Gain this number of additional crops."
            );

            AdvancedBoost = Config.Bind(
                "02. Fertilizer",
                "03. Adv. Earth or Magic Fertilizer Harvest Addition.",
                2,
                "Gain this number of additional crops."
            );

            SlimeBoost = Config.Bind(
                "02. Fertilizer",
                "04. Slime Fertilizer Harvest Addition.",
                1,
                "Gain this number of additional crops."
            );


            /* Experience Boost */


            EnableExplorationExpBoost = Config.Bind(
                "03. Experience Mulitplier",
                "01. Enable Exploration Exp Boost",
                false,
                "Enable to boost experience gained for exploration"
            );

            ExplorationExp = Config.Bind(
                "03. Experience Mulitplier",
                "02. Exploration Exp Boost Value",
                2f,
                "Experience is mulitiplied by this value."
            );


            EnableFarmingExpBoost = Config.Bind(
                "03. Experience Mulitplier",
                "03. Enable Farming Exp Boost",
                false,
                "Enable to boost experience gained for farming"
            );

            FarmingExp = Config.Bind(
                "03. Experience Mulitplier",
                "04. Farming Exp Boost Value",
                2f,
                "Experience is mulitiplied by this value."
            );

            EnableMiningExpBoost = Config.Bind(
                "03. Experience Mulitplier",
                "05. Enable Mining Exp Boost",
                false,
                "Enable to boost experience gained for mining"
            );

            MiningExp = Config.Bind(
                "03. Experience Mulitplier",
                "06. Mining Exp Boost Value",
                2f,
                "Experience is mulitiplied by this value."
            );


            EnableCombatExpBoost = Config.Bind(
                "03. Experience Mulitplier",
                "07. Enable Combat Exp Boost",
                false,
                "Enable to boost experience gained for combat"
            );

            CombatExp = Config.Bind(
                "03. Experience Mulitplier",
                "08. Combat Exp Boost Value",
                2f,
                "Experience is mulitiplied by this value."
            );


            EnableFishingExpBoost = Config.Bind(
                "03. Experience Mulitplier",
                "09. Enable Fishing Exp Boost",
                false,
                "Enable to boost experience gained for fishing"
            );

            FishingExp = Config.Bind(
                "03. Experience Mulitplier",
                "10. Fishing Exp Boost Value",
                2f,
                "Experience is mulitiplied by this value."
            );


            /* Fishing Mini Game Speed */


            EnableFishingGame = Config.Bind(
               "04. Fishing Mini Game",
               "01. Enable",
               false,
               "Enable fixed speed for fishing minigame"
            );

            FishingSliderSpeed = Config.Bind(
               "04. Fishing Mini Game",
               "2. Speed (value should be between 0.1 - 1)",
               0.5f,
               "How fast should the bar move. 0.5 is default difficulty"
            );


            /* Tree seeds drop */


            EnableTreeSeedDrop = Config.Bind(
               "05. Additional Oak Seeds",
               "01. Enable",
               false,
               "Enable guaranteed additional oak seeds to drop."
            );

            TreeSeedDropValue = Config.Bind(
               "05. Additional Oak Seeds",
               "2. Number of seeds to drop",
               1,
               "How many seeds should a tree drop"
            );

        }

    }
}
