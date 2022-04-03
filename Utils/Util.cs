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
        private const string FILE_INI = "timelinewallpaper-4.0.ini";

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
            //_ = GetPrivateProfileString("timelinewallpaper", "push", "", sb, 1024, iniFile);
            //ini.Push = sb.ToString();
            //_ = GetPrivateProfileString("timelinewallpaper", "pushprovider", "bing", sb, 1024, iniFile);
            //ini.PushProvider = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "desktopprovider", "", sb, 1024, iniFile);
            ini.DesktopProvider = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "lockprovider", "", sb, 1024, iniFile);
            ini.LockProvider = sb.ToString();
            _ = GetPrivateProfileString("timelinewallpaper", "theme", "", sb, 1024, iniFile);
            ini.Theme = sb.ToString();
            _ = GetPrivateProfileString("bing", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out int period);
            ini.Bing.DesktopPeriod = period;
            _ = GetPrivateProfileString("bing", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Bing.LockPeriod = period;
            _ = GetPrivateProfileString("bing", "lang", "", sb, 1024, iniFile);
            ini.Bing.Lang = sb.ToString();
            _ = GetPrivateProfileString("nasa", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Nasa.DesktopPeriod = period;
            _ = GetPrivateProfileString("nasa", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Nasa.LockPeriod = period;
            _ = GetPrivateProfileString("nasa", "mirror", "", sb, 1024, iniFile);
            ini.Nasa.Mirror = sb.ToString();
            _ = GetPrivateProfileString("oneplus", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Oneplus.DesktopPeriod = period;
            _ = GetPrivateProfileString("oneplus", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Oneplus.LockPeriod = period;
            _ = GetPrivateProfileString("oneplus", "order", "date", sb, 1024, iniFile);
            ini.Oneplus.Order = sb.ToString();
            _ = GetPrivateProfileString("timeline", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Timeline.DesktopPeriod = period;
            _ = GetPrivateProfileString("timeline", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Timeline.LockPeriod = period;
            _ = GetPrivateProfileString("timeline", "order", "date", sb, 1024, iniFile);
            ini.Timeline.Order = sb.ToString();
            _ = GetPrivateProfileString("timeline", "cate", "", sb, 1024, iniFile);
            ini.Timeline.Cate = sb.ToString();
            _ = GetPrivateProfileString("timeline", "authorize", "1", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out int authorize);
            ini.Timeline.Authorize = authorize;
            _ = GetPrivateProfileString("himawari8", "desktopperiod", "1", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Himawari8.DesktopPeriod = period;
            _ = GetPrivateProfileString("himawari8", "lockperiod", "2", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Himawari8.LockPeriod = period;
            _ = GetPrivateProfileString("himawari8", "offset", "0", sb, 1024, iniFile);
            _ = float.TryParse(sb.ToString(), out float offset);
            ini.Himawari8.Offset = offset;
            _ = GetPrivateProfileString("ymyouli", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Ymyouli.DesktopPeriod = period;
            _ = GetPrivateProfileString("ymyouli", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Ymyouli.LockPeriod = period;
            _ = GetPrivateProfileString("ymyouli", "order", "", sb, 1024, iniFile);
            ini.Ymyouli.Order = sb.ToString();
            _ = GetPrivateProfileString("ymyouli", "cate", "", sb, 1024, iniFile);
            ini.Ymyouli.Cate = sb.ToString();
            _ = GetPrivateProfileString("ymyouli", "qc", "1", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out int qc);
            ini.Ymyouli.Qc = qc;
            _ = GetPrivateProfileString("infinity", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Infinity.DesktopPeriod = period;
            _ = GetPrivateProfileString("infinity", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Infinity.LockPeriod = period;
            _ = GetPrivateProfileString("one", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.One.DesktopPeriod = period;
            _ = GetPrivateProfileString("one", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.One.LockPeriod = period;
            _ = GetPrivateProfileString("one", "order", "date", sb, 1024, iniFile);
            ini.One.Order = sb.ToString();
            _ = GetPrivateProfileString("infinity", "order", "", sb, 1024, iniFile);
            ini.Infinity.Order = sb.ToString();
            _ = GetPrivateProfileString("3g", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.G3.DesktopPeriod = period;
            _ = GetPrivateProfileString("3g", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.G3.LockPeriod = period;
            _ = GetPrivateProfileString("3g", "order", "date", sb, 1024, iniFile);
            ini.G3.Order = sb.ToString();
            _ = GetPrivateProfileString("bobo", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Bobo.DesktopPeriod = period;
            _ = GetPrivateProfileString("bobo", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Bobo.LockPeriod = period;
            _ = GetPrivateProfileString("lofter", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Lofter.DesktopPeriod = period;
            _ = GetPrivateProfileString("lofter", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Lofter.LockPeriod = period;
            _ = GetPrivateProfileString("abyss", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Abyss.DesktopPeriod = period;
            _ = GetPrivateProfileString("abyss", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Abyss.LockPeriod = period;
            _ = GetPrivateProfileString("daihan", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Daihan.DesktopPeriod = period;
            _ = GetPrivateProfileString("daihan", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Daihan.LockPeriod = period;
            _ = GetPrivateProfileString("dmoe", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Dmoe.DesktopPeriod = period;
            _ = GetPrivateProfileString("dmoe", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Dmoe.LockPeriod = period;
            _ = GetPrivateProfileString("toubiec", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Toubiec.DesktopPeriod = period;
            _ = GetPrivateProfileString("toubiec", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Toubiec.LockPeriod = period;
            _ = GetPrivateProfileString("seovx", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Seovx.DesktopPeriod = period;
            _ = GetPrivateProfileString("seovx", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Seovx.LockPeriod = period;
            _ = GetPrivateProfileString("seovx", "cate", "d", sb, 1024, iniFile);
            ini.Seovx.Cate = sb.ToString();
            _ = GetPrivateProfileString("paul", "desktopperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Paul.DesktopPeriod = period;
            _ = GetPrivateProfileString("paul", "lockperiod", "24", sb, 1024, iniFile);
            _ = int.TryParse(sb.ToString(), out period);
            ini.Paul.LockPeriod = period;
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
