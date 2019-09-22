using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlalaPascal
{
    class History
    {
        private static List<string> history;
        private static string path = AppPath.Data + "history";
        public History()
        {
        }
        public static void Clear()
        {
            history = new List<string>();
        }
        public static void Add(string path)
        {
            history.Add(path);
            //MessageBox.Show(path);
        }
        public static List<string> List()
        {
            return history;
        }
        public static void Save()
        {

            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string file in history.Distinct())
            {
                sw.WriteLine(file);
            }
            sw.Close();
            fs.Close();
        }
        public static void Load()
        {
            history = new List<string>();
            if (!File.Exists(path)) File.WriteAllText(path,"\n");
            history.AddRange(File.ReadAllLines(path));
        }
    }
}
