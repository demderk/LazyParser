using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LazyParser.CommandHandler
{

    public class CommandProcessorException : Exception
    {
        public CommandProcessorException()
        {
        }

        public CommandProcessorException(string message) : base(message)
        {

        }

        public CommandProcessorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandProcessorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
