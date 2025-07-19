using Business.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Shared
{
    public static class Validations
    {
        public static async Task CheckIfNotExists<T>(T entity, int id, string domainName)
        {
            if (entity == null)
                throw new NotFoundException(domainName, id);
        }

        public static void CheckIfEntityDeleted(bool isDeleted, int id, string domainName)
        {
            if (isDeleted)
                throw new InvalidOperationException(ValidationMessages.GetAlreadyDeletedMessage(domainName, id.ToString()));

        }
    }
}
