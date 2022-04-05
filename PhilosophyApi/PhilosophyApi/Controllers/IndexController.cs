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
            public int[] checkID { get; set; }
        }

        public PhilosophyDBModel SQLQuery(RequestData data)
        {
            sqlCon.Open();

            DataTable tempTable = new DataTable();
            tempTable.Columns.Add(new DataColumn("ID", typeof(int)));

            foreach (var id in data.checkID)
                tempTable.Rows.Add(id);

            SqlCommand command = new SqlCommand("dbo.SPNewStr", sqlCon);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter tempTableParams = command.Parameters.AddWithValue("@ids", tempTable);

            tempTableParams.SqlDbType = SqlDbType.Structured;
            tempTableParams.TypeName = "dbo.IDList";

            command.Parameters.AddWithValue("@type", data.type);


            PhilosophyDBModel answer = new PhilosophyDBModel(command.ExecuteReader());

            sqlCon.Close();
 
            return answer;
        }

        [HttpPost]
        public JsonResult PostTodoItem(RequestData data)
        {
            PhilosophyDBModel answer = null;

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
