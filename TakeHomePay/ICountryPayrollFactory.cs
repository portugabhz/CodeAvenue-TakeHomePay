using System;
using System.Collections.Generic;

using static TakeHomePay.SharedStrings;

namespace TakeHomePay
{
    public interface ICountryPayrollFactory
    {
        ICountryPayroll GetCountryPayrollFactory(string countryName);
    }

    public class CountryPayrollFactory : ICountryPayrollFactory
    {
        private static readonly Dictionary<string, ICountryPayroll> mAllCountryPayrollObjects = new Dictionary<string, ICountryPayroll>();

        static CountryPayrollFactory()
        {
            mAllCountryPayrollObjects[Ireland.ToLower()] = new IrelandPayroll();
            mAllCountryPayrollObjects[Italy.ToLower()] = new ItalyPayroll();
            mAllCountryPayrollObjects[Germany.ToLower()] = new GermanyPayroll();
            mAllCountryPayrollObjects[CountryNotSupported] = new CountryNotSupportedPayroll();
        }

        public ICountryPayroll GetCountryPayrollFactory(string countryName)
        {
            string lowercaseCountryName = countryName.ToLower();

            if (mAllCountryPayrollObjects.ContainsKey(lowercaseCountryName))
                return mAllCountryPayrollObjects[lowercaseCountryName];
            else
                return mAllCountryPayrollObjects[CountryNotSupported];
        }
    }
}