namespace Life
{
    public static class AnalizerMap
    {
        public static double AlivePercent(Map map)
        {
            return map.LiveDensity;
        }

        public static bool VerticalSymmety(Map map)
        {
            int midX = map.Colums / 2;

            for (int xLeft = 0, xRight = map.Colums - 1; xLeft < midX; xLeft++, xRight--)
            {
                for (int y = 0; y < map.Rows; y++)
                    if (map.Cells[xLeft, y].IsAlive != map.Cells[xRight, y].IsAlive)
                        return false;
            }

            return true;
        }

        public static bool HorizontalSymmety(Map map)
        {
            int midY = map.Rows / 2;

            for (int yUp = 0, yDown = map.Rows - 1; yUp < midY; yUp++, yDown--)
            {
                for (int x = 0; x < map.Colums; x++)
                    if (map.Cells[x, yUp].IsAlive != map.Cells[x, yDown].IsAlive)
                        return false;
            }

            return true;
        }

        public static int CheckStabilitySystem(Board board, uint countIteration)
        {
            Board copyBoard = new Board(board.Settings, board.AlgConnect);

            for (int i = 0; i < countIteration; i++)
            {
                copyBoard.Advance();
                if (copyBoard.Cells.Equals(board.Cells))
                    return i + 1;
            }

            return -1;
        }

        public static int Classification(Board figExp, Board figAct)
        {
            int count = 0;
            bool equalsCells = false;

            for (int y = 0; y < figAct.Rows; y++)
            {
                for (int x = 0; x < figExp.Colums; x++)
                {
                    int yAbs = x, yRel = 0;
                    for (; yRel < figAct.Rows; yAbs++, yRel++)
                    {
                        if (yAbs >= figExp.Rows)
                            yAbs = 0;

                        int xAbs = y, xRel = 0;

                        if (figExp.Cells[xAbs, yAbs].IsAlive != figAct.Cells[xRel, yRel].IsAlive)
                        {
                            equalsCells = false;
                            break;
                        }
                        else
                            equalsCells = true;

                        for (; xRel < figAct.Colums; xAbs++, xRel++)
                        {
                            if (xAbs >= figExp.Colums)
                                xAbs = 0;

                            if (figExp.Cells[xAbs, yAbs].IsAlive != figAct.Cells[xRel, yRel].IsAlive)
                            {
                                equalsCells = false;
                                break;
                            }
                            else
                                equalsCells = true;
                        }
                    }

                    if (equalsCells)
                        count++;
                }
            }
            return count;
        }
    }
}
