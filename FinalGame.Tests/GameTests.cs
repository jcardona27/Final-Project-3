using NUnit.Framework;
using FinalGame;
using System.Collections.Generic;

namespace FinalGame.Tests
{
    public class GameTests
    {
        [Test]
        public void GameState_UpdatesHealthCorrectly()
        {
            var state = new GameState();
            state.Update(StatType.Health, -20);

            Assert.AreEqual(30, state.Health);
        }

        [Test]
        public void ScenarioOption_ApplyEffect_ChangesStats()
        {
            var state = new GameState();
            var option = new ScenarioOption
            {
                Description = "Test Option",
                Effects = new Dictionary<StatType, int> { { StatType.Wealth, 15 } }
            };

            option.ApplyEffect(state);

            Assert.AreEqual(65, state.Wealth);
        }

                [Test]
        public void GameState_InitialValues_AreCorrect()
        {
            var state = new GameState();
            Assert.AreEqual(50, state.Health);
            Assert.AreEqual(50, state.Wealth);
            Assert.AreEqual(50, state.Popularity);
        }

        [Test]
        public void ApplyEffect_MultipleStats_AreUpdated()
        {
            var state = new GameState();
            var option = new ScenarioOption
            {
                Description = "Multi-Effect",
                Effects = new Dictionary<StatType, int>
                {
                    { StatType.Health, -10 },
                    { StatType.Popularity, 15 }
                }
            };

            option.ApplyEffect(state);

            Assert.AreEqual(40, state.Health);
            Assert.AreEqual(65, state.Popularity);
        }

        [Test]
        public void ApplyEffect_NullState_ThrowsException()
        {
            var option = new ScenarioOption
            {
                Description = "Null Test",
                Effects = new Dictionary<StatType, int> { { StatType.Health, 5 } }
            };

            Assert.Throws<Exception>(() => option.ApplyEffect(null));
        }

    }
}
