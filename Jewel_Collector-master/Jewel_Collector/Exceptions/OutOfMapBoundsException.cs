using System;

namespace Jewel_Collector.Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma posição está fora dos limites do mapa.
    /// </summary>
    public class OutOfMapBoundsException : Exception
    {
        /// <summary>
        /// Cria uma nova instância da exceção OutOfMapBoundsException com a mensagem padrão.
        /// </summary>
        public OutOfMapBoundsException() : base("A posição está fora dos limites do mapa.")
        {
        }
    }
}