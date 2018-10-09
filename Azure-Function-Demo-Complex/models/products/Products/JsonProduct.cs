// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using HelloProj.CosmosDB.Models.Products;
//
//    var jsonProduct = JsonProduct.FromJson(jsonString);

namespace HelloProj.CosmosDB.Models.Products
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JsonProduct
    {
        [JsonProperty("record")]
        public Record[] Record { get; set; }
    }

    public partial class Record
    {
        [JsonProperty("editionID")]
        public double EditionId { get; set; }

        [JsonProperty("coverTitle")]
        public string CoverTitle { get; set; }

        [JsonProperty("isbn13")]
        public string Isbn13 { get; set; }

        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("coverAuthors")]
        public string CoverAuthors { get; set; }

        [JsonProperty("workReference")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WorkReference { get; set; }

        [JsonProperty("binding")]
        public Binding? Binding { get; set; }

        [JsonProperty("division")]
        public Division Division { get; set; }

        [JsonProperty("formats")]
        public Format[] Formats { get; set; }

        [JsonProperty("sortTitle")]
        public string SortTitle { get; set; }

        [JsonProperty("workID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WorkId { get; set; }

        [JsonProperty("imprint")]
        public Imprint Imprint { get; set; }

        [JsonProperty("pubDate")]
        public string PubDate { get; set; }

        [JsonProperty("productType")]
        public ProductType ProductType { get; set; }

        [JsonProperty("seriesNumber")]
        public long? SeriesNumber { get; set; }

        [JsonProperty("seriesTitle")]
        public string SeriesTitle { get; set; }

        [JsonProperty("issuedNumber")]
        public long IssuedNumber { get; set; }

        [JsonProperty("minimumAge")]
        public long? MinimumAge { get; set; }

        [JsonProperty("maximumAge")]
        public long? MaximumAge { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("keynote")]
        public string Keynote { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("copyRightYear")]
        public object CopyRightYear { get; set; }

        [JsonProperty("pubPrice")]
        public double PubPrice { get; set; }

        [JsonProperty("keywords")]
        public string[] Keywords { get; set; }

        [JsonProperty("pageCount")]
        public long? PageCount { get; set; }

        [JsonProperty("editionTypes")]
        public long[] EditionTypes { get; set; }

        [JsonProperty("relatedProducts")]
        public string[] RelatedProducts { get; set; }

        [JsonProperty("people")]
        public Person[] People { get; set; }

        [JsonProperty("categories")]
        public Category[] Categories { get; set; }

        [JsonProperty("availability")]
        public Availability Availability { get; set; }

        [JsonProperty("timeStamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("reviews")]
        public Review[] Reviews { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("awards")]
        public object[] Awards { get; set; }

        [JsonProperty("link")]
        public object Link { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }

    public partial class Availability
    {
        [JsonProperty("code")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("mainCode")]
        public bool MainCode { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("categorytype")]
        public CategoryCategorytype Categorytype { get; set; }
    }

    public partial class Format
    {
        [JsonProperty("mainCode")]
        public object MainCode { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("description")]
        public Binding Description { get; set; }

        [JsonProperty("code")]
        public FormatCode Code { get; set; }

        [JsonProperty("categorytype")]
        public FormatCategorytype Categorytype { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }
    }

    public partial class Person
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("role")]
        public Role Role { get; set; }

        [JsonProperty("code")]
        public PersonCode Code { get; set; }

        [JsonProperty("link")]
        public object Link { get; set; }
    }

    public partial class Review
    {
        [JsonProperty("id")]
        public double Id { get; set; }

        [JsonProperty("editionId")]
        public double EditionId { get; set; }

        [JsonProperty("reviewText")]
        public string ReviewText { get; set; }

        [JsonProperty("reviewer")]
        public string Reviewer { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public enum Binding { EBook, Hardback, Paperback };

    public enum CategoryCategorytype { Bic, Bisac };

    public enum Division { OrionPublishingGroup };

    public enum FormatCategorytype { Onix };

    public enum FormatCode { Bb, Bc, Dg, Ea };

    public enum TypeEnum { Original, Thumbnail, Web };

    public enum Imprint { WN };

    public enum PersonCode { A01 };

    public enum Role { Author };

    public enum ProductType { Book, EBook };

    public enum Status { Confirmed, OutOfPrint };

    public partial struct Version
    {
        public long? Integer;
        public string String;

        public static implicit operator Version(long Integer) => new Version { Integer = Integer };
        public static implicit operator Version(string String) => new Version { String = String };
    }

    public partial class JsonProduct
    {
        public static JsonProduct FromJson(string json) => JsonConvert.DeserializeObject<JsonProduct>(json, HelloProj.CosmosDB.Models.Products.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this JsonProduct self) => JsonConvert.SerializeObject(self, HelloProj.CosmosDB.Models.Products.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                BindingConverter.Singleton,
                CategoryCategorytypeConverter.Singleton,
                VersionConverter.Singleton,
                DivisionConverter.Singleton,
                FormatCategorytypeConverter.Singleton,
                FormatCodeConverter.Singleton,
                TypeEnumConverter.Singleton,
                ImprintConverter.Singleton,
                PersonCodeConverter.Singleton,
                RoleConverter.Singleton,
                ProductTypeConverter.Singleton,
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class BindingConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Binding) || t == typeof(Binding?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "EBook":
                    return Binding.EBook;
                case "Hardback":
                    return Binding.Hardback;
                case "Paperback":
                    return Binding.Paperback;
            }
            throw new Exception("Cannot unmarshal type Binding");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Binding)untypedValue;
            switch (value)
            {
                case Binding.EBook:
                    serializer.Serialize(writer, "EBook");
                    return;
                case Binding.Hardback:
                    serializer.Serialize(writer, "Hardback");
                    return;
                case Binding.Paperback:
                    serializer.Serialize(writer, "Paperback");
                    return;
            }
            throw new Exception("Cannot marshal type Binding");
        }

        public static readonly BindingConverter Singleton = new BindingConverter();
    }

    internal class CategoryCategorytypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CategoryCategorytype) || t == typeof(CategoryCategorytype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "bic":
                    return CategoryCategorytype.Bic;
                case "bisac":
                    return CategoryCategorytype.Bisac;
            }
            throw new Exception("Cannot unmarshal type CategoryCategorytype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CategoryCategorytype)untypedValue;
            switch (value)
            {
                case CategoryCategorytype.Bic:
                    serializer.Serialize(writer, "bic");
                    return;
                case CategoryCategorytype.Bisac:
                    serializer.Serialize(writer, "bisac");
                    return;
            }
            throw new Exception("Cannot marshal type CategoryCategorytype");
        }

        public static readonly CategoryCategorytypeConverter Singleton = new CategoryCategorytypeConverter();
    }

    internal class VersionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Version) || t == typeof(Version?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Version { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Version { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type Version");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Version)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type Version");
        }

        public static readonly VersionConverter Singleton = new VersionConverter();
    }

    internal class DivisionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Division) || t == typeof(Division?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Orion Publishing Group")
            {
                return Division.OrionPublishingGroup;
            }
            throw new Exception("Cannot unmarshal type Division");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Division)untypedValue;
            if (value == Division.OrionPublishingGroup)
            {
                serializer.Serialize(writer, "Orion Publishing Group");
                return;
            }
            throw new Exception("Cannot marshal type Division");
        }

        public static readonly DivisionConverter Singleton = new DivisionConverter();
    }

    internal class FormatCategorytypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FormatCategorytype) || t == typeof(FormatCategorytype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "onix")
            {
                return FormatCategorytype.Onix;
            }
            throw new Exception("Cannot unmarshal type FormatCategorytype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FormatCategorytype)untypedValue;
            if (value == FormatCategorytype.Onix)
            {
                serializer.Serialize(writer, "onix");
                return;
            }
            throw new Exception("Cannot marshal type FormatCategorytype");
        }

        public static readonly FormatCategorytypeConverter Singleton = new FormatCategorytypeConverter();
    }

    internal class FormatCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FormatCode) || t == typeof(FormatCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BB":
                    return FormatCode.Bb;
                case "BC":
                    return FormatCode.Bc;
                case "DG":
                    return FormatCode.Dg;
                case "EA":
                    return FormatCode.Ea;
            }
            throw new Exception("Cannot unmarshal type FormatCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FormatCode)untypedValue;
            switch (value)
            {
                case FormatCode.Bb:
                    serializer.Serialize(writer, "BB");
                    return;
                case FormatCode.Bc:
                    serializer.Serialize(writer, "BC");
                    return;
                case FormatCode.Dg:
                    serializer.Serialize(writer, "DG");
                    return;
                case FormatCode.Ea:
                    serializer.Serialize(writer, "EA");
                    return;
            }
            throw new Exception("Cannot marshal type FormatCode");
        }

        public static readonly FormatCodeConverter Singleton = new FormatCodeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "original":
                    return TypeEnum.Original;
                case "thumbnail":
                    return TypeEnum.Thumbnail;
                case "web":
                    return TypeEnum.Web;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Original:
                    serializer.Serialize(writer, "original");
                    return;
                case TypeEnum.Thumbnail:
                    serializer.Serialize(writer, "thumbnail");
                    return;
                case TypeEnum.Web:
                    serializer.Serialize(writer, "web");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class ImprintConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Imprint) || t == typeof(Imprint?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "W&N")
            {
                return Imprint.WN;
            }
            throw new Exception("Cannot unmarshal type Imprint");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Imprint)untypedValue;
            if (value == Imprint.WN)
            {
                serializer.Serialize(writer, "W&N");
                return;
            }
            throw new Exception("Cannot marshal type Imprint");
        }

        public static readonly ImprintConverter Singleton = new ImprintConverter();
    }

    internal class PersonCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PersonCode) || t == typeof(PersonCode?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "A01")
            {
                return PersonCode.A01;
            }
            throw new Exception("Cannot unmarshal type PersonCode");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PersonCode)untypedValue;
            if (value == PersonCode.A01)
            {
                serializer.Serialize(writer, "A01");
                return;
            }
            throw new Exception("Cannot marshal type PersonCode");
        }

        public static readonly PersonCodeConverter Singleton = new PersonCodeConverter();
    }

    internal class RoleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Role) || t == typeof(Role?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "author")
            {
                return Role.Author;
            }
            throw new Exception("Cannot unmarshal type Role");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Role)untypedValue;
            if (value == Role.Author)
            {
                serializer.Serialize(writer, "author");
                return;
            }
            throw new Exception("Cannot marshal type Role");
        }

        public static readonly RoleConverter Singleton = new RoleConverter();
    }

    internal class ProductTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProductType) || t == typeof(ProductType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Book":
                    return ProductType.Book;
                case "EBook":
                    return ProductType.EBook;
            }
            throw new Exception("Cannot unmarshal type ProductType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProductType)untypedValue;
            switch (value)
            {
                case ProductType.Book:
                    serializer.Serialize(writer, "Book");
                    return;
                case ProductType.EBook:
                    serializer.Serialize(writer, "EBook");
                    return;
            }
            throw new Exception("Cannot marshal type ProductType");
        }

        public static readonly ProductTypeConverter Singleton = new ProductTypeConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Confirmed":
                    return Status.Confirmed;
                case "Out of Print":
                    return Status.OutOfPrint;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Confirmed:
                    serializer.Serialize(writer, "Confirmed");
                    return;
                case Status.OutOfPrint:
                    serializer.Serialize(writer, "Out of Print");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
