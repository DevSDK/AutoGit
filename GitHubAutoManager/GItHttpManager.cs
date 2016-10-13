using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Json;
using System.IO;

namespace GitHubAutoManager
{
    static class GItHttpManager
    {

        private static string __DEBUG_TOKEN = "f21279bf1efb79dad01fd221c571e46c99d8d48f";

        private static HttpWebRequest genRequest(string URI, REQUEST_TYPE type)   
        {
            if (type == REQUEST_TYPE.NULL)
                throw new NullReferenceException();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URI);
            request.ContentType = "application/JSON";

            if (type == REQUEST_TYPE.REQUEST_GET)
                request.Method = "GET";
            else 
                request.Method = "POST";

            request.ContentType = "application/JSON";
            request.UserAgent =
                  "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;" +
                  ".NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; InfoPath.2;" +
                  ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";

            return request;
        }


       

        public static string Request_Type_Get(string uri)
        {
            HttpWebRequest request = genRequest(uri, REQUEST_TYPE.REQUEST_GET);
            request.PreAuthenticate = true;
            HttpWebResponse repose = (HttpWebResponse)request.GetResponse();

            Stream stream = repose.GetResponseStream();
            StreamReader read_stream = new StreamReader(stream, Encoding.Default);
            string result = read_stream.ReadToEnd();
            stream.Close();
            return result;


                        

        }
        public static string Request_Type_Post(string uri, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(uri);

            HttpWebRequest request = genRequest(uri, REQUEST_TYPE.REQUEST_GET);
            request.ContentLength = buffer.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();            
            HttpWebResponse repose = (HttpWebResponse)request.GetResponse();

            stream = repose.GetResponseStream();
            StreamReader read_stream = new StreamReader(stream, Encoding.Default);
            string result = read_stream.ReadToEnd();
            stream.Close();
            return result;
        }

         enum REQUEST_TYPE
        {
             NULL =0,
             REQUEST_GET,
             REQUEST_POST
        }


       
    }
}
