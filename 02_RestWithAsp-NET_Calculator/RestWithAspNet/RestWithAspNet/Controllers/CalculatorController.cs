using Microsoft.AspNetCore.Mvc;
using System.Globalization;
namespace RestWithAspNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{SecondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Nâo foi possivel obter a soma");
        }
        [HttpGet("sub/{firstNumber}/{SecondNumber}")]
        public IActionResult Sub(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = Convert.ToDecimal(firstNumber) - Convert.ToDecimal(secondNumber);
                return Ok(sub.ToString());
            }
            return BadRequest("Nao foi possivel obter a subtração");
        }
        [HttpGet("mult/{firstNumber}/{secondNumber}")] 
        public IActionResult Mult(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var mult = Convert.ToDecimal(firstNumber) * Convert.ToDecimal(secondNumber);
                return Ok(mult.ToString());
            }
            return BadRequest("Não foi possivel obter a Multiplicação");
        }
        [HttpGet("div/{firstNumber}/{secondNumber}")] 
        public IActionResult Div(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = Convert.ToDecimal(firstNumber) / Convert.ToDecimal(secondNumber);
                return Ok(div.ToString());
            }
            return BadRequest("Não foi possivel realizar a divisão");
        }
        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Media(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = (Convert.ToDecimal(firstNumber) + Convert.ToDecimal(secondNumber)) / 2;
                return Ok(sum.ToString());
            }
            return BadRequest("Não foi possivel realizar a divisão");
        }

        [HttpGet("squareRoot/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var squareRoot = Math.Sqrt((double)Convert.ToDecimal(firstNumber));
                return Ok(squareRoot.ToString());
            }
            return BadRequest("Não foi possivel calcular a raiz quadrada");
        }
        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(strNumber, 
                NumberStyles.Any, 
                NumberFormatInfo.InvariantInfo, 
                out number);

            return isNumber;
        }
    }
}