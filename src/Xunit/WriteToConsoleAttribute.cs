using System;
using System.Reflection;
using Xunit.Sdk;

namespace SKUT.Xunit
{
    /// <summary>
    /// Auto-writes to the console before and after a test method is run.<br/>
    /// Before it prints out the method name and its input parameters. After it prints out the return type.
    /// </summary>
    public class WriteToConsoleAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            var parameters = methodUnderTest.GetParameters();
            if (parameters.Length > 0)
            {
                Console.WriteLine(methodUnderTest.Name + "::Params => " + methodUnderTest.GetParameters()[0].Name);
                return;
            }
            Console.WriteLine(methodUnderTest.Name);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            Console.WriteLine(methodUnderTest.ReturnType.ToString());
        }
    }
}