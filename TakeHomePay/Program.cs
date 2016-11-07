using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace TakeHomePay
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;

            do
            {
                string hoursWorked;
                decimal numberHours;
                do
                {
                    WriteLine("Please enter the # of hours worked:");
                    hoursWorked = ReadLine();
                } while (!decimal.TryParse(hoursWorked, out numberHours));

                string hourlyRate;
                decimal ratePerHour;
                do
                {
                    WriteLine("Please enter the hourly rate:");
                    hourlyRate = ReadLine();
                } while (!decimal.TryParse(hourlyRate, out ratePerHour));

                WriteLine("Please enter the employee’s location:");
                string employeeLocation = ReadLine();

                ICountryPayrollFactory mCountryPayrollFactory = new CountryPayrollFactory();

                ICountryPayroll countryPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(employeeLocation);

                decimal takeHomePay;
                List<string> log = countryPayroll.ComputeTakeHomePay(ratePerHour, numberHours, out takeHomePay);

                log.ForEach(logEntry => WriteLine(logEntry));

                WriteLine("Press 'x' to exit, press any other key to compute for another employee.");
                key = ReadKey(); 
            } while (key.KeyChar != 'x');
        }
    }
}
