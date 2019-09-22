using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Xml;
using System.Globalization;
using System.IO;
using OlalaPascal;

namespace OlalaPascal
{
    public partial class HighlightCodeSetting : Form
    {

        public HighlightCodeSetting()
        {
            if (!File.Exists(AppPath.Data + "EditorSetting.xml"))
            {
                //File.WriteAllText(@"Setting\highlightOption.txt", "Defaul");
                File.WriteAllText(AppPath.Data + "EditorSetting.xml", OlalaPascal.Properties.Resources.Light);
            }
            doc = new XmlDocument();
            doc.Load(AppPath.Data + "EditorSetting.xml");

            InitializeComponent();

        }
        private static SizeF GetCharSize(Font font, char c)
        {
            Size sz2 = TextRenderer.MeasureText("<" + c.ToString() + ">", font);
            Size sz3 = TextRenderer.MeasureText("<>", font);

            return new SizeF(sz2.Width - sz3.Width + 1, /*sz2.Height*/font.Height);
        }

        

        private void HighlightCodeSetting_Load(object sender, EventArgs e)
        {

            SetLang();
            this.listBox.Items.AddRange(new object[] {
                Translate("Comment Line"),
                Translate("Comment using { and }"),
                Translate("Comment using (* and *)"),
                Translate("Operator"),
                Translate("Number"),
                Translate("Char code"),
                Translate("String"),
                Translate("Compile Intruction"),
                Translate("Keyword"),
                Translate("Funtion name"),
                Translate("Variable type"),
                Translate("Defaul"),
                
            });

            this.SampleComboBox.Items.AddRange(new object[] 
            {
                Translate("Classic"),
                Translate("Dark"),
                Translate("Light")
            });

            InitLanguage();
            string[] list = new string[20];
            string[] fontList = new string[] 
            { 
                ".VnCourier",
                ".VnCourier New",
                ".VnCourier NewH",
                "Consolas",
                "Courier New",
                "Letter Gothic Std",
                "Lucida Console",
                "Lucida Sans Typewriter",
                "Orator Std"
            };
            this.fontComboBox.Items.AddRange(fontList);
            for (int i = 8; i <= 72; i++)
            {
                this.fontSizeComboBox.Items.Add(i);

            }

            this.fastColoredTextBox.Dispose();
            ntb.TextBox(doc);
            this.Controls.Add(ntb.textbox);

            this.fontComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontName"].Value;
            this.fontSizeComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontSize"].Value;
            this.but_Background.BackColor=ParseColor(doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value);
            this.tb_Background.Text = doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value;
            
            this.listBox.SelectedIndex = 0;
        }

