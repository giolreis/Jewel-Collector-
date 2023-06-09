using System;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe que representa uma célula vazia no mapa.
    /// </summary>
    public class EmptyCell : ICell
    {
        /// <summary>
        /// Obtém a cor de fundo da célula vazia.
        /// </summary>
        public ConsoleColor BackgroundColor => ConsoleColor.Black;

        /// <summary>
        /// Obtém a cor do texto da célula vazia.
        /// </summary>
        public ConsoleColor ForegroundColor => ConsoleColor.White;

        /// <summary>
        /// Obtém o símbolo que representa uma célula vazia.
        /// </summary>
        public string Symbol { get; } = "--";
    }
}