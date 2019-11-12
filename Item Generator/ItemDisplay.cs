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

        public string ItemAilment
        {
            get { return Label_AilmentType.Text; }
            set { Label_AilmentType.Text = value; }
        }

        public string ItemElement
        {
            get { return Label_ElementType.Text; }
            set { Label_ElementType.Text = value; }
        }

        public string AttackPower
        {
            get { return TextBox_AttackPower.Text; }
            set { TextBox_AttackPower.Text = value; }
        }

        public string DefensePower
        {
            get { return TextBox_DefensePower.Text; }
            set { TextBox_DefensePower.Text = value; }
        }

        public string MagicPower
        {
            get { return TextBox_MagicPower.Text; }
            set { TextBox_MagicPower.Text = value; }
        }

        public string Speed
        {
            get { return TextBox_Speed.Text; }
            set { TextBox_Speed.Text = value; }
        }

        public string Accuracy
        {
            get { return TextBox_Accuracy.Text; }
            set { TextBox_Accuracy.Text = value; }
        }

        public string Evasion
        {
            get { return TextBox_Evasion.Text; }
            set { TextBox_Evasion.Text = value; }
        }

        public string MagicResist
        {
            get { return TextBox_MagicResist.Text; }
            set { TextBox_MagicResist.Text = value; }
        }

        ////////Attack Elements////////////

        public string FireAtkValue
        {
            get { return TextBox_FireAtkValue.Text; }
            set { TextBox_FireAtkValue.Text = value; }
        }

        public string WaterAtkValue
        {
            get { return TextBox_WaterAtkValue.Text; }
            set { TextBox_WaterAtkValue.Text = value; }
        }

        public string EarthAtkValue
        {
            get { return TextBox_EarthAtkValue.Text; }
            set { TextBox_EarthAtkValue.Text = value; }
        }

        public string WindAtkValue
        {
            get { return TextBox_WindAtkValue.Text; }
            set { TextBox_WindAtkValue.Text = value; }
        }

        public string LightAtkValue
        {
            get { return TextBox_LightAtkValue.Text; }
            set { TextBox_LightAtkValue.Text = value; }
        }

        public string DarkAtkValue
        {
            get { return TextBox_DarkAtkValue.Text; }
            set { TextBox_DarkAtkValue.Text = value; }
        }

        ////////Attack Ailments////////////

        public string PoisonAtkValue
        {
            get { return TextBox_PoisonAtkValue.Text; }
            set { TextBox_PoisonAtkValue.Text = value; }
        }

        public string StunAtkValue
        {
            get { return TextBox_StunAtkValue.Text; }
            set { TextBox_StunAtkValue.Text = value; }
        }

        public string SleepAtkValue
        {
            get { return TextBox_SleepAtkValue.Text; }
            set { TextBox_SleepAtkValue.Text = value; }
        }

        public string DeathAtkValue
        {
            get { return TextBox_DeathAtkValue.Text; }
            set { TextBox_DeathAtkValue.Text = value; }
        }

        public string FreezeAtkValue
        {
            get { return TextBox_FreezeAtkValue.Text; }
            set { TextBox_FreezeAtkValue.Text = value; }
        }

        ////////Defense Elements////////////

        ////////Defense Ailments////////////

        #endregion
    }
}