        private class NewTextBox
        {
            public FastColoredTextBox textbox { get; set; }
            public void TextBox(XmlDocument doc)
            {
                textbox = new FastColoredTextBox();
                textbox.BackColor = ParseColor(doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value);
                textbox.AutoCompleteBracketsList = new char[] { '(', ')', '[', ']', '\"', '\"', '\'', '\'' };
                textbox.SyntaxHighlighter.SetSyntaxHighlighter(doc);
                XmlNode xmlFont = doc.SelectSingleNode("HighlightSetting/Genaral");
                textbox.Font = new Font(xmlFont.Attributes["fontName"].Value, (float)(Convert.ToDouble(xmlFont.Attributes["fontSize"].Value)));
                textbox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>:=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:" +
        "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
                textbox.AutoScrollMinSize = new System.Drawing.Size(251, 238);
                textbox.BackBrush = null;
                textbox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
                textbox.CharHeight = 14;
                textbox.CharWidth = 8;
                textbox.DefaultStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/DefaulStyle"));
                textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
                textbox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
                textbox.IsReplaceMode = false;
                textbox.Language = FastColoredTextBoxNS.Language.Pascal;
                textbox.LeftBracket = '(';
                textbox.LeftBracket2 = '[';
                textbox.Location = new System.Drawing.Point(316, 198);
                textbox.Name = "fastColoredTextBox";
                textbox.Paddings = new System.Windows.Forms.Padding(0);
                textbox.RightBracket = ')';
                textbox.RightBracket2 = ']';
                textbox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
                //textbox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox.ServiceColors")));
                textbox.Size = new System.Drawing.Size(452, 225);
                textbox.TabIndex = 6;
                //textbox.Text = resources.GetString("fastColoredTextBox.Text");
                textbox.Zoom = 100;
                textbox.Text =
    @"{$H+,R+}
program myapp;
type
    student = record
        name: string;
        age: byte;
    end;
var
    Tom, Marry: student;
begin
    // this is comment line
    { this is a comment }
    (* this is a comment *)
    Tom.name:='Hacker Tom';
    Tom.age:=20;
    Marry.name:=#65 + #66;
end.";
            }
            public TextStyle ParseStyle(XmlNode styleNode)
            {
                XmlAttribute colorA = styleNode.Attributes["foreColor"];
                XmlAttribute backColorA = styleNode.Attributes["backColor"];
                XmlAttribute fontStyleA = styleNode.Attributes["fontStyle"];
                //System.Windows.Forms.MessageBox.Show(colorA.Value+"-"+backColorA.Value+"-"+fontStyleA.Value);
                //colors
                SolidBrush foreBrush = null;
                if (colorA != null)
                    foreBrush = new SolidBrush(ParseColor(colorA.Value));
                SolidBrush backBrush = null;
                if (backColorA != null)
                    backBrush = new SolidBrush(ParseColor(backColorA.Value));
                //fontStyle
                FontStyle fontStyle = FontStyle.Regular;
                if (styleNode.Attributes["underline"].Value == "yes") fontStyle = fontStyle | FontStyle.Underline;
                if (styleNode.Attributes["italic"].Value == "yes") fontStyle = fontStyle | FontStyle.Italic;
                if (styleNode.Attributes["bold"].Value == "yes") fontStyle = fontStyle | FontStyle.Bold;
                if (styleNode.Attributes["strikeout"].Value == "yes") fontStyle = fontStyle | FontStyle.Strikeout;

                return new TextStyle(foreBrush, backBrush, fontStyle);
            }
            private Color ParseColor(string s)
            {
                if (s.StartsWith("#"))
                {
                    if (s.Length <= 7)
                        return Color.FromArgb(255,
                                              Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
                    else
                        return Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
                }
                else
                    return Color.FromName(s);
            }
            public void Dispose()
            {
                this.textbox.Dispose();
            }

        }

        private Color ParseColor(string s)
        {
            if (s.StartsWith("#"))
            {
                if (s.Length <= 7)
                    return Color.FromArgb(255,
                                          Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
                else
                    return Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
            }
            else
                return Color.FromName(s);
        }

        private string getNameType(int num)
        {
            switch (num)
            {
                case 0: return "CommentLineStyle";
                case 1: return "CommentCurlyStyle";
                case 2: return "CommentDoubleStyle";
                case 3: return "OperatorStyle";
                case 4: return "NumberStyle";
                case 5: return "SharpCharStyle";
                case 6: return "StringStyle";
                case 7: return "InstructionCompileStyle";
                case 8: return "KeywordStyle";
                case 9: return "ClassNameStyle";
                case 10: return "VariableTypeStyle";
                case 11: return "DefaulStyle";
            }
            return null;
        }

        private bool ContaintDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (System.Char.IsDigit(s[i])) return true;
            return false;
        }

