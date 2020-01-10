using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.BLL
{
    public class ConfigurationHelper
    {
        static object configVar = new object();
        public static bool SaveConfig(Dictionary<string, string> dicConfig)
        {
            try
            {
                lock (configVar)
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    foreach (var item in dicConfig)
                    {
                        if (config.AppSettings.Settings[item.Key] == null)
                        {
                            config.AppSettings.Settings.Add(item.Key, item.Value);
                        }
                        else
                        {
                            config.AppSettings.Settings[item.Key].Value = item.Value;
                        }
                        config.Save();
                    }

                    ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件   
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }
            lock (configVar)
            {
                // Open App.Config of executable   
                Configuration config =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // You need to remove the old settings object before you can replace it   
                if (isModified)
                {
                    config.AppSettings.Settings.Remove(newKey);
                }
                // Add an Application Setting.   
                config.AppSettings.Settings.Add(newKey, newValue);
                // Save the changes in App.config file.   
                config.Save(ConfigurationSaveMode.Modified);
                // Force a reload of a changed section.   
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public static string GetConfig(string key)
        {
            lock (configVar)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] == null)
                    return "";
                return config.AppSettings.Settings[key].Value;
            }

        }
    }
}
