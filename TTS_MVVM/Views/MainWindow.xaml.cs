using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using TTS_MVVM.ViewModels;

namespace TTS_MVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            bClose.Click += (s, e) => { this.Close(); };

            bMinimize.Click += (s, e) => { this.WindowState = WindowState.Minimized; };

            label.MouseDown += (s, e) => { if (e.ChangedButton == MouseButton.Left) { this.DragMove(); } };
        }
    }
}
