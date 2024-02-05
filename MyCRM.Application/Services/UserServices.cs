using Microsoft.AspNetCore.Http;
using MyCrm.Application.Extensions;
using MyCRM.Application.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Security;
using MyCRM.Application.StaticTools;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Interfaces;
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
        private readonly IUserRepository _userRepositort;

        public UserServices(IUserRepository userRepository)
        {
            this._userRepositort = userRepository;
        }

        //Create Marketer
        public async Task<AddMarketerResult> AddMarketer(AddMarketerViewModel marketer , IFormFile imageProfile)
        {
            //Upload Image
            var imageProfileName = "";

            if(imageProfile?.Length > 0 )
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

        //Filter
        public async Task<FilterUserViewModel> Filter(FilterUserViewModel filter)
        {
            return await _userRepositort.filterUser(filter);
        }

        //Get User For Edite
        public async Task<EditeMarketerViewModle> GetMarketerforEdite(long UserId)
        {
            var user = await _userRepositort.GetUserDetailById(UserId);
            if(user == null)
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
        public async Task<EditeMarketerResult> EditeMarketer(EditeMarketerViewModle marketerEdite , IFormFile imageProfile)
        {
            var userEdite = await _userRepositort.GetUserDetailById(marketerEdite.UserId);
            if(userEdite == null)
            {
                return EditeMarketerResult.Faild;
            }

            var imageProfileName = "";
            if(imageProfile?.Length > 0)
            {
                imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                imageProfile.AddImageToServer(imageProfileName , FilePath.UploadImageProfileServer, 280, 280);
            }

            userEdite.Email = marketerEdite.Email;
            userEdite.FirstName = marketerEdite.FirstName;
            userEdite.IntroduceName = marketerEdite.IntroduceName;
            userEdite.LastName = marketerEdite.LastName;
            userEdite.MobilePhone = marketerEdite.MobilePhone;
            userEdite.UserName = marketerEdite.UserName;

            if(!string.IsNullOrEmpty(imageProfileName))
            {
                userEdite.ImageName = imageProfileName; 
            }

            await _userRepositort.UpdateUser(userEdite);

            var MyMarketer = await _userRepositort.GetMarketerById(marketerEdite.UserId);

            MyMarketer.Age = marketerEdite.Age;
            MyMarketer.MarketerEducation = marketerEdite.Education;
            MyMarketer.FieldStudy = marketerEdite.FieldStudy;
            MyMarketer.IrCode = marketerEdite.IrCode;

            if(MyMarketer == null)
            {
                return EditeMarketerResult.Faild;
            }

            await _userRepositort.UpdateMarketerAsync(MyMarketer);

            await _userRepositort.SaveChangesAsync();

            return EditeMarketerResult.Success;
        }

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
    }
}
