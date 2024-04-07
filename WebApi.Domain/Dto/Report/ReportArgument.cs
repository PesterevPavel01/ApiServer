using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Dto.Report
{
    public class ReportArgument
    {
        public ReportArgument() { }
        public string Login { get; set; }
        public string Password { get; set; }
        public short Month { get; set; }
        public short Year { get; set; }

        public ReportArgument(string login, string password,short month, short year) 
        {
            this.Login = login;
            this.Password = password;
            this.Month = month;
            this.Year = year;
        }
    }
}
