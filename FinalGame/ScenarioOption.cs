using System;
using System.Collections.Generic;

namespace FinalGame
{
    public class ScenarioOption
    {
        public string Description { get; set; } = string.Empty;
        public Dictionary<StatType, int> Effects { get; set; } = new();

        public void ApplyEffect(GameState state)
        {
            if (state == null)
                throw new Exception("Game state cannot be null.");

            foreach (var effect in Effects)
            {
                state.Update(effect.Key, effect.Value);
            }
        }
    }
}
