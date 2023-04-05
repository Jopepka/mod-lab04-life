namespace Life
{
    public class SettingsRenderBoard
    {
        public char SymbLive { get; set; } = '#';
        public char SymbDead { get; set; } = ' ';
        public int HightMap { get; set; } = 240;
        public int WightMap { get; set; } = 160;
        public int? sizeCell { get; set; } = null;
    }
}
