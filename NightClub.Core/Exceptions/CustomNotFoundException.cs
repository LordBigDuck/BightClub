using System;
using System.Collections.Generic;
using System.Text;

namespace NightClub.Core.Exceptions
{
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException() { }

        public CustomNotFoundException(string message) : base(message) { }
    }
}
