using System;

namespace Life
{
    public class RenderToConsole : IRender
    {
        public void Render(Board board, SettingsRenderBoard settingsBoard)
        {
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Colums; col++)
                {
                    var cell = board.Cells[col, row];
                    if (cell.IsAlive)
                    {
                        Console.Write(settingsBoard.SymbLive);
                    }
                    else
                    {
                        Console.Write(settingsBoard.SymbDead);
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
