using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Prj.Services.Base;
using Prj.Services.Identity;
using Prj.Services.Models;
using Prj.Services.Utilities;
using Prj.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Prj.Services.Users
{
    public interface IUserService : IBaseService<User, string>
    {
        void Add(User user, string password);
        void Edit(User user, string password);
        Task<ResultModel> AddToRoleAsync(User user, string roleName);
        Task<User> FindByUserNameAsync(string userName, bool asNoTracking = true);
        Task<User> FindAsync(string userName, string password);
        Task<List<Role>> GetUserRolesAsync(string userId, bool asNoTracking = true);
        //Task<UserRegentServiceModel> GetUserRegent(string userId);
    }

    public class UserService : BaseService<User, string>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IRoleManager _roleManager;
        private readonly IRoleService _roleService;
        private readonly IDbSet<User> _users;
        private readonly IDbSet<UserRole> _userRoles;

        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IPasswordHasherService passwordHasherService,
            IRoleManager roleManager,
            IRoleService roleService) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasherService = passwordHasherService;
            _roleManager = roleManager;
            _roleService = roleService;
            _users = unitOfWork.Set<User>();
            _userRoles = unitOfWork.Set<UserRole>();
        }

        public void Add(User user, string password)
        {
            user.PasswordHash = _passwordHasherService.HashPassword(password);
            _unitOfWork.MarkAsAdded(user);
        }

        public void Edit(User user, string password)
        {
            if (password != null)
                user.PasswordHash = _passwordHasherService.HashPassword(password);

            _unitOfWork.MarkAsChanged(user);
        }

        public async Task<User> FindByUserNameAsync(string userName, bool asNoTracking = true)
        {
            var users = _users.AsQueryable();

            if (asNoTracking)
                users = users.AsNoTracking();

            return await users.FirstOrDefaultAsync(r => r.UserName == userName);
        }

        public async Task<ResultModel> AddToRoleAsync(User user, string roleName)
        {
            var role = await _roleService.FindByRoleNameAsync(roleName);

            var userRole = await _userRoles
                .AsNoTracking()
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            foreach (var item in userRole)
                _unitOfWork.MarkAsDeleted(item);

            _userRoles.Add(new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            return new ResultModel(succeeded: true, errors: null);
        }

        public async Task<User> FindAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await FindByUserNameAsync(userName);

            if (user == null)
                return null;

            var result = _passwordHasherService
                .VerifyHashedPassword(user.PasswordHash, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<List<Role>> GetUserRolesAsync(string userId, bool asNoTracking = true)
        {
            var query = _userRoles.AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.Where(q => q.UserId == userId)
                .Select(q => q.Role).ToListAsync();
        }

        //public async Task<UserRegentServiceModel> GetUserRegent(string userId)
        //{
        //    var user = await _users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == userId);

        //    var model =  new UserRegentServiceModel()
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        UserName = user.UserName,
        //        RoleFaName = user.Roles.Select(q => q.Role.FaName).FirstOrDefault(),
        //        RoleEnName = user.Roles.Select(q => q.Role.Name).FirstOrDefault(),
        //        WalletCharge = user.WalletCharge,
        //        Avatar = user.Avatar
        //    };

        //    model.Avatar = Url.ResolveDynamicUrl("PostIcon", $"photoUrl={model.Avatar ?? Url.DefaultUrl}");

        //    return model;
        //}
    }
}
