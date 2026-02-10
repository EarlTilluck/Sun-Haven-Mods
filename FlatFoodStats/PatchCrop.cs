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
}
