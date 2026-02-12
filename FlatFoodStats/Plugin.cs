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



        // log
        public static ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("Flat Food Stats");

        void Awake()
        {
            // init BepInEx configuration value
            Options.BindConfigs(Config);

            // on init, run patch all to patch our code into the game 
            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(PatchFoodData));
            harmony.PatchAll(typeof(PatchCrop));
            harmony.PatchAll(typeof(PatchPlayer));
            harmony.PatchAll(typeof(PatchBobber));
            harmony.PatchAll(typeof(PatchTree));

            Log.LogInfo("Flat Food Stats Loaded.");
        }

    }

    /**
    
    Flat Food Stats: Enable this to receive flat stat increases when eating food. 
    You can safely enable/disable this mod during gameplay, but you will have to save and restart to see changes.
    The values will continue to increase at a flat rate regardless of how much you have eaten.
    This applies to food you have already eaten as well.

    The optional multiplier value is used to increase stats faster if you want to. 
    Every food eaten will give its base value for the stat increase multiplied by this value. 
    For example: 0.5 will half the value and 2 will double it. 
    
    
    Fertilizer: Gain addition crops on fertilized plants.
    This is an added bonus and not a multiplier, so setting the value to 1 will net you one additional crop to what you would normally get.
    Likewise, setting the value to 2 will net you 2 additional crops.
    This only applies to the specified fertilizers only, other fertilizers are not affected. 

    Experience Multiplier: Boost the value of experience gained for professions.
    The experienced gained is multiplied by this value, so 0.5 will half value and 2 will double it.
    Leave the values at 1 if you don't want to use this feature

    Fishing Minigame Slider: Adjust the speed of the minigame slider:
    The value should be between 0.1 and 1, 0.5 is the default. 
    If a value is entered below 0.1 or above 1, the speed will be set to default 0.5
    
    Tree Seed Drops: Fully grown trees are guaranteed to drop their seeds.
    This applies to Oak, Nelvari and Withergate trees. 
    You can change how many seeds drop. These drop in addition to the 30% chance for a normal drop.
    Fruit trees drop a seed default in game.


    */

}
