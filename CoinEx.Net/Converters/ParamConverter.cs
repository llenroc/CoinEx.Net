﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CoinEx.Net.Converters
{
    public class ParamConverter : JsonConverter
    {
        private readonly Type[] types;

        public ParamConverter(params Type[] types)
        {
            this.types = types;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.Load(reader);
            int maxParse = Math.Min(array.Count, types.Length);
            var result = new object[maxParse];

            for (int i = 0; i < maxParse; i++)
                result[i] = array[i].ToObject(types[i]);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class ParamListConverter : JsonConverter
    {
        private readonly Type type;

        public ParamListConverter(Type type)
        {
            this.type = type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(object[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.Load(reader);
            var result = new object[array.Count];
            for (int i = 0; i < array.Count; i++)
                result[i] = array[i].ToObject(type);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
