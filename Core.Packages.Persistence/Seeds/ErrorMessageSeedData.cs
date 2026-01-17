using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class ErrorMessageSeedData
    {
        public static List<ErrorMessage> GetErrorMessages()
        {
            var messages = new List<ErrorMessage>();
            int id = 1;

            // Turkish messages
            messages.AddRange(new[]
            {
                CreateMessage(id++, "ALREADY_EXISTS", "tr", "Zaten mevcut."),
                CreateMessage(id++, "ADDED_SUCCESSFULLY", "tr", "Başarıyla eklendi."),
                CreateMessage(id++, "UPDATED_SUCCESSFULLY", "tr", "Başarıyla güncellendi."),
                CreateMessage(id++, "DELETED_SUCCESSFULLY", "tr", "Başarıyla silindi."),
                CreateMessage(id++, "NOT_FOUND", "tr", "Bulunamadı."),
                CreateMessage(id++, "UNAUTHORIZED_ACCESS", "tr", "Yetkisiz erişim."),
                CreateMessage(id++, "INVALID_OPERATION", "tr", "Geçersiz işlem."),
                CreateMessage(id++, "SERVER_ERROR", "tr", "Sunucu hatası oluştu."),
                
                // Validation messages
                CreateMessage(id++, "VALIDATION_NOT_EMPTY", "tr", "{PropertyName} alanı zorunludur."),
                CreateMessage(id++, "VALIDATION_EMAIL_INVALID", "tr", "E-posta adresi geçerli değil."),
                CreateMessage(id++, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "tr", "Şifreler eşleşmiyor."),
                CreateMessage(id++, "VALIDATION_PASSWORD_LENGTH", "tr", "Şifre en az 6 karakter olmalıdır."),
                CreateMessage(id++, "VALIDATION_PASSWORD_UPPERCASE", "tr", "Şifre en az bir büyük harf içermelidir."),
                CreateMessage(id++, "VALIDATION_PASSWORD_DIGIT", "tr", "Şifre en az bir rakam içermelidir."),
                CreateMessage(id++, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "tr", "Şifre en az bir özel karakter içermelidir."),
                
                // Email messages
                CreateMessage(id++, "EMAIL_SENT_SUCCESSFULLY", "tr", "E-posta başarıyla gönderildi!"),
                CreateMessage(id++, "EMAIL_SEND_FAILED", "tr", "E-posta gönderilemedi!"),
                
                // Client messages
                CreateMessage(id++, "CLIENT_CODE_EXISTS", "tr", "{Code} koduna sahip müşteri zaten mevcut."),
                CreateMessage(id++, "CLIENT_NOT_FOUND", "tr", "Müşteri bulunamadı."),
                
                // Vehicle messages
                CreateMessage(id++, "VEHICLE_KM_LOWER_THAN_CURRENT", "tr", "{LicensePlate} plakalı araç için yeni kilometre ({NewKilometers}) mevcut kilometreden ({CurrentKilometers}) düşük olamaz."),
                CreateMessage(id++, "VEHICLE_KM_NEGATIVE", "tr", "{LicensePlate} plakalı araç için kilometre negatif olamaz ({NewKilometers})."),
                
                // Employee messages
                CreateMessage(id++, "EMPLOYEE_NO_EXISTS", "tr", "{EmployeeNo} numaralı personel zaten mevcut."),
                CreateMessage(id++, "EMPLOYEE_NOT_FOUND", "tr", "Personel bulunamadı."),
            });

            // English messages
            messages.AddRange(new[]
            {
                CreateMessage(id++, "ALREADY_EXISTS", "en", "Already exists."),
                CreateMessage(id++, "ADDED_SUCCESSFULLY", "en", "Added successfully."),
                CreateMessage(id++, "UPDATED_SUCCESSFULLY", "en", "Updated successfully."),
                CreateMessage(id++, "DELETED_SUCCESSFULLY", "en", "Deleted successfully."),
                CreateMessage(id++, "NOT_FOUND", "en", "Not found."),
                CreateMessage(id++, "UNAUTHORIZED_ACCESS", "en", "Unauthorized access."),
                CreateMessage(id++, "INVALID_OPERATION", "en", "Invalid operation."),
                CreateMessage(id++, "SERVER_ERROR", "en", "A server error occurred."),
                
                // Validation messages
                CreateMessage(id++, "VALIDATION_NOT_EMPTY", "en", "{PropertyName} is required."),
                CreateMessage(id++, "VALIDATION_EMAIL_INVALID", "en", "Email address is not valid."),
                CreateMessage(id++, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "en", "Passwords do not match."),
                CreateMessage(id++, "VALIDATION_PASSWORD_LENGTH", "en", "Password must be at least 6 characters."),
                CreateMessage(id++, "VALIDATION_PASSWORD_UPPERCASE", "en", "Password must contain at least one uppercase letter."),
                CreateMessage(id++, "VALIDATION_PASSWORD_DIGIT", "en", "Password must contain at least one digit."),
                CreateMessage(id++, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "en", "Password must contain at least one special character."),
                
                // Email messages
                CreateMessage(id++, "EMAIL_SENT_SUCCESSFULLY", "en", "Email sent successfully!"),
                CreateMessage(id++, "EMAIL_SEND_FAILED", "en", "Failed to send email!"),
                
                // Client messages
                CreateMessage(id++, "CLIENT_CODE_EXISTS", "en", "A client with code {Code} already exists."),
                CreateMessage(id++, "CLIENT_NOT_FOUND", "en", "Client not found."),
                
                // Vehicle messages
                CreateMessage(id++, "VEHICLE_KM_LOWER_THAN_CURRENT", "en", "New kilometers ({NewKilometers}) for vehicle {LicensePlate} cannot be lower than current kilometers ({CurrentKilometers})."),
                CreateMessage(id++, "VEHICLE_KM_NEGATIVE", "en", "Kilometers cannot be negative ({NewKilometers}) for vehicle {LicensePlate}."),
                
                // Employee messages
                CreateMessage(id++, "EMPLOYEE_NO_EXISTS", "en", "Employee with number {EmployeeNo} already exists."),
                CreateMessage(id++, "EMPLOYEE_NOT_FOUND", "en", "Employee not found."),
            });

            // Arabic messages
            messages.AddRange(new[]
            {
                CreateMessage(id++, "ALREADY_EXISTS", "ar", "موجود بالفعل."),
                CreateMessage(id++, "ADDED_SUCCESSFULLY", "ar", "تمت الإضافة بنجاح."),
                CreateMessage(id++, "UPDATED_SUCCESSFULLY", "ar", "تم التحديث بنجاح."),
                CreateMessage(id++, "DELETED_SUCCESSFULLY", "ar", "تم الحذف بنجاح."),
                CreateMessage(id++, "NOT_FOUND", "ar", "غير موجود."),
                CreateMessage(id++, "UNAUTHORIZED_ACCESS", "ar", "وصول غير مصرح به."),
                CreateMessage(id++, "INVALID_OPERATION", "ar", "عملية غير صالحة."),
                CreateMessage(id++, "SERVER_ERROR", "ar", "حدث خطأ في الخادم."),
                
                // Validation messages
                CreateMessage(id++, "VALIDATION_NOT_EMPTY", "ar", "{PropertyName} مطلوب."),
                CreateMessage(id++, "VALIDATION_EMAIL_INVALID", "ar", "البريد الإلكتروني غير صالح."),
                CreateMessage(id++, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "ar", "كلمات المرور غير متطابقة."),
                CreateMessage(id++, "VALIDATION_PASSWORD_LENGTH", "ar", "يجب أن تكون كلمة المرور 6 أحرف على الأقل."),
                CreateMessage(id++, "VALIDATION_PASSWORD_UPPERCASE", "ar", "يجب أن تحتوي كلمة المرور على حرف كبير واحد على الأقل."),
                CreateMessage(id++, "VALIDATION_PASSWORD_DIGIT", "ar", "يجب أن تحتوي كلمة المرور على رقم واحد على الأقل."),
                CreateMessage(id++, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "ar", "يجب أن تحتوي كلمة المرور على حرف خاص واحد على الأقل."),
                
                // Email messages
                CreateMessage(id++, "EMAIL_SENT_SUCCESSFULLY", "ar", "تم إرسال البريد الإلكتروني بنجاح!"),
                CreateMessage(id++, "EMAIL_SEND_FAILED", "ar", "فشل إرسال البريد الإلكتروني!"),
                
                // Client messages
                CreateMessage(id++, "CLIENT_CODE_EXISTS", "ar", "العميل برمز {Code} موجود بالفعل."),
                CreateMessage(id++, "CLIENT_NOT_FOUND", "ar", "العميل غير موجود."),
                
                // Vehicle messages
                CreateMessage(id++, "VEHICLE_KM_LOWER_THAN_CURRENT", "ar", "الكيلومترات الجديدة ({NewKilometers}) للمركبة {LicensePlate} لا يمكن أن تكون أقل من الكيلومترات الحالية ({CurrentKilometers})."),
                CreateMessage(id++, "VEHICLE_KM_NEGATIVE", "ar", "لا يمكن أن تكون الكيلومترات سالبة ({NewKilometers}) للمركبة {LicensePlate}."),
                
                // Employee messages
                CreateMessage(id++, "EMPLOYEE_NO_EXISTS", "ar", "الموظف برقم {EmployeeNo} موجود بالفعل."),
                CreateMessage(id++, "EMPLOYEE_NOT_FOUND", "ar", "الموظف غير موجود."),
            });

            return messages;
        }

        private static ErrorMessage CreateMessage(int id, string errorCode, string language, string message)
        {
            return new ErrorMessage
            {
                Id = id,
                ErrorCode = errorCode,
                Language = language,
                Message = message,
                CreatedDate = DateTime.UtcNow,
                Status = Status.Active
            };
        }
    }
}

