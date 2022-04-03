using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using LiveHouse.Models;

namespace LiveHouse.Services
{
    public class FastWebScraper
    {
       
            public List<House> GetHouses(string url)
            {
                List<House> houses = new List<House>();
            
                HtmlWeb web = new HtmlWeb();

                var doc = web.Load(url);

                var nodes = doc.DocumentNode.SelectNodes("/html/body/div[1]/div/section[1]/div/div/div/div/div[2]/form/div[2]/ul/li[position()>1]");
                foreach (var node in nodes)
                {
                    var house = new House
                    {

                        Name = node.SelectSingleNode("div[1]/a").InnerText,

                        Location = node.SelectSingleNode("div[2]/text()").InnerText,
                    };

                }
                return houses;


            }
        
    }
}
