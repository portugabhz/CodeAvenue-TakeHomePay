using System;
using System.Collections.Generic;

namespace TakeHomePay
{
    public interface ICountryPayroll
    {
        string Country { get; }
        List<string> ComputeTakeHomePay(decimal ratePerHour, decimal numberHours, out decimal takeHomePay);
    }
}