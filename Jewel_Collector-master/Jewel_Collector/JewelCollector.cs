using System;
using Jewel_Collector.Enums;
using Jewel_Collector.Exceptions;

namespace Jewel_Collector
{
    /// <summary>
    /// Classe principal do jogo Jewel Collector.
    /// </summary>
    public class JewelCollector
    {
        /// <summary>
        /// Método de entrada do jogo.
        /// </summary>
        public static void Main()
        {
            var map = new Map(10);

            map.SetCell(1, 9, new Jewel(JewelType.Red));
            map.SetCell(8, 8, new Jewel(JewelType.Red));
            map.SetCell(9, 1, new Jewel(JewelType.Green));
            map.SetCell(7, 6, new Jewel(JewelType.Green));
            map.SetCell(3, 4, new Jewel(JewelType.Blue));
            map.SetCell(2, 1, new Jewel(JewelType.Blue));

            map.SetCell(5, 0, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 1, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 2, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 3, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 4, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 5, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 6, new Obstacle(ObstacleType.Water));
            map.SetCell(5, 9, new Obstacle(ObstacleType.Tree));
            map.SetCell(3, 9, new Obstacle(ObstacleType.Tree));
            map.SetCell(8, 3, new Obstacle(ObstacleType.Tree));
            map.SetCell(2, 5, new Obstacle(ObstacleType.Tree));
            map.SetCell(1, 4, new Obstacle(ObstacleType.Tree));

            var robot = new Robot(0, 0, map);

            while (true)
            {
                map.PrintMap(robot);
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'w':
                        HandleRobotMovement(robot, robot.X - 1, robot.Y);
                        break;
                    case 's':
                        HandleRobotMovement(robot, robot.X + 1, robot.Y);
                        break;
                    case 'a':
                        HandleRobotMovement(robot, robot.X, robot.Y - 1);
                        break;
                    case 'd':
                        HandleRobotMovement(robot, robot.X, robot.Y + 1);
                        break;
                    case 'g':
                        robot.InteractWithAdjacentItems();
                        if (map.GetTotalJewels() == 0)
                        {
                            map.IncreaseSize();
                            map.IncrementPhase();
                            map.RandomizeItems();

                            var robotX = robot.X;
                            var robotY = robot.Y;
                            
                            // Procurar a próxima célula vazia adjacente
                            var adjacentPositions = robot.GetAdjacentPositions();
                            foreach (var (adjX, adjY) in adjacentPositions)
                            {
                                if (!map.IsWithinBounds(adjX, adjY) || map.GetCell(adjX, adjY) is not EmptyCell)
                                    continue;
                                robotX = adjX;
                                robotY = adjY;
                                break;
                            }
                            
                            robot.Move(robotX, robotY);
                        }

                        break;
                    default:
                        return;
                }
            }
        }

        /// <summary>
        /// Lida com o movimento do robô.
        /// </summary>
        /// <param name="robot">O robô.</param>
        /// <param name="newX">A nova coordenada X.</param>
        /// <param name="newY">A nova coordenada Y.</param>
        private static void HandleRobotMovement(Robot robot, int newX, int newY)
        {
            try
            {
                robot.Move(newX, newY);

                var map = robot.GetMap();
                var destinationCell = map.GetCell(newX, newY);
                var phase = map.GetPhase();

                var adjacentPositions = robot.GetAdjacentPositions();
                foreach (var (adjX, adjY) in adjacentPositions)
                {
                    if (!map.IsWithinBounds(adjX, adjY)) continue;
                    var cell = map.GetCell(adjX, adjY);
                    if (cell is Radioactive)
                    {
                        robot.LoseEnergy(10);
                    }
                }

                if (phase < 2 || destinationCell is not Radioactive) return;
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'g')
                {
                    robot.TransposeRadioactive(newX, newY);
                }
            }
            catch (OutOfMapBoundsException)
            {
                Console.WriteLine("ERRO: Movimento inválido. A posição está fora dos limites do mapa.");
                throw;
            }
            catch (InvalidMoveException)
            {
                Console.WriteLine("ERRO: Movimento inválido. A posição está ocupada por outro item.");
                throw;
            }
        }
    }
}