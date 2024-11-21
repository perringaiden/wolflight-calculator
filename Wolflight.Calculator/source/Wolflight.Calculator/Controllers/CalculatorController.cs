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

        private void StoreTotal(decimal value)
        {
            HttpContext.Session.SetString(TotalName, FromDecimal(value));
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
        public void AddValue(decimal value)
        {
            StoreTotal(RetrieveTotal() + value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Subtract")]
        public void SubtractValue(decimal value)
        {
            StoreTotal(RetrieveTotal() - value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Multiply")]
        public void MultiplyValue(decimal value)
        {
            StoreTotal(RetrieveTotal() * value);
        }

        [HttpPut()]
        [Route("/api/Calculator/Divide")]
        public void DivideValue(decimal value)
        {
            StoreTotal(RetrieveTotal() / value);
        }


    }
}
