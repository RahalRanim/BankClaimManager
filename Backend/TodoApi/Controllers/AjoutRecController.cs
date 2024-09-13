using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using TodoApi.Models;

[Route("api/[controller]")]
[ApiController]
public class AjoutRecController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AjoutRecController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("ajoutreclamation")]
    public async Task<IActionResult> AjoutReclamation([FromBody] Reclamation reclamationModel)
    {
        String constr = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";
        SqlConnection connection = new SqlConnection(constr);

        try
        {
            using (connection)
            {
                connection.Open();
                var sql = @"INSERT INTO Reclamation (servs, canal_reception, descpt, mail, TelRec, Etat, idClient) 
                            VALUES (@Servs, @CanalReception, @Descpt, @Mail, @TelRec, @Etat, @IdClient)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Servs", reclamationModel.Servs);
                command.Parameters.AddWithValue("@CanalReception", reclamationModel.CanalReception);
                command.Parameters.AddWithValue("@Descpt", reclamationModel.Descpt);
                command.Parameters.AddWithValue("@Mail", reclamationModel.Mail);
                command.Parameters.AddWithValue("@TelRec", reclamationModel.TelRec);
                command.Parameters.AddWithValue("@Etat", reclamationModel.Etat);
                command.Parameters.AddWithValue("@IdClient", reclamationModel.IdClient);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return Ok(new { message = "OK" });
                }
                else
                {
                    return StatusCode(500, new { message = "NOT OK" });
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "NOT OK" });
        }
    }
}


