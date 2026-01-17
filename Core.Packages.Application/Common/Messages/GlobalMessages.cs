using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Application.Common.Messages
{
    public static partial class Messages
    {
        // Static messages as fallback (will be overridden by database messages if available)
        public static string Exists = "ALREADY_EXISTS";
        public static string Added = "ADDED_SUCCESSFULLY";
        public static string Updated = "UPDATED_SUCCESSFULLY";
        public static string Deleted = "DELETED_SUCCESSFULLY";
        public static string NotFound = "NOT_FOUND";
        public static string UnauthorizedAccess = "UNAUTHORIZED_ACCESS";
        public static string InvalidOperation = "INVALID_OPERATION";
        public static string ServerError = "SERVER_ERROR";
    }
}
