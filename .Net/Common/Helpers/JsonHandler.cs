using Newtonsoft.Json;
using System;

namespace Common.Helpers
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