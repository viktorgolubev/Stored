using System.Data;
using System.Text;
using Dapper;
using ExampleDuo.DataAccess.Extensions;
using ExampleDuo.Infrastructure.Interfaces.Repositories;
using ExampleDuo.Infrastructure.Models.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ExampleDuo.DataAccess;


public class PatientRepositorySql(IConfiguration configuration) : IPatientRepository
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
        (string commandText, DynamicParameters parameters) = GetSearchPatientRequestElements(firstName, lastName);
        CommandDefinition command = new(
            commandText: commandText,
            parameters: parameters,
            commandType: CommandType.Text,
            cancellationToken: token);

        return (await connection.QueryAsync<PatientEntity>(command))
            .ToList();
    }

    private static (string commandText, DynamicParameters parameters) GetSearchPatientRequestElements(
        string? firstName,
        string lastName)
    {
        DynamicParameters parameters = new();
        parameters.Add("LastNameNormalized", $"%{lastName.ToNormalize()}%");

        StringBuilder queryBuilder = new("SELECT Id, FirstName, LastName, Gender, DateOfBirth, ZipCode, City, State " +
                                         "FROM Patients " +
                                         "WHERE IsDeleted = 0 " +
										 "AND LastNameNormalized LIKE @LastNameNormalized ");

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            parameters.Add("FirstNameNormalized", $"%{firstName.ToNormalize()}%");
            queryBuilder.Append("AND FirstNameNormalized LIKE @FirstNameNormalized ");
        }

        queryBuilder.Append(';');

        return (queryBuilder.ToString(), parameters);
    }
}