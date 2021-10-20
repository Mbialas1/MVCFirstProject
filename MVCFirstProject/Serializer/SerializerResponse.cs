using MVCFirstProject.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCFirstProject.Serializer
{
    public class SerializerResponse
    {
        private string responseContent;
        public SerializerResponse(string _res)
        {
            responseContent = _res;
        }

        public IEnumerable<Tags> DeserializeTagsResponse()
        {
            IEnumerable<Tags> lTags = new List<Tags>();
            try
            {
                lTags = JsonConvert.DeserializeObject<List<Tags>>(responseContent);
                return lTags;
            }
            catch(Exception e)
            {
                string inncorrectRes = !String.IsNullOrEmpty(responseContent) ? "Can't deserialize object. Check content response." : e.ToString();
                throw new Exception(inncorrectRes);
            }
            
        }
    }
}
