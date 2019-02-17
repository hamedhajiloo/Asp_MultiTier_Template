using AutoMapper;
using Prj.DataAccess.Context;
using Prj.Domain.Locations;
using Prj.Services.Base;
using System.Data.Entity;

namespace Prj.Services.Locations
{
    public interface ICityService : IBaseService<City, int>
    {
    }

    public class CityService : BaseService<City, int>, ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDbSet<City> _cities;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cities = unitOfWork.Set<City>();
        }
    }
}
