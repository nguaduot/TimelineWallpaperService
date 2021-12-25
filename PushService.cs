using Microsoft.Graphics.Canvas;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimelineWallpaperService.Beans;
using TimelineWallpaperService.Services;
using TimelineWallpaperService.Utils;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI;

namespace TimelineWallpaperService {
    public sealed class PushService : IBackgroundTask {
        private ApplicationDataContainer localSettings;
        private Ini ini;
        private int pushPeriod = 24;
        private string pushTag; // 免重复推送标记
        private bool pushNow = false; // 立即运行一次

        public async void Run(IBackgroundTaskInstance taskInstance) {
            Init(taskInstance);
            Log.Information("PushService.Run() trigger: " + taskInstance.TriggerDetails);
            Log.Information("PushService.Run() pushProvider: " + ini.PushProvider);
            Log.Information("PushService.Run() push: " + ini.Push);
            Log.Information("PushService.Run() pushPeriod: " + pushPeriod);

            if (!CheckNecessary()) {
                Log.Information("PushService.Run() skip pushTag: " + pushTag);
                return;
            }
            // 检查网络连接
            if (!NetworkInterface.GetIsNetworkAvailable()) {
                Log.Warning("PushService.Run() network not available");
                return;
            }

            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try {
                bool done = false;
                if (NasaIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadNasa();
                } else if (OneplusIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadOneplus();
                } else if (TimelineIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadTimeline();
                } else if (YmyouliIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadYmyouli();
                } else if (InfinityIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadInfinity();
                } else if (Himawari8Ini.GetId().Equals(ini.PushProvider)) {
                    done = await LoadHimawari8();
                } else if (G3Ini.GetId().Equals(ini.PushProvider)) {
                    done = await Load3G();
                } else if (PixivelIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadPixivel();
                } else if (LofterIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadLofter();
                } else if (DaihanIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadDaihan();
                } else if (DmoeIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadDmoe();
                } else if (ToubiecIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadToubiec();
                } else if (MtyIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadMty();
                } else if (SeovxIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadSeovx();
                } else if (PaulIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadPaul();
                } else if (BingIni.GetId().Equals(ini.PushProvider)) {
                    done = await LoadBing();
                } else {
                    done = await LoadBing();
                }
                if (done) {
                    localSettings.Values[pushTag] = (int)localSettings.Values[pushTag] + 1;
                }
                ApiService.Stats(ini, done);
            } catch (Exception e) {
                Log.Error("PushService.Run() " + e.GetType().Name);
            } finally {
                deferral.Complete();
            }
        }

        private void Init(IBackgroundTaskInstance taskInstance) {
            pushNow = taskInstance.TriggerDetails is ApplicationTriggerDetails;
            ini = IniUtil.GetIni();
            pushPeriod = ini.GetPushPeriod(ini.PushProvider);
            pushTag = string.Format("{0}{1}-{2}-{3}", DateTime.Now.ToString("yyyyMMdd"),
                DateTime.Now.Hour / pushPeriod, ini.PushProvider, ini.Push);

            if (localSettings == null) {
                localSettings = ApplicationData.Current.LocalSettings;
                if (!localSettings.Values.ContainsKey(pushTag)) {
                    localSettings.Values[pushTag] = 0;
                }

                string logFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "log.txt");
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(logFile, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Month)
                    .CreateLogger();
            }
        }

        private bool CheckNecessary() {
            if (!UserProfilePersonalizationSettings.IsSupported()) {
                Log.Information("PushService.CheckNecessary() UserProfilePersonalizationSettings false");
                return false;
            }
            if (pushNow) { // 立即运行一次
                return true;
            }
            if (string.IsNullOrEmpty(ini.Push)) { // 未开启推送
                return false;
            }
            return (int)localSettings.Values[pushTag] == 0;
        }

