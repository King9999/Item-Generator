/* This form displays all information of a generated item. The stats of the item can be modified to suit the user's needs, and can also be saved as an XML for use
 * in a game. */

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
    public partial class ItemDisplay : Form
    {
        public ItemDisplay()
        {
            InitializeComponent();
        }

        private void Label_ItemName_Click(object sender, EventArgs e)
        {
            
        }

        private void Tooltip_HealthPoints_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void TextBox_HealthPoints_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

#region Getters/Setters
        public string ItemName
        {
            get { return Label_ItemName.Text; }
            set { Label_ItemName.Text = value; }
        }

        public string ItemType
        {
            get { return Label_ItemType.Text; }
            set { Label_ItemType.Text = value; }
        }

        public string ItemSubType
        {
            get { return Label_ItemSubType.Text; }
            set { Label_ItemSubType.Text = value; }
        }

        public string ItemLevel
        {
            get { return Label_ItemLevel.Text; }
            set { Label_ItemLevel.Text = value; }
        }
        #endregion
    }
}
