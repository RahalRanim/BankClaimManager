using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Web;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailReclamationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetailReclamationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idClient}/{idRec}")]
        public async Task<ActionResult<object>> GetReclamationDetail(int idClient, int idRec)
        {
            String constr = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(constr);
            try
            {
                using (connection)
                {
                    connection.Open();
                    var sql = @"select c.MoyenPayement, r.servs, r.Etat, r.canal_reception, r.descpt
                                from dbo.Reclamation r, dbo.Compte c
                                   where r.idClient = @idClient and r.idRec = @idRec and c.idClient=@idClient;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idClient", idClient);
                        command.Parameters.AddWithValue("@idRec", idRec);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                var reclamationDetail = new
                                {
                                    MoyenPayement = reader["MoyenPayement"].ToString(),
                                    Descpt = reader["descpt"].ToString(),
                                    serv = reader["servs"].ToString(),
                                    Etat = reader["Etat"].ToString(),
                                    canal_reception = reader["canal_reception"].ToString(),
                                };

                                return reclamationDetail;
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
