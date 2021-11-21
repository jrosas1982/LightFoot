using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dominio
{
    [Serializable]
    public class ExcepcionControlada : Exception
    {
        public ExcepcionControlada()
        { }

        public ExcepcionControlada(string message)
            : base(message)
        { }

        public ExcepcionControlada(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
