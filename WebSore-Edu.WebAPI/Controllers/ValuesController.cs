using Microsoft.AspNetCore.Mvc;

namespace WebSore_Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<int, string> _Values = Enumerable.Range(1, 20)
            .Select(i => (key: i, value: $"Значение {i}"))
            .ToDictionary(v => v.key, v => v.value);


        [HttpGet]
        public IActionResult Get() => Ok(_Values.Values);


        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var result = _Values.TryGetValue(Id, out var value);
            return result ? Ok(value) : NotFound();
        }


        [HttpGet("count")]
        //public IActionResult Count() => Ok(_Values.Count);
        //public ActionResult<int> Count() => _Values.Count;
        public int Count() => _Values.Count;

        [HttpPost] // На адрес контроллера
        [HttpPost("add")]
        public IActionResult Add( /*[FromBody]*/ string Value)
        {
            var id = _Values.Count == 0 ? 1 : _Values.Keys.Max() +1;
            _Values[id] = Value;

            return CreatedAtAction(nameof(Get), new { Id = id });
        }


        [HttpPut("{Id}")]
        public IActionResult Replace(int Id, [FromBody] string Value)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();

            _Values[Id] = Value;
            return Ok();
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();

            _Values.Remove(Id);
            return Ok();
        }
    }
}
