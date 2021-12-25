using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace TimelineWallpaperService.Utils {
    public sealed class IniUtil {
        // TODO: 参数有变动时需调整配置名
        private const string FILE_INI = "timelinewallpaper-2.8.ini";

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defValue,
            StringBuilder returnedString, int size, string filePath);

        private static string GetIniFile() {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            string iniFile = Path.Combine(folder.Path, FILE_INI);
            return File.Exists(iniFile) ? iniFile : null;
        }

        public static Ini GetIni() {
            string iniFile = GetIniFile();
            Debug.WriteLine("ini: " + FILE_INI);
            Ini ini = new Ini();
            if (iniFile == null) { // 尚未初始化
                return ini;
            }
            StringBuilder sb = new StringBuilder(1024);
            _ = GetPrivateProfileString("timelinewallpaper", "provider", "bing", sb, 1024, iniFile);
            ini.Provider = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "push", "", sb, 1024, iniFile);
            ini.Push = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "pushprovider", "bing", sb, 1024, iniFile);
            ini.PushProvider = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "theme", "", sb, 1024, iniFile);
            ini.Theme = sb.ToString();
            _ = GetPrivateProfileString("bing", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out int period);
            ini.Bing.PushPeriod = period;
            _ = GetPrivateProfileString("bing", "lang", "", sb, 1024, iniFile);
            ini.Bing.Lang = sb.ToString();
            _ = GetPrivateProfileString("nasa", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Nasa.PushPeriod = period;
            _ = GetPrivateProfileString("nasa", "mirror", "", sb, 1024, iniFile);
            ini.Nasa.Mirror = sb.ToString();
            _ = GetPrivateProfileString("oneplus", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Oneplus.PushPeriod = period;
            _ = GetPrivateProfileString("oneplus", "order", "date", sb, 1024, iniFile);
            ini.Oneplus.Order = sb.ToString();
            _ = GetPrivateProfileString("timeline", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Timeline.PushPeriod = period;
            _ = GetPrivateProfileString("timeline", "order", "date", sb, 1024, iniFile);
            ini.Timeline.Order = sb.ToString();
            _ = GetPrivateProfileString("timeline", "cate", "", sb, 1024, iniFile);
            ini.Timeline.Cate = sb.ToString();
            _ = GetPrivateProfileString("himawari8", "pushperiod", "1", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Himawari8.PushPeriod = period;
            _ = GetPrivateProfileString("himawari8", "offset", "0", sb, 1024, iniFile);
            _ = float.TryParse(sb.ToString(), out float offset);
            ini.Himawari8.Offset = offset;
            _ = GetPrivateProfileString("ymyouli", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Ymyouli.PushPeriod = period;
            _ = GetPrivateProfileString("ymyouli", "col", "", sb, 1024, iniFile);
            ini.Ymyouli.Col = sb.ToString();
            _ = GetPrivateProfileString("infinity", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Infinity.PushPeriod = period;
            _ = GetPrivateProfileString("infinity", "order", "", sb, 1024, iniFile);
            ini.Infinity.Order = sb.ToString();
            _ = GetPrivateProfileString("3g", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.G3.PushPeriod = period;
            _ = GetPrivateProfileString("3g", "order", "date", sb, 1024, iniFile);
            ini.G3.Order = sb.ToString();
            _ = GetPrivateProfileString("pixivel", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Pixivel.PushPeriod = period;
            _ = GetPrivateProfileString("pixivel", "sanity", "5", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out int sanity);
            ini.Pixivel.Sanity = sanity;
            _ = GetPrivateProfileString("lofter", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Lofter.PushPeriod = period;
            _ = GetPrivateProfileString("daihan", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Daihan.PushPeriod = period;
            _ = GetPrivateProfileString("dmoe", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Dmoe.PushPeriod = period;
            _ = GetPrivateProfileString("toubiec", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Toubiec.PushPeriod = period;
            _ = GetPrivateProfileString("seovx", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Seovx.PushPeriod = period;
            _ = GetPrivateProfileString("seovx", "cate", "d", sb, 1024, iniFile);
            ini.Seovx.Cate = sb.ToString();
            _ = GetPrivateProfileString("paul", "pushperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Paul.PushPeriod = period;
            return ini;
        }
    }

    public sealed class VerUtil {
        public static string GetPkgVer(bool forShort) {
            if (forShort) {
                return string.Format("{0}.{1}",
                    Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor);
            }
            return string.Format("{0}.{1}.{2}.{3}",
                Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor,
                Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
        }

        public static string GetOsVer() {
            ulong version = ulong.Parse(AnalyticsInfo.VersionInfo.DeviceFamilyVersion);
            ulong major = (version & 0xFFFF000000000000L) >> 48;
            ulong minor = (version & 0x0000FFFF00000000L) >> 32;
            ulong build = (version & 0x00000000FFFF0000L) >> 16;
            ulong revision = (version & 0x000000000000FFFFL);
            return $"{major}.{minor}.{build}.{revision}";
        }

        public static string GetDevice() {
            var deviceInfo = new EasClientDeviceInformation();
            if (deviceInfo.SystemSku.Length > 0) {
                return deviceInfo.SystemSku;
            }
            return string.Format("{0}_{1}", deviceInfo.SystemManufacturer,
                deviceInfo.SystemProductName);
        }

        public static string GetDeviceId() {
            SystemIdentificationInfo systemId = SystemIdentification.GetSystemIdForPublisher();
            // Make sure this device can generate the IDs
            if (systemId.Source != SystemIdentificationSource.None) {
                // The Id property has a buffer with the unique ID
                DataReader dataReader = DataReader.FromBuffer(systemId.Id);
                return dataReader.ReadGuid().ToString();
            }
            return "";
        }
    }
}
