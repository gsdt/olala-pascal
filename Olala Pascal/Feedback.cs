using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OlalaPascal
{
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pass = 0;

            if (IsValidEmail(email.Text)) Disable(ref wEmail);
            else Enable(ref wEmail);

            if (IsValidName(name.Text)) Disable(ref wName);
            else Enable(ref wName);

            if (IsValidName(job.Text)) Disable(ref wJob);
            else Enable(ref wJob);

            if (IsValidName(province.Text)) Disable(ref wProvince);
            else Enable(ref wProvince);

            if (rated != 0) Disable(ref wRating);
            else Enable(ref wRating);

            if (pass == 5)
            {
                try
                {
                    Process.Start(@"www.giaosudauto.xyz\olalapascal\feedback.php?name=" + name.Text + "&province=" + province.Text +
                        "&job=" + job.Text + "&email=" + email.Text + "&rating=" + rated.ToString() + "&feedback=" + content.Text);
                }
                catch { }
                this.Close();
            }
        }

        private void Enable(ref PictureBox x)
        {
            x.Enabled = true;
            x.Image = global::OlalaPascal.Properties.Resources._1465666949_Warning;
            
        }

        private void Disable(ref PictureBox x)
        {
            x.Enabled = false;
            x.Image = null;
            pass++;
        }

        private bool IsValidEmail(string emailaddress)
        {
            if (emailaddress == "") return false;
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool IsValidName(string name)
        {
            return ! (name == "");
        }

        private void InitRating()
        {
            rate1.Image = global::OlalaPascal.Properties.Resources.star_null;
            rate2.Image = global::OlalaPascal.Properties.Resources.star_null;
            rate3.Image = global::OlalaPascal.Properties.Resources.star_null;
            rate4.Image = global::OlalaPascal.Properties.Resources.star_null;
            rate5.Image = global::OlalaPascal.Properties.Resources.star_null;
        }

        private void rate1_Click(object sender, EventArgs e)
        {
            rated = 1;
            InitRating();
            rate1.Image = global::OlalaPascal.Properties.Resources.star_yellow;
        }

        private void rate2_Click(object sender, EventArgs e)
        {
            rated = 2;
            InitRating();
            rate1.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate2.Image = global::OlalaPascal.Properties.Resources.star_yellow;
        }

        private void rate3_Click(object sender, EventArgs e)
        {
            rated = 3;
            InitRating();
            rate1.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate2.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate3.Image = global::OlalaPascal.Properties.Resources.star_yellow;
        }

        private void rate4_Click(object sender, EventArgs e)
        {
            rated = 4;
            InitRating();
            rate1.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate2.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate3.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate4.Image = global::OlalaPascal.Properties.Resources.star_yellow;
        }

        private void rate5_Click(object sender, EventArgs e)
        {
            rated = 5;
            InitRating();
            rate1.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate2.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate3.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate4.Image = global::OlalaPascal.Properties.Resources.star_yellow;
            rate5.Image = global::OlalaPascal.Properties.Resources.star_yellow;
        }

        int rated;
        int pass = 0;

        private void Feedback_Load(object sender, EventArgs e)
        {
            Disable(ref wRating);
            Disable(ref wJob);
            Disable(ref wName);
            Disable(ref wProvince);
            Disable(ref wEmail);
            rated = 0;
        }

        private void wName_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Invalid name", this.wName);
        }

        private void wProvince_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Invalid province", this.wProvince);
        }

        private void wJob_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Invalid job", this.wJob);
        }

        private void wEmail_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Invalid email", this.wEmail);
        }

        private void wRating_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("You have not rating", this.wRating);
        }

        private void Feedback_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pass>5) e.Cancel = true;
        }


    }
}
