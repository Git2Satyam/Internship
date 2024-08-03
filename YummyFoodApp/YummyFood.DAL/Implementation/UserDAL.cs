using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YummyFood.Core.Entities;
using YummyFood.DAL.Interface;
using YummyFood.Models;

namespace YummyFood.DAL.Implementation
{
    public class UserDAL : DataAccessLayer<User>, IUserDAL
    {
        YummyFoodContext _context
        {
            get
            {
                return db as YummyFoodContext;
            }
        }

        public UserDAL(YummyFoodContext _DB) : base(_DB)
        {

        }

        public IEnumerable<UserModel> GetAllUser()
        {
            try
            {
                var getUser = _context.Users.Where(u => u.Enabled == true && u.Deleted == false)
                    .Select(x => new UserModel 
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        Password = x.Password,
                        PhoneNumber = x.PhoneNumber,
                        Enabled = x.Enabled,
                        Deleted = x.Deleted,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate,
                        DateOFBirth = x.DateOfBirth
                    }).ToList();

                return getUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUserById(int id)
        {
            UserModel userML = new UserModel();
            try
            {
                var getUser = _context.Users.FirstOrDefault(u => u.Id == id && u.Enabled == true && u.Deleted == false);
                if(getUser != null)
                {
                    userML.FirstName = getUser.FirstName;
                    userML.LastName = getUser.LastName;
                    userML.Email = getUser.Email;
                    userML.Password = getUser.Password;
                    userML.PhoneNumber = getUser.PhoneNumber;
                    userML.Enabled = getUser.Enabled;
                    userML.Deleted = getUser.Deleted;
                    userML.CreatedBy = getUser.CreatedBy;
                    userML.CreatedDate = getUser.CreatedDate;
                    userML.ModifiedDate = getUser.ModifiedDate;
                    userML.ModifiedBy = getUser.ModifiedBy;
                    userML.DateOFBirth = getUser.DateOfBirth;

                    return userML;
                }
                else
                {
                    return userML;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrUpdateUser(UserModel userModel)
        {
            int Id = 0;
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userModel.Id);
                if(user != null)
                {
                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    user.Email = userModel.Email;
                    user.Password = userModel.Password;
                    user.PhoneNumber = userModel.PhoneNumber;
                    user.Enabled = userModel.Enabled;
                    user.Deleted = userModel.Deleted;
                    user.ModifiedBy = userModel.ModifiedBy;
                    user.ModifiedDate = userModel.ModifiedDate;
                    user.DateOfBirth = userModel.DateOFBirth;

                    _context.SaveChanges();
                    Id = user.Id;
                }
                else
                {
                    var birthdate = Convert.ToDateTime(userModel.DateOFBirth).ToString(); 
                    var add = new User
                    {
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        Email = userModel.Email,
                        Password = userModel.Password,
                        PhoneNumber = userModel.PhoneNumber,
                        DateOfBirth = birthdate,
                        Enabled = true,
                        Deleted = false,
                        //CreatedBy = userModel.CreatedBy,
                        CreatedDate = DateTime.Now,

                    };
                    _context.Users.Add(add);
                    _context.SaveChanges();
                    Id = add.Id;
                }
                return Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var delete = _context.Users.FirstOrDefault(x => x.Id == id && x.Deleted == false);
                if(delete != null)
                {
                    delete.Deleted = true;
                    _context.Entry(delete).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
