namespace CancherksWebApp.Model
{
    public class InstallationScheduleViewModel
    {
        public int IdInstallation { get; set; }
        public string InstallationName { get; set; }
        public string Picture { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int MaxCantPeople { get; set; }
        public double TimeSplitReservation { get; set; } // Assuming this is a float as per previous discussions
        public TimeSpan StartTime { get; set; } // Assuming this is a TimeSpan to represent time
        public TimeSpan EndTime { get; set; } // Assuming this is a TimeSpan to represent time
        public int IdDay { get; set; }
        public string DayName { get; set; }
    }
}
