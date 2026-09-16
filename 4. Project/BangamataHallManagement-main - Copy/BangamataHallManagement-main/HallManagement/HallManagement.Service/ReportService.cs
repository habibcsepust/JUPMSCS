using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using System.Data;
using HallManagement.Service.Interfaces;
using HallManagement.Model.ViewModels;
using HallManagement.Core.Interfaces;

namespace HallManagement.SmsServiceReference
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }



        //public Task<bool> IsReceiverInfoNameExists(string emailOrPhone, int id = 0)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> IsOtpHistoryExists(string? mobileno, string? email)
        //{
        //    throw new NotImplementedException();
        //}


        //async Task<IEnumerable<VMBankInfo>> GetBankInfoList(string branchCode, DateTime fromDate, DateTime toDate)
        //{
        //    return await _reportRepository.GetBankInfoList(branchCode, fromDate, toDate);
        //}


        //public async Task<string> GetSingleOtpHistory(string emailOrPhone, string otp, int timeoutInSecond)
        //{
        //    return await _reportRepository.GetSingleOtpHistory(emailOrPhone, otp, timeoutInSecond);
        //}

        //public IEnumerable<BranchInfo> GetBranchInfoList()
        //{
        //    return _reportRepository.GetBranchInfoList();
        //}

        //Task<IEnumerable<VMBankInfo>> IReportService.GetBankInfoList(string branchCode, DateTime fromDate, DateTime toDate)
        //{
        //    throw new NotImplementedException();
        //}


        //Task<IEnumerable<VMReceiverInfo>> IReportService.GetReceiverInfoList(int ReceiverId, string TransactionId, string MobileNo)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<bool> IReportService.IsBankInfoExists(string Number, int id)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<RemittanceDetail>> IReportService.GetRemittanceDetailList(string branchCode, int? page, int pageSize, DateTime fromDate, DateTime toDate)
        //{
        //    return _reportRepository.GetRemittanceDetailList(branchCode, page, pageSize, fromDate, toDate);
        //}

        //public DataTable GetFormCReportData(int receiverId)
        //{
        //    return _reportRepository.GetFormCReportData(receiverId);
        //}
    }
}
