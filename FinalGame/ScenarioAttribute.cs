using System;

namespace FinalGame
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ScenarioAttribute : Attribute
    {
        public string Tag { get; }

        public ScenarioAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
