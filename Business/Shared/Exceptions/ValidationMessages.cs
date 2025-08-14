using System;


namespace Business.Shared.Exceptions
{
    public static class ValidationMessages
    {
        public static string GetNotFoundMessage(string entityName, string id) =>
            $"{entityName} with ID '{id}' was not found.";

        public static string GetAlreadyExistsMessage(string entityName, string value) =>
            $"{entityName} with value '{value}' already exists.";

        public static string GetAlreadyDeletedMessage(string entityName, string value) =>
            $"{entityName} with value '{value}' already deleted.";

        public static string GetDuplicateMessage(string entityName, string duplicateValue) =>
            $"Duplicate item found: {entityName} with value '{duplicateValue}'.";

    }

}
