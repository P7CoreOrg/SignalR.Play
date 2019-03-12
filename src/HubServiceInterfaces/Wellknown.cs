namespace HubServiceInterfaces
{
    public static class Wellknown
    {
        public static string ClockHubUrl => "https://localhost:44329/hubs/clock";

        public static class Events
        {
            public static string TimeSent => nameof(IClock.ShowTime);
        }
    }
}