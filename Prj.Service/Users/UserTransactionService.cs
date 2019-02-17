using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Domain.Users;
using Prj.Services.Base;
using System.Data.Entity;

namespace Prj.Services.Users
{
    public interface IUserTransactionService : IBaseService<UserTransaction, long>
    {
    }

    public class UserTransactionService : BaseService<UserTransaction, long>, IUserTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<UserTransaction> _userTransactions;

        public UserTransactionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userTransactions = unitOfWork.Set<UserTransaction>();
        }
    }
}
