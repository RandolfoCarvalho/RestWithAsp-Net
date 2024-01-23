using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Model;
using RestWithAspNet.Business;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Filters;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        //injecao de dependencias
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());

        }
        //O id será recebido na requisição, assim diferenciando as duas funções Get
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if(person == null)
            {
                return NotFound("Usuario não encontrado na base de dados");
            }
            return Ok(person);
        }
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        //converte o que vier no body em um tipo "Person"
        public IActionResult Post([FromBody] PersonVO person)
        {
            
            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Create(person));
        }
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        //recebe do corpo da requisicao
        public IActionResult Put([FromBody] PersonVO person)
        {

            if (person == null)
            {
                return BadRequest();
            }
            return Ok(_personBusiness.Update(person));
        }
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}