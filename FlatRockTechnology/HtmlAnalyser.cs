using FlatRockTechnology;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlatRockTechnology
{
    public class HtmlAnalyser
    {
        private readonly HtmlDocument _document;

        public HtmlAnalyser(string rawHtml)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(rawHtml);
        }

        public IEnumerable<Product> ReadProducts()
        {
            var nodes = GetProductNodes();
            return ConvertNodesToProducts(nodes);
        }

        private HtmlNodeCollection GetProductNodes() =>
            _document.DocumentNode.SelectNodes("//div[@class='item']");

        private IEnumerable<Product> ConvertNodesToProducts(HtmlNodeCollection nodes)
        {
            if (nodes is null) throw new ArgumentNullException($"{nameof(nodes)} can not be null");

            foreach (var node in nodes)
            {
                string productName = ExtractName(node.SelectSingleNode(".//h4/a")?.InnerText.Trim());
                string price = ExtractPrice(node.SelectSingleNode(".//p")?.InnerText);
                float rating = ExtractRating(node.GetAttributeValue("rating", ""));

                yield return new Product { ProductName = productName, Price = decimal.Parse(price), Rating = rating };
            }
        }

        private string ExtractName(string htmlString) => System.Net.WebUtility.HtmlDecode(htmlString);

        private string ExtractPrice(string priceString) => priceString.Split('$')[1].Trim();

        private float ExtractRating(string ratingString)
        {
            if (float.TryParse(ratingString, out float rating))
                return Math.Min(5, rating / 2);

            return 0;
        }
    }
}

