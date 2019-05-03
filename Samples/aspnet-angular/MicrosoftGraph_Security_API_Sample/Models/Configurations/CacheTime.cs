namespace MicrosoftGraph_Security_API_Sample.Models.Configurations
{
    public class CacheTime
    {
        public CacheTime(
            int getActions = 0,
            int addAction = 0,
            int actionsFilters = 0,
            int updateAlert = 0,
            int getAlerts = 0,
            int getAlertById = 0,
            int alertsFilters = 0,
            int dashboard = 0,
            int getSecureScores = 0,
            int getSecureDetails = 0,
            int getSubscriptions = 0,
            int subscribe = 0)
        {
            GetActions = getActions;
            AddAction = addAction;
            ActionsFilters = actionsFilters;
            UpdateAlert = updateAlert;
            GetAlerts = getAlerts;
            GetAlertById = getAlertById;
            AlertsFilters = alertsFilters;
            Dashboard = dashboard;
            GetSecureScores = getSecureScores;
            GetSecureDetails = getSecureDetails;
            GetSubscriptions = getSubscriptions;
            Subscribe = subscribe;
        }

        public int GetActions { get; set; }

        public int AddAction { get; set; }

        public int ActionsFilters { get; set; }

        public int UpdateAlert { get; set; }

        public int GetAlerts { get; set; }

        public int GetAlertById { get; set; }

        public int AlertsFilters { get; set; }

        public int Dashboard { get; set; }

        public int GetSecureScores { get; set; }

        public int GetSecureDetails { get; set; }

        public int GetSubscriptions { get; set; }

        public int Subscribe { get; set; }
    }
}