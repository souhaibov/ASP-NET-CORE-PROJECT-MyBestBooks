
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyBestBooks.Utility
{
    public class EmailSender : IEmailSender{
    
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // in the future if we have to send email here we will have actually the logic here to send email
            // for now we are using a fake one
        return Task.CompletedTask; 
        }
    }
}
