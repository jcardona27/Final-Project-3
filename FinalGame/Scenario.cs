using System.Collections.Generic;

namespace FinalGame
{
    public class Scenario
    {
        public string Text { get; set; } = string.Empty;
        public List<ScenarioOption> Options { get; set; } = new();
    }
}
