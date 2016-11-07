using System;

using static TakeHomePay.SharedStrings;

namespace TakeHomePay
{
    public interface IDeduction
    {
        string Name { get; }
        decimal ComputeDeduction(decimal grossIncome);
    }
#region Ireland
    public class IrelandIncomeTaxDeduction : IDeduction
    {
        public string Name => IncomeTax;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            decimal cutOff = 600m;

            if (grossIncome <= cutOff) return grossIncome*0.25m;
            else
            {
                return cutOff * 0.25m + (grossIncome - cutOff) * 0.40m;
            }
        }
    }

    public class IrelandUniversalSocialChargeDeduction : IDeduction
    {
        public string Name => UniversalSocialCharge;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            decimal cutOff = 500;
            if (grossIncome <= cutOff) return grossIncome * 0.07m;
            else
            {
                return cutOff * 0.07m + (grossIncome - cutOff) * 0.08m;
            }
        }
    }

    public class IrelandCompulsoryPensionContributionDeduction : IDeduction
    {
        public string Name => CompulsoryPensionContribution;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            return grossIncome * 0.04m;
        }
    }
#endregion
#region Italy
    public class ItalyIncomeTaxDeduction : IDeduction
    {
        public string Name => IncomeTax;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            return grossIncome * 0.25m;
        }
    }

    public class ItalySocialSecurityContributionDeduction : IDeduction
    {
        public string Name => SocialSecurityContribution;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            return grossIncome * 0.0919m;
        }
    }
#endregion
#region Germany
    public class GermanyIncomeTaxDeduction : IDeduction
    {
        public string Name => IncomeTax;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            decimal cutOff = 400;
            if (grossIncome <= cutOff) return grossIncome * 0.25m;
            else
            {
                return cutOff * 0.25m + (grossIncome - cutOff) * 0.32m;
            }
        }
    }

    public class GermanyCompulsoryPensionContributionDeduction : IDeduction
    {
        public string Name => CompulsoryPensionContribution;

        public decimal ComputeDeduction(decimal grossIncome)
        {
            return grossIncome * 0.02m;
        }
    }
#endregion
}
