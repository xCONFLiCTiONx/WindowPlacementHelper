using System;
using System.Management;
using System.Windows;
using static WindowPlacementHelper.Properties.Settings;

namespace WindowPlacementHelper
{
    /// <summary>
    /// SetWindowByResolution is the entry point for this class
    /// </summary>
    public class WindowPlacement
    {
        /// <summary>
        /// window: the window you want to show at startup. You need to name it first
        /// SaveOnly: Saves the window position per user only
        /// windowOpacity: the opacity you want the window after location has been set
        /// </summary>
        public static void SetWindowByResolution(Window window, bool SaveOnly, double windowOpacity = 1)
        {
            try
            {
                if (Default.UpgradeRequired)
                {
                    Default.Upgrade();

                    Default.UpgradeRequired = false;

                    Default.Save();

                    Default.Reload();
                }

                var scope = new ManagementScope();
                var query = new ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    var results = searcher.Get();

                    foreach (var result in results)
                    {
                        string CapableHorizontalResolution = result["HorizontalResolution"].ToString();

                        bool ItemExits = false;

                        foreach (string item in Default.CapableResolutionSets)
                        {
                            if (item.Contains(CapableHorizontalResolution + ":Left:"))
                            {
                                ItemExits = true;
                            }
                        }
                        if (!ItemExits)
                        {
                            Default.CapableResolutionSets.Add(CapableHorizontalResolution + ":Left:" + ";Top:");
                        }
                    }

                    Default.Save();

                    Default.Reload();
                }

                if (SaveOnly)
                {
                    foreach (string item in Default.CapableResolutionSets)
                    {
                        if (item.Contains(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:"))
                        {
                            Default.CapableResolutionSets.Remove(item);

                            Default.CapableResolutionSets.Add(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:" + window.Left + ";Top:" + window.Top);

                            break;
                        }
                    }
                }
                else
                {
                    foreach (string item in Default.CapableResolutionSets)
                    {
                        if (item.Contains(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:"))
                        {
                            string TempString = item.Replace(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:", "");

                            // Window.Left
                            string WindowLeft = TempString.Replace(TempString.Substring(TempString.IndexOf(";")), "");

                            if (WindowLeft == "")
                            {
                                break;
                            }

                            window.Left = Convert.ToDouble(WindowLeft);

                            // Window.Top
                            string WindowTop = TempString.Substring(TempString.LastIndexOf(";")).Replace(";Top:", "");

                            if (WindowTop == "")
                            {
                                break;
                            }

                            window.Top = Convert.ToDouble(WindowTop);
                        }
                    }

                    foreach (string item in Default.CapableResolutionSets)
                    {
                        if (item.Contains(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:"))
                        {
                            Default.CapableResolutionSets.Remove(item);

                            Default.CapableResolutionSets.Add(SystemParameters.PrimaryScreenWidth.ToString() + ":Left:" + window.Left + ";Top:" + window.Top);

                            break;
                        }
                    }

                    window.Opacity = windowOpacity;
                }

                Default.Save();
            }
            catch
            {
                // All ERRORS SHOULD BE CAUGHT IN THE APP ITSELF
            }
        }
    }
}
