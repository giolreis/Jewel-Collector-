using System;
using System.Collections.Generic;
using Jewel_Collector.Exceptions;
using Jewel_Collector.Interfaces;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe que representa o robô no jogo.
    /// </summary>
    public class Robot : ICell
    {
        private readonly Map map;
        private readonly List<Jewel> Bag;
        private int Score { get; set; }

        /// <summary>
        /// Obtém ou define a cor de fundo do robô.
        /// </summary>
        public ConsoleColor BackgroundColor => ConsoleColor.Black;

        /// <summary>
        /// Obtém ou define a cor do texto do robô.
        /// </summary>
        public ConsoleColor ForegroundColor => ConsoleColor.Magenta;

        /// <summary>
        /// Obtém o símbolo do robô.
        /// </summary>
        public string Symbol { get; } = "ME";

        /// <summary>
        /// Obtém ou define a posição X do robô.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Obtém ou define a posição Y do robô.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Obtém a energia atual do robô.
        /// </summary>
        public int Energy { get; private set; } = 5;

        /// <summary>
        /// Cria uma nova instância da classe Robot.
        /// </summary>
        /// <param name="x">A posição X inicial do robô.</param>
        /// <param name="y">A posição Y inicial do robô.</param>
        /// <param name="map">O mapa do jogo.</param>
        public Robot(int x, int y, Map map)
        {
            X = x;
            Y = y;
            Score = 0;
            this.map = map;
            Bag = new List<Jewel>();
            map.SetCell(x, y, this);
        }

        /// <summary>
        /// Move o robô para uma nova posição no mapa.
        /// </summary>
        /// <param name="newX">A nova posição X do robô.</param>
        /// <param name="newY">A nova posição Y do robô.</param>
        /// <exception cref="OutOfMapBoundsException">Exceção lançada quando a nova posição está fora dos limites do mapa.</exception>
        /// <exception cref="InvalidMoveException">Exceção lançada quando a nova posição está ocupada por outro item.</exception>
        public void Move(int newX, int newY)
        {
            if (!map.IsWithinBounds(newX, newY))
            {
                throw new OutOfMapBoundsException();
            }

            var destinationCell = map.GetCell(newX, newY);

            if (Energy == 0)
            {
                Console.WriteLine("A energia do robô acabou. O jogo terminou.");
                Environment.Exit(0);
            }

            if (!map.IsWithinBounds(newX, newY)) return;
            if (destinationCell is EmptyCell)
            {
                map.SetCell(X, Y, new EmptyCell());
                X = newX;
                Y = newY;
                map.SetCell(X, Y, this);
                Energy--; // Reduz a energia em 1 após o movimento
            }
            else
            {
                throw new InvalidMoveException();
            }
        }

        /// <summary>
        /// Imprime na tela o total de joias coletadas pelo robô e o valor total das joias coletadas.
        /// </summary>
        public void PrintTotalJewels()
        {
            Console.WriteLine("Total de joias coletadas: " + Bag.Count);
            Console.WriteLine("Valor total das joias coletadas: " + Score);
        }

        /// <summary>
        /// Interage com os itens adjacentes ao robô, coletando joias e recarregando energia.
        /// </summary>
        public void InteractWithAdjacentItems()
        {
            CollectJewel();
            var adjacentPositions = GetAdjacentPositions();
            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY)) continue;
                var cell = map.GetCell(adjX, adjY);
                switch (cell)
                {
                    case Jewel:
                        map.SetCell(adjX, adjY, new EmptyCell());
                        break;
                    case Obstacle obstacle:
                        RechargeEnergy(obstacle);
                        break;
                }
            }
        }

        /// <summary>
        /// Obtém as posições adjacentes ao robô.
        /// </summary>
        /// <returns>Uma lista de tuplas representando as posições adjacentes.</returns>
        public List<(int, int)> GetAdjacentPositions()
        {
            var positions = new List<(int, int)>
            {
                (X - 1, Y), // Cima
                (X + 1, Y), // Baixo
                (X, Y - 1), // Esquerda
                (X, Y + 1) // Direita
            };
            return positions;
        }

        private void CollectJewel()
        {
            var adjacentPositions = GetAdjacentPositions();
            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY) || map.GetCell(adjX, adjY) is not Jewel jewel) continue;
                Score += jewel.Points;
                Bag.Add(jewel);

                if (jewel.Symbol == "JB")
                {
                    Energy += 5; // Adiciona 5 pontos de energia para a joia azul
                }

                map.SetCell(adjX, adjY, new EmptyCell());
                return;
            }

            foreach (var (adjX, adjY) in adjacentPositions)
            {
                if (!map.IsWithinBounds(adjX, adjY)) continue;
                var cell = map.GetCell(adjX, adjY);
                if (cell is not Radioactive) continue;
                var penalty = Math.Min(30, Energy); // Calcula a penalidade mínima de energia
                Energy -= penalty;
                map.SetCell(adjX, adjY, new EmptyCell());
            }
        }

        private void RechargeEnergy(Obstacle obstacle)
        {
            Energy += obstacle.EnergyPoints;
        }

        /// <summary>
        /// Perde uma quantidade especificada de energia.
        /// </summary>
        /// <param name="amount">A quantidade de energia a ser perdida.</param>
        public void LoseEnergy(int amount)
        {
            Energy -= amount;
        }

        /// <summary>
        /// Transpõe um objeto radioativo em uma nova posição, reduzindo a energia do robô.
        /// </summary>
        /// <param name="newX">A nova posição X do objeto radioativo.</param>
        /// <param name="newY">A nova posição Y do objeto radioativo.</param>
        public void TransposeRadioactive(int newX, int newY)
        {
            if (Energy < 30) return;
            Energy -= 30;
            map.SetCell(newX, newY, new EmptyCell());
        }

        /// <summary>
        /// Obtém o mapa do jogo.
        /// </summary>
        /// <returns>O mapa do jogo.</returns>
        public Map GetMap()
        {
            return map;
        }
    }
}
