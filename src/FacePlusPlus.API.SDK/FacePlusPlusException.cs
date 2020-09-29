using System;
using System.Runtime.Serialization;

namespace FacePlusPlus.API.SDK
{
    [Serializable]
    public class FacePlusPlusException : Exception
    {
        public FacePlusPlusException()
        {
        }

        public FacePlusPlusException(string message) : base(message)
        {
        }

        public FacePlusPlusException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FacePlusPlusException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}