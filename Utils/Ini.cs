using System.Collections.Generic;

namespace TimelineWallpaperService.Utils {
    public sealed class Ini {
        private readonly HashSet<string> PROVIDER = new HashSet<string>() {
            BingIni.GetId(), NasaIni.GetId(), OneplusIni.GetId(), TimelineIni.GetId(), Himawari8Ini.GetId(),
            YmyouliIni.GetId(), InfinityIni.GetId(), G3Ini.GetId(), BoboIni.GetId(), LofterIni.GetId(),
            AbyssIni.GetId(), DaihanIni.GetId(), DmoeIni.GetId(), ToubiecIni.GetId(), MtyIni.GetId(),
            SeovxIni.GetId(), PaulIni.GetId()
        };
        private readonly HashSet<string> PUSH = new HashSet<string>() { "", "desktop", "lock" };
        private readonly HashSet<string> THEME = new HashSet<string>() { "", "light", "dark" };

        private string provider = BingIni.GetId();
        public string Provider {
            set => provider = PROVIDER.Contains(value) ? value : BingIni.GetId();
            get => provider;
        }

        //private string push = "";
        //public string Push {
        //    set => push = PUSH.Contains(value) ? value : "";
        //    get => push;
        //}

        //private string pushProvider = BingIni.GetId();
        //public string PushProvider {
        //    set => pushProvider = PROVIDER.Contains(value) ? value : BingIni.GetId();
        //    get => pushProvider;
        //}

        public string DesktopProvider { set; get; }

        public string LockProvider { set; get; }

        private string theme = "";
        public string Theme {
            set => theme = THEME.Contains(value) ? value : "";
            get => theme;
        }

        public BingIni Bing { set; get; } = new BingIni();

        public NasaIni Nasa { set; get; } = new NasaIni();

        public OneplusIni Oneplus { set; get; } = new OneplusIni();

        public TimelineIni Timeline { set; get; } = new TimelineIni();

        public Himawari8Ini Himawari8 { set; get; } = new Himawari8Ini();

        public YmyouliIni Ymyouli { set; get; } = new YmyouliIni();

        public InfinityIni Infinity { set; get; } = new InfinityIni();

        public G3Ini G3 { set; get; } = new G3Ini();

        public BoboIni Bobo { set; get; } = new BoboIni();

        public LofterIni Lofter { set; get; } = new LofterIni();

        public AbyssIni Abyss { set; get; } = new AbyssIni();

        public DaihanIni Daihan { set; get; } = new DaihanIni();

        public DmoeIni Dmoe { set; get; } = new DmoeIni();

        public ToubiecIni Toubiec { set; get; } = new ToubiecIni();

        public MtyIni Mty { set; get; } = new MtyIni();

        public SeovxIni Seovx { set; get; } = new SeovxIni();

        public PaulIni Paul { set; get; } = new PaulIni();

        public int GetDesktopPeriod(string provider) {
            if (NasaIni.GetId().Equals(provider)) {
                return Nasa.DesktopPeriod;
            } else if (OneplusIni.GetId().Equals(provider)) {
                return Oneplus.DesktopPeriod;
            } else if (TimelineIni.GetId().Equals(provider)) {
                return Timeline.DesktopPeriod;
            } else if (Himawari8Ini.GetId().Equals(provider)) {
                return Himawari8.DesktopPeriod;
            } else if (YmyouliIni.GetId().Equals(provider)) {
                return Ymyouli.DesktopPeriod;
            } else if (InfinityIni.GetId().Equals(provider)) {
                return Infinity.DesktopPeriod;
            } else if (G3Ini.GetId().Equals(provider)) {
                return G3.DesktopPeriod;
            } else if (BoboIni.GetId().Equals(provider)) {
                return Bobo.DesktopPeriod;
            } else if (LofterIni.GetId().Equals(provider)) {
                return Lofter.DesktopPeriod;
            } else if (AbyssIni.GetId().Equals(provider)) {
                return Abyss.DesktopPeriod;
            } else if (DaihanIni.GetId().Equals(provider)) {
                return Daihan.DesktopPeriod;
            } else if (DmoeIni.GetId().Equals(provider)) {
                return Dmoe.DesktopPeriod;
            } else if (ToubiecIni.GetId().Equals(provider)) {
                return Toubiec.DesktopPeriod;
            } else if (MtyIni.GetId().Equals(provider)) {
                return Mty.DesktopPeriod;
            } else if (SeovxIni.GetId().Equals(provider)) {
                return Seovx.DesktopPeriod;
            } else if (PaulIni.GetId().Equals(provider)) {
                return Paul.DesktopPeriod;
            } else if (BingIni.GetId().Equals(provider)) {
                return Bing.DesktopPeriod;
            } else {
                return Bing.DesktopPeriod;
            }
        }

