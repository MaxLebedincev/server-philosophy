using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhilosophyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhilosophyApi;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using PhilosophyApi.HelperTools;

namespace PhilosophyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public SqlConnection sqlCon;

        public IndexController(IConfiguration conf)
        {
            DataBase DB = new DataBase(conf);

            sqlCon = DB.sqlConnection;
        }

        public class RequestData
        {
            public string type { get; set; }
            //public int[] checkID { get; set; }
        }

        public List<PhilosophyDBModel> SQLQuery(RequestData data)
        {
            sqlCon.Open();
           
            SqlCommand command = new SqlCommand("SELECT * FROM PhilosophyDB WHERE type = @type", sqlCon);

            command.Parameters.AddWithValue("@type", data.type);

            List<PhilosophyDBModel> listM = new List<PhilosophyDBModel>();

            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                listM.Add(new PhilosophyDBModel(reader));
            }

            sqlCon.Close();

            return listM;
        }

        [HttpPost]
        public JsonResult PostTodoItem(RequestData data)
        {
            List<PhilosophyDBModel> answer = null;

            try
            {
                answer = SQLQuery(data);
            }
            catch (Exception ex)
            {
                var AE = new ApiException(ex, "Ошибка при обращении к базе данных.");

                return new JsonResult(AE);
            }

            return new JsonResult(answer);
        }

        [HttpGet]
        public JsonResult Get()
        {
            var AE = new ApiException(null, "Такого запроса быть не могло на этой страничке.", "Ошибки нет, просто запрос GET для этой страницы не существует.");

            return new JsonResult(AE);
        }
    }
}