        private async Task<bool> SetWallpaper(string urlImg, bool setDesktopOrLock, Size resize, float offset) {
            if (urlImg == null) {
                Log.Information("PushService.SetWallpaper() invalid url");
                return false;
            }

            StorageFile fileWallpaper = await ApplicationData.Current.LocalFolder.CreateFileAsync(setDesktopOrLock ? "desktop" : "lock",
                    CreationCollisionOption.ReplaceExisting);
            Debug.WriteLine(fileWallpaper.Path);
            BackgroundDownloader downloader = new BackgroundDownloader();
            DownloadOperation operation = downloader.CreateDownload(new Uri(urlImg), fileWallpaper);
            DownloadOperation resOperation = await operation.StartAsync().AsTask();
            if (resOperation.Progress.Status != BackgroundTransferStatus.Completed) {
                Log.Information("PushService.SetWallpaper() download error");
                return false;
            }
            if (!resize.IsEmpty) {
                CanvasDevice device = CanvasDevice.GetSharedDevice();
                CanvasBitmap bitmap = null;
                using (var stream = await fileWallpaper.OpenReadAsync()) {
                    bitmap = await CanvasBitmap.LoadAsync(device, stream);
                }
                if (bitmap == null) {
                    return false;
                }
                float offsetWidthPixels = (resize.Width + bitmap.SizeInPixels.Width) / 2.0f * offset;
                CanvasRenderTarget target = new CanvasRenderTarget(device, resize.Width, resize.Height, 96);
                using (var session = target.CreateDrawingSession()) {
                    session.Clear(Colors.Black);
                    session.DrawImage(bitmap, (resize.Width - bitmap.SizeInPixels.Width) / 2.0f + offsetWidthPixels,
                        (resize.Height - bitmap.SizeInPixels.Height) / 2.0f);
                }
                fileWallpaper = await ApplicationData.Current.LocalFolder.CreateFileAsync(setDesktopOrLock ? "desktop-reset" : "lock-reset",
                    CreationCollisionOption.ReplaceExisting);
                await target.SaveAsync(fileWallpaper.Path, CanvasBitmapFileFormat.Png);
            }
            UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
            return setDesktopOrLock ? await profileSettings.TrySetWallpaperImageAsync(fileWallpaper)
                : await profileSettings.TrySetLockScreenImageAsync(fileWallpaper);
        }

