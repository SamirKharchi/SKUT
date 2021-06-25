using System;

namespace SKUT.Xunit
{
    /// <summary>
    /// <para>
    /// Original Source: https://github.com/xunit/samples.xunit/blob/main/TestOrderExamples/TestCaseOrdering/TestPriorityAttribute.cs
    /// </para>
    /// Use [Fact, TestPriority(priority)] on test methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority) => Priority = priority;

        public int Priority { get; }
    }
}