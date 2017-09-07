using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Services;

namespace Module.Business.Business
{
    public class GAService
    {
        //Google analytics API in progress
        /*
        public void GA()
        {
            var filepath = "..\\..\\Pernicek-7b09feacf7b2.json";  // path to the json file for the Service account
            GoogleCredential credentials;
            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                string[] scopes = { AnalyticsReportingService.Scope.AnalyticsReadonly };
                var googleCredential = GoogleCredential.FromStream(stream);
                credentials = googleCredential.CreateScoped(scopes);
            }

            var reportingService = new AnalyticsReportingService(
                new BaseClientService.Initializer
                {
                    HttpClientInitializer = credentials
                });

            var dateRange = new DateRange
            {
                StartDate = "2016-09-06",
                EndDate = "today"
            };
            var sessions = new Metric
            {
                Expression = "ga:pageviews",
                Alias = "Sessions"
            };
            var date = new Dimension { Name = "ga:date" };

            var reportRequest = new ReportRequest
            {
                DateRanges = new List<DateRange> { dateRange },
                Dimensions = new List<Dimension> { date },
                Metrics = new List<Metric> { sessions },
                ViewId = "159699513" // your view id
            };

            var getReportsRequest = new GetReportsRequest
            {
                ReportRequests = new List<ReportRequest> { reportRequest }
            };
            var batchRequest = reportingService.Reports.BatchGet(getReportsRequest);
            var response = batchRequest.Execute();
        }
        */
        

    }

}
