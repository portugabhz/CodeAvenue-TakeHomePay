using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static TakeHomePay.SharedStrings;

namespace TakeHomePay.Test
{
    [TestClass]
    public class TestTakeHomePay
    {
        //Arrange
        private readonly ICountryPayrollFactory mCountryPayrollFactory = new CountryPayrollFactory();

        #region Test Payroll Factory
        [TestMethod]
        public void CountryPayrollFactoryShouldCorrectlyResolveIreland()
        {
            ICountryPayroll irelandPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Ireland);

            Assert.IsNotNull(irelandPayroll);
            Assert.IsTrue(irelandPayroll is IrelandPayroll);
        }

        [TestMethod]
        public void IrelandCountryPayrollFactoryShouldHaveCorrectName()
        {
            ICountryPayroll irelandPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Ireland);

            Assert.IsTrue(irelandPayroll.Country == Ireland);
        }

        [TestMethod]
        public void CountryPayrollFactoryShouldCorrectlyResolveItaly()
        {
            ICountryPayroll italyPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Italy);

            Assert.IsNotNull(italyPayroll);
            Assert.IsTrue(italyPayroll is ItalyPayroll);
        }

        [TestMethod]
        public void ItalyCountryPayrollFactoryShouldHaveCorrectName()
        {
            ICountryPayroll italyPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Italy);

            Assert.IsTrue(italyPayroll.Country == Italy);
        }

        [TestMethod]
        public void CountryPayrollFactoryShouldCorrectlyResolveGermany()
        {
            ICountryPayroll germanyPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Germany);

            Assert.IsNotNull(germanyPayroll);
            Assert.IsTrue(germanyPayroll is GermanyPayroll);
        }

        [TestMethod]
        public void GermanyCountryPayrollFactoryShouldHaveCorrectName()
        {
            ICountryPayroll germanyPayroll = mCountryPayrollFactory.GetCountryPayrollFactory(Germany);

            Assert.IsTrue(germanyPayroll.Country == Germany);
        }

        [TestMethod]
        public void CountryPayrollFactoryShouldCorrectlyResolveToUnsupported()
        {
            ICountryPayroll unsupportedPayroll = mCountryPayrollFactory.GetCountryPayrollFactory("India");

            Assert.IsNotNull(unsupportedPayroll);
            Assert.IsTrue(unsupportedPayroll is CountryNotSupportedPayroll);
        }

        [TestMethod]
        public void UnsupportedCountryPayrollFactoryShouldHaveCorrectName()
        {
            ICountryPayroll unsupportedPayroll = mCountryPayrollFactory.GetCountryPayrollFactory("India");

            Assert.IsTrue(unsupportedPayroll.Country == CountryNotSupported);
        }
        #endregion
        #region Test Gross Income
        [TestMethod]
        public void GrossIncomeShouldCalculateCorrectly()
        {
            //Gross income calculation is the same for all countries, including unsupported, so grab any one.
            CountryNotSupportedPayroll unsupportedPayroll = (CountryNotSupportedPayroll)mCountryPayrollFactory.GetCountryPayrollFactory("Irrelevant");

            decimal grossIncome = unsupportedPayroll.ComputeGrossIncome(10, 40);

            Assert.IsTrue(grossIncome == 400);
        }
        #endregion
        #region Test Ireland Deductions
        /// <summary>
        /// Ireland
        /// </summary>
        [TestMethod]
        public void IrelandIncomeTaxDeductionShouldBeCorrectlyNamed()
        {
            IrelandIncomeTaxDeduction irelandIncomeTaxDeduction = new IrelandIncomeTaxDeduction();

            Assert.IsTrue(irelandIncomeTaxDeduction.Name == IncomeTax);
        }

        [TestMethod]
        public void IrelandUniversalSocialChargeDeductionShouldBeCorrectlyNamed()
        {
            IrelandUniversalSocialChargeDeduction irelandUniversalSocialChargeDeduction = new IrelandUniversalSocialChargeDeduction();

            Assert.IsTrue(irelandUniversalSocialChargeDeduction.Name == UniversalSocialCharge);
        }

        [TestMethod]
        public void IrelandCompulsoryPensionContributionShouldBeCorrectlyNamed()
        {
            IrelandCompulsoryPensionContributionDeduction irelandCompulsoryPensionContributionDeduction  = new IrelandCompulsoryPensionContributionDeduction();

            Assert.IsTrue(irelandCompulsoryPensionContributionDeduction.Name == CompulsoryPensionContribution);
        }

        [TestMethod]
        public void IrelandIncomeTaxDeductionShouldBe25PctUpToAndIncluding600()
        {
            IrelandIncomeTaxDeduction irelandIncomeTaxDeduction = new IrelandIncomeTaxDeduction();

            decimal grossIncome = 600;

            decimal deduction = irelandIncomeTaxDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.25m);
        }

        [TestMethod]
        public void IrelandIncomeTaxDeductionShouldBe25PctUpToAndIncluding600And40pctThereafter()
        {
            IrelandIncomeTaxDeduction irelandIncomeTaxDeduction = new IrelandIncomeTaxDeduction();

            decimal grossIncome = 1000;

            decimal deduction = irelandIncomeTaxDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == ((600 * 0.25m) + (grossIncome - 600) * 0.40m));
        }

        [TestMethod]
        public void IrelandUniversalSocialChargeDeductionShouldBe7PctUpToAndIncluding500()
        {
            IrelandUniversalSocialChargeDeduction irelandUniversalSocialChargeDeduction = new IrelandUniversalSocialChargeDeduction();

            decimal grossIncome = 500;

            decimal deduction = irelandUniversalSocialChargeDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.07m);
        }

        [TestMethod]
        public void IrelandUniversalSocialChargeDeductionShouldBe7PctUpToAndIncluding500And8pctThereafter()
        {
            IrelandUniversalSocialChargeDeduction irelandUniversalSocialChargeDeduction = new IrelandUniversalSocialChargeDeduction();

            decimal grossIncome = 1000;

            decimal deduction = irelandUniversalSocialChargeDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == ((500 * 0.07m) + (grossIncome - 500) * 0.08m));
        }

        [TestMethod]
        public void IrelandCompulsoryPensionContributionDeductionShouldBe4Pct()
        {
            IrelandCompulsoryPensionContributionDeduction irelandCompulsoryPensionContributionDeduction  = new IrelandCompulsoryPensionContributionDeduction();

            decimal grossIncome = 1000;

            decimal deduction = irelandCompulsoryPensionContributionDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.04m);
        }
        #endregion
        #region Test Italy Deductions
        /// <summary>
        /// Italy
        /// </summary>
        [TestMethod]
        public void ItalyIncomeTaxDeductionShouldBeCorrectlyNamed()
        {
            ItalyIncomeTaxDeduction italyIncomeTaxDeduction = new ItalyIncomeTaxDeduction();

            Assert.IsTrue(italyIncomeTaxDeduction.Name == IncomeTax);
        }
        [TestMethod]
        public void ItalySocialSecurityContributionDeductionShouldBeCorrectlyNamed()
        {
            ItalySocialSecurityContributionDeduction italySocialSecurityContributionDeduction = new ItalySocialSecurityContributionDeduction();

            Assert.IsTrue(italySocialSecurityContributionDeduction.Name == SocialSecurityContribution);
        }

        [TestMethod]
        public void ItalyIncomeTaxDeductionShouldBe25Pct()
        {
            ItalyIncomeTaxDeduction italyIncomeTaxDeduction  = new ItalyIncomeTaxDeduction();

            decimal grossIncome = 1000;

            decimal deduction = italyIncomeTaxDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.25m);
        }

        [TestMethod]
        public void ItalySocialSecurityContributionShouldBe9pt19Pct()
        {
            ItalySocialSecurityContributionDeduction italySocialSecurityContributionDeduction = new ItalySocialSecurityContributionDeduction();

            decimal grossIncome = 1000;

            decimal deduction = italySocialSecurityContributionDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.0919m);
        }
        #endregion
        #region Test Germany Deductions
        /// <summary>
        /// Germany
        /// </summary>
        [TestMethod]
        public void GermanyIncomeTaxDeductionShouldBeCorrectlyNamed()
        {
            GermanyIncomeTaxDeduction germanyIncomeTaxDeduction = new GermanyIncomeTaxDeduction();

            Assert.IsTrue(germanyIncomeTaxDeduction.Name == IncomeTax);
        }

        [TestMethod]
        public void GermanyCompulsoryPensionContributionShouldBeCorrectlyNamed()
        {
            GermanyCompulsoryPensionContributionDeduction germanyCompulsoryPensionContributionDeduction = new GermanyCompulsoryPensionContributionDeduction();

            Assert.IsTrue(germanyCompulsoryPensionContributionDeduction.Name == CompulsoryPensionContribution);
        }

        public void GermanyIncomeTaxDeductionShouldBe25PctUpToAndIncluding400()
        {
            GermanyIncomeTaxDeduction germanyIncomeTaxDeduction = new GermanyIncomeTaxDeduction();

            decimal grossIncome = 400;

            decimal deduction = germanyIncomeTaxDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.25m);
        }

        [TestMethod]
        public void GermanyIncomeTaxDeductionShouldBe25PctUpToAndIncluding400And32pctThereafter()
        {
            GermanyIncomeTaxDeduction germanyIncomeTaxDeduction = new GermanyIncomeTaxDeduction();

            decimal grossIncome = 1000;

            decimal deduction = germanyIncomeTaxDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == ((400 * 0.25m) + (grossIncome - 400) * 0.32m));
        }

        [TestMethod]
        public void GermanyCompulsoryPensionContributionDeductionShouldBe2Pct()
        {
            GermanyCompulsoryPensionContributionDeduction germanyCompulsoryPensionContributionDeduction = new GermanyCompulsoryPensionContributionDeduction();

            decimal grossIncome = 1000;

            decimal deduction = germanyCompulsoryPensionContributionDeduction.ComputeDeduction(grossIncome);

            Assert.IsTrue(deduction == grossIncome * 0.02m);
        }
