using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace OlalaPascal
{

    class Translator
    {
        static private Dictionary<string, string> Tran;
        static private Lang lang;
        enum Lang
        {
            Vietnames,
            English
        }
        //static string spath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\";
        public static void SetLang()
        {
            string tmp;
            string path;
            string[] lines;
            string Language = File.ReadAllText(AppPath.Data+"lang");
            //**** Go to form ****
            path = "Language\\" + Language + "\\Tran.lang";
            Tran = new Dictionary<string, string>();

            lines = File.ReadAllLines("Language\\" + Language + "\\Suggest.lang");
            foreach (string curent in lines)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                if (!Tran.TryGetValue(s[0], out tmp)) Tran.Add(s[0], s[1]);
            }

            if (Language == "Vietnamese") lang = Lang.Vietnames;
            else
            {
                lang = Lang.English;
                return;
            }
            lines = File.ReadAllLines(path);
            foreach (string curent in lines)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                if (!Tran.TryGetValue(s[0], out tmp)) Tran.Add(s[0], s[1]);
            }
            //Console.WriteLine(Language);
        }

        private static string[] SplitDigit(string s)
        {
            Match match = Regex.Match(s, @"(\d+.\d+)|(\d+)");
            string[] output = new string[10];
            int dem = 0;
            if (match.Success)
            {
                output.SetValue(match.Value, dem++);
            }
            else return null;
            while (true)
            {
                match = match.NextMatch();
                if (match.Success)
                {
                    output.SetValue(match.Value, dem++);
                }
                else return output;
            }
        }

        public static string Translate(string s)
        {
            if (lang == Lang.English) return s;
            //Console.WriteLine("-" + s + "-");
            string[] word = s.Split(' ');
            string result;
            string ctmp ;
            if(s.Length>0 && s[0]=='[')
            {
                return word[0] + " " + Translate(s.Substring(word[0].Length).Trim());
            }
            if (Regex.IsMatch(word[0], @"\(\d+\,\d+\)")||Regex.IsMatch(word[0],@"\(\d+\)"))
            {
                return word[0] + " " + Translate(word[1]) + " " + Translate(s.Substring(s.IndexOf(':') + 2));
            }
            if (word.Length > 1 && (word[0] == "Compiling" || word[0] == "Linking" || word[0] == "Running" || word[0] == "Fatal:" || word[0] == "identifier"))
            {
                return Translate(word[0]) + " " + Translate(s.Substring(s.IndexOf(' ') + 1));
            }

            if (Regex.IsMatch(s, @"\d+ lines compiled, \d+.\d+ sec , \d+ bytes code, \d+ bytes data"))
            {
                string[] num = SplitDigit(s);
                return num[0] + " " + Translate("lines compiled,") + " " + num[1] + " " + Translate("sec ,") + " " + num[2] + " " + Translate("bytes code,") + " " + num[3] + " " + Translate("bytes data");
            }
            if(word.Length==3 && word[2]=="issued")
            {
                return word[0] +" "+ Translate(s.Substring(word[0].Length + 1).Trim());
            }
            ctmp = "Can't find unit";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate("Can't find unit") + " " + word[3] + " " + Translate("used by") + " " + Translate( word[6]);
            }//Operator is not overloaded: "Constant String" - "Extended"
            ctmp = "Operator is not overloaded:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                string[] a = s.Split('"');
                return Translate(ctmp) + " \"" + Translate(a[1]) + "\" - \"" + Translate(a[3])+"\"";
            }
            ctmp = "Value ranges:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Size:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }//:
            ctmp = "Note:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Fatal:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Error:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Hint:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Warning:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Label not defined";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + Translate(s.Substring(ctmp.Length + 1));
            }
            ctmp = "Variable:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                //return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
                string[] a = s.Split('"');
                return Translate(ctmp) + " \"" + a[1] + "\" " + Translate(s.Substring(ctmp.Length + a[1].Length + 4).Trim());
            }
            ctmp = "Number of characters:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1));
            }//

            ctmp = "Incompatible type for arg no.";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + word[5] + " " + Translate(word[6]) + " " + word[7] + " " + Translate(word[8]) + " " + word[9];
            }

            if (Regex.IsMatch(s, @"There were \d+ errors compiling module, stopping"))
            {
                return Translate("There were") + " " + word[2] + " " + Translate("errors compiling module, stopping");
            }
            ctmp = "No option inside";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {// config file
                return "Không có tùy chọn trong file cấu hình: " + s.Substring(ctmp.Length + 1).Remove(s.Substring(ctmp.Length + 1).Length - 12);
            }//
            ctmp = "Wrong number of parameters specified for call to";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + s.Substring(ctmp.Length + 1);
            }
            ctmp = "Illegal unit name:";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + s.Substring(ctmp.Length + 1);
            }
            ctmp = "Start of reading config file";
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + s.Substring(ctmp.Length + 1);
            }   
            ctmp = "Number of significant digits:";
            ////////Number of sigificant digits: 19 or 20.
            if (s.Length > ctmp.Length && s.Substring(0, ctmp.Length) == ctmp)
            {
                return Translate(ctmp) + " " + Translate(s.Substring(ctmp.Length + 1).Trim());
            }

            if (word.Length == 3 && word[1] == "or")
            {
                return word[0] + " " + Translate(word[1]) + " " + word[2];
            }
            //-----------------------------
            word = s.Split('\"');
            if (word.Length > 1)
            {
                result = "";
                for (int i = 0; i < word.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        result += Translate(word[i].Trim()) + " ";
                    }
                    else
                    {
                        result += "\"" + Translate(word[i].Trim()) + "\" ";
                    }
                }
                return result;
            }

            //-----------------------------
            try
            {
                return Tran[s.Trim()];
            }
            catch
            {
                return s;
            }
        }

        public static string Translate2(string s)
        {
            string tmp;
            string oup="";
            if (!Tran.TryGetValue(s, out tmp))
                tmp = s;
            string[] w = tmp.Split('$');
            oup = Char.ToUpper(w[0][0])+w[0].Substring(1);
            for(int i=1; i<w.Length; i++)
            {
                oup += "\n" + Char.ToUpper(w[i][0]) + w[i].Substring(1);
            }
            return oup;
        }

    }
}
