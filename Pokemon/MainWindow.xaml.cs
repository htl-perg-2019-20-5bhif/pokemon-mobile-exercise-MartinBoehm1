using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokemon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] names;
        int[] ids;
        public List<PokemonObject> pokemons { get; set; }
        public MainWindow()
        {
            pokemons = new List<PokemonObject>();

            List l = new List();
            string html = string.Empty;
            string url = @"https://pokeapi.co/api/v2/pokemon/?limit=20";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            names = html.Split("},{");
            ids = new int[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                
                names[i] = names[i].Split("name\":\"")[1];
                string s3 = names[i].Split("https://pokeapi.co/api/v2/pokemon/")[1];
                ids[i] = Int32.Parse(s3.Split("/\"")[0]);
                names[i] = names[i].Split("\"")[0];




                html = string.Empty;
                url = @"https://pokeapi.co/api/v2/pokemon/"+ids[i];

                request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                html = html.Split("front_default\":\"")[1];
                html = html.Split("\"")[0];
                pokemons.Add(new PokemonObject()
                {
                    Name = names[i],
                    id=ids[i],



                    


                    Front = html

                });
                Console.WriteLine();
            }

            InitializeComponent();
            DataContext = this;
        }

        private async void ListView_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
