# CodeAvenue-TakeHomePay
Coding challenge for Code Avenue.

Domain:
Payroll

Bounded Contex:
Employee Paycheck

Ubiquitous Language:
+ paycheck
+ pay period
+ gross income
	+ rate per hour
	+ number of hours worked
+ deductions:
	+ income tax
	+ universal social charge
	+ compulsory pension contribution
	+ social security contribution
+ take home pay

The calculation for gross income is the same for each country, but the deductions vary in both type and compute rules.

Deduction computations are simple, and are either straight %'s or 'first $x at y%, rest at z%'.

Design Patterns
+ **Strategy**: define interface and create concrete implementations for specific countries
+ **Template**: define how to compute take home pay (including gross income, deductions, and take home pay)
+ **Factory**: simple, but pass back concrete implementation of payroll interface for user-specified country
+ **Null Object**: factory returns a Null*Template*Object instead of a null for unsupported country

**Factory class** for creating *Country*Payroll objects, will create one object for each country, plus **Null Object**, when class is created, and store in a Dictionary object: 
```C#
Dictionary<string, IPayroll> mAllCountryPayrollObjects

ICountryPayrollFactory:
	IPayroll GetCountryPayrollFactory(string countryName)
```
**Template**: abstract base class with one concrete method that implements the interface IPayroll
```C#
IPayroll:
	string Country
	decimal ComputeGrossIncome(decimal ratePerHour, decimal numberHours)
	decimal ComputeDeductions(decimal grossIncome, List<IDeduction>)
	List<string> ComputeTakeHomePay(decimal ratePerHour, decimal numberHours, out decimal takeHomePay)
	
IDeduction:
	string Name
	decimal ComputeDeduction(decimal grossIncome)
```

Build from Unit Tests

Arrange
+ 
Act
+ test methods
Assert
+ make sure that proper exceptions are thrown and proper values are returned

Flat hierarchies
Idempotent functions
Immutable objects
