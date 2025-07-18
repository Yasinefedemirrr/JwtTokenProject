using JWT.Application.Features.CQRS.Commands.DistrictCommands;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Handlers.Districthandlers
{
    public class UpdateDistrictCommandHandlers
    {
        private readonly IRepository<District> _repository;

        public UpdateDistrictCommandHandlers(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDistrictCommand command)
        {
            var values = await _repository.GetByIdAsync(command.DistrictId);
            if (values != null)
            {
                values.DistrictName = command.DistrictName;
                values.Date = command.Date;
                values.Temperature = command.Temperature;
                values.CityId = command.CityId;

               
                await _repository.UpdateAsync(values);
            }
        }
    }
}
