using CFormManagement.SmsServiceReference;
using CFormManagement.SmsServiceReference.Interfaces;

namespace CFormManagement.Test
{
    public class ReportServiceTests
    {
        private readonly IReportService _reportService;
        ReportServiceTests(IReportService reportService)
        {
            _reportService = reportService;
        }

        [SetUp]
        public void Setup()
        {
            var dt = _reportService.IsBankInfoExists("1");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}