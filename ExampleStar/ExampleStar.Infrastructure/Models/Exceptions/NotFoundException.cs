namespace ExampleStar.Infrastructure.Models.Exceptions;

public class NotFoundException(string message) : ApplicationException(message);