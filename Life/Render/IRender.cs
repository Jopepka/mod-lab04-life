namespace Life
{
    public interface IRender
    {
        public void Render(Board board, SettingsRender settingsBoard);

        public void RenderStep(Board board, SettingsRender settingsBoard);
    }
}
