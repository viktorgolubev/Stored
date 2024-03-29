using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using ExampleDuo.Infrastructure.Interfaces.Repositories;
using ExampleDuo.Infrastructure.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace ExampleDuo.DataAccess;

public class PatientRepositoryStoredProcedures(IConfiguration configuration) : IPatientRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("Default")
                                                ?? throw new ApplicationException("Missing Default connection string");
    public async Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token)
    {
        await using MySqlConnection connection = new MySqlConnection(_connectionString);
        CommandDefinition command = new(
            commandText: "Patient_GetById",
            parameters: new { Id = id },
            commandType: CommandType.StoredProcedure,
            cancellationToken: token
        );
        
        SqlMapper.GridReader reader = await connection.QueryMultipleAsync(command);

        PatientEntity? patient = await reader.ReadFirstOrDefaultAsync<PatientEntity>();
        if (patient is null)
        {
            return null;
        }
        
        patient.Hins = (await reader.ReadAsync<HinEntity>()).ToList();
        return patient;
    }

    public async Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName, CancellationToken token)
    {
        await using MySqlConnection connection = new MySqlConnection(_connectionString);
        CommandDefinition command = new(
            commandText: "Patient_FindByFilter",
            parameters: new { FirstName = firstName, LastName = lastName },
            commandType: CommandType.StoredProcedure,
            cancellationToken: token);
        
        return (await connection.QueryAsync<PatientEntity>(command))
            .ToList();
    }
}