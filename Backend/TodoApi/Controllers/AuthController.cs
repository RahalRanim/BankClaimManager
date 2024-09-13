using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Web;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpPost("authenticate")]
    public Client  Authenticate([FromBody] LoginModel loginModel)
    {
        Client client = new Client();
        String constr = @"Server=RANIMS-PC1;Database=Gestion_Rec;Integrated Security=true;TrustServerCertificate=True";
        SqlConnection connection = new SqlConnection(constr);
        
        try
        {
            using (connection)
            {
                connection.Open();
                var sql = @"select * from client where login='"+loginModel.Login+"' and mtp='"+loginModel.Mtp+"'";
                SqlCommand command = new SqlCommand(sql, connection);
                

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    client.IdClient = Convert.ToInt32(reader[0]);
                    client.NomClient = reader[1].ToString();
                    client.Prenom = reader[2].ToString();
                    client.Login = reader[3].ToString();
                    client.Mtp = reader[4].ToString();
                    client.NumCIN = reader[5].ToString();
                    client.Tel = reader[6].ToString();
                    client.Type = reader[7].ToString();

                    client.Status = "OK";
                    return  client;
                }
                else
                {
                    client.Status = "NotOK";
                    return client;
                }
            }
        }
        catch (Exception ex)
        {
            client.Status = "NotOK";
            Console.WriteLine(ex.Message);
            return client;
        }

      
    }
}

public class LoginModel
{
    public string Login { get; set; }
    public string Mtp { get; set; }
}
