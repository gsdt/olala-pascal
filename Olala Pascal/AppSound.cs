using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlalaPascal
{
    class AppSound
    {
        public static void Play(string type)
        {
            try
            {
                using (System.Media.SoundPlayer simpleSound = new System.Media.SoundPlayer("Sound\\" + type))
                {
                    if (Properties.Settings.Default.Sound) simpleSound.Play();
                }
            }
            catch
            {
                System.IO.File.WriteAllText(AppPath.Data + "sound-error.log", "file not found: "+"Sound\\" + type);
            }
        }
    }
}
