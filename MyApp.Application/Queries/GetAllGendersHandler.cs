using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.Queries
{
    public class GetAllGendersHandler : IQueryHandler<GetAllGendersQuery, IEnumerable<GenderDto>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGendersHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<IEnumerable<GenderDto>> HandleAsync(GetAllGendersQuery query)
        {
            var genders = await _genderRepository.GetAllAsync();
            return genders.Select(gender => new GenderDto
            {
                Id = gender.Id,
                Name = gender.Name
            });
        }
    }
}
