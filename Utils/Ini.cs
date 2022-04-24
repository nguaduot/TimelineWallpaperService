using System.Collections.Generic;

namespace TimelineWallpaperService.Utils {
    public sealed class Ini {
        private readonly HashSet<string> PROVIDER = new HashSet<string>() {
            BingIni.GetId(), NasaIni.GetId(), OneplusIni.GetId(), TimelineIni.GetId(), Himawari8Ini.GetId(),
            YmyouliIni.GetId(), InfinityIni.GetId(), OneIni.GetId(), QingbzIni.GetId(), ObzhiIni.GetId(),
            G3Ini.GetId(), BoboIni.GetId(), AbyssIni.GetId(), DaihanIni.GetId(), DmoeIni.GetId(),
            ToubiecIni.GetId(), MtyIni.GetId(), SeovxIni.GetId()
        };
        private readonly HashSet<string> THEME = new HashSet<string>() { "", "light", "dark" };

        private string provider = BingIni.GetId();
        public string Provider {
            set => provider = PROVIDER.Contains(value) ? value : BingIni.GetId();
            get => provider;
        }

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

        public OneIni One { set; get; } = new OneIni();

        public G3Ini G3 { set; get; } = new G3Ini();

        public QingbzIni Qingbz { set; get; } = new QingbzIni();

        public ObzhiIni Obzhi { set; get; } = new ObzhiIni();

        public BoboIni Bobo { set; get; } = new BoboIni();

        public AbyssIni Abyss { set; get; } = new AbyssIni();

        public DaihanIni Daihan { set; get; } = new DaihanIni();

        public DmoeIni Dmoe { set; get; } = new DmoeIni();

        public ToubiecIni Toubiec { set; get; } = new ToubiecIni();

        public MtyIni Mty { set; get; } = new MtyIni();

        public SeovxIni Seovx { set; get; } = new SeovxIni();

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
            } else if (OneIni.GetId().Equals(provider)) {
                return One.DesktopPeriod;
            } else if (QingbzIni.GetId().Equals(provider)) {
                return Qingbz.DesktopPeriod;
            } else if (ObzhiIni.GetId().Equals(provider)) {
                return Obzhi.DesktopPeriod;
            } else if (G3Ini.GetId().Equals(provider)) {
                return G3.DesktopPeriod;
            } else if (BoboIni.GetId().Equals(provider)) {
                return Bobo.DesktopPeriod;
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
            } else if (OneIni.GetId().Equals(provider)) {
                return One.LockPeriod;
            } else if (QingbzIni.GetId().Equals(provider)) {
                return Qingbz.LockPeriod;
            } else if (ObzhiIni.GetId().Equals(provider)) {
                return Obzhi.LockPeriod;
            } else if (G3Ini.GetId().Equals(provider)) {
                return G3.LockPeriod;
            } else if (BoboIni.GetId().Equals(provider)) {
                return Bobo.LockPeriod;
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
            } else if (OneIni.GetId().Equals(provider)) {
                paras = One.ToString();
            } else if (QingbzIni.GetId().Equals(provider)) {
                paras = Qingbz.ToString();
            } else if (ObzhiIni.GetId().Equals(provider)) {
                paras = Obzhi.ToString();
            } else if (Himawari8Ini.GetId().Equals(provider)) {
                paras = Himawari8.ToString();
            } else if (G3Ini.GetId().Equals(provider)) {
                paras = G3.ToString();
            } else if (BoboIni.GetId().Equals(provider)) {
                paras = Bobo.ToString();
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
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "score", "random" };
        private readonly HashSet<string> CATE = new HashSet<string>() { "", "landscape", "portrait", "culture", "term" };

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
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "score", "random" };
        private readonly List<string> CATE = new List<string>() { "", "acgcharacter", "acgscene", "sky",
            "war", "sword", "artistry", "car", "portrait", "animal", "delicacy", "nature" };

        private string order = "random";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "random";
            get => order;
        }

        private string cate = "";
        public string Cate {
            set => cate = CATE.Contains(value) ? value : "";
            get => cate;
        }

        public int R18 { set; get; } = 0;

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

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}&cate={Cate}&r18={R18}";

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

    public sealed class OneIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "random" };

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

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}";

        public static string GetId() => "one";
    }

    public sealed class QingbzIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "score", "random" };
        private readonly List<string> CATE = new List<string>() { "", "portrait", "acg", "nature",
            "star", "color", "car", "game", "animal" };

        private string order = "random";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "random";
            get => order;
        }

        private string cate = "";
        public string Cate {
            set => cate = CATE.Contains(value) ? value : "";
            get => cate;
        }

        public int R18 { set; get; } = 0;

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

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}&cate={Cate}&r18={R18}";

        public static string GetId() => "qingbz";
    }

    public sealed class ObzhiIni {
        private readonly HashSet<string> ORDER = new HashSet<string>() { "date", "score", "random" };
        private readonly List<string> CATE = new List<string>() { "", "acg", "specific", "concise",
            "nature", "portrait", "game", "animal" };

        private string order = "random";
        public string Order {
            set => order = ORDER.Contains(value) ? value : "random";
            get => order;
        }

        private string cate = "";
        public string Cate {
            set => cate = CATE.Contains(value) ? value : "";
            get => cate;
        }

        public int R18 { set; get; } = 0;

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

        override public string ToString() => $"desktopperiod={DesktopPeriod}&lockperiod={LockPeriod}&order={Order}&cate={Cate}&r18={R18}";

        public static string GetId() => "obzhi";
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
}
