namespace Chrome.Configuration
{
    public static class Config
    {
        public static string InteractKey { get; set; } = ".";
        public static string ProcessName { get; set; } = "WoW";
        public static int VolumeTreshold { get; set; } = 30;
        public static int DelayInMs { get; set; } = 1500;
        public static int CastFailChance { get; set; } = 85;
    }
}
