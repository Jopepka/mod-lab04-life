using Life;

namespace cli_life
{
    class Program
    {
        static Board board;
        static private void Reset()
        {
            SettingsMap settings = new SettingsMap(150, 150);
            board = new Board(settings, new SphereConnect(), 0.1);

            board.AddFigure(10, 10, new SettingsMap(3, 1, "111\n"));
        }

        static void Main(string[] args)
        {
            Reset();
            SettingsRender sRB = new SettingsRender();
            sRB.MaxIteration = 100;

            RenderToConsole renderToConsole = new RenderToConsole();
            //sRB.MaxIteration = 5;

            //renderToConsole.Render(board, sRB);
            RenderInScottPlot plot = new RenderInScottPlot();

            for (int i = 0; i < sRB.MaxIteration; i++)
                board.Advance();

            plot.SavePict(board, sRB, "NewImage.png");
        }
    }
}