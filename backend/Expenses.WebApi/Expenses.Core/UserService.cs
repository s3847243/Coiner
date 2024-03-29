﻿using Expenses.Core.CustomExceptions;
using Expenses.Core.DTO;
using Expenses.Core.Utilties;
using Expenses.DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (dbUser == null || dbUser.Password == null || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }
            return new AuthenticatedUser()
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateAuthToken(user.Username),
                //Token = ""
            };


        }




        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if (checkUsername != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            //if (!string.IsNullOrEmpty(user.Password))
            //{
            //    user.Password = _passwordHasher.HashPassword(user.Password);
            //}
            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateAuthToken(user.Username)
                //Token = "test token"
            };
        }

    }

}
