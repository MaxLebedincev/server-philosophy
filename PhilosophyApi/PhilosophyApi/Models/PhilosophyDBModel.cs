using System.Data.SqlClient;

namespace PhilosophyApi.Models
{
    
    public class PhilosophyDBModel
    {
        public PhilosophyDBModel(SqlDataReader tb)
        {
            tb.Read();
            id = (int)tb["id"];
            author = (string)tb["author"];
            content = (string)tb["content"];
            type = (string)tb["type"];
        }

        public int id { get; set; }
        public string author { get; set; }
        public string content { get; set; }
        public string type { get; set; }
    }
}
