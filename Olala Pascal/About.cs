using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlalaPascal
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/OlalaPascal");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //string spath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\";
        private void SetLanguage()
        {
            string path;
            string[] line;
            string Language = File.ReadAllText(AppPath.Data+"lang");

            AboutForm = new Dictionary<string, string>();
            if (Language == "English") return;
            path ="Language\\" + Language + "\\AboutForm.lang";
            line = File.ReadAllLines(path);

            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                string tmp;
                if (!AboutForm.TryGetValue(s[0], out tmp)) AboutForm.Add(s[0], s[1]);
            }

        }

        private void InitInterface()
        {
            SetLanguage();
            this.label1.Text = Translate("OLALA PASCAL 1.0");
            this.label3.Text = Translate("Name:    ANH TUAN NGUYEN");
            this.label4.Text = Translate("Class:                12a1");
            this.label5.Text = Translate("School:   Le Truc High School");
            this.Text = Translate("About");

            this.groupBox1.Text = Translate("Author");
            this.groupBox2.Text = Translate("Product uses the following components");
        }

        private string Translate(string s)
        {
            string output;
            if (AboutForm.TryGetValue(s, out output)) return output;
            return s;
        }

        Dictionary<string, string> AboutForm = new Dictionary<string, string>();

        private void About_Load(object sender, EventArgs e)
        {
            InitInterface();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:giaosudauto.omega@gmail.com?subject=Ý kiến đóng góp Olala Pascal");
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            AppSound.Play("hover.wav");
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            AppSound.Play("hover.wav");
        }
    }
}
