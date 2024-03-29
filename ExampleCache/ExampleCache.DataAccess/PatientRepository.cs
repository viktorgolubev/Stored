using System.Data;
using System.Text;
using Dapper;
using ExampleCache.DataAccess.Extensions;
using ExampleCache.Infrastructure.Interfaces.Repositories;
using ExampleCache.Infrastructure.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ExampleCache.DataAccess;

public class PatientRepository(IConfiguration configuration) : IPatientRepository
{
    private const string GetPatientSql = @"SELECT Id, FirstName, LastName, Gender, DateOfBirth, ZipCode, City, State
                                           FROM Patients
                                           WHERE Id = @Id;";

    private const string GetHinsSql = @"SELECT Number, Code
                                        FROM Hins
                                        WHERE PatientId = @Id;";

    private readonly string _connectionString = configuration.GetConnectionString("Default")
                                                ?? throw new ApplicationException("Missing Default connection string");

    public async Task<PatientEntity?> GetPatientByIdAsync(int id, CancellationToken token)
    {
        await using MySqlConnection connection = new MySqlConnection(_connectionString);
        CommandDefinition command = new CommandDefinition(
            commandText: GetPatientSql +
                         GetHinsSql,
            parameters: new { Id = id },
            commandType: CommandType.Text,
            cancellationToken: token);

        SqlMapper.GridReader reader = await connection.QueryMultipleAsync(command);

        PatientEntity? patient = await reader.ReadFirstOrDefaultAsync<PatientEntity>();
        if (patient is null) return null;

        patient.Hins = (await reader.ReadAsync<HinEntity>()).ToList();
        return patient;
    }

    public async Task<List<PatientEntity>> SearchPatientsAsync(string? firstName, string lastName,
        CancellationToken token)
    {
        DynamicParameters parameters = new();
        parameters.Add("LastNameNormalized", $"%{lastName.ToNormalize()}%");
        
        StringBuilder commandBuilder = new("SELECT Id, FirstName, LastName, Gender, DateOfBirth, ZipCode, City, State " +
                                           "FROM Patients " +
                                           "WHERE IsDeleted = 0 " +
                                           "AND LastNameNormalized LIKE @LastNameNormalized ");
        
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            parameters.Add("FirstNameNormalized", $"%{firstName.ToNormalize()}%");
            commandBuilder.Append("AND FirstNameNormalized LIKE @FirstNameNormalized ");
        }

        commandBuilder.Append(';');
        
        CommandDefinition command = new(
            commandText: commandBuilder.ToString(),
            parameters,
            commandType: CommandType.Text,
            cancellationToken: token);

        await using MySqlConnection connection = new MySqlConnection(_connectionString);
        return (await connection.QueryAsync<PatientEntity>(command))
            .ToList();
    }
}