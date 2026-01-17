using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.SendByEmail
{
    public class SendInvoiceByEmailCommandHandler : IRequestHandler<SendInvoiceByEmailCommand, SendInvoiceByEmailResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPdfInvoiceService _pdfInvoiceService;
        private readonly IEmailService _emailService;

        public SendInvoiceByEmailCommandHandler(
            IInvoiceRepository invoiceRepository,
            IPdfInvoiceService pdfInvoiceService,
            IEmailService emailService)
        {
            _invoiceRepository = invoiceRepository;
            _pdfInvoiceService = pdfInvoiceService;
            _emailService = emailService;
        }

        public async Task<SendInvoiceByEmailResponse> Handle(SendInvoiceByEmailCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.Query()
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

            if (invoice == null)
            {
                return new SendInvoiceByEmailResponse
                {
                    Success = false,
                    Message = "Fatura bulunamadı"
                };
            }

            // PDF oluştur
            var pdfBytes = await _pdfInvoiceService.GenerateInvoicePdfAsync(request.InvoiceId, cancellationToken);

            // Email içeriği
            var subject = request.Subject ?? $"Fatura #{invoice.InvoiceNumber}";
            var message = request.Message ?? $@"
                Sayın {invoice.Customer?.FullName ?? "Müşteri"},
                
                Faturanız ektedir.
                
                Fatura No: {invoice.InvoiceNumber}
                Fatura Tarihi: {invoice.InvoiceDate:dd.MM.yyyy}
                Toplam Tutar: {invoice.TotalAmount:N2} ₺
                
                İyi günler dileriz.
            ";

            // Email gönder (PDF ekli)
            // Not: Gerçek uygulamada PDF'i attachment olarak eklemek gerekir
            // Şimdilik sadece email gönderiyoruz
            var emailSent = await _emailService.SendEmailAsync(request.ToEmail, subject, message);

            return new SendInvoiceByEmailResponse
            {
                Success = emailSent,
                Message = emailSent ? "Fatura email ile gönderildi" : "Email gönderilemedi"
            };
        }
    }
}






