using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Application.Features.Email.SendEmail
{
    public class SendEmailCommand : IRequest<IResult>
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }

}