        private void foregroundButton_Click(object sender, EventArgs e)
        {

            DialogResult res = colorDialog.ShowDialog();
            bool Digit = false;
            if (res == DialogResult.OK)
            {
                if (ContaintDigit(this.colorDialog.Color.Name))
                    Digit = true;
                string name = getNameType(this.listBox.SelectedIndex);
                XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
                node.Attributes["foreColor"].Value = (Digit) ? "#" + colorDialog.Color.Name : colorDialog.Color.Name;
                this.foregroundText.Text = colorDialog.Color.Name;
                this.foregroundButton.BackColor = colorDialog.Color;
                UpdateTextbox();

                //this.ntb.textbox.SyntaxHighlighter.SetSyntaxHighlighter(doc);
                //this.ntb.textbox.SyntaxHighlighter.PascalSyntaxHighlight(ntb.textbox.AllRange, ntb.textbox.AllRange);
            }
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            DialogResult res = colorDialog.ShowDialog();
            bool Digit = false;
            if (res == DialogResult.OK)
            {
                if (ContaintDigit(this.colorDialog.Color.Name))
                    Digit = true;
                string name = getNameType(this.listBox.SelectedIndex);
                XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
                node.Attributes["backColor"].Value = (Digit) ? "#" + colorDialog.Color.Name : colorDialog.Color.Name;
                this.backgroundText.Text = colorDialog.Color.Name;
                this.backgroundButton.BackColor = colorDialog.Color;
                UpdateTextbox();
            }
            //MessageBox.Show("dđ");
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);

            this.foregroundText.Text = node.Attributes["foreColor"].Value;
            this.foregroundButton.BackColor = ParseColor(node.Attributes["foreColor"].Value);

            this.backgroundText.Text = node.Attributes["backColor"].Value;
            this.backgroundButton.BackColor = ParseColor(node.Attributes["backColor"].Value);

            this.boldCheckbox.Checked = (node.Attributes["bold"].Value == "yes") ? true : false;
            this.italicsCheckbox.Checked = (node.Attributes["italic"].Value == "yes") ? true : false;
            this.underlineCheckbox.Checked = (node.Attributes["underline"].Value == "yes") ? true : false;
            this.strikeoutCheckbox.Checked = (node.Attributes["strikeout"].Value == "yes") ? true : false;
        }
        //string spath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\";
        private void okButton_Click(object sender, EventArgs e)
        {
            doc.Save(AppPath.Data+"EditorSetting.xml");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/Genaral");
            node.Attributes["fontName"].Value = this.fontComboBox.Text;
            UpdateTextbox();
        }


        private void UpdateTextbox()
        {
            int ver = ntb.textbox.VerticalScroll.Value;
            int hor = ntb.textbox.HorizontalScroll.Value;
            ntb.Dispose();
            ntb.TextBox(doc);
            this.Controls.Add(ntb.textbox);
            if (ntb.textbox.VerticalScroll.Maximum > ver) ntb.textbox.VerticalScroll.Value = ver; else ntb.textbox.VerticalScroll.Value = 0;
            if (ntb.textbox.HorizontalScroll.Maximum > hor) ntb.textbox.HorizontalScroll.Value = hor; else ntb.textbox.HorizontalScroll.Value = 0;
            //ntb.textbox.HorizontalScroll.Value = hor;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/Genaral");
            node.Attributes["fontSize"].Value = this.fontSizeComboBox.Text;
            UpdateTextbox();
        }

        private void restoreDefaulButton_Click(object sender, EventArgs e)
        {
            doc = new XmlDocument();
            File.Delete(AppPath.Data+"ditorSetting.xml");
            //File.Copy(@"Setting\Defaul.xml", @"Setting\EditorSetting.xml");
            File.WriteAllText(AppPath.Data+"EditorSetting.xml", Properties.Resources.Light.ToString());
            doc.Load(AppPath.Data + "EditorSetting.xml");
            listBox1_SelectedIndexChanged(null, null);
            this.fontComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontName"].Value;
            this.fontSizeComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontSize"].Value;
            UpdateTextbox();
        }
        NewTextBox ntb = new NewTextBox();
        XmlDocument doc;

        private void boldCheckbox_Click(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
            node.Attributes["bold"].Value = (this.boldCheckbox.Checked) ? "yes" : "no";
            UpdateTextbox();
        }

        private void strikeoutCheckbox_Click(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
            node.Attributes["strikeout"].Value = (this.strikeoutCheckbox.Checked) ? "yes" : "no";
            UpdateTextbox();
        }

        private void italicsCheckbox_Click(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
            node.Attributes["italic"].Value = (this.italicsCheckbox.Checked) ? "yes" : "no";
            UpdateTextbox();
        }

        private void underlineCheckbox_Click(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
            node.Attributes["underline"].Value = (this.underlineCheckbox.Checked) ? "yes" : "no";
            UpdateTextbox();
        }

