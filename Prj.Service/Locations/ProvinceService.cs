using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Domain.Locations;
using Prj.Services.Base;
using System.Data.Entity;

namespace Prj.Services.Locations
{
    public interface IProvinceService : IBaseService<Province, int>
    {
    }

    public class ProvinceService : BaseService<Province, int>, IProvinceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<Province> _provinces;

        public ProvinceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _provinces = unitOfWork.Set<Province>();
        }
    }
}
