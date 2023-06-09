using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe que representa uma joia no jogo.
    /// </summary>
    public class Jewel : ICell
    {
        /// <summary>
        /// Obtém a cor de fundo da joia.
        /// </summary>
        public ConsoleColor BackgroundColor => ConsoleColor.Black;

        /// <summary>
        /// Obtém a cor do texto (ou símbolo) da joia.
        /// </summary>
        public ConsoleColor ForegroundColor => ConsoleColor.Yellow;

        /// <summary>
        /// Obtém o símbolo que representa a joia.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Obtém a quantidade de pontos que a joia concede.
        /// </summary>
        public int Points { get; }

        /// <summary>
        /// Cria uma nova instância da classe Jewel com base no tipo especificado.
        /// </summary>
        /// <param name="type">O tipo de joia.</param>
        public Jewel(JewelType type)
        {
            Symbol = GetSymbol(type);
            Points = GetPoints(type);
        }

        private static string GetSymbol(JewelType type)
        {
            return type switch
            {
                JewelType.Red => "JR",
                JewelType.Green => "JG",
                JewelType.Blue => "JB",
                _ => ""
            };
        }

        private static int GetPoints(JewelType type)
        {
            return type switch
            {
                JewelType.Red => 100,
                JewelType.Green => 50,
                JewelType.Blue => 10,
                _ => 0
            };
        }
    }
}