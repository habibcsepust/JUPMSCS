using HallManagement.Core.Interfaces;
using HallManagement.Model.ViewModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallManagement.Core.Interfaces
{
    public interface IReportRepository : IRepositoryBase<StudentVm> //, IRepositoryBase<VMOtpHistory>
    {
        //Task<IEnumerable<VMBankInfo>> GetBankInfoList(string branchCode, DateTime fromDate, DateTime toDate);
        //IEnumerable<BranchInfo> GetBranchInfoList();
        //Task<string> GetSingleOtpHistory(string emailOrPhone, string otp, int timeoutInSecond);
        //Task<IEnumerable<VMReceiverInfo>> GetReceiverInfoList(int ReceiverId, string TransactionId, string MobileNo);
        //Task<IEnumerable<RemittanceDetail>> GetRemittanceDetailList(string branchCode, int? page, int pageSize, DateTime fromDate, DateTime toDate);
        //Task<bool> IsBankInfoExists(string Number, int id = 0);
        //Task<bool> IsReceiverNameExists(string receiverName, int id = 0);
        //DataTable GetFormCReportData(int remittanceId);
    }
}
