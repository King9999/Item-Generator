/* This class is the parent of all other items. Every piece of gear is derived from an item. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item_Generator
{
    abstract class Item
    {
        protected byte itemLevel;    //item's level. Will be used to determine an item's stats during generation. Minimum level is 1.
        protected const byte LEVEL_MAX = 200;
        protected string itemName;      //Names are procedurally generated and can determine what effects/stats an item has.

        protected byte itemRank;       //Item quality from 0 to 3, with 3 being the best. Rank is a multiplier that increases stats on an item. 
                                        //Rank is indicated by a plus sign next to an item's name.

        protected const byte RANK_MAX = 3;

        /* This function is used to execute any special effects an item might have. I won't actually use this for this tool, but it's my interpretation
        * of how a game might check for any special effects. */
        protected void ExecuteSpecialAbility(short effectID)
        {

        }

    }
}
