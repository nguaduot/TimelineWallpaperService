using Microsoft.Graphics.Canvas;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TimelineWallpaperService.Beans;
using TimelineWallpaperService.Services;
using TimelineWallpaperService.Utils;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.System.UserProfile;
using Windows.UI;

namespace TimelineWallpaperService {
    public sealed class PushService : IBackgroundTask {
        private ApplicationDataContainer localSettings;
        private Ini ini;
        private int desktopPeriod = 24;
        private int lockPeriod = 24;
        private string desktopTag; // 免重复推送桌面背景标记
        private string lockTag; // 免重复推送锁屏背景标记
        private bool pushNow = false; // 立即运行一次

        public async void Run(IBackgroundTaskInstance taskInstance) {
            Init(taskInstance);
            Log.Information("PushService.Run() trigger: " + taskInstance.TriggerDetails);
            Log.Information("PushService.Run() desktopProvider: " + ini.DesktopProvider);
            Log.Information("PushService.Run() lockProvider: " + ini.LockProvider);
            Log.Information("PushService.Run() desktopPeriod: " + desktopPeriod);
            Log.Information("PushService.Run() lockPeriod: " + lockPeriod);
            // 检查网络连接
            if (!NetworkInterface.GetIsNetworkAvailable()) {
                Log.Warning("PushService.Run() network not available");
                return;
            }

            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            try {
                bool stats = false;
                if (CheckDesktopNecessary()) {
                    bool done = false;
                    if (BingIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadBing(true);
                    } else if (NasaIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadNasa(true);
                    } else if (OneplusIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadOneplus(true);
                    } else if (TimelineIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadTimeline(true);
                    } else if (YmyouliIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadYmyouli(true);
                    } else if (InfinityIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadInfinity(true);
                    } else if (OneIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadOne(true);
                    } else if (QingbzIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadQingbz(true);
                    } else if (ObzhiIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadObzhi(true);
                    } else if (Himawari8Ini.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadHimawari8(true);
                    } else if (G3Ini.GetId().Equals(ini.DesktopProvider)) {
                        done = await Load3G(true);
                    } else if (WallhereIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadWallhere(true);
                    } else if (AbyssIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadAbyss(true);
                    } else if (DaihanIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadDaihan(true);
                    } else if (DmoeIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadDmoe(true);
                    } else if (ToubiecIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadToubiec(true);
                    } else if (MtyIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadMty(true);
                    } else if (SeovxIni.GetId().Equals(ini.DesktopProvider)) {
                        done = await LoadSeovx(true);
                    }
                    if (done) {
                        localSettings.Values[desktopTag] = (int)localSettings.Values[desktopTag] + 1;
                        stats = true;
                    }
                } else {
                    Log.Information("PushService.Run() skip desktopTag: " + desktopPeriod);
                }
                if (CheckLockNecessary()) {
                    bool done = false;
                    if (BingIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadBing(false);
                    } else if (NasaIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadNasa(false);
                    } else if (OneplusIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadOneplus(false);
                    } else if (TimelineIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadTimeline(false);
                    } else if (YmyouliIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadYmyouli(false);
                    } else if (InfinityIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadInfinity(false);
                    } else if (OneIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadOne(false);
                    } else if (QingbzIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadQingbz(false);
                    } else if (ObzhiIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadObzhi(false);
                    } else if (Himawari8Ini.GetId().Equals(ini.LockProvider)) {
                        done = await LoadHimawari8(false);
                    } else if (G3Ini.GetId().Equals(ini.LockProvider)) {
                        done = await Load3G(false);
                    } else if (WallhereIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadWallhere(false);
                    } else if (AbyssIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadAbyss(false);
                    } else if (DaihanIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadDaihan(false);
                    } else if (DmoeIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadDmoe(false);
                    } else if (ToubiecIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadToubiec(false);
                    } else if (MtyIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadMty(false);
                    } else if (SeovxIni.GetId().Equals(ini.LockProvider)) {
                        done = await LoadSeovx(false);
                    }
                    if (done) {
                        localSettings.Values[lockTag] = (int)localSettings.Values[lockTag] + 1;
                        stats = true;
                    }
                } else {
                    Log.Information("PushService.Run() skip lockTag: " + desktopPeriod);
                }
                if (stats) {
                    _ = await ApiService.Stats(ini, true);
                }
            } catch (Exception e) {
                Log.Error("PushService.Run() " + e.GetType().Name);
            } finally {
                deferral.Complete();
            }
        }

