using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Exceptions
{
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string entity) : base ($"{entity} vec postoji")
        {
        }
    }
}
