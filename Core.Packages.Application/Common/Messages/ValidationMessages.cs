using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Application.Common.Messages
{
    public static partial class Messages
    {
        // Error codes for validation messages
        public static string NotEmpty = "VALIDATION_NOT_EMPTY";
        public static string NotValidEmail = "VALIDATION_EMAIL_INVALID";
        public static string PasswordsDoNotMatch = "VALIDATION_PASSWORDS_DO_NOT_MATCH";
        public static string PasswordLength = "VALIDATION_PASSWORD_LENGTH";
        public static string PasswordUppercase = "VALIDATION_PASSWORD_UPPERCASE";
        public static string PasswordDigit = "VALIDATION_PASSWORD_DIGIT";
        public static string PasswordSpecialCharacter = "VALIDATION_PASSWORD_SPECIAL_CHARACTER";
    }
}
