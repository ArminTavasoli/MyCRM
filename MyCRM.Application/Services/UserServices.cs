using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyCrm.Application.Extensions;
using MyCRM.Application.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Security;
using MyCRM.Application.StaticTools;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.Account;
using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRM.Application.Services
{
    public class UserServices : IUserService
    {
        #region Ctor
        private readonly IUserRepository _userRepositort;

        public UserServices(IUserRepository userRepository)
        {
            this._userRepositort = userRepository;
        }
        #endregion

        #region Create Marketer
        //Create Marketer
        public async Task<AddMarketerResult> AddMarketer(AddMarketerViewModel marketer, IFormFile imageProfile)
        {
            //Upload Image
            var imageProfileName = "";

            if (imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
            }

            //Add User
            var user = new User()
            {
                FirstName = marketer.FirstName,
                LastName = marketer.LastName,
                UserName = marketer.UserName,
                Password = PasswordHelper.EncodePasswordMd5(marketer.Password),
                Email = marketer.Email,
                MobilePhone = marketer.MobilePhone,
                IntroduceName = marketer.IntroduceName,
            };

            if (!string.IsNullOrEmpty(imageProfileName))
            {
                user.ImageName = imageProfileName;
            }

            await _userRepositort.AddUser(user);
            await _userRepositort.SaveChangesAsync();


            //Add Marketer
            var newMarketer = new Marketer()
            {
                UserID = user.UserID,
                FieldStudy = marketer.FieldStudy,
                Age = marketer.Age,
                IrCode = marketer.IrCode,
                MarketerEducation = marketer.MarketerEducation
            };

            await _userRepositort.AddMarketerAsync(newMarketer);
            await _userRepositort.SaveChangesAsync();


            return AddMarketerResult.Success;

        }
        #endregion


        #region Filter User
        //Filter
        public async Task<FilterUserViewModel> Filter(FilterUserViewModel filter)
        {
            return await _userRepositort.filterUser(filter);
        }
        #endregion

        #region Edite Marketer
        //Get User For Edite
        public async Task<EditeMarketerViewModle> GetMarketerforEdite(long UserId)
        {
            var user = await _userRepositort.GetUserDetailById(UserId);
            if (user == null)
            {
                return null;
            }

            var result = new EditeMarketerViewModle()
            {
                UserId = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobilePhone = user.MobilePhone,
                Age = user.Marketer.Age,
                Education = user.Marketer.MarketerEducation,
                Email = user.Email,
                FieldStudy = user.Marketer.FieldStudy,
                IntroduceName = user.IntroduceName,
                IrCode = user.Marketer.IrCode,
                ImageName = user.ImageName


            };

            return result;
        }


        //Edite Marketer
        public async Task<EditeMarketerResult> EditeMarketer(EditeMarketerViewModle marketerEdite, IFormFile imageProfile)
        {
            var userEdite = await _userRepositort.GetUserDetailById(marketerEdite.UserId);
            if (userEdite == null)
            {
                return EditeMarketerResult.Faild;
            }

            var imageProfileName = "";
            if (imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
            }

            userEdite.Email = marketerEdite.Email;
            userEdite.FirstName = marketerEdite.FirstName;
            userEdite.IntroduceName = marketerEdite.IntroduceName;
            userEdite.LastName = marketerEdite.LastName;
            userEdite.MobilePhone = marketerEdite.MobilePhone;
            userEdite.UserName = marketerEdite.UserName;

            if (!string.IsNullOrEmpty(imageProfileName))
            {
                userEdite.ImageName = imageProfileName;
            }

            await _userRepositort.UpdateUser(userEdite);

            var MyMarketer = await _userRepositort.GetMarketerById(marketerEdite.UserId);

            MyMarketer.Age = marketerEdite.Age;
            MyMarketer.MarketerEducation = marketerEdite.Education;
            MyMarketer.FieldStudy = marketerEdite.FieldStudy;
            MyMarketer.IrCode = marketerEdite.IrCode;

            if (MyMarketer == null)
            {
                return EditeMarketerResult.Faild;
            }

            await _userRepositort.UpdateMarketerAsync(MyMarketer);

            await _userRepositort.SaveChangesAsync();

            return EditeMarketerResult.Success;
        }
        #endregion



        #region Create Customer
        //Create Customer
        public async Task<AddCustomerResult> AddCustomer(AddCustomerViewModel newCustomer, IFormFile imageProfile)
        {
            //Upload Image
            var imageProfileName = "";

            if (imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
            }

            //Add User
            var user = new User()
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                UserName = newCustomer.UserName,
                Password = PasswordHelper.EncodePasswordMd5(newCustomer.Password),
                Email = newCustomer.Email,
                MobilePhone = newCustomer.MobilePhone,
                IntroduceName = newCustomer.IntroduceName,
            };

            if (!string.IsNullOrEmpty(imageProfileName))
            {
                user.ImageName = imageProfileName;
            }

            await _userRepositort.AddUser(user);
            await _userRepositort.SaveChangesAsync();


            var customer = new Customer()
            {
                UserID = user.UserID,
                Job = newCustomer.Job,
                CompanyName = newCustomer.CompanyName
            };

            await _userRepositort.AddCustomer(customer);
            await _userRepositort.SaveChangesAsync();

            return AddCustomerResult.Success;

        }
        #endregion


        #region Edite Customer
        //Get Customer for Edite
        public async Task<EditeCustomerViewModel> FillEditeCustomerViewModel(long UserId)
        {
            var customer = await _userRepositort.GetCustomerWithId(UserId);
            if (customer == null)
            {
                return null;
            }

            var user = await _userRepositort.GetUserWithID(UserId);

            var result = new EditeCustomerViewModel
            {
                UserId = user.UserID,
                Job = customer.Job,
                CompanyName = customer.CompanyName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IntroduceName = user.IntroduceName,
                Email = user.Email,
                MobilePhone = user.MobilePhone,
                ImageName = user.ImageName,
                UserName = user.UserName
            };
            return result;
        }

        //Edite Customer
        public async Task<EditeCustomerResult> EditeCustomer(EditeCustomerViewModel editeCustomer, IFormFile imageProfile)
        {
            var customer = await _userRepositort.GetCustomerWithId(editeCustomer.UserId);
            if (customer == null)
            {
                return EditeCustomerResult.Fail;
            }

            var user = await _userRepositort.GetUserWithID(editeCustomer.UserId);
            if (user == null)
            {
                return EditeCustomerResult.Fail;
            }

            var imageProfileName = "";
            if (imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
            }

            customer.Job = editeCustomer.Job;
            customer.CompanyName = editeCustomer.CompanyName;

            user.UserID = editeCustomer.UserId;
            user.UserName = editeCustomer.UserName;
            user.FirstName = editeCustomer.FirstName;
            user.LastName = editeCustomer.LastName;
            user.IntroduceName = editeCustomer.IntroduceName;
            user.MobilePhone = editeCustomer.MobilePhone;
            user.Email = editeCustomer.Email;

            if (!string.IsNullOrEmpty(imageProfileName))
            {
                user.ImageName = imageProfileName;
            }
            //To do add gender

            await _userRepositort.UpdateCustomer(customer);
            await _userRepositort.UpdateUser(user);

            await _userRepositort.SaveChangesAsync();

            return EditeCustomerResult.Success;

        }
        #endregion



        #region Get Customer By ID
        //Get Customer with Id
        public async Task<Customer> GetCustomerById(long CustomerId)
        {
            return await _userRepositort.GetCustomerWithId(CustomerId);
        }
        #endregion

        #region Get MarketerList
        public async Task<List<Marketer>> GetMarketerList()
        {
            var marketers = await _userRepositort.GetMarketerQueryable();
            return marketers.ToList();
        }
        #endregion


        public async Task<LoginUserResult> LoginUser(LoginUserViewModel loginViewModel)
        {
            var userList = await _userRepositort.GetUserQueryable();
            var user = await userList.FirstOrDefaultAsync(u => u.UserName == loginViewModel.UserName);

            if (user == null)
            {
                return LoginUserResult.NotFound;
            }

            if (user.Password != PasswordHelper.EncodePasswordMd5(loginViewModel.Password))
            {
                return LoginUserResult.PasswordNotCorrect;
            }

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var userList = await _userRepositort.GetUserQueryable();
            var user = userList.FirstOrDefault(u => u.UserName == userName);

            return user;
        }
    }
}
