using System.Runtime.Serialization;
using Newtonsoft.Json;
using Sat.Recruitment.Entities.Common;

namespace Sat.Recruitment.Entities.Models
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum UserType
    {
        [EnumMember(Value = "Normal")]
        Normal,
        [EnumMember(Value = "SuperUser")]
        SuperUser,
        [EnumMember(Value = "Premium")]
        Premium
    }
}