#endregion
        #region Take Home Pay Template
        [TestMethod()]
        public void ComputeGrossIncomeTest()
        {
            //The same method is used for each country, so testing one country is sufficient.
            IrelandPayroll irelandPayroll = (IrelandPayroll)mCountryPayrollFactory.GetCountryPayrollFactory(Ireland);

            decimal grossIncome = irelandPayroll.ComputeGrossIncome(10, 40);

            Assert.IsTrue(grossIncome == 400m);
        }

        [TestMethod()]
        public void ComputeDeductionsTest()
        {
            //The same method is used for each country, so testing one country is sufficient.
            IrelandPayroll irelandPayroll = (IrelandPayroll)mCountryPayrollFactory.GetCountryPayrollFactory(Ireland);

            List<string> log = new List<string>();

            decimal deductions = irelandPayroll.ComputeDeductions(log, 400m);

            Assert.IsTrue(deductions == 144m);
        }

        [TestMethod()]
        public void ComputeTakeHomePayTest()
        {
            //The same method is used for each country, so testing one country is sufficient.
            IrelandPayroll irelandPayroll = (IrelandPayroll)mCountryPayrollFactory.GetCountryPayrollFactory(Ireland);

            decimal takeHomePay;
            List<string> log = irelandPayroll.ComputeTakeHomePay(10m, 40m, out takeHomePay);

            Assert.IsTrue(takeHomePay == 256m);
        }
        #endregion
    }
}
