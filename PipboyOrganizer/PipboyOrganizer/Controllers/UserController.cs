using System;
using PipboyOrganizer.Models;
namespace PipboyOrganizer.Controllers
{
    public class UserController
    {
        //Un comentario chidori

        public UserController(User u)
        {
        }

        public User user
        {
            get;
            set;
        }

        public void AddExperience(int amount){
            
        }

        public void AdvanceLevel(){
            user.UserLevel += 1;
        }

        private void CalculateLevel(){
            
        }
    }
}
