using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace TueursEnSeries.Models
{
    public class Episode
    {
        private string _id, _title;
        public Episode(string id, string title)
        {
            this._id=id;
            this._title=title;
        }
        public string Id { get { return this._id; } }
        public string Title { get { return this._title; } }
    }

    public class Serie
    {
        private string _id, _name, _banner, _overview, _imdb;
        private DateTime _firstAired;
        private List<Episode> _episodes;

        private string FromXElement(XElement xe, string subtag)
        { 
            XElement sub=xe.Element(subtag);
            return sub == null ? String.Empty : sub.Value;
        }
        public Serie(XElement xe, bool checkimgs)
        { 
            this._id        =FromXElement(xe, "seriesid");
            this._name      =FromXElement(xe, "SeriesName");
            this._banner    =FromXElement(xe, "banner");
            this._overview  =FromXElement(xe, "Overview");
            this._imdb      =FromXElement(xe, "IMDB_ID");
            this._episodes = new List<Episode>();

            if(checkimgs && this._banner.Length!=0)
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        using (wc.OpenRead(String.Format("http://thetvdb.com/banners/_cache/{0}", this._banner)))
                        {
                        }
                    }
                }
                catch (WebException x)
                {
                    this._banner = String.Empty;
                }
            XElement sub=xe.Element("FirstAired");
            
            if(sub!=null)
                DateTime.TryParseExact(
                    sub.Value,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out this._firstAired);
        }
        public string Id { get { return this._id; } }
        public string Name { get { return this._name; } }
        public string Banner { get { return this._banner; } }
        public string Overview { get { return this._overview; } }
        public DateTime? FirstAired { get { return this._firstAired; } }
        public string Imdb { get { return this._imdb; } }
        public IEnumerable<Episode> Episodes
        {
            get { return this._episodes; }
            set
            {
                this._episodes.Clear(); 
                this._episodes.AddRange(value);
            }
        }
    }

    public class SerieList
    {
        private List<Serie> _series;

        public SerieList()
        {
            this._series = new List<Serie>();
        }
        public void AppendSearch(string url_encoded_query, bool checkimgs)
        {
            using (WebClient wc = new WebClient())
            {
                string wsquery = String.Format(
                    @"http://thetvdb.com/api/GetSeries.php?seriesname={0}&language=fr",
                    url_encoded_query
                );
                using (Stream s = wc.OpenRead(wsquery))
                {
                    XDocument xdoc = XDocument.Load(s);

                    this._series.AddRange(
                        from serie in xdoc.Descendants("Series")
                        select new Serie(serie, checkimgs)
                    );
                }
            }
        }

        public IEnumerable<Serie> All
        {
            get { return this._series; }
        }
    }
}