        public int GetLockPeriod(string provider) {
            if (NasaIni.GetId().Equals(provider)) {
                return Nasa.LockPeriod;
            } else if (OneplusIni.GetId().Equals(provider)) {
                return Oneplus.LockPeriod;
            } else if (TimelineIni.GetId().Equals(provider)) {
                return Timeline.LockPeriod;
            } else if (Himawari8Ini.GetId().Equals(provider)) {
                return Himawari8.LockPeriod;
            } else if (YmyouliIni.GetId().Equals(provider)) {
                return Ymyouli.LockPeriod;
            } else if (InfinityIni.GetId().Equals(provider)) {
                return Infinity.LockPeriod;
            } else if (G3Ini.GetId().Equals(provider)) {
                return G3.LockPeriod;
            } else if (BoboIni.GetId().Equals(provider)) {
                return Bobo.LockPeriod;
            } else if (LofterIni.GetId().Equals(provider)) {
                return Lofter.LockPeriod;
            } else if (AbyssIni.GetId().Equals(provider)) {
                return Abyss.LockPeriod;
            } else if (DaihanIni.GetId().Equals(provider)) {
                return Daihan.LockPeriod;
            } else if (DmoeIni.GetId().Equals(provider)) {
                return Dmoe.LockPeriod;
            } else if (ToubiecIni.GetId().Equals(provider)) {
                return Toubiec.LockPeriod;
            } else if (MtyIni.GetId().Equals(provider)) {
                return Mty.LockPeriod;
            } else if (SeovxIni.GetId().Equals(provider)) {
                return Seovx.LockPeriod;
            } else if (PaulIni.GetId().Equals(provider)) {
                return Paul.LockPeriod;
            } else if (BingIni.GetId().Equals(provider)) {
                return Bing.LockPeriod;
            } else {
                return Bing.LockPeriod;
            }
        }

        override public string ToString() {
            string paras;
            if (NasaIni.GetId().Equals(provider)) {
                paras = Nasa.ToString();
            } else if (OneplusIni.GetId().Equals(provider)) {
                paras = Oneplus.ToString();
            } else if (TimelineIni.GetId().Equals(provider)) {
                paras = Timeline.ToString();
            } else if (YmyouliIni.GetId().Equals(provider)) {
                paras = Ymyouli.ToString();
            } else if (InfinityIni.GetId().Equals(provider)) {
                paras = Infinity.ToString();
            } else if (Himawari8Ini.GetId().Equals(provider)) {
                paras = Himawari8.ToString();
            } else if (G3Ini.GetId().Equals(provider)) {
                paras = G3.ToString();
            } else if (BoboIni.GetId().Equals(provider)) {
                paras = Bobo.ToString();
            } else if (LofterIni.GetId().Equals(provider)) {
                paras = Lofter.ToString();
            } else if (AbyssIni.GetId().Equals(provider)) {
                paras = Abyss.ToString();
            } else if (DaihanIni.GetId().Equals(provider)) {
                paras = Daihan.ToString();
            } else if (DmoeIni.GetId().Equals(provider)) {
                paras = Dmoe.ToString();
            } else if (ToubiecIni.GetId().Equals(provider)) {
                paras = Toubiec.ToString();
            } else if (MtyIni.GetId().Equals(provider)) {
                paras = Mty.ToString();
            } else if (SeovxIni.GetId().Equals(provider)) {
                paras = Seovx.ToString();
            } else if (PaulIni.GetId().Equals(provider)) {
                paras = Paul.ToString();
            } else if (BingIni.GetId().Equals(provider)) {
                paras = Bing.ToString();
            } else {
                paras = Bing.ToString();
            }
            return $"/{Provider}?desktopprovider={DesktopProvider}&lockprovider={LockProvider}" + (paras.Length > 0 ? "&" : "") + paras;
        }
    }

    public sealed class BingIni {
        private readonly HashSet<string> LANG = new HashSet<string>() { "", "zh-cn", "en-us", "ja-jp", "de-de", "fr-fr" };

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        private string lang = "";
        public string Lang {
            set => lang = LANG.Contains(value) ? value : "";
            get => lang;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&lang={Lang}";

        public static string GetId() => "bing";
    }

    public sealed class NasaIni {
        private readonly HashSet<string> MIRROR = new HashSet<string>() { "", "bjp" };

        private string mirror = "";
        public string Mirror {
            set => mirror = MIRROR.Contains(value) ? value : "";
            get => mirror;
        }

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&mirror={Mirror}";

        public static string GetId() => "nasa";
    }

    public sealed class OneplusIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "rate", "view" };

