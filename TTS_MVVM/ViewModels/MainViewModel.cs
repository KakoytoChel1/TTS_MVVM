using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using TTS_MVVM.Models;
using System;

namespace TTS_MVVM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        //set pause
        bool flagPause = false;
        bool IsActiveSpeak = false;

        SpeechSynthesizer tts = new SpeechSynthesizer();

        public ObservableCollection<string> availableVoices { get; set; }

        public MainViewModel()
        {
            availableVoices = new ObservableCollection<string>();
            Pr = "Pause";
            VText = "Hello\nYou have to choose voice which can speak your language(text)";

            foreach (InstalledVoice iv in tts.GetInstalledVoices())
            {
                availableVoices.Add(iv.VoiceInfo.Name.ToString());
            }
            SelectedVoice = availableVoices[0];
        }

        private string selectedvoice;
        private string vtext;
        //  Pause / Resume
        private string pr;

        public string Pr
        {
            get { return pr; }
            set { pr = value; OnPropertyChanged("Pr"); }
        }

        public string SelectedVoice
        {
            get { return selectedvoice; }
            set { selectedvoice = value; OnPropertyChanged("SelectedVoice"); }
        }
        public string VText
        {
            get { return vtext; }
            set { vtext = value; OnPropertyChanged("VText"); }
        }

        private DelegateCommand settext;
        public DelegateCommand SetText
        {
            get
            {
                return settext ??
                  (settext = new DelegateCommand(obj =>
                  {
                      OpenFileDialog open = new OpenFileDialog();
                      open.Filter = "Text files (*.txt)|*.txt";
                      if (open.ShowDialog() == true)
                      {
                          VText = File.ReadAllText(open.FileName);
                      }
                  }));
            }
        }
        private DelegateCommand start;
        public DelegateCommand Start
        {
            get
            {
                return start ??
                  (start = new DelegateCommand(obj =>
                  {
                      IsActiveSpeak =  Speech.Speak(VText,SelectedVoice,100,1);
                  }, (obj) => VText.Length != 0));
            }
        }

        private DelegateCommand stop;
        public DelegateCommand Stop
        {
            get
            {
                return stop ??
                  (stop = new DelegateCommand(obj =>
                  {
                      IsActiveSpeak =  Speech.Stop();
                  } , (obj) => IsActiveSpeak == true));
            }
        }

        private DelegateCommand resumeorpause;
        public DelegateCommand ResumeOrPause
        {
            get
            {
                return resumeorpause ??
                  (resumeorpause = new DelegateCommand(obj =>
                  {
                      if(flagPause == false)
                      {
                          Speech.PauseV();
                          flagPause = true;
                          Pr = "Resume";
                      }
                      else
                      {
                          Speech.ResumeV();
                          flagPause = false;
                          Pr = "Pause";
                      }
                  }));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
