using Microsoft.AspNetCore.Http;
using MyCRM.Application.Extensions;
using MyCRM.Application.Interfaces;
using MyCRM.Application.Security;
using MyCRM.Application.StaticTools;
using MyCRM.Domain.Entities.Account;
using MyCRM.Domain.Interfaces;
using MyCRM.Domain.ViewModel.User;
using System;
using System.Collections.Generic;
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


        
        public async Task<AddMarketerResult> AddMarketer(AddMarketerViewModel marketer , IFormFile imageProfile)
        {
            //Upload Image
            var imageProfileName = "";

            imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
            imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);

            /*            if(imageProfile.Length>0)
                        {
                            imageProfileName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(imageProfile.FileName);
                            imageProfile.AddImageToServer(imageProfileName, FilePath.UploadImageProfileServer, 280, 280);
                        }*/


            //Add User
            var user = new User()
            {
                FirstName = marketer.FirstName,
                LastName = marketer.LastName,
                UserName = marketer.UserName,
                Password = marketer.Password,
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


        public async Task<FilterUserViewModel> Filter(FilterUserViewModel filter)
        {
            return await _userRepositort.filterUser(filter);
        }
    }
}
