using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe que representa um obstáculo no jogo.
    /// </summary>
    public class Obstacle : ICell
    {
        /// <summary>
        /// Obtém ou define a cor de fundo do obstáculo.
        /// </summary>
        public ConsoleColor BackgroundColor => ConsoleColor.Black;

        /// <summary>
        /// Obtém ou define a cor do texto do obstáculo.
        /// </summary>
        public ConsoleColor ForegroundColor => ConsoleColor.Gray;

        /// <summary>
        /// Obtém o símbolo do obstáculo.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Obtém a quantidade de pontos de energia fornecidos pelo obstáculo.
        /// </summary>
        public int EnergyPoints { get; }

        /// <summary>
        /// Cria uma nova instância da classe Obstacle.
        /// </summary>
        /// <param name="type">O tipo do obstáculo.</param>
        public Obstacle(ObstacleType type)
        {
            Symbol = GetSymbol(type);
            EnergyPoints = GetEnergyPoints(type);
        }

        /// <summary>
        /// Obtém o símbolo correspondente ao tipo de obstáculo.
        /// </summary>
        /// <param name="type">O tipo do obstáculo.</param>
        /// <returns>O símbolo correspondente ao tipo de obstáculo.</returns>
        private static string GetSymbol(ObstacleType type)
        {
            return type switch
            {
                ObstacleType.Water => "##",
                ObstacleType.Tree => "$$",
                _ => ""
            };
        }
        
        /// <summary>
        /// Obtém a quantidade de pontos de energia fornecidos pelo tipo de obstáculo.
        /// </summary>
        /// <param name="type">O tipo do obstáculo.</param>
        /// <returns>A quantidade de pontos de energia fornecidos pelo tipo de obstáculo.</returns>
        private static int GetEnergyPoints(ObstacleType type)
        {
            return type switch
            {
                ObstacleType.Water => 0, // A água não fornece energia
                ObstacleType.Tree => 3, // Cada árvore fornece 3 pontos de energia
                _ => 0
            };
        }
    }
}
