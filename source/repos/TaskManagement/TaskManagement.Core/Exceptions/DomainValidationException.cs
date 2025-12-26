using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Exceptions
{
    public sealed class DomainValidationException : Exception
    {
        public DomainValidationException(string message) : base(message) { }
    }
}
