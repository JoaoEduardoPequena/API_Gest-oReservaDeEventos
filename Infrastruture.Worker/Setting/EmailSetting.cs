namespace Infrastruture.Worker.Setting
{
    public class EmailSetting
    {
        public string HostSMT { get; set; }
        public string User { get; set; }
        public string From { get; set; }
        public string PassWord { get; set; }
        public int Port { get; set; }
        public string EmailSubject { get; set; }
    }
}
