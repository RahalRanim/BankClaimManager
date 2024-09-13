using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListReclamationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListReclamationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{idClient}")]
        public async Task<ActionResult<IEnumerable<object>>> GetReclamations(int idClient)
        {
            List<object> reclamationsList = new List<object>();
            String constr = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(constr);
            try
            {
                using (connection)
                {
                    connection.Open();
                    var sql = @"SELECT r.idRec,r.servs, r.servs, r.canal_reception, r.mail, r.TelRec, c.MoyenPayement, r.descpt, r.Etat
                        FROM dbo.Reclamation r
                        INNER JOIN dbo.Compte c ON r.idClient = c.idClient
                        INNER JOIN dbo.Client cl ON r.idClient = cl.idClient
                        WHERE cl.idClient = @idClient;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idClient", idClient);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var reclamation = new
                                {
                                    IdRec = Convert.ToInt32(reader["idRec"]),
                                    MoyenPayement = reader["MoyenPayement"].ToString(),
                                    CanalReception = reader["canal_reception"].ToString(),
                                    Servs = reader["servs"].ToString(),
                                    Mail = reader["mail"].ToString(),
                                    TelRec = reader["TelRec"].ToString(),
                                    Descpt = reader["descpt"].ToString(),
                                    Etat = reader["Etat"].ToString()
                                };

                                reclamationsList.Add(reclamation);
                            }
                        }
                    }
                }

                return Ok(reclamationsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
