using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sat.Recruitment.Entities.Common
{
    public class CustomStringEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString()))
                return null;

            object parsedEnumValue;
            Type enumType = objectType.IsGenericType ? objectType.GenericTypeArguments[0] : objectType;

            var isValidEnumValue = Enum.TryParse(enumType, reader.Value.ToString(), true, out parsedEnumValue);

            if (!isValidEnumValue)
                return null;
            
            return parsedEnumValue;
        }
    }
}
