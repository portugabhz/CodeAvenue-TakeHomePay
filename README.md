# CodeAvenue-TakeHomePay
Coding challenge for Code Avenue, written using Visual Studio 2015

Domain:
Payroll

Bounded Contex:
Employee Paycheck

Ubiquitous Language:
+ paycheck
+ payroll
+ pay period
+ gross income
	+ rate per hour
	+ number of hours worked
+ deductions:
	+ income tax
	+ universal social charge
	+ compulsory pension contribution
	+ pension
	+ social security contribution
+ take home pay
+ net amount

The calculation for gross income is the same for each country, but the deductions vary in both type and compute rules.

Deduction computations are simple, and are either straight %'s or 'first $x at y%, rest at z%'.

Design Patterns
+ **Factory**: simple, but pass back payroll compute object for user-specified country
+ **Null Object**: factory returns a Null*Template*Payroll object instead of a *null* for unsupported country
+ **Template**: defines how to compute take home pay (including gross income, deductions, and take home pay)
+ **Strategy**: define interface and create concrete implementations for specific country's payroll, plus 'country not supported' **Null Object**

**Factory class** for creating *Country*Payroll objects, will create one object for each country (plus 'country not supported' **Null Object**) when factory class is created, and store these objects in a Dictionary object: 
```C#
Dictionary<string, ICountryPayroll> mAllCountryPayrollObjects

ICountryPayrollFactory:
	ICountryPayroll GetCountryPayrollFactory(string countryName)
```
**Template**: abstract base class with three concrete methods that also implements the interface ICountryPayroll
```C#
ICountryPayroll:
	string Country
	List<string> ComputeTakeHomePay(decimal ratePerHour, decimal numberHours, out decimal takeHomePay)
	
IDeduction:
	string Name
	decimal ComputeDeduction(decimal grossIncome)
```

Each concrete 'template' implementation simply defines the deductions it supports, and sets its Country string value.

Design goals: Flat class hierarchies, Idempotent methods, Immutable data objects, Mimimize state, Minimize side effects
