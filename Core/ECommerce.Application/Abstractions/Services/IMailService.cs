using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml=true );
        Task SendMessageAsync(string[] toS, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsyncNew(string[] toS, string subject, string body, bool isBodyHtml = true);
    }
}
