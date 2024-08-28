using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace WeatherForecastingRestAPI.Tests.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class BeforeAndAfterAttribute : BeforeAfterTestAttribute
    {
        //here I save all mocks to reset them when needed
        public static List<Mock> Mocks { get; set; } = new List<Mock>();
        public override void Before(MethodInfo methodUnderTest)
        {
            // Reset all mocks before the test
            foreach (var mock in Mocks)
            {
                mock.Reset();
            }
        }
        public override void After(MethodInfo methodUnderTest)
        {
            // Optionally reset mocks after the test
            foreach (var mock in Mocks)
            {
                mock.Reset();
            }
        }
    }
}
