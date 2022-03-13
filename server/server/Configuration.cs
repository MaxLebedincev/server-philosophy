using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhilosophyServer
{
    class Configuration
    {
        public static string RealPath { get; set; }
        public static string PathFront { get; set; }
        public static string NameDB { get; set; }
        public static string UserNameDB { get; set; }
        public static string UserPasswordDB { get; set; }
        public static string ServerNameDB { get; set; }

        public string _pathFront { get; set; }

        public string _nameDB { get; set; }
        public string _userNameDB { get; set; }
        public string _userPasswordDB { get; set; }
        public string _serverNameDB { get; set; }

    }

    class Config
    {

        public Config()
        {

            Configuration.RealPath = Regex.Match(Environment.CurrentDirectory, @"([\W\w]+)server(?=(\W)server)", RegexOptions.Multiline).Value;
            string json = ReadFile(Configuration.RealPath + "/Config.json");

            Configuration config = JsonConvert.DeserializeObject<Configuration>(json);

            Configuration.PathFront = config._pathFront;
            Configuration.UserNameDB = config._userNameDB;
            Configuration.UserPasswordDB = config._userPasswordDB;
            Configuration.ServerNameDB = config._serverNameDB;
            Configuration.NameDB = config._nameDB;
        }

        private string ReadFile(string pathConfig)
        {
            string answer = "";
            try
            {
                StreamReader sr = new StreamReader(pathConfig);

                string line = sr.ReadLine();
                answer = line;

                while (line != null)
                {
                    line = sr.ReadLine();
                    answer += line;
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return answer;
        }

    }
}