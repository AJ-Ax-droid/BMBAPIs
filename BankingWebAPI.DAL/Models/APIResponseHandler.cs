using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingWebAPI.DAL.Models
{
    public class APIResponseHandler<T>
    {
       public bool isSuccess { get; set; }
       public string? Message { get; set; }
       public T? Data { get; set; }

    }
}