        private Dictionary<string, string> HighlightSettingForm;
        private void SetLang()
        {
            string path;
            string[] line;
            string Language = File.ReadAllText(AppPath.Data+"lang");
            HighlightSettingForm = new Dictionary<string, string>();
            if (Language == "English") return;
            path =  "Language\\" + Language + "\\HighlightSettingForm.lang";
            line = File.ReadAllLines(path);
            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                string tmp;
                if (!HighlightSettingForm.TryGetValue(s[0], out tmp)) HighlightSettingForm.Add(s[0], s[1]);
            }
        }

        private string Translate(string s)
        {
            string tmp;
            if (!HighlightSettingForm.TryGetValue(s, out tmp))
                return s;
            return tmp;
        }

        private void InitLanguage()
        {
            this.label5.Text = Translate("Background");
            this.genaralGroupBox.Text = Translate("General");
            this.sizefontLabel.Text = Translate("Size");
            this.fontLabel.Text = Translate("Font");
            this.typeLabel.Text = Translate("Type");
            this.OptionGroupBox.Text = Translate("Optional");
            this.groupBox2.Text = Translate("Color");
            this.backgroundText.Text = Translate("Foreground");
            this.foregroundText.Text = Translate("Foreground");
            this.label2.Text = Translate("Background");
            this.label1.Text = Translate("Foreground");
            this.groupBox1.Text = Translate("Style");
            this.strikeoutCheckbox.Text = Translate("Strikeout");
            this.underlineCheckbox.Text = Translate("Underline");
            this.italicsCheckbox.Text = Translate("Italics");
            this.boldCheckbox.Text = Translate("Bold");
            this.label3.Text = Translate("Preview");
            this.okButton.Text = Translate("OK");
            this.cancelButton.Text = Translate("Cancel");
            this.Text = Translate("HighlightCodeSetting");
            this.label4.Text = Translate("Sample");
        }

        private void but_Background_Click(object sender, EventArgs e)
        {
            DialogResult res = colorDialog.ShowDialog();
            bool Digit = false;
            if (res == DialogResult.OK)
            {
                if (ContaintDigit(this.colorDialog.Color.Name))
                    Digit = true;
                XmlNode node = doc.SelectSingleNode("HighlightSetting/Background");
                node.Attributes["color"].Value = (Digit) ? "#" + colorDialog.Color.Name : colorDialog.Color.Name;
                this.tb_Background.Text = colorDialog.Color.Name;
                this.but_Background.BackColor = colorDialog.Color;
                UpdateTextbox();
            }
        }

        private void backgroundText_Click(object sender, EventArgs e)
        {
            string name = getNameType(this.listBox.SelectedIndex);
            XmlNode node = doc.SelectSingleNode("HighlightSetting/" + name);
            node.Attributes["backColor"].Value = Color.Transparent.Name;
            this.backgroundText.Text = Color.Transparent.Name;
            this.backgroundButton.BackColor = Color.Transparent;
            UpdateTextbox();
        }

        private void WriteFile(string path, string contents)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(contents);
            sw.Close();
            fs.Close();
        }

        private void SampleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string spath= Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            switch(SampleComboBox.SelectedIndex)
            {
                case 0: // classic
                    WriteFile( AppPath.Data + "EditorSetting.xml", Properties.Resources.Classic);
                    break;
                case 1: // dark
                    WriteFile( AppPath.Data + "EditorSetting.xml", Properties.Resources.Dark);
                    break;
                case 2: // light
                    WriteFile( AppPath.Data + "EditorSetting.xml", Properties.Resources.Light);
                    break;
            }
            doc = new XmlDocument();
            doc.Load(AppPath.Data+"EditorSetting.xml");
            this.fontComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontName"].Value;
            this.fontSizeComboBox.Text = doc.SelectSingleNode("HighlightSetting/Genaral").Attributes["fontSize"].Value;
            this.but_Background.BackColor = ParseColor(doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value);
            this.tb_Background.Text = doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value;
            UpdateTextbox();
        }


        
    }

}
