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
    class PatchPlayer
    {
        // this method adds experience to the player
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), nameof(Player.AddEXP))]
        public static void Pre_Player_AddEXP(ref ProfessionType profession, ref float amount)
        {

            float boost = 1f;

            if (profession == ProfessionType.Exploration)
            {
                if (Options.EnableExplorationExpBoost.Value) boost = Options.ExplorationExp.Value;
            }
            else if (profession == ProfessionType.Farming)
            {
                if (Options.EnableFarmingExpBoost.Value) boost = Options.FarmingExp.Value;
            }
            else if (profession == ProfessionType.Mining)
            {
                if (Options.EnableMiningExpBoost.Value) boost = Options.MiningExp.Value;
            }
            else if (profession == ProfessionType.Combat)
            {
                if (Options.EnableCombatExpBoost.Value) boost = Options.CombatExp.Value;
            }
            else if (profession == ProfessionType.Fishing)
            {
                if (Options.EnableFishingExpBoost.Value) boost = Options.FishingExp.Value;
            }

            if (boost <= 0) boost = 1f;
            amount *= boost;

        }
    }
}
