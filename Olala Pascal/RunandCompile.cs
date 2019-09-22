//#define debug
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Media;

namespace OlalaPascal
{
   
    class RunandCompile
    {
        // Propertite
        private string pathcompiler=null;
        private string pathfile=null;
        private string command=null;
        //public string tmp = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Olala\\";
        
        private string option
        {
            get
            {
                string res = "";
                res += (Properties.Settings.Default.StopAffterFirstErrorCheck) ? "-Se " : "";
                res += (Properties.Settings.Default.AllowLABELandGOTO) ? "-Sg " : "";
                res += (Properties.Settings.Default.EnableMacros) ? "-Sm " : "";
                res += (Properties.Settings.Default.AllowInline) ? "-Si " : "";
                res += (Properties.Settings.Default.AllowSTATICinObject) ? "-St " : "";
                res += (Properties.Settings.Default.IncludeAssertionCode) ? "-Sa " : "";
                res += (Properties.Settings.Default.LoadKylixcompact) ? "-Sk " : "";
                res += (Properties.Settings.Default.CLikeOperator) ? "-Se " : "";
                switch(Properties.Settings.Default.CompilerMode)
                {
                    case 'F': res += "-Mfpc "; break;
                    case 'O': res += "-Mobjfpc "; break;
                    case 'D': res += "-Mdelphi "; break;
                    case 'T': res += "-Mtp "; break;
                    case 'M': res += "-Mmacpas "; break;
                }
                
                res += (Properties.Settings.Default.RangeChecking) ? "-Cr " : "";
                res += (Properties.Settings.Default.StackChecking) ? "-Ct " : "";
                res += (Properties.Settings.Default.IOChecking) ? "-Ci " : "";
                res += (Properties.Settings.Default.IntegerOverflowChecking) ? "-Co " : "";
                res += (Properties.Settings.Default.ObjectMethodCallChecking) ? "-CR " : "";
                res += (Properties.Settings.Default.PositionIndepentCode) ? "-Cg " : "";
                res += (Properties.Settings.Default.CreateSmartlinkableUnits) ? "-Cx " : "";

                res += (Properties.Settings.Default.GenerateSmallerCode) ? "-Os " : "";
                res += (Properties.Settings.Default.UseResisterVariable) ? "-oREGVAR " : "";
                res += (Properties.Settings.Default.UncertainOptimizations) ? "-oUNCERTAIN " : "";
                res += (Properties.Settings.Default.Level1Optimizations) ? "-O1 " : "";
                res += (Properties.Settings.Default.Level2Optimizations) ? "-O2 " : "";
                res += (Properties.Settings.Default.Level3Optimizations) ? "-O3 " : "";

                res += (Properties.Settings.Default.Warnings) ? "-vw " : "";
                res += (Properties.Settings.Default.Hints) ? "-vh " : "";
                res += (Properties.Settings.Default.Notes) ? "-vn " : "";
                res += (Properties.Settings.Default.GeneralInfo) ? "-vi " : "";
                res += (Properties.Settings.Default.UseTryedInfo) ? "-vut " : "";
                res += (Properties.Settings.Default.All) ? "-va " : "";
                res += (Properties.Settings.Default.ShowallProcedures) ? "-vb " : "";

                switch(Properties.Settings.Default.Brownser)
                {
                    case 'O': res += "-b "; break;
                    case 'L': res+="-bl "; break;
                }

                switch(Properties.Settings.Default.AssemblerReader)
                {
                    case 'D': res += "-Rdefault "; break;
                    case 'A': res += "-Ratt "; break;
                    case 'I': res += "-Rintel "; break;
                }

                res += (Properties.Settings.Default.ListSource) ? "-al " : "";
                res += (Properties.Settings.Default.ListRegisterAllocation) ? "-ar " : "";
                res += (Properties.Settings.Default.ListTempAllocation) ? "-at " : "";
                res += (Properties.Settings.Default.ListNodeAllocation) ? "-an " : "";
                res += (Properties.Settings.Default.UsePipeWithAssembler) ? "-ap " : "";


                res += Properties.Settings.Default.AdditionalCompilerArgs+" ";
                string[] def = Properties.Settings.Default.ConditionalDefines.Split(' ');
                foreach(string x in def)
                {
                    res += "-d" + x.ToUpper() + " ";
                }
                return res;
            }
        }
        private string fileExe()
        {
            string extension = System.IO.Path.GetExtension(this.pathfile);
            return this.pathfile.Substring(0, this.pathfile.Length - extension.Length) + ".exe";
        }
        private Process compiler;
        private Process runner;
        private bool pathCompilerExist
        {
            get 
            {
                string path = pathcompiler.Substring(1, pathcompiler.Length - 2);
                return File.Exists(path);
            }
        }
        // Methol
        public string fileName()
        {
            if (pathfile == null) return null;
            return Path.GetFileName(pathfile);
        }

