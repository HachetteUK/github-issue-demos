using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelloProj
{
    [Serializable]
    public class City
    {
        public string  Name {get;set;}
        public int Number {get;set;}
    }

    [Serializable]
    public class CollectionOfCities 
    {
        public List<City> Cities {get;set;} = new List<City>();
    }

    [Serializable]
    public class HttpReq
    {
        public string DeveloperId {get;set;}
        public string QueryString {get;set;}

        public string APIName {get;set;}
        public string APIVersion {get;set;}
    }

    [Serializable]
    public partial class TestToDo
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }
    }

    public partial class TestToDo
    {
        public static TestToDo FromJson(string json) => JsonConvert.DeserializeObject<TestToDo>(json, HelloProj.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TestToDo self) => JsonConvert.SerializeObject(self, HelloProj.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}