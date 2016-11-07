using System;
using System.Collections.Generic;

using static TakeHomePay.SharedStrings;

namespace TakeHomePay
{
    public abstract class TakeHomePayTemplate : ICountryPayroll
    {
        public virtual string Country => CountryNotSupported;

        internal readonly List<IDeduction> mAllDeductions = new List<IDeduction>();

        public decimal ComputeGrossIncome(decimal ratePerHour, decimal numberHours)
        {
            return ratePerHour * numberHours;
        }

        public decimal ComputeDeductions(List<string> log, decimal grossIncome)
        {
            decimal totalDeductions = 0;

            mAllDeductions.ForEach(d =>
            {
                decimal oneDeduction = d.ComputeDeduction(grossIncome);

                log.Add(d.Name + ": " + $"{oneDeduction:C}");

                totalDeductions += oneDeduction;
            });

            return totalDeductions;
        }

        public List<string> ComputeTakeHomePay(decimal ratePerHour, decimal numberHours, out decimal takeHomePay)
        {
            List<string> log = new List<string>();

            takeHomePay = 0;

            try
            {
                log.Add("Employee location: " + Country);

                decimal grossIncome = ComputeGrossIncome(ratePerHour, numberHours);

                log.Add("Gross Amount: " + $"{grossIncome:C}");

                decimal totalDeductions = ComputeDeductions(log, grossIncome);

                takeHomePay = grossIncome - totalDeductions;

                log.Add("Net Amount: " + $"{takeHomePay:C}");
            }
            catch (Exception ex)
            {
                log.Add("Error: " + ex);
            }

            return log;
        }
    }

    public class IrelandPayroll : TakeHomePayTemplate
    {
        public override string Country => Ireland;

        public IrelandPayroll()
        {
            mAllDeductions.Add(new IrelandIncomeTaxDeduction());
            mAllDeductions.Add(new IrelandUniversalSocialChargeDeduction());
            mAllDeductions.Add(new IrelandCompulsoryPensionContributionDeduction());
        }
    }

    public class ItalyPayroll : TakeHomePayTemplate
    {
        public override string Country => Italy;

        public ItalyPayroll()
        {
            mAllDeductions.Add(new ItalyIncomeTaxDeduction());
            mAllDeductions.Add(new ItalySocialSecurityContributionDeduction());
        }
    }

    public class GermanyPayroll : TakeHomePayTemplate
    {
        public override string Country => Germany;

        public GermanyPayroll()
        {
            mAllDeductions.Add(new GermanyIncomeTaxDeduction());
            mAllDeductions.Add(new GermanyCompulsoryPensionContributionDeduction());
        }
    }

    public class CountryNotSupportedPayroll : TakeHomePayTemplate
    {
        public override string Country => CountryNotSupported;

        public CountryNotSupportedPayroll()
        {
            //No deductions since not supported.
        }
    }
}
