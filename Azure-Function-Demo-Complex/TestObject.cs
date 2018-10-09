using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelloProj
{
    [Serializable]
    public class HttpReq
    {
        public string DeveloperId {get;set;}
        public string QueryString {get;set;}

        public string APIName {get;set;}
        public string APIVersion {get;set;}
    }

}