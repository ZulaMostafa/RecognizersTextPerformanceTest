using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Helpers
{
    class JsonHandler
    {
        public static T DeserializeObject<T>(string value, string fileName = "")
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                // todo: handle exception
                throw new Exception(ex.Message);
            }
        }
    }
}
