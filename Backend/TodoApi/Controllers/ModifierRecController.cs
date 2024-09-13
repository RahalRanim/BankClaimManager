using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModifierRecController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModifierRecController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("modifierreclamation/{clientId}/{idRec}")]
        public async Task<IActionResult> ModifierReclamation(string clientId, int idRec, [FromBody] ReclamationUpdate reclamationModel)
        {
            string constr = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                try
                {
                    connection.Open();
                    var sql = @"UPDATE Reclamation 
                            SET servs = @Servs, 
                                canal_reception = @CanalReception, 
                                descpt = @Descpt, 
                                mail = @Mail, 
                                TelRec = @TelRec
                            WHERE idRec = @IdRec AND idClient = @IdClient";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Servs", reclamationModel.Servs);
                    command.Parameters.AddWithValue("@CanalReception", reclamationModel.CanalReception);
                    command.Parameters.AddWithValue("@Descpt", reclamationModel.Descpt);
                    command.Parameters.AddWithValue("@Mail", reclamationModel.Mail);
                    command.Parameters.AddWithValue("@TelRec", reclamationModel.TelRec);
                    command.Parameters.AddWithValue("@IdRec", idRec);
                    command.Parameters.AddWithValue("@IdClient", clientId);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "Réclamation modifiée avec succès" });
                    }
                    else
                    {
                        return NotFound(new { message = "Réclamation non trouvée ou non modifiée" });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "Erreur lors de la modification de la réclamation", error = ex.Message });
                }
            }
        }
    }

}
