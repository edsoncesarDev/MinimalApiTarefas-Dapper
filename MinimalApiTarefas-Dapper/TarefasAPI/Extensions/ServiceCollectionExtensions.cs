using System.Data.SqlClient;
using static TarefasAPI.Data.TarefaContext;

namespace TarefasAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddPersistense(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Connection");
            builder.Services.AddScoped<GetConnection>(sp => async () => {

                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
                
            });

            return builder;
        }
    }
}
