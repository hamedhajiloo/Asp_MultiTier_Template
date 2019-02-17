using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Prj.Services.Base;
using System.Data.Entity;

namespace Prj.Services.Users
{
    public interface IUserActiveCodeService : IBaseService<UserActiveCode, string>
    {
        bool CheckTestShop(string phoneNumber);
    }

    public class UserActiveCodeService : BaseService<UserActiveCode, string>, IUserActiveCodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<UserActiveCode> _userActiveCodes;

        public UserActiveCodeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userActiveCodes = unitOfWork.Set<UserActiveCode>();
        }

        public bool CheckTestShop(string phoneNumber)
        {
            if (phoneNumber == "09191111111")
                return true;
            return false;
        }
    }
}
