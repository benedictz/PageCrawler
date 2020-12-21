using System;
using System.Net;
using System.IO;
using System.Threading;

namespace PageCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ApplicationException("Specify the URI to retrieve");
            }

            //Global Objects
            //string url = "https://www.scorptec.com.au/product/graphics-cards/geforcertx3080";
            int sleepSeconds = 30;

            //Randomise browser and OS
            Random rand = new Random();
            string[] user_agents = new string[8] {
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; it; rv:1.8.1.11) Gecko/20071127 Firefox/2.0.0.11",
                "Opera/9.25 (Windows NT 5.1; U; en)",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
                "Mozilla/5.0 (compatible; Konqueror/3.5; Linux) KHTML/3.5.5 (like Gecko) (Kubuntu)",
                "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.142 Safari/535.19",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.7; rv:11.0) Gecko/20100101 Firefox/11.0",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:8.0.1) Gecko/20100101 Firefox/8.0.1",
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.151 Safari/535.19"
            };

            string user = user_agents[rand.Next(user_agents.Length)];

            //Load page
            WebClient client = new WebClient();
            Console.WriteLine($"Searching '{args[0]}' with {user}");
            client.Headers.Add("User-Agent", user);

            Stream data = client.OpenRead(args[0]);
            StreamReader reader = new StreamReader(data);
            string HTML = reader.ReadToEnd();
            //Console.WriteLine(HTML);
            Console.WriteLine("Initial check complete");

            /*
            Commented out because we never want to close, dunno if necessary
            data.Close();
            reader.Close();
            */

            //Loop infinitely with new checks
            while (true)
            {
                //Sleep
                Thread.Sleep(sleepSeconds * 1000);

                //Load page
                WebClient newClient = new WebClient();
                Console.WriteLine($"Searching '{args[0]}' with {user}");
                newClient.Headers.Add("User-Agent", user);
                Stream newData = newClient.OpenRead(args[0]);
                StreamReader newReader = new StreamReader(newData);
                string newHTML = newReader.ReadToEnd();

                //Compare HTML
                if (HTML.Equals(newHTML))
                {
                    Console.WriteLine("No change");
                }
                else
                {
                    Console.WriteLine("Changed");
                }

                newData.Close();
                newReader.Close();
            }
        }
    }
}
