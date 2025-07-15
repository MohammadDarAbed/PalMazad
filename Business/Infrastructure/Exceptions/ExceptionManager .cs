using Business.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;
public static class ExceptionManager
{
    public static void ThrowItemNotFoundException(string entityName, object key)
    {
        var message = ValidationMessages.GetNotFoundMessage(entityName, key.ToString());
        throw new NotFoundException(entityName, key, message);
    }

    public static void ThrowValidationException(string field, string message)
    {
        throw new ValidationException($"Validation failed on field '{field}': {message}");
    }
}