        public void SetInfo(string pathcompiler, string pathfile)
        {
            this.pathcompiler = "\"" + pathcompiler+"\"";
            this.pathfile =pathfile;
            this.command = this.pathcompiler + " " + this.option + " " + "\"" + this.pathfile + "\"";
        }

        public RunandCompile()
        {
            this.compiler = new Process();
            //compiler.StartInfo.FileName = @"C:\Program Files (x86)\Giaosudauto\Olala Pascal 1.0\FPC\bin\i386-win32\ppc386.exe";
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.CreateNoWindow = true;
            compiler.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            //compiler.StartInfo.Arguments = "-Sg -Si -Mfpc -vw -vi  -d  \"E:\\VisualC#Project\\OlalaPascal\\Tester\\bin\\Debug\\chen_mang.pas\"";
            compiler.StartInfo.UseShellExecute = false;

            runner = new Process();
            //runner.StartInfo.FileName = "cmd.exe";
            //runner.StartInfo.CreateNoWindow = false;
            //runner.StartInfo.UseShellExecute = true;
        }

        

        #region ForListBox...
        public bool Compile(ref ListBox list, bool ShowMessage)
        {
            //ShowCompiling mess= new ShowCompiling();
            
            list.Items.Clear();
            if (!pathCompilerExist)
            {
                list.Items.Add(Translator.Translate("Fatal: Compiler not found!"));
                File.WriteAllText(AppPath.Data + "error-compiler.log", pathcompiler.Substring(1, pathcompiler.Length - 2));
                return false;
            }
            list.Items.Add(Translator.Translate("Compiling \"" + this.pathfile+"\""));
            compiler.StartInfo.FileName = pathcompiler;
            compiler.StartInfo.Arguments = this.option + " " + "\"" + this.pathfile + "\"";
            //compiler.StartInfo.Arguments = "/c " + this.command;
            
            //MessageBox.Show(command);
#if debug
            File.WriteAllText(AppPath.Data + "compile-command.log", command);
            Process.Start(AppPath.Data + "compile-command.log");
#endif
            compiler.Start();
            string lineInfo;
            lineInfo = compiler.StandardOutput.ReadToEnd();
            
            if (lineInfo == "") return false;
            //MessageBox.Show("next...");
            bool success = true;
            string[] result = lineInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            list.Items.Clear();
            foreach (string s in result)
            {
                list.Items.Add(Translator.Translate(s));
                if (s.Contains("Fatal:"))
                {
                    success = false;
                }
            }
            list.SelectedIndex = list.Items.Count - 1;
            list.Select();
#if debug
            //--- Xuat loi ra file
            FileStream fs = new FileStream("error.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach(string xxx in list.Items)
            {
                sw.WriteLine(xxx);
            }

            sw.Close();
            fs.Close();
            Process.Start("error.txt");
            //---
#endif
            return success;
        }
        
        public void Run(ref ListBox list)
        {
            //if (!Compile(ref list,false)) return;
            //runner = new Process();
            list.Items.Add("---------------------------------------------------------------------------------------");
            list.Items.Add(Translator.Translate("Running " + this.fileExe()));
            if(!File.Exists(this.fileExe()))
            {
                list.Items.Add(Translator.Translate("Error: This code has not compiled!"));
                return;
            }
            
            try
            {
                runner.StartInfo.FileName = this.fileExe();
                runner.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.fileExe());
                //runner.StartInfo.Arguments="/c cd "+Path.GetDirectoryName(this.fileExe())+" && "+Path.GetFileName(this.fileExe());
                //MessageBox.Show(this.fileExe());
                runner.EnableRaisingEvents = true;
                Stopwatch watch = new Stopwatch();
                watch.Start();// tính thời gian bắt đầu....
               
                runner.Start();
                //MessageBox.Show("");
                runner.WaitForExit();
                watch.Stop(); // tính thời gian dừng lại...
                
                if (runner.ExitCode != 0)
                {
                    list.Items.Add(Translator.Translate("Process terminated with status")+" " + runner.ExitCode.ToString());
                    list.Items.Add(Translator.Translate("Reason:") + " " + Translator.Translate2(runner.ExitCode.ToString()));
                    list.SelectedIndex = list.Items.Count - 1;
                    AppSound.Play("fail.wav"); 
                    MessageBox.Show(Translator.Translate("The programs exited with exit code: " + runner.ExitCode.ToString()), "Olala Pascal Runner");
                    
                }
                else
                {

                    list.Items.Add(Translator.Translate("Process exited successful with runtime: ") +" "+ watch.Elapsed.TotalSeconds.ToString()+" "+Translator.Translate(" seconds."));
                    AppSound.Play("success.wav");
                    
                }
                list.Items.Add("");
                list.SelectedIndex = list.Items.Count-1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //runner.Dispose();
                return;
            }
            //runner.Dispose();

        }
        #endregion

    }
}
