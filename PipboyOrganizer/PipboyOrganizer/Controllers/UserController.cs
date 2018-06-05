using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class UserController
    {
        public UserController(User u)
        {
            user = u;
        }

        public User user
        {
            get;
            set;
        }

        /// <summary>
        /// Adds experience to the User.
        /// </summary>
        /// <param name="amount">Amount of experience you want to give</param>
        public void AddExperience(int amount){
            user.Experience += amount;
            CalculateLevel();
        }

        /// <summary>
        /// Calculates the level.
        /// </summary>
        private void CalculateLevel(){
            if (user.Experience == 1000)
                user.UserLevel = 2;
            else if (user.Experience > 1000 && user.Experience < 3000)
                user.UserLevel = 3;
            else if (user.Experience > 3000 && user.Experience < 6000)
                user.UserLevel = 4;
            else if (user.Experience > 6000 && user.Experience < 10000)
                user.UserLevel = 5;
            else if (user.Experience > 10000 && user.Experience < 15000)
                user.UserLevel = 6;
            else if (user.Experience > 15000 && user.Experience < 21000)
                user.UserLevel = 7;
            else if (user.Experience > 21000 && user.Experience < 28000)
                user.UserLevel = 8;
            else if (user.Experience > 28000 && user.Experience < 36000)
                user.UserLevel = 9;
            else if (user.Experience > 36000 && user.Experience < 45000)
                user.UserLevel = 10;
            else if (user.Experience > 45000 && user.Experience < 55000)
                user.UserLevel = 11;
            else if (user.Experience > 55000 && user.Experience < 66000)
                user.UserLevel = 12;
            else if (user.Experience > 66000 && user.Experience < 78000)
                user.UserLevel = 13;
            else if (user.Experience > 78000 && user.Experience < 91000)
                user.UserLevel = 14;
            else if (user.Experience > 91000 && user.Experience < 105000)
                user.UserLevel = 15;
            else if (user.Experience > 105000 && user.Experience < 120000)
                user.UserLevel = 16;
            else if (user.Experience > 120000 && user.Experience < 136000)
                user.UserLevel = 17;
            else if (user.Experience > 136000 && user.Experience < 153000)
                user.UserLevel = 18;
            else if (user.Experience > 153000 && user.Experience < 171000)
                user.UserLevel = 19;
            else if (user.Experience > 171000 && user.Experience < 190000)
                user.UserLevel = 20;
            else if (user.Experience > 190000 && user.Experience < 210000)
                user.UserLevel = 21;
            else if (user.Experience > 210000){
                int lvls = 0;
                lvls = (user.Experience - 210000) / 40000;
                user.UserLevel = 21 + lvls;
            }
        }
    }
}
