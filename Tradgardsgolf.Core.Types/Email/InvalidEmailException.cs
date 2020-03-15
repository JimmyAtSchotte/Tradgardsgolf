using System;

namespace Tradgardsgolf.Core.Email
{
    [Serializable]
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email) : base($"{email} is not an valid email address") { }
    }
}
