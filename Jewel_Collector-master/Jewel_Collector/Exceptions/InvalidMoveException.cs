using System;

namespace Jewel_Collector.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um movimento é considerado inválido devido à ocupação da posição por outro item.
    /// </summary>
    public class InvalidMoveException : Exception
    {
        /// <summary>
        /// Cria uma nova instância da exceção InvalidMoveException com a mensagem padrão.
        /// </summary>
        public InvalidMoveException() : base("Movimento inválido. A posição está ocupada por outro item.")
        {
        }
    }
}