        private async Task<bool> LoadBing() {
            const string URL_API_HOST = "https://global.bing.com";
            const string URL_API = URL_API_HOST + "/HPImageArchive.aspx?pid=hp&format=js&uhd=1&idx=0&n=1";
            Log.Information("PushService.LoadBing() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            BingApi bing = JsonConvert.DeserializeObject<BingApi>(jsonData);
            string urlUhd = string.Format("{0}{1}_UHD.jpg", URL_API_HOST, bing.Images[0].UrlBase);
            Log.Information("PushService.LoadBing() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadNasa() {
            string urlApi = string.Format("https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY&thumbs=True&start_date={0}&end_date={1}",
                DateTime.UtcNow.AddHours(-4).AddDays(-6).ToString("yyyy-MM-dd"), DateTime.UtcNow.AddHours(-4).ToString("yyyy-MM-dd"));
            Log.Information("PushService.LoadNasa() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            IList<NasaApiItem> items = JsonConvert.DeserializeObject<IList<NasaApiItem>>(jsonData);
            string urlUhd = null;
            for (int i = items.Count - 1; i >= 0; --i) { // 取最近日期
                if ("image".Equals(items[i].MediaType)) {
                    urlUhd = items[i].HdUrl;
                    break;
                }
            }
            Log.Information("PushService.LoadBing() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadOneplus() {
            OneplusRequest request = new OneplusRequest {
                PageSize = 1,
                CurrentPage = 1,
                SortMethod = "1"
            };
            string requestStr = JsonConvert.SerializeObject(request);
            const string URL_API = "https://photos.oneplus.com/cn/shot/photo/schedule";
            Log.Information("PushService.LoadOneplus() api url: " + URL_API + " " + requestStr);
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(requestStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(URL_API, content);
            _ = response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();
            OneplusApi oneplusApi = JsonConvert.DeserializeObject<OneplusApi>(jsonData);
            string urlUhd = oneplusApi.Items[0].PhotoUrl;
            Log.Information("PushService.LoadBing() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadTimeline() {
            const string URL_API = "https://api.nguaduot.cn/timeline?client=timelinewallpaper&cate={0}&order={1}";
            string urlApi = string.Format(URL_API, ini.Timeline.Cate, ini.Timeline.Order);
            Log.Information("PushService.LoadTimeline() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadTimeline() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadYmyouli() {
            const string URL_UHD = "https://27146103.s21i.faiusr.com/2/{0}";
            const string URL_API = "https://www.ymyouli.com/ajax/ajaxLoadModuleDom_h.jsp";
            List<string> cols = Enumerable.ToList(YmyouliIni.GetDic().Keys);
            string col = ini.Ymyouli.Col;
            if (!cols.Contains(col)) {
                col = cols[new Random().Next(cols.Count)];
            }
            List<string> modules = Enumerable.ToList(YmyouliIni.GetDic()[col].Keys);
            string module = modules[new Random().Next(modules.Count)];
            col = YmyouliIni.GetDic()[col][module];
            Dictionary<string, string> formData = new Dictionary<string, string>() {
                { "cmd", "getWafNotCk_getAjaxPageModuleInfo" },
                { "href", string.Format("/col.jsp?id={0}&m{1}pageno=1", col, module) },
                { "_colId", col },
                { "moduleId", module }
            };
            Log.Information("PushService.LoadYmyouli() api url: " + URL_API);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("timelinewallpaper",
                Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync(URL_API, new FormUrlEncodedContent(formData));
            string jsonData = await response.Content.ReadAsStringAsync();
            List<string> urls = new List<string>();
            foreach (Match m in Regex.Matches(jsonData, @"""id"": ?""(.+?)""")) {
                urls.Add(string.Format(URL_UHD, m.Groups[1].Value));
            }
            string urlUhd = urls[new Random().Next(urls.Count)];
            Log.Information("PushService.LoadYmyouli() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadInfinity() {
            const string URL_API = "https://infinity-api.infinitynewtab.com/random-wallpaper?_={0}";
            string urlApi = string.Format(URL_API,
                (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            Log.Information("PushService.LoadInfinity() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""rawSrc"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadInfinity() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadHimawari8() {
            const string URL_API = "https://himawari8.nict.go.jp/img/D531106/1d/550/{0}/{1}_0_0.png";
            DateTime now = DateTime.UtcNow.AddMinutes(-15);
            now = now.AddMinutes(-now.Minute % 10);
            string urlUhd = null;
            for (int i = 0; i < 5; i++) {
                string urlApi = string.Format(URL_API, now.AddMinutes(-10 * i).ToString(@"yyyy\/MM\/dd"),
                    string.Format("{0}{1}000", now.AddMinutes(-10 * i).ToString("HH"),
                    (now.AddMinutes(-10 * i).Minute / 10)));
                Log.Information("PushService.LoadHimawari8() api url: " + urlApi);
                HttpWebRequest req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(urlApi));
                req.Method = HttpMethod.Head.Method;
                var res = (HttpWebResponse)await req.GetResponseAsync();
                if (res.StatusCode == HttpStatusCode.OK && res.ContentLength > 10 * 1024) {
                    urlUhd = urlApi;
                    break;
                }
                res.Close();
            }
            Log.Information("PushService.LoadHimawari8() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(1920, 1080), ini.Himawari8.Offset);
        }

        private async Task<bool> Load3G() {
            const string URL_API = "https://desk.3gbizhi.com/index_1.html";
            Log.Information("PushService.Load3G() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string htmlData = await client.GetStringAsync(URL_API);
            List<string> urls = new List<string>();
            foreach (Match m in Regex.Matches(htmlData, @"<img lazysrc=""(https://pic.3gbizhi.com/\d{4}/\d{4}/\d+\.[^.]+)")) {
                urls.Add(m.Groups[1].Value);
            }
            string urlUhd = urls[new Random().Next(urls.Count)];
            Log.Information("PushService.Load3G() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadPixivel() {
            const string URL_HOST_PROXY = "https://i.pixiv.re";
            const string URL_API = "https://api.pixivel.moe/pixiv?type=illust_recommended";
            Log.Information("PushService.LoadPixivel() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            List<string> urls = new List<string>();
            foreach (Match m in Regex.Matches(jsonData, @"""original_image_url"": ?""(.+?)""")) {
                urls.Add(m.Groups[1].Value);
            }
            foreach (Match m in Regex.Matches(jsonData, @"""original"": ?""(.+?)""")) {
                urls.Add(m.Groups[1].Value);
            }
            string urlUhd = urls[new Random().Next(urls.Count)];
            urlUhd = URL_HOST_PROXY + new Uri(urlUhd).AbsolutePath; // 替换国内代理
            Log.Information("PushService.LoadPixivel() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadLofter() {
            const string URL_API = "https://www.lofter.com/front/login";
            Log.Information("PushService.LoadLofter() api url: " + URL_API);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("timelinewallpaper",
                Package.Current.Id.Version.Major + "." + Package.Current.Id.Version.Minor));
            string htmlData = await client.GetStringAsync(URL_API);
            List<string> urls = new List<string>();
            foreach (Match m in Regex.Matches(htmlData, @"""image"": ?""(.+?)""")) {
                urls.Add(m.Groups[1].Value);
            }
            string urlUhd = urls[new Random().Next(urls.Count)];
            Log.Information("PushService.LoadLofter() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadDaihan() {
            const string URL_API = "https://api.daihan.top/api/acg/index.php";
            Log.Information("PushService.LoadDaihan() api url: " + URL_API);
            HttpClient client = new HttpClient(new HttpClientHandler {
                AllowAutoRedirect = false
            });
            HttpResponseMessage msg = await client.GetAsync(URL_API);
            string urlUhd = msg.Headers.Location.AbsoluteUri;
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadDaihan() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadDmoe() {
            const string URL_API = "https://www.dmoe.cc/random.php?return=json";
            Log.Information("PushService.LoadDmoe() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = Regex.Unescape(match.Groups[1].Value);
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadDmoe() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadToubiec() {
            const string URL_API = "https://acg.toubiec.cn/random.php?ret=json";
            Log.Information("PushService.LoadToubiec() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadToubiec() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadMty() {
            const string URL_API = "https://api.mtyqx.cn/api/random.php?return=json";
            Log.Information("PushService.LoadMty() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = Regex.Unescape(match.Groups[1].Value);
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadMty() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadSeovx() {
            const string URL_API = "https://cdn.seovx.com/{0}/?mom=302";
            string urlApi = string.Format(URL_API, ini.Seovx.Cate);
            Log.Information("PushService.LoadSeovx() api url: " + urlApi);
            HttpClient client = new HttpClient(new HttpClientHandler {
                AllowAutoRedirect = false
            });
            HttpResponseMessage msg = await client.GetAsync(urlApi);
            string urlUhd = msg.Headers.Location.ToString();
            if (urlUhd.StartsWith("//")) {
                urlUhd = new Uri("https:" + urlUhd).AbsoluteUri;
            }
            Log.Information("PushService.LoadSeovx() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }

        private async Task<bool> LoadPaul() {
            const string URL_API = "https://api.paugram.com/wallpaper/?source=gh";
            Log.Information("PushService.LoadPaul() api url: " + URL_API);
            HttpClient client = new HttpClient(new HttpClientHandler {
                AllowAutoRedirect = false
            });
            HttpResponseMessage msg = await client.GetAsync(URL_API);
            string urlUhd = msg.Headers.Location.AbsoluteUri;
            Log.Information("PushService.LoadPaul() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, "desktop".Equals(ini.Push), new Size(), 0);
        }
    }
}
