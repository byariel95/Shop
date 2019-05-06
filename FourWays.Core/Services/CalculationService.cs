using System;
using System.Collections.Generic;
using System.Text;

namespace FourWays.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public decimal TipAmount(decimal subTotal, double generosity)
        {
            return subTotal * (decimal)(generosity / 100);
        }
    }

}
