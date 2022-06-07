using System;

namespace Domain.Exceptions
{
    [Serializable]
    public class ViaCepException : Exception
    {
        public ViaCepException() { }

        public ViaCepException(string message)
            : base(message) { }

        public ViaCepException(string message, Exception inner)
            : base(message, inner) { }
    }
}
