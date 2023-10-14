namespace CancherksWebApp.Model
{
    public class InstallationAvailabilityViewModel
    {

        public TimeSpan StartTime { get; set; } // Assuming this is a TimeSpan to represent time
        public TimeSpan EndTime { get; set; } // Assuming this is a TimeSpan to represent time
        public Boolean IsAvailable { get; set; }
    }
}
