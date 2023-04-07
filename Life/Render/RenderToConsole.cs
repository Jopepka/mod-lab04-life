using System;
using System.Threading;

namespace Life
{
    public class RenderToConsole : IRender
    {
        public void Render(Board board, SettingsRender settingsRender)
        {
            for (int i = 0; i < settingsRender.MaxIteration; i++)
            {
                Console.Clear();

                RenderStep(board, settingsRender);
                board.Advance();

                Thread.Sleep(settingsRender.TimeDelay);
            }
        }

        public void RenderStep(Board board, SettingsRender settingsRender)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Colums; col++)
                {
                    var cell = board.Cells[col, row];
                    if (cell.IsAlive)
                        Console.Write(settingsRender.SymbLive);
                    else
                        Console.Write(settingsRender.SymbDead);
                }
                Console.Write('\n');
            }
        }
    }
}
