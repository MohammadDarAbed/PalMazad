

namespace Business.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public string EntityName { get; }
        public object Key { get; }

        public NotFoundException(string entityName, object key)
            : base($"{entityName} with identifier {key} was not found.")
        {
            EntityName = entityName;
            Key = key;
        }

        public NotFoundException(string entityName, object key, string customMessage)
            : base(customMessage)
        {
            EntityName = entityName;
            Key = key;
        }
    }
}
