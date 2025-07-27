using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.DtoClass
{
    public class GetUserDetails
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public string AccountNo { get; set; }
        public string Account_Type { get; set; }
        public string PanNo { get; set; }
    }
}
