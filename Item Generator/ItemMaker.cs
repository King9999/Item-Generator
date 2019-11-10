/* This class is used to make items, completed with randomized stats. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Item_Generator
{
    class ItemMaker
    {
        //When user generates an item, start by checking what type of item they want.

        public ItemMaker()
        {

        }

        public void GenerateItem (Weapon weapon, byte level)
        {
            weapon.SetItemLevel(level);

            //generate name, starting with prefix if applicable, then the weapon type, and then the suffix.\
            int total = weapon.GetWeaponStatusEffectNames().ElementAt(1).Keys.Count;
            int elementCount = weapon.GetCount(weapon.GetWeaponElementNames());
            int ailmentCount = weapon.GetCount(weapon.GetWeaponStatusEffectNames());
            Random randNum = new Random();

            for (int i = 0; i < total; i++)
            {
                int val1 = randNum.Next(1, ailmentCount);   //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);

                string text = weapon.GetWeaponStatusEffectNames().ElementAt(val1).Keys.ElementAt(val2) + " ";   //prefix
                text += weapon.GetItemSubtype() + " ";

                val1 = randNum.Next(1, elementCount);       //we never reference index 0 because there's nothing there
                val2 = randNum.Next(0, total);
                text += weapon.GetWeaponElementNames().ElementAt(val1).Keys.ElementAt(val2);  //suffix
                Console.WriteLine(text);
            }
        }

        /*public void GenerateItem(Armor item, byte level)
        {

        }*/

    }
}
