using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace Utils.Sitemap
{
	[XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
	public class Sitemap
	{
		private readonly List<SitemapLocation> _map;

		public Sitemap()
		{
			_map = new List<SitemapLocation>();
		}

		[XmlElement("url")]
		public SitemapLocation[] Locations
		{
			get { return _map.ToArray(); }
			set
			{
				if (value == null)
					return;

				_map.Clear();
				_map.AddRange(value);
			}
		}

		public string GetSitemapXml()
		{
			return string.Empty;
		}

		public void Add(SitemapLocation item)
		{
			_map.Add(item);
		}


		public void ExportToFile(string path)
		{
			using (var fs = new FileStream(path, FileMode.Create))
			{
				var ns = new XmlSerializerNamespaces();
				ns.Add("image", "http://www.google.com/schemas/sitemap-image/1.1");

				var xs = new XmlSerializer(typeof(Sitemap));
				xs.Serialize(fs, this, ns);
			}
		}

		public string Export()
		{
			using (var fs = new StringWriter())
			{
				var ns = new XmlSerializerNamespaces();
				ns.Add("image", "http://www.google.com/schemas/sitemap-image/1.1");

				var xs = new XmlSerializer(typeof(Sitemap));
				xs.Serialize(fs, this, ns);

				return fs.ToString();
			}
		}
	}
}
