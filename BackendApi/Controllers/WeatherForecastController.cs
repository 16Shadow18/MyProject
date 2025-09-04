using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            List<string> result = Summaries;

            if (sortStrategy.HasValue)
            {
                switch (sortStrategy.Value)
                {
                    case 1: // по возрастанию
                        result = result.OrderBy(x => x).ToList();
                        break;

                    case -1: // по убыванию
                        result = result.OrderByDescending(x => x).ToList();
                        break;

                    default:
                        return BadRequest("Некорректное значение параметра sortStrategy");
                }
            }

            return Ok(result);
        }

        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Неверный индекс!");
            }

            return Ok(Summaries[index]);
        }

        [HttpGet("count/{name}")]
        public IActionResult GetCountByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя не может быть пустым!");
            }

            int count = Summaries.Count(x => x.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Ok(count);
        }


        [HttpPost]
        public IActionResult Add( string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя не может быть пустым!");
            }

            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Неверный индекс!");
            }

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя не может быть пустым!");
            }

            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Неверный индекс!");
            }

            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}
