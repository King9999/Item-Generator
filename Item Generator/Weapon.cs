using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item_Generator
{
    class Weapon : Item
    {
        protected short wpnAtk; //Attack Power. Determines physical damage. Minimum value is 0
        protected byte wpnAcc; //Accuracy. Hit chance. Minimum value is 1, max value is 100.
        protected short wpnMag; //Magic Power. Determines magical damage. Min value is 0. Generally applies to staff weapons, but other weapons can boost Mag.
        protected byte wpnActiveElement;        //if a weapon has a special effect, it will be determined here. Defaults to none, or 0.
        protected byte wpnActiveStatusEffect;

        /* The following lists of dictionaries are used to contain the prefixes and the strength values of each element/ailment */
        protected List<Dictionary<string, byte>> wpnElementNames = new List<Dictionary<string, byte>>();
        protected List<Dictionary<string, byte>> wpnStatusEffectNames = new List<Dictionary<string, byte>>();

        protected enum wpnStatusEffect:byte
        {
            Poison,
            Stun,           //A weapon can come with a status effect. Weapons can only have one status effect enabled.
            Freeze,             //The success rate of landing an effect is determined by the suffix in the item name.
            Death,
            Sleep
        }

        protected enum wpnElement:byte
        {
            Fire,
            Water,
            Earth,              //A weapon can have an element. A staff is most likely to have an element than any other weapon type.
            Wind,               //A weapon can have up to two elements, and the name of item will reflect that. The only invalid combinations
            Light,              //are: Fire + Water, Earth + Wind, and Light + Dark.       
            Dark
        }

        

        public Weapon()
        {
            itemLevel = 1;
            wpnAtk = 0;
            wpnAcc = 1;
            wpnMag = 0;
            wpnActiveElement = 0;
            wpnActiveStatusEffect = 0;
            itemName = "";
            itemRank = 0;

            /* Set up names. Prefixes are always status effects, while suffixes are always an element. The second value in the dictionary is the 
             * max percentage the weapon can have in a given tier. */
            for (int i = 0; i < Enum.GetNames(typeof(wpnStatusEffect)).Length; i++)
                wpnStatusEffectNames.Add(new Dictionary<string, byte>());

            for (int i = 0; i < Enum.GetNames(typeof(wpnElement)).Length; i++)
                wpnElementNames.Add(new Dictionary<string, byte>());

            /****Ailments. These are prefixes, which means weapon names can begin with these names. *****/

            //Poison tiers
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Bacterial", 10);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Contaminated", 20);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Tainted", 30);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Toxic", 40);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Infected", 50);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Viral", 60);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Venomous", 70);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Virulent", 80);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Noxious", 90);
            wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Add("Biohazardous", 100);

           
            //Console.WriteLine(wpnStatusEffectNames[(int)wpnStatusEffect.Poison].Keys);

            //Stun tiers
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Tingling", 10);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Numbing", 20);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Stiffening", 30);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Immobilizing", 40);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Disabling", 50);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Debilitating", 60);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Paralyzing", 70);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Disarming", 80);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Arresting", 90);
            wpnStatusEffectNames[(int)wpnStatusEffect.Stun].Add("Enfeebling", 100);

            //Freezing tiers
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Chilling", 10);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Frosty", 20);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Icy", 30);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Biting", 40);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Polar", 50);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Glacial", 60);    //a weapon with this effect should be more likely to have water element
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Wintry", 70);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Frigid", 80);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Shivering", 90);
            wpnStatusEffectNames[(int)wpnStatusEffect.Freeze].Add("Icicle", 100);

            //Death tiers
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Dim", 10);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Shadow", 20);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Sinister", 30);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Blackened", 40);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Death", 50);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Disastrous", 60);    //a weapon with this effect should be more likely to have dark element
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Calamitous", 70);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Obliterating", 80);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Nihil", 90);
            wpnStatusEffectNames[(int)wpnStatusEffect.Death].Add("Void", 100);

            //Sleep tiers
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Drowsy", 10);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Torpid", 20);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Slumberous", 30);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Soporific", 40);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Sluggish", 50);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Somnolent", 60);    
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Hypnotic", 70);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Lethargic", 80);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Dreamy", 90);
            wpnStatusEffectNames[(int)wpnStatusEffect.Sleep].Add("Nightmarish", 100);

            /*****Elements. These are suffixes, which means they are added at the end of weapon names****/

            //Fire tiers
            wpnElementNames[(int)wpnElement.Fire].Add("of Heat", 10);
            wpnElementNames[(int)wpnElement.Fire].Add("of Charring", 20);
            wpnElementNames[(int)wpnElement.Fire].Add("of Embers", 30);
            wpnElementNames[(int)wpnElement.Fire].Add("of Searing", 40);
            wpnElementNames[(int)wpnElement.Fire].Add("of the Bonfire", 50);
            wpnElementNames[(int)wpnElement.Fire].Add("of Incandescence", 60);
            wpnElementNames[(int)wpnElement.Fire].Add("of Brimstone", 70);
            wpnElementNames[(int)wpnElement.Fire].Add("of Flames", 80);
            wpnElementNames[(int)wpnElement.Fire].Add("of Combustion", 90);
            wpnElementNames[(int)wpnElement.Fire].Add("of the Inferno", 100);

            //Water tiers
            wpnElementNames[(int)wpnElement.Water].Add("of Droplets", 10);
            wpnElementNames[(int)wpnElement.Water].Add("of the Puddle", 20);
            wpnElementNames[(int)wpnElement.Water].Add("of the Pond", 30);
            wpnElementNames[(int)wpnElement.Water].Add("of the Lake", 40);
            wpnElementNames[(int)wpnElement.Water].Add("of the Ocean", 50);
            wpnElementNames[(int)wpnElement.Water].Add("of the Tsunami", 60);
            wpnElementNames[(int)wpnElement.Water].Add("of the Whirlpool", 70);
            wpnElementNames[(int)wpnElement.Water].Add("of the Flood", 80);
            wpnElementNames[(int)wpnElement.Water].Add("of the Deluge", 90);
            wpnElementNames[(int)wpnElement.Water].Add("of the Torrent", 100);

            //Earth tiers
            wpnElementNames[(int)wpnElement.Earth].Add("of the Pebble", 10);
            wpnElementNames[(int)wpnElement.Earth].Add("of Gravel", 20);
            wpnElementNames[(int)wpnElement.Earth].Add("of Rubble", 30);
            wpnElementNames[(int)wpnElement.Earth].Add("of Sands", 40);
            wpnElementNames[(int)wpnElement.Earth].Add("of Bedrock", 50);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Boulder", 60);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Crag", 70);
            wpnElementNames[(int)wpnElement.Earth].Add("of Seimicity", 80);
            wpnElementNames[(int)wpnElement.Earth].Add("of Tremors", 90);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Quake", 100);

            //Wind tiers
            wpnElementNames[(int)wpnElement.Earth].Add("of Air", 10);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Breeze", 20);
            wpnElementNames[(int)wpnElement.Earth].Add("of Gust", 30);
            wpnElementNames[(int)wpnElement.Earth].Add("of Gales", 40);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Mistral", 50);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Twister", 60);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Squall", 70);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Maelstrom", 80);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Tempest", 90);
            wpnElementNames[(int)wpnElement.Earth].Add("of the Cyclone", 100);

            //Light tiers
            wpnElementNames[(int)wpnElement.Light].Add("of Dawn", 10);
            wpnElementNames[(int)wpnElement.Light].Add("of Luminance", 20);
            wpnElementNames[(int)wpnElement.Light].Add("of Daybreak", 30);
            wpnElementNames[(int)wpnElement.Light].Add("of Brilliance", 40);
            wpnElementNames[(int)wpnElement.Light].Add("of Splendor", 50);
            wpnElementNames[(int)wpnElement.Light].Add("of Resplendence", 60);
            wpnElementNames[(int)wpnElement.Light].Add("of Starlight", 70);
            wpnElementNames[(int)wpnElement.Light].Add("of Radiance", 80);
            wpnElementNames[(int)wpnElement.Light].Add("of Scintillation", 90);
            wpnElementNames[(int)wpnElement.Light].Add("of the Sun", 100);

            //Dark tiers
            wpnElementNames[(int)wpnElement.Dark].Add("of Dusk", 10);
            wpnElementNames[(int)wpnElement.Dark].Add("of Gloom", 20);
            wpnElementNames[(int)wpnElement.Dark].Add("of Shade", 30);
            wpnElementNames[(int)wpnElement.Dark].Add("of Eventide", 40);
            wpnElementNames[(int)wpnElement.Dark].Add("of Midnight", 50);
            wpnElementNames[(int)wpnElement.Dark].Add("of the Umbra", 60);
            wpnElementNames[(int)wpnElement.Dark].Add("of Spirits", 70);
            wpnElementNames[(int)wpnElement.Dark].Add("of Sin", 80);
            wpnElementNames[(int)wpnElement.Dark].Add("of Twilight", 90);
            wpnElementNames[(int)wpnElement.Dark].Add("of the End", 100);


        }



    }
}
