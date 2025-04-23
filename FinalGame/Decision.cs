namespace FinalGame
{

    public abstract class Decision
    {
        public string Description { get; set; } = string.Empty;

        public abstract void Apply(GameState state);
    }

    public class BasicDecision : Decision
    {
        public Dictionary<StatType, int> Effects { get; set; } = new();

        public override void Apply(GameState state)
        {
            foreach (var effect in Effects)
            {
                state.Update(effect.Key, effect.Value);
            }
        }
    }
}
