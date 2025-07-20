using Microsoft.Extensions.Configuration;
using Seller.Clases;
using System;
using System.Data;
using System.Data.SqlClient;

public class ESeller
{
    private readonly string _connectionString;

    public ESeller(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ESellerDb");
    }

    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<Cliente>> ObtenerClientesAsync()
    {
        var clientes = new List<Cliente>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var command = new SqlCommand("S_Clientes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var cliente = new Cliente
                        {
                            cliente = reader.GetInt32(0),          
                            nombre = reader.GetString(1)     
                        };

                        clientes.Add(cliente);
                    }
                }
            }
        }

        return clientes;
    }

}
