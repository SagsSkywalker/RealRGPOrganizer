using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class SkillController
    {
        public SkillController(Skill s)
        {
            skill = s;
        }

        public Skill skill
        {
            get;
            set;
        }

        /// <summary>
        /// Increases the level by a given amount.
        /// </summary>
        /// <param name="amount">Amount of Level Points</param>
        public void IncreaseLevel(int amount)
        {
            skill.Level += amount;
        }

        /// <summary>
        /// Decreases the level by a given amount.
        /// </summary>
        /// <param name="amount">Amount of Level Points</param>
        public void DecreaseLevel(int amount)
        {
            skill.Level -= amount;
        }
    }
}
