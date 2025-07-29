using JWT.Application.Features.CQRS.Commands.DistrictCommands;
using JWT.Application.Features.CQRS.Handlers.Districthandlers;
using JWT.Application.Features.CQRS.Queries.DistrictQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly CreateDistrictCommandHandlers _createDistrictCommandHandler;
        private readonly GetDistrictByIdQueryHandler _getDistrictByIdQueryHandler;
        private readonly GetDistrictQueryHandler _getDistrictQueryHandler;
        private readonly UpdateDistrictCommandHandlers _updateDistrictCommandHandler;
        private readonly RemoveDistrictCommandHandlers _removeDistrictCommandHandler;

        public DistrictsController(
            CreateDistrictCommandHandlers createDistrictCommandHandler,
            GetDistrictByIdQueryHandler getDistrictByIdQueryHandler,
            GetDistrictQueryHandler getDistrictQueryHandler,
            UpdateDistrictCommandHandlers updateDistrictCommandHandler,
            RemoveDistrictCommandHandlers removeDistrictCommandHandler)
        {
            _createDistrictCommandHandler = createDistrictCommandHandler;
            _getDistrictByIdQueryHandler = getDistrictByIdQueryHandler;
            _getDistrictQueryHandler = getDistrictQueryHandler;
            _updateDistrictCommandHandler = updateDistrictCommandHandler;
            _removeDistrictCommandHandler = removeDistrictCommandHandler;
        }

       
        [HttpGet]
        public async Task<IActionResult> DistrictList([FromQuery] int id)
        {
            var values = await _getDistrictQueryHandler.Handle();
            var filtered = values.Where(x => x.CityId == id).ToList();
            return Ok(filtered);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistrict(int id)
        {
            var values = await _getDistrictByIdQueryHandler.Handle(new GetDistrictByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrict(CreateDistrictCommand command)
        {
            await _createDistrictCommandHandler.Handle(command);
            return Ok("Hava Durumu Bilgisi Eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveDistrict(int id)
        {
            await _removeDistrictCommandHandler.Handle(new RemoveDistrictCommand(id));
            return Ok("Hava Durumu Bilgisi Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDistrict(UpdateDistrictCommand command)
        {
            await _updateDistrictCommandHandler.Handle(command);
            return Ok("Hava Durumu Bilgisi Güncellendi");
        }
    }
}
