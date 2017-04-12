using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server;
using TestWebApplication4.Models;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace TestWebApplication4.Controllers
{
    public class MessagesController : ApiController
    {
        public static string connectionString = "Endpoint=sb://distsystemsnotifications-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=BS1z8panbcYaogf7/uu1sEUPkXufLzeKNzMYPVLR+Io="; // DefaultFullSharedAccess

        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M}
        };

        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            await SendNotificationAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        private static async Task SendNotificationAsync()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3040/server");

            request.Method = "POST";
            string apiKey = "AAAAF35V4ok:APA91bEcpgViVvPvEmrMpUYlDxYLjZUByo6KWAMVaBxBU3IQgN7n3Di1xbgClZhtD5-MB9NPchNHIN8riD2TJ64aQ1iKPW461lUt6ihd3qKhE9W1uaaPLlciqeNaEyeYHOJFNxVw_pY1";
            string to = "cvPxd7GPLxk:APA91bEos2RR_DfCuo1YHxiL9KBhTcmBuiPRdPlviC5Jrz_8aHVJUFBQWLVL6Kx07W7nmSBGIZ1WMTVSWrmINzEwH3st0oRpluy3pqsh8_eSqOvcdqDHcF6-obJr4wz-Y_umpC3CKNiq";
            string jsonPayload = "  {\"to\" : \"" + to + "\",\"data\" : {\"5033330685\" : \"Hello, it's me!\" } }";
            request.ContentType = "application/json";
            request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
            request.Accept = "text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonPayload);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            // Get the original response.
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();
            Console.Write(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
