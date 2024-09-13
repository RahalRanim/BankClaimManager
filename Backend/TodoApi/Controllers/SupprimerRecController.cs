using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupprimerRecController : ControllerBase
    {
        private readonly string _connectionString = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";

        // Méthode pour supprimer une réclamation
        [HttpDelete("{idClient}/{idRec}")]
        public async Task<IActionResult> DeleteReclamation(int idClient, int idRec)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = @"DELETE FROM dbo.Reclamation
                            WHERE idClient = @idClient AND idRec = @idRec;";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@idClient", idClient);
                command.Parameters.AddWithValue("@idRec", idRec);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                   return Ok(new { message = "OK" }); 
                }

                return StatusCode(500, new { message = "NOT OK" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
