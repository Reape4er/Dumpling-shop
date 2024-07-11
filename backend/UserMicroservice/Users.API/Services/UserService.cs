using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Cryptography;
using System.Text;
using Users.API.Controllers;
using Users.API.Middlewares;
using Users.API.Models;
using Users.API.Utils;
using Users.db;
using Users.db.Entities;

namespace Users.API.Services
{
    public interface IUserService {
        Task<List<DtoUser>> GetUsersAsync();
        Task<DtoUser> CreateUser(DtoUserRegistration userData);
        Task<DtoUser> GetUserById (int id);
        Task PutUserById(DtoUser userData);
        Task DeleteUserByIdAsync(int id);
        Task<DtoAuthenticationResponse?> UserAuthentication(DtoAuthenticationRequest user);
        //Task<DtoAuthenticationResponse> UserAuthentication(DtoAuthenticationRequest userRequest);
    }
    public class UserService: IUserService
    {
        private readonly MainContext _context;
        private readonly IMapper _automapper;
        private readonly IJWTUtils _jwtUtils;
        public UserService(MainContext context, IMapper automapper, IJWTUtils jwtUtils)
        {
            _context = context;
            _automapper = automapper;
            _jwtUtils = jwtUtils;
        }
        public async Task<List<DtoUser>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _automapper.Map<List<DtoUser>>(users);
        }
        public async Task<DtoUser> CreateUser(DtoUserRegistration userData) {
            var password = GeneratePasswordHash(userData.Password);
            var newUser = _automapper.Map<DbUser>(userData);
            newUser.PasswordHash = password.PasswordHash;
            newUser.PasswordSalt = password.PasswordSalt;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            DtoUser newDtoUser = _automapper.Map<DtoUser>(newUser);
            return newDtoUser;
        }
        public async Task<DtoUser> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return _automapper.Map<DtoUser>(user);
        }
        private Password GeneratePasswordHash(String password)
        {
            var hmac = new HMACSHA512();
            var password_hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var password_salt = hmac.Key;
            return new Password { PasswordHash = password_hash, PasswordSalt = password_salt };
        }
        public async Task<DtoAuthenticationResponse?> UserAuthentication (DtoAuthenticationRequest userRequest)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userRequest.Email);
            if (user == null) {
                return null;
            }
            if (!checkPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt)) {
                return null;
            }
            var token = _jwtUtils.GenerateJWTToken(user);
            var userResponse = _automapper.Map<DtoAuthenticationResponse>(user);
            userResponse.Token = token;
            return userResponse;
        }
        private Boolean checkPasswordHash(string password, byte[] password_hash, byte[] password_salt)
        {
            var hmac = new HMACSHA512(password_salt);
            var password_hash_for_check = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            var string_password_hash = Convert.ToBase64String(password_hash);
            if (password_hash_for_check == string_password_hash)
            {
                return true;
            }
            return false;

        }
        public async Task PutUserById(DtoUser user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (dbUser == null)
            {
                return;
            }
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.MiddleName = user.MiddleName;
            dbUser.Email = user.Email;
            dbUser.Phone = user.Phone;
            dbUser.gender = user.gender;
            dbUser.Role = user.Role;
            dbUser.Address = user.Address;
            _context.Entry(dbUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserByIdAsync(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUser == null)
            {
                return;
            }
            dbUser.Deleted = DateTimeOffset.UtcNow;
            _context.Entry(dbUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
