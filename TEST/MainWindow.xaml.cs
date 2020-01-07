using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using WindowPlacementHelper;
using static TEST.Properties.Settings;

namespace TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (Default.UpgradeRequired)
            {
                if (Default.FirstRun)
                {
                    // This is not mandatory
                }

                Default.FirstRun = false;
                Default.UpgradeRequired = false;

                Default.Upgrade();

                Default.Save();
            }
            InitializeComponent();

            window.ContentRendered += Window_ContentRendered;

            window.Closing += Window_Closing;

            window.MouseDown += Window_MouseDown;

            window.MouseUp += Window_MouseUp;

            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            WindowPlacement.SetWindowByResolution(window, false);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowPlacement.SetWindowByResolution(window, true);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            WindowPlacement.SetWindowByResolution(window, false);
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowPlacement.SetWindowByResolution(window, true);
        }
    }
}
