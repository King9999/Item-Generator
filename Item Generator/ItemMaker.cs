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
        ItemDisplay itemDisplay;    //used to display the generated item.
        static Random randNum;

        public ItemMaker()
        {
            itemDisplay = new ItemDisplay();    //used to display the generated item.
            randNum = new Random();
        }

        public void GenerateItem(Weapon weapon, byte level)
        {
            weapon.SetItemLevel(level);

            //generate name, starting with prefix if applicable, then the weapon type, and then the suffix.
            int total = weapon.GetWeaponAilmentNames().ElementAt(1).Keys.Count;
            total = total * (level / weapon.GetMaxLevel());     //This will control the strength of generated items. Higher level = better gear
            int elementCount = weapon.GetCount(weapon.GetWeaponElementNames());
            int ailmentCount = weapon.GetCount(weapon.GetWeaponAilmentNames());
            
            string text = "";                    //this will be the ranomized name

            //set up attack power
            int atk = randNum.Next(level * 5, level * 5 * weapon.GetAtkMod() + 1);
            weapon.SetAttackPower((short)atk);

            //magic power
            if (weapon.GetItemSubtype().ToLower() == "staff")
            {
                int mag = randNum.Next(level * 5, level * 5 * weapon.GetMagMod() + 1);
                weapon.SetMagicPower((short)mag);
            }

            //Accuracy will have some variance, either -10 or +10 of the base
            double accuracyRate = randNum.NextDouble() * ((weapon.GetAccuracy() + 0.1) - (weapon.GetAccuracy() - 0.1)) + (weapon.GetAccuracy() - 0.1);
            accuracyRate = Math.Round(accuracyRate, 2);
            weapon.SetAccuracy((float)accuracyRate);
            

            //Each item has a success rate of having ailments or elements applied to them. The highest success rate is 50%.
            double randomRate = randNum.NextDouble();
            double ailmentRate = ((randomRate * level) / level) * 0.5;
            randomRate = randNum.NextDouble();
            double elementRate = ((randomRate * level) / level) * 0.5;

            Console.WriteLine("Chance to add Ailment: " + ailmentRate * 100 + "%");
            Console.WriteLine("Chance to add Element: " + elementRate * 100 + "%");

            //roll for ailments first
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Ailments: " + randomRate * 100 + "%");

            if (randomRate <= ailmentRate)
            {
                //weapon has an ailment so we apply it
                int val1 = randNum.Next(1, ailmentCount);   //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text = weapon.GetWeaponAilmentNames().ElementAt(val1).Keys.ElementAt(val2) + " ";   //prefix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte ailmentValue = weapon.GetWeaponAilmentNames().ElementAt(val1).Values.ElementAt(val2);
                int ailmentAtk = randNum.Next(ailmentValue - 10, ailmentValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                weapon.SetAilment((Weapon.WpnAilment)val1, (byte)ailmentAtk);

                //Console.WriteLine("New Ailment is " + Enum.GetName(typeof(Weapon.WpnAilment), weapon.GetAilment()));
            }

            text += weapon.GetItemSubtype();

            //roll for elements
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Element: " + randomRate * 100 + "%");

            if (randomRate <= elementRate * weapon.GetMagMod()) //staff has a better chance of having an element
            {
                int val1 = randNum.Next(1, elementCount);       //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text += " " + weapon.GetWeaponElementNames().ElementAt(val1).Keys.ElementAt(val2);  //suffix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte elementValue = weapon.GetWeaponElementNames().ElementAt(val1).Values.ElementAt(val2);
                int elementAtk = randNum.Next(elementValue - 10, elementValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                weapon.SetElement((Weapon.WpnElement)val1, (byte)elementAtk);
            }

            //Check for any ranks. Ranks boost a weapon's attack power (or mag power if weapon is a staff).
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Rank: " + randomRate * 100 + "%");
            double rankRate3 = 0.02;
            double rankRate2 = 0.1;
            double rankRate1 = 0.25;

            if (randomRate <= rankRate3)
            {
                text += "+++";
                weapon.SetItemType("Enhanced Weapon");
                weapon.SetRank(3);
                if (weapon.GetItemSubtype().ToLower() == "staff")
                {
                    double boost = Math.Round(1.4f * weapon.GetMagicPower());
                    weapon.SetMagicPower((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.4f * weapon.GetAttackPower());
                    weapon.SetAttackPower((short)boost);
                }
            }
            else if (randomRate <= rankRate2)
            {
                text += "++";
                weapon.SetItemType("Enhanced Weapon");
                weapon.SetRank(2);
                if (weapon.GetItemSubtype().ToLower() == "staff")
                {
                    double boost = Math.Round(1.25f * weapon.GetMagicPower());
                    weapon.SetMagicPower((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.25f * weapon.GetAttackPower());
                    weapon.SetAttackPower((short)boost);
                }
            }
            else if (randomRate <= rankRate1)
            {
                text += "+";
                weapon.SetItemType("Enhanced Weapon");
                weapon.SetRank(1);
                //Console.WriteLine("Atk Value before boost: " + weapon.GetAttackPower());
                if (weapon.GetItemSubtype().ToLower() == "staff")
                {
                    double boost = Math.Round(1.15f * weapon.GetMagicPower());
                    weapon.SetMagicPower((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.15f * weapon.GetAttackPower());
                    weapon.SetAttackPower((short)boost);
                }
            }

             //Name is complete
             weapon.SetItemName(text);

            //set all values for the item display form
            itemDisplay.ItemName = text;
            itemDisplay.ItemLevel = "Level: " + level;
            itemDisplay.ItemType = weapon.GetItemType();
            itemDisplay.ItemSubType = weapon.GetItemSubtype();
            itemDisplay.ItemAilment = Enum.GetName(typeof(Weapon.WpnAilment), weapon.GetAilment());
            itemDisplay.ItemElement = Enum.GetName(typeof(Weapon.WpnElement), weapon.GetElement());
            itemDisplay.Accuracy = (weapon.GetAccuracy() * 100).ToString();
            itemDisplay.AttackPower = weapon.GetAttackPower().ToString();
            itemDisplay.MagicPower = weapon.GetMagicPower().ToString();

            //need to check which elements/ailment values are set
            if (weapon.GetElement() != Weapon.WpnElement.None)
            { 
                switch (weapon.GetElement())
                {
                    case Weapon.WpnElement.Fire:
                        itemDisplay.FireAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    case Weapon.WpnElement.Water:
                        itemDisplay.WaterAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    case Weapon.WpnElement.Wind:
                        itemDisplay.WindAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    case Weapon.WpnElement.Earth:
                        itemDisplay.EarthAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    case Weapon.WpnElement.Light:
                        itemDisplay.LightAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    case Weapon.WpnElement.Dark:
                        itemDisplay.DarkAtkValue = weapon.GetElementAtkValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            if (weapon.GetAilment() != Weapon.WpnAilment.None)
            {
                switch (weapon.GetAilment())
                {
                    case Weapon.WpnAilment.Poison:
                        itemDisplay.PoisonAtkValue = weapon.GetAilmentAtkValue().ToString();
                        break;

                    case Weapon.WpnAilment.Stun:
                        itemDisplay.StunAtkValue = weapon.GetAilmentAtkValue().ToString();
                        break;

                    case Weapon.WpnAilment.Freeze:
                        itemDisplay.FreezeAtkValue = weapon.GetAilmentAtkValue().ToString();
                        break;

                    case Weapon.WpnAilment.Death:
                        itemDisplay.DeathAtkValue = weapon.GetAilmentAtkValue().ToString();
                        break;

                    case Weapon.WpnAilment.Sleep:
                        itemDisplay.SleepAtkValue = weapon.GetAilmentAtkValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            //all done! Show the generated item.
            itemDisplay.Show();
            
        }

        public void GenerateItem(Armor armor, byte level)
        {
            armor.SetItemLevel(level);

            //generate name, starting with prefix if applicable, then the armor type, and then the suffix.
            int total = armor.GetArmorAilmentNames().ElementAt(1).Keys.Count;
            total = total * (level / armor.GetMaxLevel());     //This will control the strength of generated items. Higher level = better gear
            int elementCount = armor.GetCount(armor.GetArmorElementNames());
            int ailmentCount = armor.GetCount(armor.GetArmorAilmentNames());

            string text = "";                    //this will be the ranomized name

            //set up defense power
            int def = randNum.Next(level * 5, level * 5 * armor.GetDefMod() + 1);   //note that the defense for a robe has no variance
            armor.SetDefensePower((short)def);

            //magic resist
            if (armor.GetItemSubtype().ToLower() == "robe")
            {
                int res = randNum.Next(level * 5, level * 5 * armor.GetResMod() + 1);
                armor.SetMagicResist((short)res);
            }

            //Evasion will have some variance, either -10 or +10 of the base
            double evadeRate = randNum.NextDouble() * ((armor.GetEvade() + 0.1) - (armor.GetEvade() - 0.1)) + (armor.GetEvade() - 0.1);
            evadeRate = Math.Round(evadeRate, 2);
            armor.SetEvade((float)evadeRate);


            //Each item has a success rate of having ailment or element resistance applied to them. The highest success rate is 50%.
            double randomRate = randNum.NextDouble();
            double ailmentRate = ((randomRate * level) / level) * 0.5;
            randomRate = randNum.NextDouble();
            double elementRate = ((randomRate * level) / level) * 0.5;

            Console.WriteLine("Chance to add Ailment Res: " + ailmentRate * 100 + "%");
            Console.WriteLine("Chance to add Element Res: " + elementRate * 100 + "%");

            //roll for ailments first
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Ailments: " + randomRate * 100 + "%");

            if (randomRate <= ailmentRate)
            {
                //armor has an ailment so we apply it
                int val1 = randNum.Next(1, ailmentCount);   //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text = armor.GetArmorAilmentNames().ElementAt(val1).Keys.ElementAt(val2) + " ";   //prefix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte ailmentValue = armor.GetArmorAilmentNames().ElementAt(val1).Values.ElementAt(val2);
                int ailmentDef = randNum.Next(ailmentValue - 10, ailmentValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                armor.SetAilment((Armor.ArmAilment)val1, (byte)ailmentDef);

                //Console.WriteLine("New Ailment is " + Enum.GetName(typeof(Armor.ArmAilment), armor.GetAilment()));
            }

            text += armor.GetItemSubtype();

            //roll for elements
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Element: " + randomRate * 100 + "%");

            if (randomRate <= elementRate * armor.GetResMod()) //robe has a better chance of having an element
            {
                int val1 = randNum.Next(1, elementCount);       //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text += " " + armor.GetArmorElementNames().ElementAt(val1).Keys.ElementAt(val2);  //suffix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte elementValue = armor.GetArmorElementNames().ElementAt(val1).Values.ElementAt(val2);
                int elementDef = randNum.Next(elementValue - 10, elementValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                armor.SetElement((Armor.ArmElement)val1, (byte)elementDef);
            }

            //Check for any ranks. Ranks boost armor defense power (or resist if armor is a robe).
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Rank: " + randomRate * 100 + "%");
            double rankRate3 = 0.02;
            double rankRate2 = 0.1;
            double rankRate1 = 0.25;

            if (randomRate <= rankRate3)
            {
                text += "+++";
                armor.SetItemType("Enhanced Armor");
                armor.SetRank(3);

                if (armor.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.4f * armor.GetMagicResist());
                    armor.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.4f * armor.GetDefensePower());
                    armor.SetDefensePower((short)boost);
                }
            }
            else if (randomRate <= rankRate2)
            {
                text += "++";
                armor.SetItemType("Enhanced Armor");
                armor.SetRank(2);

                if (armor.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.25f * armor.GetMagicResist());
                    armor.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.25f * armor.GetDefensePower());
                    armor.SetDefensePower((short)boost);
                }
            }
            else if (randomRate <= rankRate1)
            {
                text += "+";
                armor.SetItemType("Enhanced Armor");
                armor.SetRank(1);
                
                if (armor.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.15f * armor.GetMagicResist());
                    armor.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.15f * armor.GetDefensePower());
                    armor.SetDefensePower((short)boost);
                }
            }

            //Name is complete
            armor.SetItemName(text);

            //set all values for the item display form
            itemDisplay.ItemName = text;
            itemDisplay.ItemLevel = "Level: " + level;
            itemDisplay.ItemType = armor.GetItemType();
            itemDisplay.ItemSubType = armor.GetItemSubtype();
            itemDisplay.ItemAilment = Enum.GetName(typeof(Armor.ArmAilment), armor.GetAilment());
            itemDisplay.ItemElement = Enum.GetName(typeof(Armor.ArmElement), armor.GetElement());
            itemDisplay.Evasion = (armor.GetEvade() * 100).ToString();
            itemDisplay.DefensePower = armor.GetDefensePower().ToString();
            itemDisplay.MagicResist = armor.GetMagicResist().ToString();

            //need to check which elements/ailment values are set
            if (armor.GetElement() != Armor.ArmElement.None)
            {
                switch (armor.GetElement())
                {
                    case Armor.ArmElement.Fire:
                        itemDisplay.FireDefValue = armor.GetElementDefValue().ToString();
                        break;

                    case Armor.ArmElement.Water:
                        itemDisplay.WaterDefValue = armor.GetElementDefValue().ToString();
                        break;

                    case Armor.ArmElement.Wind:
                        itemDisplay.WindDefValue = armor.GetElementDefValue().ToString();
                        break;

                    case Armor.ArmElement.Earth:
                        itemDisplay.EarthDefValue = armor.GetElementDefValue().ToString();
                        break;

                    case Armor.ArmElement.Light:
                        itemDisplay.LightDefValue = armor.GetElementDefValue().ToString();
                        break;

                    case Armor.ArmElement.Dark:
                        itemDisplay.DarkDefValue = armor.GetElementDefValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            if (armor.GetAilment() != Armor.ArmAilment.None)
            {
                switch (armor.GetAilment())
                {
                    case Armor.ArmAilment.Poison:
                        itemDisplay.PoisonDefValue = armor.GetAilmentDefValue().ToString();
                        break;

                    case Armor.ArmAilment.Stun:
                        itemDisplay.StunDefValue = armor.GetAilmentDefValue().ToString();
                        break;

                    case Armor.ArmAilment.Freeze:
                        itemDisplay.FreezeDefValue = armor.GetAilmentDefValue().ToString();
                        break;

                    case Armor.ArmAilment.Death:
                        itemDisplay.DeathDefValue = armor.GetAilmentDefValue().ToString();
                        break;

                    case Armor.ArmAilment.Sleep:
                        itemDisplay.SleepDefValue = armor.GetAilmentDefValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            //all done! Show the generated item.
            itemDisplay.Show();
        }

        public void GenerateItem(Accessory accessory, byte level)
        {
            //Accessories can come with any stat, with different accessories having affinity towards certain stats. But generally, they will have
            //stat boosts for health, magic, and speed.

            accessory.SetItemLevel(level);

            string text = "";                    //this will be the ranomized name

            //generate name, starting with prefix if applicable, then the accessory type, and then the suffix.
            int total = accessory.GetAccBonusNames().ElementAt(1).Keys.Count;
            total = total * (level / accessory.GetMaxLevel());     //This will control the strength of generated items. Higher level = better gear

            switch(accessory.GetItemSubtype().ToLower())
            {
                case "ring":
                    {
                        int nameIndex = randNum.Next(0, total);
                        int val = (int)Accessory.AccBonus.Health;
                        text = accessory.GetAccBonusNames().ElementAt(val).Keys.ElementAt(nameIndex) + " ";

                        //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                        byte bonusValue = accessory.GetAccAilmentNames().ElementAt(val).Values.ElementAt(nameIndex);
                        int randVal = randNum.Next(bonusValue - 5, bonusValue) + 1;

                        //set the wepaon's ailment by casting val1 as enum
                        accessory.SetHealthPointBonus((short)randVal);
                    }
                    break;

                case "necklace":
                    {
                        int nameIndex = randNum.Next(0, total);
                        int val = (int)Accessory.AccBonus.Magic;
                        text = accessory.GetAccBonusNames().ElementAt(val).Keys.ElementAt(nameIndex) + " ";
                        byte bonusValue = accessory.GetAccAilmentNames().ElementAt(val).Values.ElementAt(nameIndex);
                        int randVal = randNum.Next(bonusValue - 5, bonusValue) + 1;

                        //set the wepaon's ailment by casting val1 as enum
                        accessory.SetMagicPointBonus((short)randVal);
                    }
                    break;

                case "boots":
                    {
                        int nameIndex = randNum.Next(0, total);
                        int val = (int)Accessory.AccBonus.Speed;
                        text = accessory.GetAccBonusNames().ElementAt(val).Keys.ElementAt(nameIndex) + " ";
                        byte bonusValue = accessory.GetAccAilmentNames().ElementAt(val).Values.ElementAt(nameIndex);
                        int randVal = randNum.Next(bonusValue - 5, bonusValue) + 1;

                        //set the wepaon's ailment by casting val1 as enum
                        accessory.SetSpeedBonus((short)randVal);
                    }
                    break;

                default:
                    break;
            }


            //start checking if other stats get boosted. Begin with defense
            double randomRate = randNum.NextDouble();
            double defenseRate = ((randomRate * level) / level) * 0.25;

            //roll
            randomRate = randNum.NextDouble();

            if (randomRate <= defenseRate)
            {
                //set up defense power
                int def = randNum.Next((level / 4) * 2, ((level / 4) * 5) + 1);
                accessory.SetDefensePower((short)def);
            }

            //roll for attack next
            randomRate = randNum.NextDouble();
            double attackRate = ((randomRate * level) / level) * 0.25;
            randomRate = randNum.NextDouble();

            if (randomRate <= attackRate)
            {
                int atk = randNum.Next((level / 4) * 2, ((level / 4) * 5) + 1);
                accessory.SetAttackPower((short)atk);
            }


            //int total = accessory.GetAccAilmentNames().ElementAt(1).Keys.Count;
            //total = total * (level / accessory.GetMaxLevel());     //This will control the strength of generated items. Higher level = better gear
            int elementCount = accessory.GetCount(accessory.GetAccElementNames());
            int ailmentCount = accessory.GetCount(accessory.GetAccAilmentNames());

            

            

            //magic resist
            if (accessory.GetItemSubtype().ToLower() == "robe")
            {
                int res = randNum.Next(level * 5, level * 5 * accessory.GetResMod());
                accessory.SetMagicResist((short)res);
            }

            //Evasion will have some variance, either -10 or +10 of the base
            double evadeRate = randNum.NextDouble() * ((accessory.GetEvade() + 0.1) - (accessory.GetEvade() - 0.1)) + (accessory.GetEvade() - 0.1);
            evadeRate = Math.Round(evadeRate, 2);
            accessory.SetEvade((float)evadeRate);


            //Accessories have a lower chance to get ailments or elements than other items.
            double randomRate = randNum.NextDouble();
            double ailmentRate = ((randomRate * level) / level) * 0.25;
            randomRate = randNum.NextDouble();
            double elementRate = ((randomRate * level) / level) * 0.25;

            Console.WriteLine("Chance to add Ailment Res: " + ailmentRate * 100 + "%");
            Console.WriteLine("Chance to add Element Res: " + elementRate * 100 + "%");

            //roll for ailments first
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Ailments: " + randomRate * 100 + "%");

            if (randomRate <= ailmentRate)
            {
                //accessory has an ailment so we apply it
                int val1 = randNum.Next(1, ailmentCount);   //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text = accessory.GetAccAilmentNames().ElementAt(val1).Keys.ElementAt(val2) + " ";   //prefix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte ailmentValue = accessory.GetAccAilmentNames().ElementAt(val1).Values.ElementAt(val2);
                int ailmentDef = randNum.Next(ailmentValue - 10, ailmentValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                accessory.SetAilment((Accessory.AccAilment)val1, (byte)ailmentDef);

                //Console.WriteLine("New Ailment is " + Enum.GetName(typeof(Accessory.AccAilment), accessory.GetAilment()));
            }

            text += accessory.GetItemSubtype();

            //roll for elements
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Element: " + randomRate * 100 + "%");

            if (randomRate <= elementRate * accessory.GetResMod()) //robe has a better chance of having an element
            {
                int val1 = randNum.Next(1, elementCount);       //we never reference index 0 because there's nothing there
                int val2 = randNum.Next(0, total);
                text += " " + accessory.GetAccElementNames().ElementAt(val1).Keys.ElementAt(val2);  //suffix

                //set the strength of the effect. We get a random number between the lowest value and highest value of the current tier.
                byte elementValue = accessory.GetAccElementNames().ElementAt(val1).Values.ElementAt(val2);
                int elementDef = randNum.Next(elementValue - 10, elementValue) + 1;

                //set the wepaon's ailment by casting val1 as enum
                accessory.SetElement((Accessory.AccElement)val1, (byte)elementDef);
            }

            //Check for any ranks. Ranks boost accessory defense power (or resist if accessory is a robe).
            randomRate = randNum.NextDouble();
            Console.WriteLine("Random Rate for Rank: " + randomRate * 100 + "%");
            double rankRate3 = 0.02;
            double rankRate2 = 0.1;
            double rankRate1 = 0.25;

            if (randomRate <= rankRate3)
            {
                text += "+++";
                accessory.SetItemType("Enhanced Acc");
                accessory.SetRank(3);

                if (accessory.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.4f * accessory.GetMagicResist());
                    accessory.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.4f * accessory.GetDefensePower());
                    accessory.SetDefensePower((short)boost);
                }
            }
            else if (randomRate <= rankRate2)
            {
                text += "++";
                accessory.SetItemType("Enhanced Acc");
                accessory.SetRank(2);

                if (accessory.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.25f * accessory.GetMagicResist());
                    accessory.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.25f * accessory.GetDefensePower());
                    accessory.SetDefensePower((short)boost);
                }
            }
            else if (randomRate <= rankRate1)
            {
                text += "+";
                accessory.SetItemType("Enhanced Acc");
                accessory.SetRank(1);

                if (accessory.GetItemSubtype().ToLower() == "robe")
                {
                    double boost = Math.Round(1.15f * accessory.GetMagicResist());
                    accessory.SetMagicResist((short)boost);
                }
                else
                {
                    double boost = Math.Round(1.15f * accessory.GetDefensePower());
                    accessory.SetDefensePower((short)boost);
                }
            }

            //Name is complete
            accessory.SetItemName(text);

            //set all values for the item display form
            itemDisplay.ItemName = text;
            itemDisplay.ItemLevel = "Level: " + level;
            itemDisplay.ItemType = accessory.GetItemType();
            itemDisplay.ItemSubType = accessory.GetItemSubtype();
            itemDisplay.ItemAilment = Enum.GetName(typeof(Accessory.AccAilment), accessory.GetAilment());
            itemDisplay.ItemElement = Enum.GetName(typeof(Accessory.AccElement), accessory.GetElement());
            itemDisplay.Evasion = (accessory.GetEvade() * 100).ToString();
            itemDisplay.DefensePower = accessory.GetDefensePower().ToString();
            itemDisplay.MagicResist = accessory.GetMagicResist().ToString();

            //need to check which elements/ailment values are set
            if (accessory.GetElement() != Accessory.AccElement.None)
            {
                switch (accessory.GetElement())
                {
                    case Accessory.AccElement.Fire:
                        itemDisplay.FireDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    case Accessory.AccElement.Water:
                        itemDisplay.WaterDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    case Accessory.AccElement.Wind:
                        itemDisplay.WindDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    case Accessory.AccElement.Earth:
                        itemDisplay.EarthDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    case Accessory.AccElement.Light:
                        itemDisplay.LightDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    case Accessory.AccElement.Dark:
                        itemDisplay.DarkDefValue = accessory.GetElementDefValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            if (accessory.GetAilment() != Accessory.AccAilment.None)
            {
                switch (accessory.GetAilment())
                {
                    case Accessory.AccAilment.Poison:
                        itemDisplay.PoisonDefValue = accessory.GetAilmentDefValue().ToString();
                        break;

                    case Accessory.AccAilment.Stun:
                        itemDisplay.StunDefValue = accessory.GetAilmentDefValue().ToString();
                        break;

                    case Accessory.AccAilment.Freeze:
                        itemDisplay.FreezeDefValue = accessory.GetAilmentDefValue().ToString();
                        break;

                    case Accessory.AccAilment.Death:
                        itemDisplay.DeathDefValue = accessory.GetAilmentDefValue().ToString();
                        break;

                    case Accessory.AccAilment.Sleep:
                        itemDisplay.SleepDefValue = accessory.GetAilmentDefValue().ToString();
                        break;

                    default:
                        break;
                }
            }

            //all done! Show the generated item.
            itemDisplay.Show();
        }

        //displays all item data
        private void DisplayItemData(ItemDisplay display)
        {

        }

       

    }
}
