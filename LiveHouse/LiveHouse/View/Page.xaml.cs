using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using LiveHouse.Models;
using HtmlAgilityPack;
using LiveHouse;
using System.Diagnostics;
using LiveHouse.Services;
using Xamarin.Essentials;
using Geocoding.Google;
using Geocoding;
namespace LiveHouse.View
{
    public partial class Page : ContentPage

    {
        
        public static List<House> Houses = new List<House>();

        public Page()
        {
            InitializeComponent();


            string Link = "https://www.tokyogigguide.com/en/livehouses?start=0";
            string Link1 = "https://www.tokyogigguide.com/en/livehouses?start=50";
            string Link2 = "https://www.tokyogigguide.com/en/livehouses?start=100";
            string Link3 = "https://www.tokyogigguide.com/en/livehouses?start=150";
            string Link4 = "https://www.tokyogigguide.com/en/livehouses?start=200";
            string Link5 = "https://www.tokyogigguide.com/en/livehouses?start=250";
            string Link6 = "https://www.tokyogigguide.com/en/livehouses?start=300";
            string Link7 = "https://www.tokyogigguide.com/en/livehouses?start=350";
            string Link8 = "https://www.tokyogigguide.com/en/livehouses?start=400";
            string Link9 = "https://www.tokyogigguide.com/en/livehouses?start=450";
            string Link10 = "https://www.tokyogigguide.com/en/livehouses?start=500";
            string Link11 = "https://www.tokyogigguide.com/en/livehouses?start=550";
            string Link12 = "https://www.tokyogigguide.com/en/livehouses?start=600";
            string Link13 = "https://www.tokyogigguide.com/en/livehouses?start=650";
            string Link14 = "https://www.tokyogigguide.com/en/livehouses?start=700";
            string Link15 = "https://www.tokyogigguide.com/en/livehouses?start=750";


            List<string> links = new List<string>()
            {
                Link, Link1, Link2, Link3, Link4, Link5, Link6, Link7, Link8, Link9, Link10, Link11, Link12, Link13, Link14, Link15,
            };
            string Xpath = "/html/body/div[1]/div/section[1]/div/div/div/div/div[2]/form/div[2]/ul/li[position()>0]";
            HtmlWeb web = new HtmlWeb();




            foreach (string i in links)
            {
                var Web = new HtmlWeb();
                
                var Dock = Web.Load(i);
                var Nodes = Dock.DocumentNode.SelectNodes(Xpath);
                foreach (var Node in Nodes)
                {
                    
                    var house = new House
                    {
                        Name = Node.SelectSingleNode("div[1]/a").InnerText,
                        Location = Node.SelectSingleNode("div[2]/text()").InnerText,
                        
                        Country = "Japan"
                };

          


                    Houses.Add(house);
                    


            }
            
            }
            

            HouseList.ItemsSource = Houses;
           
        }



        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                HouseList.ItemsSource = Houses;
            }

            else
            {
                HouseList.ItemsSource = Houses.Where(x => x.Name.Contains(e.NewTextValue));
            }
        }

        async void Venue_Clicked(System.Object sender, System.EventArgs e)
        {
            var buttonClickHandler = (Button)sender;


            StackLayout ParentStackLayout = (StackLayout)buttonClickHandler.Parent;

            Button VenueLabel = (Button)ParentStackLayout.Children[0];

            var placemark = new Placemark
            {

                Thoroughfare = $"{VenueLabel.Text} ライブハウス　東京"

            };
            var options = new MapLaunchOptions { Name = VenueLabel.Text };

            try
            {
                await Xamarin.Essentials.Map.OpenAsync(placemark, options);
            }
            catch (Exception ex)
            {
                // No map application available to open or placemark can not be located
            }

        }
        
    }
}
