using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhilosophyProject
{
    class Configuration
    {
        public static string RealPath { get; set; }
        public static string PathFront { get; set; }
        public string _pathFront { get; set; }
    }

    class Config
    {

        public Config()
        {

            Configuration.RealPath = Regex.Match(Environment.CurrentDirectory, @"([\W\w]+)server(?=(\W)server)", RegexOptions.Multiline).Value;
            string json = ReadFile(Configuration.RealPath + "/Config.json");

            Configuration config = JsonConvert.DeserializeObject<Configuration>(json);

            Configuration.PathFront = config._pathFront;
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
