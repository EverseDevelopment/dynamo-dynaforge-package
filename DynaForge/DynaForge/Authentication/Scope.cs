using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class Scope
    {
        private Scope() { }
        public static string Set(bool user_profile_read = false, bool user_read = false, bool user_write = false,
            bool viewables_read = false,bool bucket_create = false, bool bucket_read = false, bool bucket_update = false, bool bucket_delete = false,
            bool code_all = false, bool account_read = false, bool account_write = false,  bool data_search = false , bool data_create = false , bool data_write = false, 
            bool data_read = false)
        {
            string result = "";
            result = result + addString(user_profile_read, " user-profile:read");
            result = result + addString(user_read, " user:read");
            result = result + addString(user_write, " user:write");
            result = result + addString(viewables_read, " viewables:read");
            result = result + addString(data_read, " data:read");
            result = result + addString(data_write, " data:write");
            result = result + addString(data_create, " data:create");
            result = result + addString(data_search, " data:search");
            result = result + addString(bucket_create, " bucket:create");
            result = result + addString(bucket_read, " bucket:read");
            result = result + addString(bucket_update, " bucket:update");
            result = result + addString(bucket_delete, " bucket:delete");
            result = result + addString(code_all, " code:all");
            result = result + addString(account_read, " account:read");
            result = result + addString(account_write, " account:write");

            return result;
        }

        private static string addString (bool scopeBool, string value)
        {
            string resultString = "";
            if(scopeBool)
            {
                resultString =  value;
            }
            return resultString;
        }
    }
}
