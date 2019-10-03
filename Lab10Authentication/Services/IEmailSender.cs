using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Services
{
    /// <summary>
    /// this interface is used as a base class for EmailSender class implementations
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// this method signature is for sending an email with the the email, subject, and message inputted as parameters
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
