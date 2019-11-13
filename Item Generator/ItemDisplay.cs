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
using System.Xml;

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

        public string HealthBonus
        {
            get { return TextBox_HealthPoints.Text; }
            set { TextBox_HealthPoints.Text = value; }
        }

        public string MagicBonus
        {
            get { return TextBox_MagicPoints.Text; }
            set { TextBox_MagicPoints.Text = value; }
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

        public string FireDefValue
        {
            get { return TextBox_FireDefValue.Text; }
            set { TextBox_FireDefValue.Text = value; }
        }

        public string WaterDefValue
        {
            get { return TextBox_WaterDefValue.Text; }
            set { TextBox_WaterDefValue.Text = value; }
        }

        public string EarthDefValue
        {
            get { return TextBox_EarthDefValue.Text; }
            set { TextBox_EarthDefValue.Text = value; }
        }

        public string WindDefValue
        {
            get { return TextBox_WindDefValue.Text; }
            set { TextBox_WindDefValue.Text = value; }
        }

        public string LightDefValue
        {
            get { return TextBox_LightDefValue.Text; }
            set { TextBox_LightDefValue.Text = value; }
        }

        public string DarkDefValue
        {
            get { return TextBox_DarkDefValue.Text; }
            set { TextBox_DarkDefValue.Text = value; }
        }

        ////////Defense Ailments////////////

        public string PoisonDefValue
        {
            get { return TextBox_PoisonDefValue.Text; }
            set { TextBox_PoisonDefValue.Text = value; }
        }

        public string StunDefValue
        {
            get { return TextBox_StunDefValue.Text; }
            set { TextBox_StunDefValue.Text = value; }
        }

        public string SleepDefValue
        {
            get { return TextBox_SleepDefValue.Text; }
            set { TextBox_SleepDefValue.Text = value; }
        }

        public string DeathDefValue
        {
            get { return TextBox_DeathDefValue.Text; }
            set { TextBox_DeathDefValue.Text = value; }
        }

        public string FreezeDefValue
        {
            get { return TextBox_FreezeDefValue.Text; }
            set { TextBox_FreezeDefValue.Text = value; }
        }

        #endregion

        //Write the contents to an XML file.
        private void Button_SaveAsXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);            
            defaultPath += "\\My Generated Items\\";
            System.IO.Directory.CreateDirectory(defaultPath);

            //create folders
            string weaponsPath = defaultPath + "\\Weapons\\";
            string armorPath = defaultPath + "\\Armor\\";
            string accPath = defaultPath + "\\Accessories\\";

            //weapons subfolders
            string swordPath = weaponsPath + "\\Swords\\";
            string axePath = weaponsPath + "\\Axes\\";
            string bowPath = weaponsPath + "\\Bows\\";
            string staffPath = weaponsPath + "\\Staves";

            //armor subfolders
            string suitPath = armorPath + "\\Suits\\";
            string vestPath = armorPath + "\\Vests\\";
            string robePath = armorPath + "\\Robes\\";

            //accessory subfolders
            string ringPath = accPath + "\\Rings\\";
            string bootPath = accPath + "\\Boots\\";
            string neckPath = accPath + "\\Necklaces\\";

            System.IO.Directory.CreateDirectory(swordPath);
            System.IO.Directory.CreateDirectory(axePath);
            System.IO.Directory.CreateDirectory(bowPath);
            System.IO.Directory.CreateDirectory(staffPath);
            System.IO.Directory.CreateDirectory(suitPath);
            System.IO.Directory.CreateDirectory(vestPath);
            System.IO.Directory.CreateDirectory(robePath);
            System.IO.Directory.CreateDirectory(ringPath);
            System.IO.Directory.CreateDirectory(bootPath);
            System.IO.Directory.CreateDirectory(neckPath);

            //create an initial filepath by checking what the item subtype is.
            switch (Label_ItemSubType.Text.ToLower())
            {
                case "sword":
                    fileDialog.InitialDirectory = swordPath;
                    break;

                case "axe":
                    fileDialog.InitialDirectory = axePath;
                    break;

                case "bow":
                    fileDialog.InitialDirectory = bowPath;
                    break;

                case "staff":
                    fileDialog.InitialDirectory = staffPath;
                    break;

                case "suit":
                    fileDialog.InitialDirectory = suitPath;
                    break;

                case "vest":
                    fileDialog.InitialDirectory = vestPath;
                    break;

                case "robe":
                    fileDialog.InitialDirectory = robePath;
                    break;

                case "ring":
                    fileDialog.InitialDirectory = ringPath;
                    break;

                case "boots":
                    fileDialog.InitialDirectory = bootPath;
                    break;

                case "necklace":
                    fileDialog.InitialDirectory = neckPath;
                    break;

            }

            fileDialog.FileName = Label_ItemName.Text;
            fileDialog.DefaultExt = "xml";
            fileDialog.AddExtension = true;                 //Ensures that the file extension is added even if removed
            fileDialog.OverwritePrompt = true;
            fileDialog.Filter = "eXtensible Markup Language (.xml)|*.xml|Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            

            //fileDialog.Dis

            //fileDialog.InitialDirectory = swordPath;
            //fileDialog.ShowDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

               Console.WriteLine(fileDialog.InitialDirectory);

                //write to file
                string fileName = fileDialog.InitialDirectory + fileDialog.FileName;
                XmlTextWriter writer = new XmlTextWriter(fileDialog.FileName, Encoding.UTF8);
                writer.WriteStartDocument();
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement(Label_ItemType.Text);  //This is a tag
                writer.WriteStartElement(Label_ItemSubType.Text);
                writer.WriteStartElement("Name");
                writer.WriteString(Label_ItemName.Text);

                writer.WriteEndElement();                       //close name tag
                writer.WriteEndElement();                       //close subtype tag
                writer.WriteEndElement();                       //close type tag

                writer.Close();
            }

        }
    }
}