        private string order = "date";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "date";
            get => order;
        }

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}";

        public static string GetId() => "oneplus";
    }

    public sealed class TimelineIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "random" };
        private readonly HashSet<string> CATE = new HashSet<string>() { "", "landscape", "portrait", "culture" };

        private string order = "date";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "date";
            get => order;
        }

        private string cate = "";
        public string Cate {
            set => cate = CATE.Contains(value) ? value : "";
            get => cate;
        }

        public int Authorize { set; get; } = 1;

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}&cate={Cate}&authorize={Authorize}";

        public static string GetId() => "timeline";
    }

    public sealed class Himawari8Ini {
        private float offset = 0;
        public float Offset {
            set => offset = value < -1 ? -1 : (value > 1 ? 1 : value);
            get => offset;
        }

        private int desktopPeriod = 1;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 2;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "himawari8";
    }

    public sealed class YmyouliIni {
        private static readonly IDictionary<string, IDictionary<string, string>> COL_MODULE_DIC = new Dictionary<string, IDictionary<string, string>> {
            { "182", new Dictionary<string, string> {
                { "577", "126" }, // 第一栏
                { "606", "126" },
                { "607", "126" },
                { "611", "126" },
                { "681", "126" }, // 第五栏
                { "575", "182" }, // 第一栏
                { "610", "182" },
                { "695", "182" },
                { "743", "182" },
                { "744", "182" },
                { "768", "182" },
                { "776", "182" },
                { "786", "182" },
                { "787", "182" },
                { "792", "182" },
                { "833", "182" },
                { "842", "182" },
                { "834", "182" },
                { "844", "182" } // 第十四栏
            } }, // 游戏动漫人物（4K+8K）
            { "183", new Dictionary<string, string> {
                { "677", "127" },
                { "673", "183" },
                { "777", "183" }
            } }, // 游戏动漫场景（4K+8K）
            { "184", new Dictionary<string, string> {
                { "678", "134" },
                { "675", "184" },
                { "791", "184" }
            } }, // 自然风景（4K+8K）
            { "185", new Dictionary<string, string> {
                { "578", "185" },
                { "679", "185" },
                { "680", "185" },
                { "754", "185" }
            } }, // 花草植物
            { "186", new Dictionary<string, string> {
                { "753", "186" }
            } }, // 美女女孩
            { "187", new Dictionary<string, string> {
                { "670", "187" },
                { "741", "187" },
                { "790", "187" }
            } }, // 机车
            { "214", new Dictionary<string, string> {
                { "690", "214" },
                { "691", "214" }
            } }, // 科幻
            { "215", new Dictionary<string, string> {
                { "693", "215" },
                { "694", "215" },
                { "742", "215" },
                { "836", "215" }
            } }, // 意境
            { "224", new Dictionary<string, string> {
                { "746", "224" }
            } }, // 武器刀剑
            { "225", new Dictionary<string, string> {
                { "748", "225" }
            } }, // 动物
            { "226", new Dictionary<string, string> {
                { "682", "128" },
                { "751", "226" }
            } }, // 古风人物（4K+8K）
            { "227", new Dictionary<string, string> {
                { "756", "227" },
                { "773", "227" }
            } }, // 日暮云天
            { "228", new Dictionary<string, string> {
                { "758", "228" }
            } }, // 夜空星河
            { "229", new Dictionary<string, string> {
                { "760", "229" },
                { "761", "229" },
                { "762", "229" },
                { "843", "229" }
            } }, // 战场战争
            { "230", new Dictionary<string, string> {
                { "763", "230" }
            } }, // 冰雪之境
            { "231", new Dictionary<string, string> {
                { "766", "231" }
            } }, // 油画
            { "232", new Dictionary<string, string> {
                { "775", "232" }
            } }, // 国漫壁纸
            { "233", new Dictionary<string, string> {
                { "778", "233" }
            } }, // 美食蔬果
            { "241", new Dictionary<string, string> {
                { "830", "241" }
            } } // 樱落
        }; // { col: { module: col } }

        private string col = "";
        public string Col {
            set => col = COL_MODULE_DIC.ContainsKey(value) ? value : "";
            get => col;
        }

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&col={Col}";

        public static IDictionary<string, IDictionary<string, string>> GetDic() {
            return COL_MODULE_DIC;
        }

        public static string GetId() => "ymyouli";
    }

    public sealed class InfinityIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "", "rate" };

        private string order = "";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "";
            get => order;
        }

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}";

        public static string GetId() => "infinity";
    }

    public sealed class G3Ini {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "view" };

        private string order = "date";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "date";
            get => order;
        }

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}";

        public static string GetId() => "3g";
    }

    public sealed class BoboIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "bobo";
    }

    public sealed class LofterIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "lofter";
    }

    public sealed class AbyssIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "abyss";
    }

    public sealed class DaihanIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "daihan";
    }

    public sealed class DmoeIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "dmoe";
    }

    public sealed class ToubiecIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "toubiec";
    }

    public sealed class MtyIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "mty";
    }

    public sealed class SeovxIni {
        private readonly HashSet<string> CATE = new HashSet<string>() { "", "d", "ha" };

        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        private string cate = "d";
        public string Cate {
            set => cate = CATE.Contains(value) ? value : "d";
            get => cate;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&cate={Cate}";

        public static string GetId() => "seovx";
    }

    public sealed class PaulIni {
        private int desktopPeriod = 24;
        public int DesktopPeriod {
            set => desktopPeriod = value <= 0 || value > 24 ? 24 : value;
            get => desktopPeriod;
        }

        private int lockPeriod = 24;
        public int LockPeriod {
            set => lockPeriod = value <= 0 || value > 24 ? 24 : value;
            get => lockPeriod;
        }

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "paul";
    }
}
