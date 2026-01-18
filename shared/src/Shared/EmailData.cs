using Auth.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class EmailData
    { 
        public EmailData(
            Email sender, 
            ICollection<Email> recievers, 
            string title, 
            string body) 
        {
            Sender = sender;
            Recievers = recievers;
            Title = title;
            Body = body;
        }

        public Email Sender { get; private set; }
        public ICollection<Email> Recievers { get; private set; }
        public string Title { get; private set; } 
        public string Body { get; private set; } 
    }
}
