using Business.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
public static class ExceptionManager
{
    public static void ThrowItemNotFoundException(string entityName, object key)
    {
        var message = ValidationMessages.GetNotFoundMessage(entityName, key.ToString()!);
        throw new NotFoundException(entityName, key, message);
    }

    public static void ThrowValidationException(string field, string message)
    {
        throw new ValidationException($"Validation failed on field '{field}': {message}");
    }

    public static void ThrowDuplicationException(string entityName, object duplicateValue)
    {
        var message = ValidationMessages.GetDuplicateMessage(entityName, duplicateValue.ToString()!);
        message += "you can add the quantity instead of duplicate the item.";
        throw new ValidationException(message);
    }
}
