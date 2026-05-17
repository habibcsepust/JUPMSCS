using Microsoft.VisualStudio.TestTools.UnitTesting;
using CFormManagement.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFormManagement.SmsServiceReference.Interfaces;
using CFormManagement.SmsServiceReference;
using NUnit.Framework;
using Moq;

namespace CFormManagement.Test.Tests
{
    [TestFixture]
    public class ReportServiceTestsTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var reportService = new Mock<IReportService>() { CallBase = true, DefaultValue = DefaultValue.Custom};
            var dt = reportService.Object.IsBankInfoExists("1").Result;
            //string dt2 = dt + 't';
        }
    }
}