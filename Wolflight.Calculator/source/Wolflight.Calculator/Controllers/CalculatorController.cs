using Microsoft.AspNetCore.Mvc;

namespace Wolflight.Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : Controller
    {
        private const string TotalName = "Total";


        private decimal RetrieveTotal()
        {
            return ToDecimal(HttpContext.Session.GetString(TotalName));
        }

        private async Task<IActionResult> StoreTotalAsync(decimal value)
        {
            HttpContext.Session.SetString(TotalName, FromDecimal(value));

            // Ensure the value is persisted.
            await HttpContext.Session.CommitAsync();

            return Ok();
        }

        private static string FromDecimal(decimal value)
        {
            return value.ToString();
        }

        private static decimal ToDecimal(string? value)
        {
            if (value != null)
            {
                return Decimal.Parse(value);
            }
            else
            {
                return 0M;
            }

        }

        [HttpGet()]
        [Route("/api/Calculator/Total")]
        public decimal? GetTotal()
        {
            return RetrieveTotal();
        }

        [HttpPut()]
        [Route("/api/Calculator/Add")]
        public async Task<IActionResult> AddValueAsync(decimal value)
        {
            return await StoreTotalAsync(RetrieveTotal() + value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Subtract")]
        public async Task<IActionResult> SubtractValueAsync(decimal value)
        {
            return await StoreTotalAsync(RetrieveTotal() - value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Multiply")]
        public async Task<IActionResult> MultiplyValueAsync(decimal value)
        {
            return await StoreTotalAsync(RetrieveTotal() * value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Divide")]
        public async Task<IActionResult> DivideValueAsync(decimal value)
        {
            if (value == 0M)
            {
                return BadRequest(
                    new ArgumentOutOfRangeException(
                        nameof(value),
                        $"{nameof(value)} cannot be zero."
                    )
                );
            }
            return await StoreTotalAsync(RetrieveTotal() / value);
        }


    }
}
