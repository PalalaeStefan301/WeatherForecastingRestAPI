using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models
{
    [Flags]
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum EnumTimezones
    {
        America_Anchorage = 0,
        America_Los_Angeles = 1,
        America_Denver = 2,
        America_Chicago = 3,
        America_New_York = 4,
        America_Sao_Paulo = 5,
        GMT = 6,
        Europe_London = 7,
        Europe_Berlin = 8,
        Europe_Moscow = 9,
        Africa_Cairo = 10,
        Asia_Bangkok = 11,
        Asia_Singapore = 12,
        Asia_Tokyo = 13,
        Australia_Sydney = 14,
        Pacific_Auckland = 15,
    }
}
