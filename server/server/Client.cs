using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace PhilosophyProject
{
    class Client
    {
        Socket client; // подключенный клиент
        HTTPHeaders Headers; // распарсенные заголовки

        struct HTTPHeaders
        {
            public string Method;
            public string RealPath;
            public string File;
            public bool   ExistData;
            public string QueryData;

            public static HTTPHeaders Parse(string headers)
            {
                HTTPHeaders result = new HTTPHeaders();
                result.Method = Regex.Match(headers, @"\A\w[a-zA-Z]+", RegexOptions.Multiline).Value;
                result.File = Regex.Match(headers, @"(?<=\w\s)([\Wa-zA-Z0-9]+)((?=\sHTTP)|((?=\?[\Wa-zA-Z0-9_]+\sHTTP)))", RegexOptions.Multiline).Value;
                result.RealPath = $"{Configuration.RealPath}{Configuration.PathFront}{result.File}";
                result.ExistData = (Regex.IsMatch(headers, @"(?<=\w\s[\?\.\/a-zA-Z0-9]+)(?<=\?)([\a-zA-Z0-9]+)(?=\sHTTP)", RegexOptions.Multiline)) ? true : false;
                result.QueryData = Regex.Match(headers, @"(?<=\w\s[\?\.\/a-zA-Z0-9]+)(?<=\?)([\a-zA-Z0-9]+)(?=\sHTTP)", RegexOptions.Multiline).Value;
                return result;
            }

            public static string FileExtention(string file)
            {
                return Regex.Match(file, @"(?<=[\W])\w+(?=[\W]{0,}$)").Value;
            }
        }

        public Client(Socket c)
        {
            client = c;
            byte[] data = new byte[1024];
            string request = "";
            client.Receive(data); // считываем входящий запрос и записываем его в наш буфер data
            request = Encoding.UTF8.GetString(data); // преобразуем принятые нами байты с помощью кодировки UTF8 в читабельный вид


            if (request == "")
            {
                client.Close();
                return;
            }
            else
            {
                Headers = HTTPHeaders.Parse(request);
                //Console.WriteLine($@"[{client.RemoteEndPoint}]
                //File: {Headers.File}
                //Date: {DateTime.Now}");

                if (Headers.RealPath.IndexOf("..") != -1)
                {
                    SendError(404);
                    client.Close();
                    return;
                }

                if (File.Exists(Headers.RealPath))
                    GetSheet(Headers);
                else
                    SendError(404);
                client.Close();
            }
        }

        public void SendError(int code)
        {
            string html = $"<html><head><title></title></head><body><h1>Error {code}</h1></body></html>";
            string headers = $"HTTP/1.1 {code} OK\nContent-type: text/html\nContent-Length: {html.Length}\n\n{html}";
            byte[] data = Encoding.UTF8.GetBytes(headers);
            client.Send(data, data.Length, SocketFlags.None);
            client.Close();
        }

        string GetContentType(HTTPHeaders head)
        {
            string result = "";
            string format = HTTPHeaders.FileExtention(Headers.File);
            switch (format)
            {
                //image
                case "gif":
                case "jpeg":
                case "pjpeg":
                case "png":
                case "tiff":
                case "webp":
                    result = $"image/{format}";
                    break;
                case "svg":
                    result = $"image/svg+xml";
                    break;
                case "ico":
                    result = $"image/vnd.microsoft.icon";
                    break;
                case "wbmp":
                    result = $"image/vnd.map.wbmp";
                    break;
                case "jpg":
                    result = $"image/jpeg";
                    break;
                // text
                case "css":
                    result = $"text/css";
                    break;
                case "html":
                    result = $"text/{format}";
                    break;
                case "javascript":
                case "js":
                    result = $"text/javascript";
                    break;
                case "php":
                    result = $"text/html";
                    break;
                case "htm":
                    result = $"text/html";
                    break;
                default:
                    result = "application/unknown";
                    break;
            }
            return result;
        }

        void GetSheet(HTTPHeaders head) 
        {
            try
            {
                // тело оператора try
                string content_type = GetContentType(head);
                FileStream fs = new FileStream(head.RealPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                string headers = $"HTTP/1.1 200 OK\nContent-type: {content_type}\nContent-Length: {fs.Length}\n\n";
                // OUTPUT HEADERS    
                byte[] data_headers = Encoding.UTF8.GetBytes(headers);
                client.Send(data_headers, data_headers.Length, SocketFlags.None);
                // OUTPUT CONTENT
                while (fs.Position < fs.Length)
                {
                    byte[] data = new byte[1024];
                    int length = fs.Read(data, 0, data.Length);
                    client.Send(data, data.Length, SocketFlags.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: GetSheet()    link: {head.RealPath}\nException: {ex}/nMessage: {ex.Message}");
            }
        }
    }
}
