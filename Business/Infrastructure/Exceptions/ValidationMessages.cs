using System;


namespace Business.Infrastructure.Exceptions
{
    public static class ValidationMessages
    {
        public static string GetNotFoundMessage(string entityName, string id) =>
            $"{entityName} with ID '{id}' was not found.";

        public static string GetAlreadyExistsMessage(string entityName, string value) =>
            $"{entityName} with value '{value}' already exists.";

    }

}
