using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGreen.Model
{
    public class SendEmail
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }

    public class SendCode
    {
        public string Code { get; set; }
    }
}
