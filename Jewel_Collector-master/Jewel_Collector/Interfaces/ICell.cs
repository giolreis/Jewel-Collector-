using System;

namespace Jewel_Collector.Interfaces
{
    public interface ICell
    {
        ConsoleColor BackgroundColor { get; }
        ConsoleColor ForegroundColor { get; }
        string Symbol { get; }
    }
}