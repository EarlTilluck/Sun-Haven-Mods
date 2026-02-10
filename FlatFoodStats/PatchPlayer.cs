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
