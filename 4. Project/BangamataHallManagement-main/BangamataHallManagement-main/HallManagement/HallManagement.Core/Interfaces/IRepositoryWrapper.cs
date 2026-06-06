namespace HallManagement.Core.Interfaces
{
    public interface IRepositoryWrapper
    {
        IDepartmentRepository Department { get; }
        //IDivisionRepository Division { get; }
        //IReceiverInfoRepository ReceiverInfo { get; }
        //IOtpHistoryRepository OtpHistory { get; }
        //  IViewBranchInfoRepository ViewBranchInfo { get; }
        void Save();
    }
}