        private void Init(IBackgroundTaskInstance taskInstance) {
            pushNow = taskInstance.TriggerDetails is ApplicationTriggerDetails;
            ini = IniUtil.GetIni();
            desktopPeriod = ini.GetDesktopPeriod(ini.DesktopProvider);
            lockPeriod = ini.GetLockPeriod(ini.LockProvider);
            desktopTag = string.Format("{0}{1}-{2}", DateTime.Now.ToString("yyyyMMdd"),
                DateTime.Now.Hour / desktopPeriod, ini.DesktopProvider);
            lockTag = string.Format("{0}{1}-{2}", DateTime.Now.ToString("yyyyMMdd"),
                DateTime.Now.Hour / lockPeriod, ini.LockProvider);

            if (localSettings == null) {
                localSettings = ApplicationData.Current.LocalSettings;
                if (!localSettings.Values.ContainsKey(desktopTag)) {
                    localSettings.Values[desktopTag] = 0;
                }
                if (!localSettings.Values.ContainsKey(lockTag)) {
                    localSettings.Values[lockTag] = 0;
                }

                string logFile = Path.Combine(ApplicationData.Current.LocalFolder.Path, "log.txt");
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File(logFile, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Month)
                    .CreateLogger();
            }
        }

        private bool CheckDesktopNecessary() {
            if (!UserProfilePersonalizationSettings.IsSupported()) {
                Log.Information("PushService.CheckNecessary() UserProfilePersonalizationSettings false");
                return false;
            }
            if (pushNow) { // 立即运行一次
                return true;
            }
            if (string.IsNullOrEmpty(ini.DesktopProvider)) { // 未开启推送
                return false;
            }
            return (int)localSettings.Values[desktopTag] == 0;
        }

