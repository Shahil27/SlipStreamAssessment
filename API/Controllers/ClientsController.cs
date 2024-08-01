using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using API.Models;
using System.Data;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IConfiguration Configuration;

        public ClientsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private string GetConnectionString()
        {
            return Configuration.GetConnectionString("MyConn");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients() //public List<Client> GetClients(int? Id)
        {
            List<Client> Clients = new List<Client>();

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    //if (Id != null)
                    //{
                    //    cmd.CommandText = "SELECT * FROM Clients WHERE ClientId = @Id";
                    //    cmd.Parameters.AddWithValue("@Id", Id);
                    //}
                    //else
                        cmd.CommandText = "SELECT * FROM Clients";

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Clients.Add(new Client()
                        {
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Name = reader["Name"].ToString(),
                            ResidentialAddress = reader["ResidentialAddress"].ToString(),
                            WorkAddress = reader["WorkAddress"].ToString(),
                            PostalAddress = reader["PostalAddress"].ToString(),
                            CellNumber = reader["CellNumber"].ToString(),
                            WorkNumber = reader["WorkNumber"].ToString()
                        });
                    }
                    con.Close();
                }
            }

            return Clients;
        }

        // GET api/<ClientsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientsController>
        [HttpPost]
        public async Task<ActionResult<int>> PostClient(Client client)
        {
            string SQL = "INSERT INTO Clients (Name, ResidentialAddress, WorkAddress, PostalAddress, CellNumber, WorkNumber) VALUES (@N, @RA, @WA, @PA, @CN, @WN)";

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.Add("@N", SqlDbType.NVarChar).Value = client.Name;
                    cmd.Parameters.Add("@RA", SqlDbType.NVarChar).Value = client.ResidentialAddress;
                    cmd.Parameters.Add("@WA", SqlDbType.NVarChar).Value = client.WorkAddress;
                    cmd.Parameters.Add("@PA", SqlDbType.NVarChar).Value = client.PostalAddress;
                    cmd.Parameters.Add("@CN", SqlDbType.NVarChar).Value = client.CellNumber;
                    cmd.Parameters.Add("@WN", SqlDbType.NVarChar).Value = client.WorkNumber;

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        [HttpGet("export")]
        public async void ExportClients() 
        {
            List<Client> Clients = new List<Client>();

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT ClientId, Name, ResidentialAddress, WorkAddress, PostalAddress FROM Clients";

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Clients.Add(new Client()
                        {
                            ClientId = Convert.ToInt32(reader["ClientId"]),
                            Name = reader["Name"].ToString(),
                            ResidentialAddress = reader["ResidentialAddress"].ToString(),
                            WorkAddress = reader["WorkAddress"].ToString(),
                            PostalAddress = reader["PostalAddress"].ToString()                 
                        });
                    }
                    con.Close();
                }
            }

            var fileName = "ClientsExportFile" + DateTime.Now.ToString("dd-MM-yyyy-HH_mm_ss");
            var sb = new StringBuilder();
            var basePath =  AppDomain.CurrentDomain.BaseDirectory; //"C:";
            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var header = "";
            var info = typeof(Client).GetProperties();

            if (!System.IO.File.Exists(finalPath))
            {
                var file = System.IO.File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(Client).GetProperties())
                {
                    header += prop.Name + ", ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in Clients)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + ", ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        // PUT api/<ClientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
