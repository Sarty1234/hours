using Exiled.API.Interfaces;

namespace hours
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        public int seconds_between_scan { get; set; } = 120;
        public string file_name { get; set; } = "data.json";
        public string custom_prefix = "Легенда ";
        public float hours_for_prefix = 200;
    }

}
