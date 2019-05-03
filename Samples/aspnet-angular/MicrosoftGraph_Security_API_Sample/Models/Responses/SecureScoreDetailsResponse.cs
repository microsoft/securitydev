using MicrosoftGraph_Security_API_Sample.Models.ViewModels;

namespace MicrosoftGraph_Security_API_Sample.Models.Responses
{
    public class SecureScoreDetailsResponse
    {
        public SecureScoreDetailsResponse(SecureScoreDetailsViewModel secureScoreDetails, ResultQueriesViewModel queries)
        {
            SecureScoreDetails = secureScoreDetails;
            Queries = queries;
        }

        public SecureScoreDetailsViewModel SecureScoreDetails { get; set; }

        public ResultQueriesViewModel Queries { get; set; }
    }
}