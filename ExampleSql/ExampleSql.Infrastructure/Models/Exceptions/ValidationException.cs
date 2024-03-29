namespace ExampleSql.Infrastructure.Models.Exceptions;

public class ValidationException(string message) : ApplicationException(message);