﻿using Chotiskazal.Dal.Repo;
using Chotiskazal.DAL;


namespace Chotiskazal.Dal.Services
{
    public class UserService
    {
        private readonly UserRepo _repository;
        public UserService(UserRepo repository)
        {
            _repository = repository;
        }

        public int AddUser(User user)
        {
            return _repository.AddUser(user);
        }

        public void DeleteUser(User user)
        {

        }

        public User GetUser(int userId)
        {
            return null;
        }

   

        public bool IsUserOnline(User user)
        {
            return true;
        }

        public User[] GetAllUser()
        {
            return _repository.GetAllUsers();
        }


        public User GetUserByLogin(string login)
        {
            return _repository.GetUserByLogin(login);
        }
    }
}
