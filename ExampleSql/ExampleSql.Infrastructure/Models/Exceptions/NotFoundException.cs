namespace ExampleSql.Infrastructure.Models.Exceptions;

public class NotFoundException(string message) : ApplicationException(message);