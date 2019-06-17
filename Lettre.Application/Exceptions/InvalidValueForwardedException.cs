using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Exceptions
{
   public class InvalidValueForwardedException : Exception
    {
        public InvalidValueForwardedException()
        {
        }

        public InvalidValueForwardedException(string err) : base(err)
        {
        }
    }
}
