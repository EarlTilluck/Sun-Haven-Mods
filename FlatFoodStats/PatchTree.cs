using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Wish;

namespace FlatFoodStats
{
    [Harmony]
    class PatchTree
    {

        // spawn drops is called whenever a tree is chopped down, or stump is chopped down.
        // we add an extra oak seed drop is tree is fully grown and not a stump.
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Tree), "SpawnDrops", new Type[] { typeof(Vector3), typeof(bool) })]
        public static void Post_Tree_SpawnDrops(Vector3 fallPosition, bool stumpDrop,
            Tree __instance, TreeType ___treeType, ItemData ___seeds)
        {
            if (Options.EnableTreeSeedDrop.Value == false) return; // skip if not enabled

            bool isGrown = Traverse.Create(__instance).Property("FullyGrown").GetValue<bool>();

            if (!stumpDrop && isGrown)
            {
                // if one of the lumber trees
                if (___treeType == TreeType.Oak ||
                    ___treeType == TreeType.NelvariTree ||
                    ___treeType == TreeType.Withergate)
                {
                    // get num of seeds to drop from options
                    int value = Options.TreeSeedDropValue.Value;
                    if (value < 0) value = 1;

                    // use game code to spawn additional seeds.
                    int x = 0;
                    while (x < value)
                    {
                        float num9 = 2f;
                        float num10 = 0.35f;
                        Vector2 vector3 = new Vector2(UnityEngine.Random.Range(-num9, num9), UnityEngine.Random.Range(-num10, num10));
                        Pickup.Spawn(fallPosition.x + vector3.x, (fallPosition.y + vector3.y - 0.5f) * 1.4142135f, 0f,
                            ___seeds.id, 1, false, 0.4f, Pickup.BounceAnimation.Normal, 0.3f, 250f, false);
                        x++;
                    }

                }
            }

        }

    }
}
