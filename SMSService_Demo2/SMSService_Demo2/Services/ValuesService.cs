using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMSService_Demo2.Services
{
    public class ValuesService
    {
        public T ExtractValueFromRaw<T>(T raw, T val) 
        {
            return default(T);
        }

        public IDictionary<T,T> GetCollectionFromRaw<T>(IEnumerable<T> input, T internalDelimiter) 
            where T : IConvertible
        {
            try
            {
                Dictionary<T, T> retVal = new Dictionary<T, T>();
                for (int i = 0; i < input.Count(); i++)
                {
                    string[] vals = input.ElementAt(i).ToString().Split(internalDelimiter.ToString());
                    retVal.Add((T)Convert.ChangeType(vals[0], typeof(T)), (T)Convert.ChangeType(vals[1], typeof(T)));
                }

                return retVal;
            }
            catch
            {
                return default(IDictionary<T, T>);
            }
        }
    }
}


//private string getServiceFromRaw(string request)
//{
//    string service = string.Empty;
//    List<string> vals = request.Split("\r\n").ToList();
//    foreach (string s in vals)
//    {
//        string[] arr = new string[1];
//        arr = s.Split(':');
//        if (arr[0].ToUpper() == "SERVICE")
//        {
//            service = arr[1];
//            break;
//        }
//    }
//    return service;
//}
