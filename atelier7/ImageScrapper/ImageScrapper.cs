using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ImageScrapper
{
    class ImageScrapper
    {
        #region Members
        private ConcurrentBag<string> links = new ConcurrentBag<string>();
        private Regex regxLink = new Regex(@"<a\s+[^>]*href\s*=\s*['""]([^'""]*)['""]", RegexOptions.IgnoreCase | RegexOptions.Multiline),
                      regxImg = new Regex(@"<img\s+[^>]*src\s*=\s*['""]([^'""]*)['""]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        private string imagedir, baseurl;
        #endregion

        public ImageScrapper(string imagedir, string baseurl)
        {
            this.imagedir = imagedir;
            this.baseurl = baseurl;
        }
        private void ScrapPage(Uri root)
        {
            Console.WriteLine(root.AbsoluteUri);
            this.links.Add(root.AbsoluteUri);
            using (WebClient wc = new WebClient())
            {
                try
                {
                    using (Stream s = wc.OpenRead(root))
                    {
                        using (StreamReader rdr = new StreamReader(s))
                        {
                            string page = rdr.ReadToEnd();

                            foreach (Match img in this.regxImg.Matches(page))
                                DownloadImg(root, img.Groups[1].Value);

                            foreach (Match link in this.regxLink.Matches(page))
                            {
                                string sLink = link.Groups[1].Value;
                                Uri url;

                                if (Uri.TryCreate(root, sLink, out url)
                                    && !sLink.StartsWith("javascript:")
                                    && !sLink.Contains('#')
                                    && url.AbsoluteUri.StartsWith(this.baseurl)
                                    && !this.links.Contains(url.AbsoluteUri))
                                {
                                    ScrapPage(url);
                                }
                            }
                        }
                    }
                }
                catch (WebException xWeb)
                {
                    Trace.WriteLine(String.Format("Unable to visit {0} ({1})", root.AbsoluteUri, xWeb.Message));
                }
            }
        }
        public void Scrap()
        {
            ScrapPage(new Uri(this.baseurl));
        }
        private void DownloadImg(Uri root, string imgpath)
        {
            try
            {
                using (WebClient downloader = new WebClient())
                {
                    Uri url;

                    if (Uri.TryCreate(root, imgpath, out url))
                    {
                        downloader.DownloadFile(
                            url,
                            Path.Combine(
                                this.imagedir,
                                Path.GetFileName(imgpath)
                            )
                        );
                    }
                }
            }
            catch (Exception x)
            {
                Trace.WriteLine(String.Format("Couln't download image {0} ({1})", imgpath, x.Message));
            }
        }
    }
}
