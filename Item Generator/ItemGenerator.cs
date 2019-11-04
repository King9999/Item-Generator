/* ITEM GENERATOR BY MIKE MURRAY - NOVEMBER 3, 2019 - Twitter: @MikeADMurray - king9999.itch.io
 * This program is a tool made for ProcJam 2019. What it will do is procedurally generate items for use in a RPG. The items created can be saved as a XML file.
 * I will try to be as generic as possible so that potential devs can just customize the XML files or even the code itself to suit their needs. */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





namespace Item_Generator
{

    public partial class ItemGenerator : Form
    {
        const byte WEAPON = 0;
        const byte ARMOR = 1;
        const byte ACCESSORY = 2;
        const byte CONSUMABLE = 3;

        //list


        public ItemGenerator()
        {
            InitializeComponent();
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello World!");

            /* If the user doesn't select anything in the drop down boxes, throw an exception */

            ItemDisplay item = new ItemDisplay();
            item.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox_ItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Every time the item type is selected, the contents of the subtype combo box is cleared and re-populated.
            ComboBox_ItemSubType.Items.Clear();

            //Now we populate the subtype again
            int selectedItem = ComboBox_ItemType.SelectedIndex;
            switch (selectedItem)
            {
                case WEAPON:
                    MessageBox.Show("You selected Weapon");
                    break;

                case ARMOR:
                    MessageBox.Show("You selected Armor");
                    break;

                default:
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox_ItemSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* The contents of this box changes depending on what's selected for the item type in the first combo box. The contents are
             * generated at runtime. */

           
        }
    }
}
