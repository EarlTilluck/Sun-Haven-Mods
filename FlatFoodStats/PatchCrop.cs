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
    class PatchCrop
    {

        // this method is used to calculate how many crops drop when harvested
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Crop), "GetDropAmount")]
        public static void Post_Crop_GetDropAmount(ref float __result, ref Crop __instance)
        {

            if (Options.EnableFertilizerBoost.Value == false) return; // skip if option not enabled

            int cropAddition = 0;

            switch (__instance.data.fertilizerType)
            {
                case FertilizerType.Earth1: cropAddition = Options.SimpleBoost.Value; break;
                case FertilizerType.Magic1: cropAddition = Options.SimpleBoost.Value; break;
                case FertilizerType.Earth2: cropAddition = Options.AdvancedBoost.Value; break;
                case FertilizerType.Magic2: cropAddition = Options.AdvancedBoost.Value; break;
                case FertilizerType.Slime: cropAddition = Options.SlimeBoost.Value; break;
            }

            if (cropAddition > 0) __result += cropAddition;

        }
    }
}