        private bool CheckLockNecessary() {
            if (!UserProfilePersonalizationSettings.IsSupported()) {
                Log.Information("PushService.CheckNecessary() UserProfilePersonalizationSettings false");
                return false;
            }
            if (pushNow) { // 立即运行一次
                return true;
            }
            if (string.IsNullOrEmpty(ini.LockProvider)) { // 未开启推送
                return false;
            }
            return (int)localSettings.Values[lockTag] == 0;
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

        private async Task<bool> LoadBing(bool setDesktopOrLock) {
            const string URL_API_HOST = "https://global.bing.com";
            const string URL_API = URL_API_HOST + "/HPImageArchive.aspx?pid=hp&format=js&uhd=1&idx=0&n=1";
            Log.Information("PushService.LoadBing() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            BingApi bing = JsonConvert.DeserializeObject<BingApi>(jsonData);
            string urlUhd = string.Format("{0}{1}_UHD.jpg", URL_API_HOST, bing.Images[0].UrlBase);
            Log.Information("PushService.LoadBing() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadNasa(bool setDesktopOrLock) {
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
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadOneplus(bool setDesktopOrLock) {
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
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadTimeline(bool setDesktopOrLock) {
            const string URL_API = "https://api.nguaduot.cn/timeline?client=timelinewallpaper&cate={0}&order={1}&authorize={2}";
            string urlApi = string.Format(URL_API, ini.Timeline.Cate, ini.Timeline.Order, ini.Timeline.Authorize);
            Log.Information("PushService.LoadTimeline() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadTimeline() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadYmyouli(bool setDesktopOrLock) {
            const string URL_API = "https://api.nguaduot.cn/ymyouli?client=timelinewallpaper&cate={0}&order=random&r18=0";
            string urlApi = string.Format(URL_API, ini.Ymyouli.Cate);
            Log.Information("PushService.LoadYmyouli() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadYmyouli() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadInfinity(bool setDesktopOrLock) {
            const string URL_API = "https://infinity-api.infinitynewtab.com/random-wallpaper?_={0}";
            string urlApi = string.Format(URL_API,
                (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            Log.Information("PushService.LoadInfinity() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""rawSrc"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadInfinity() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadHimawari8(bool setDesktopOrLock) {
            const string URL_API = "https://himawari8.nict.go.jp/img/D531106/thumbnail/550/{0}/{1}_0_0.png";
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
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(1920, 1080), ini.Himawari8.Offset);
        }

        private async Task<bool> LoadOne(bool setDesktopOrLock) {
            const string URL_TOKEN = "http://m.wufazhuce.com/one";
            const string URL_API = "http://m.wufazhuce.com/one/ajaxlist/{0}?_token={1}";
            HttpClient client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync(URL_TOKEN); // cookie 无需手动取，自动包含
            string htmlData = await msg.Content.ReadAsStringAsync();
            Match match = Regex.Match(htmlData, @"One.token ?= ?[""'](.+?)[""']");
            string token = match.Groups[1].Value;
            string id = "0";
            if ("random".Equals(ini.One.Order)) {
                id = (3012 + new Random().Next((DateTime.Now - DateTime.Parse("2020-11-10")).Days)).ToString();
            }
            string urlApi = string.Format(URL_API, id, token);
            Log.Information("PushService.LoadOne() api url: " + urlApi);
            string jsonData = await client.GetStringAsync(urlApi);
            match = Regex.Match(jsonData, @"""img_url"": ?""(.+?)""");
            string urlUhd = Regex.Unescape(match.Groups[1].Value); // 反转义
            Log.Information("PushService.LoadOne() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadQingbz(bool setDesktopOrLock) {
            const string URL_API = "https://api.nguaduot.cn/qingbz?client=timelinewallpaper&cate={0}&order=random&r18=0";
            string urlApi = string.Format(URL_API, ini.Qingbz.Cate);
            Log.Information("PushService.LoadQingbz() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadQingbz() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadObzhi(bool setDesktopOrLock) {
            const string URL_API = "https://api.nguaduot.cn/obzhi?client=timelinewallpaper&cate={0}&order=random&r18=0";
            string urlApi = string.Format(URL_API, ini.Qingbz.Cate);
            Log.Information("PushService.LoadObzhi() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadObzhi() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> Load3G(bool setDesktopOrLock) {
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
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadWallhere(bool setDesktopOrLock) {
            const string URL_API = "https://api.nguaduot.cn/wallhere?client=timelinewallpaper&order=random&cate={0}&r18=0";
            string urlApi = string.Format(URL_API, ini.Wallhere.Cate);
            Log.Information("PushService.LoadWallhere() api url: " + urlApi);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(urlApi);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            Log.Information("PushService.LoadWallhere() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadAbyss(bool setDesktopOrLock) {
            const string URL_API = "https://wall.alphacoders.com/popular.php";
            Log.Information("PushService.LoadAbyss() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string htmlData = await client.GetStringAsync(URL_API);
            List<string> urls = new List<string>();
            foreach (Match m in Regex.Matches(htmlData, @"src=[""']([^""]+thumbbig-\d+\.[^""']+)[""']")) {
                urls.Add(m.Groups[1].Value);
            }
            string urlUhd = urls[new Random().Next(urls.Count)].Replace("thumbbig-", "");
            Log.Information("PushService.LoadAbyss() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadDaihan(bool setDesktopOrLock) {
            const string URL_API = "https://api.daihan.top/api/acg/index.php";
            Log.Information("PushService.LoadDaihan() api url: " + URL_API);
            HttpClient client = new HttpClient(new HttpClientHandler {
                AllowAutoRedirect = false
            });
            HttpResponseMessage msg = await client.GetAsync(URL_API);
            string urlUhd = msg.Headers.Location.AbsoluteUri;
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadDaihan() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadDmoe(bool setDesktopOrLock) {
            const string URL_API = "https://www.dmoe.cc/random.php?return=json";
            Log.Information("PushService.LoadDmoe() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = Regex.Unescape(match.Groups[1].Value);
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadDmoe() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadToubiec(bool setDesktopOrLock) {
            const string URL_API = "https://acg.toubiec.cn/random.php?ret=json";
            Log.Information("PushService.LoadToubiec() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = match.Groups[1].Value;
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadToubiec() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadMty(bool setDesktopOrLock) {
            const string URL_API = "https://api.mtyqx.cn/api/random.php?return=json";
            Log.Information("PushService.LoadMty() api url: " + URL_API);
            HttpClient client = new HttpClient();
            string jsonData = await client.GetStringAsync(URL_API);
            Match match = Regex.Match(jsonData, @"""imgurl"": ?""(.+?)""");
            string urlUhd = Regex.Unescape(match.Groups[1].Value);
            urlUhd = Regex.Replace(urlUhd, @"(?<=\.sinaimg\.cn/)[^/]+", "large");
            Log.Information("PushService.LoadMty() img url: " + urlUhd);
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }

        private async Task<bool> LoadSeovx(bool setDesktopOrLock) {
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
            return await SetWallpaper(urlUhd, setDesktopOrLock, new Size(), 0);
        }
    }
}
