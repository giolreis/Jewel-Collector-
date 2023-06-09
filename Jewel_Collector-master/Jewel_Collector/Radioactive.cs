using System;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe que representa um objeto radioativo no jogo.
    /// </summary>
    public class Radioactive : ICell
    {
        /// <summary>
        /// Obtém ou define a cor de fundo do objeto radioativo.
        /// </summary>
        public ConsoleColor BackgroundColor => ConsoleColor.Black;

        /// <summary>
        /// Obtém ou define a cor do texto do objeto radioativo.
        /// </summary>
        public ConsoleColor ForegroundColor => ConsoleColor.Magenta;

        /// <summary>
        /// Obtém o símbolo do objeto radioativo.
        /// </summary>
        public string Symbol { get; } = "!!";
    }
}