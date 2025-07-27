using BankingWebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.DtoClass
{
    public class RegisterNewUsercs
    {

        public virtual User User { get; set; } = new User();    

        public virtual UserAccountDetail UserAccountDetais { get; set; } = new UserAccountDetail();


        public virtual UserLoginDetails? UserLoginDetails { get; set; } = new UserLoginDetails();


    }
}
