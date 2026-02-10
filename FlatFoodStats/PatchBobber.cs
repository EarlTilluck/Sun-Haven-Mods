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
    class PatchBobber
    {

        // this method is called to start the minigame, we want to modify the fishingdata,
        // so the minigame always has a slower speed.
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Bobber), nameof(Bobber.StartMiniGame), new Type[] { typeof(FishData) })]
        public static void Pre_Bobber_StartMiniGame(ref FishData fishData)
        {
            if (Plugin.ConfigEnableFishingMod.Value == false) { return; }

            if (fishData == null) { return; }
            if (fishData.fishingMinigame == null) { return; }
            if (fishData.fishingMinigame.Count == 0) { return; }

            foreach (FishingMiniGame fmg in fishData.fishingMinigame)
            {
                Plugin.Log.LogWarning("doing a speed mod");
                float speed = Plugin.ConfigFishSpeed.Value;
                if (speed <= 0) speed = 0.5f;
                if (speed > 1) speed = 0.5f;
                fmg.barMovementSpeed = speed;
            }
        }

    }
}
