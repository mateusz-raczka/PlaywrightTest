namespace PlaywrightTests.Config
{
    public class PlaywrightConfig
    {
        public string[] Browsers { get; set; }
        public bool Headless { get; set; }
        public int SlowMo { get; set; }
        public ViewportSettings Viewport { get; set; }
    }

    public class ViewportSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
