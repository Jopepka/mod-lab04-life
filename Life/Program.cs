using System;
using System.Threading;
using Life;

namespace cli_life
{
    class Program
    {
        static Board board;
        static private void Reset()
        {
            SettingsBoard settings = new SettingsBoard();
            settings.SetBoardStr(0.01);
            board = new Board(settings);
        }

        static void Main(string[] args)
        {
            Reset();
            RenderToConsole renderToConsole = new RenderToConsole();
            SettingsRenderBoard sRB = new SettingsRenderBoard();
            int steps = 0;
            while (steps < 10)
            {
                steps++;
                Console.Clear();
                renderToConsole.Render(board, sRB);
                board.Advance();
                Thread.Sleep(100);
            }
        }
    }
}