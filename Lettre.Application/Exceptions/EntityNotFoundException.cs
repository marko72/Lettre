using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity) : base($"{entity} ne postoji.")
        {

        }

        public EntityNotFoundException()
        {

        }
    }
}
