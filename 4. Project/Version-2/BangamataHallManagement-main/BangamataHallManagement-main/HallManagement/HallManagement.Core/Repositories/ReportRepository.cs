using HallManagement.Repositories.GenericRepository;
using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace HallManagement.Core.Repositories
{
    public class ReportRepository : RepositoryBase<StudentVm>, IReportRepository
    {
        public ReportRepository(BangamataHallContext context) : base(context) { }

        public async Task<string> Load(string emailOrPhone, string otp, int timeoutInSecond)
        {
            string dbMessage = "";
            try
            {

                //var dbMessageParameter = new SqlParameter("@dbMessage", SqlDbType.VarChar);

                var dbMessageParameter = new SqlParameter("@dbMessage", dbMessage);
                dbMessageParameter.SqlDbType = SqlDbType.VarChar;
                dbMessageParameter.Size = 255;
                dbMessageParameter.Direction = ParameterDirection.Output;
                var dt = await _context.Database.ExecuteSqlRawAsync("EXEC dbo.GetSingleOtpHistory @EmailOrPhone, @Otp, @timeoutInSecond, @dbMessage OUTPUT",
                new SqlParameter("@EmailOrPhone", emailOrPhone), new SqlParameter("@Otp", otp), new SqlParameter("@timeoutInSecond", timeoutInSecond),
                dbMessageParameter);
                dbMessage = dbMessageParameter.Value.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                dbMessage = "";
                throw;
            }
            return dbMessage;
        }

        public async Task<string> GetSingleOtpHistory(string emailOrPhone, string otp, int timeoutInSecond)
        {
            string dbMessage = "";
            try
            {

                //var dbMessageParameter = new SqlParameter("@dbMessage", SqlDbType.VarChar);

                var dbMessageParameter = new SqlParameter("@dbMessage", dbMessage);
                dbMessageParameter.SqlDbType = SqlDbType.VarChar;
                dbMessageParameter.Size = 255;
                dbMessageParameter.Direction = ParameterDirection.Output;
                var dt = await _context.Database.ExecuteSqlRawAsync("EXEC dbo.GetSingleOtpHistory @EmailOrPhone, @Otp, @timeoutInSecond, @dbMessage OUTPUT",
                new SqlParameter("@EmailOrPhone", emailOrPhone), new SqlParameter("@Otp", otp), new SqlParameter("@timeoutInSecond", timeoutInSecond),
                dbMessageParameter);
                dbMessage = dbMessageParameter.Value.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                dbMessage = "";
                throw;
            }
            return dbMessage;
        }

        public async Task<bool> IsOtpHistoryExists(string? mobileno, string? email)
        {
            try
            {
                var recordExistsParameter = new SqlParameter("@IsExists", SqlDbType.Bit);
                recordExistsParameter.Direction = ParameterDirection.Output;

                var dt = await _context.Database.ExecuteSqlRawAsync("EXEC dbo.IsOtpHistoryExists  @MobileNo, @Email, @IsExists OUTPUT",
                    new SqlParameter("@MobileNo", mobileno), new SqlParameter("@Email", email),
                    recordExistsParameter);

                bool recordExists = (bool)recordExistsParameter.Value;
                return recordExists;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> IsReceiverNameExists(string receiverName, int id = 0)
        {
            try
            {
                var recordExistsParameter = new SqlParameter("@IsExists", SqlDbType.Bit);
                recordExistsParameter.Direction = ParameterDirection.Output;

                var dt = await _context.Database.ExecuteSqlRawAsync("EXEC dbo.IsReceiverNameExists @ReceiverName, @ReceiverId, @IsExists OUTPUT",
                    new SqlParameter("@ReceiverName", receiverName), new SqlParameter("@ReceiverId", id),
                    recordExistsParameter);

                bool recordExists = (bool)recordExistsParameter.Value;
                return recordExists;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataTable GetFormCReportData(int remittanceId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetFormCRemittanceDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id", SqlDbType.Int).Value = remittanceId;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
