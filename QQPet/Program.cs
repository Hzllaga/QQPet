using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace QQPet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "QQ超级萌宠自动领金币";
            Console.Write("请输入Auth值（Bearer后面，不带空白）：");
            string auth = Console.ReadLine();
            using (StreamReader sr = new StreamReader("id.txt"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    request(line, auth);
                }
            }
            Console.WriteLine("领完啦~~");
            Console.ReadKey();
        }
        private static void request(string userID, string Authorization)
        {
            HttpWebRequest getCounters = (HttpWebRequest)WebRequest.Create("https://pet.vip.qq.com/api/counters?userId=" + userID);
            getCounters.Headers.Add("Authorization", "Bearer " + Authorization);
            getCounters.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_1_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/16C5050a QQ/8.1.0.437 V1_IPH_SQ_8.1.0_1_APP_A Pixel/1080 Core/WKWebView Device/Apple(iPhone 8Plus) NetType/WIFI QBWebViewType/1 WKType/1";
            getCounters.ContentType = "application/json";
            var getResponse = (HttpWebResponse)getCounters.GetResponse();
            using (var streamReader = new StreamReader(getResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
            Thread.Sleep(1000);
            HttpWebRequest postCounters = (HttpWebRequest)WebRequest.Create("https://pet.vip.qq.com/api/counters");
            postCounters.ContentType = "application/json";
            postCounters.Headers.Add("Authorization", "Bearer " + Authorization);
            postCounters.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_1_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/16C5050a QQ/8.1.0.437 V1_IPH_SQ_8.1.0_1_APP_A Pixel/1080 Core/WKWebView Device/Apple(iPhone 8Plus) NetType/WIFI QBWebViewType/1 WKType/1";
            postCounters.Method = "POST";

            using (var streamWriter = new StreamWriter(postCounters.GetRequestStream()))
            {
                string json = "{\"userId\":\"" + userID + "\",\"ad\":false}";

                streamWriter.Write(json);
            }

            try
            {
                var postResponse = (HttpWebResponse)postCounters.GetResponse();
                using (var streamReader = new StreamReader(postResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Thread.Sleep(2000);
        }
    }
}
