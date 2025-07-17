using JWT.Application.Features.CQRS.Commands.CityWeatherCommands;
using JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers;
using JWT.Application.Features.CQRS.Queries.CityWeatherQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAPI.Controllers
{
    [Authorize(Roles = "admin")] 
    [Route("api/[controller]")]
    [ApiController]
    public class CityWeathersController : ControllerBase
    {
        private readonly CreateCityWeatherCommandHandler _createCityWeatherCommandHandler;
        private readonly GetCityWeatherByIdQueryHandler _getCityWeatherByIdQueryHandler;
        private readonly GetCityWeatherQueryHandler _getCityWeatherQueryHandler;
        private readonly UpdateCityWeatherCommandHandler _updateCityWeatherCommandHandler;
        private readonly RemoveCityWeatherCommandHandler _removeCityWeatherCommandHandler;

        public CityWeathersController(
            CreateCityWeatherCommandHandler createCityWeatherCommandHandler,
            GetCityWeatherByIdQueryHandler getCityWeatherByIdQueryHandler,
            GetCityWeatherQueryHandler getCityWeatherQueryHandler,
            UpdateCityWeatherCommandHandler updateCityWeatherCommandHandler,
            RemoveCityWeatherCommandHandler removeCityWeatherCommandHandler)
        {
            _createCityWeatherCommandHandler = createCityWeatherCommandHandler;
            _getCityWeatherByIdQueryHandler = getCityWeatherByIdQueryHandler;
            _getCityWeatherQueryHandler = getCityWeatherQueryHandler;
            _updateCityWeatherCommandHandler = updateCityWeatherCommandHandler;
            _removeCityWeatherCommandHandler = removeCityWeatherCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CityWeatherList()
        {
            var values = await _getCityWeatherQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityWeather(int id)
        {
            var values = await _getCityWeatherByIdQueryHandler.Handle(new GetCityWeatherByIdQuery(id));
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCityWeather(CreateCityWeatherCommand command)
        {
            await _createCityWeatherCommandHandler.Handle(command);
            return Ok("Hava Durumu Bilgisi Eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCityWeather(int id)
        {
            await _removeCityWeatherCommandHandler.Handle(new RemoveCityWeatherCommand(id));
            return Ok("Hava Durumu Bilgisi Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCityWeather(UpdateCityWeatherCommand command)
        {
            await _updateCityWeatherCommandHandler.Handle(command);
            return Ok("Hava Durumu Bilgisi Güncellendi");
        }
    }
}
