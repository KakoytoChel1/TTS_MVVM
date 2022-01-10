using System;
using System.Speech.Synthesis;

namespace TTS_MVVM.Models
{
    public static class Speech
    {
        public static SpeechSynthesizer tts = new SpeechSynthesizer();

        public static bool Speak(string text, string selectedvoice, int volume, int rate)
        {
            try
            {
                tts.SelectVoice(selectedvoice);
                tts.Rate = rate;
                tts.Volume = volume;
                tts.SpeakAsync(text);
            }
            catch(Exception ex) { System.Windows.MessageBox.Show(ex.ToString()); }
            return true;
        }
        public static void PauseV()
        {
            tts.Pause();
        }
        public static bool Stop()
        {
            tts.SpeakAsyncCancelAll();
            return false;
        }
        public static void ResumeV()
        {
            tts.Resume();
        }
    }
}
