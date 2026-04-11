namespace BudgetMe.API.Features.Reports.Models.GroupedTransactions;

public record MonthData(int Month, int Year, List<DayData> DayData)
{
    public decimal Income => DayData.Sum(x => x.Income);
    public decimal Expense => DayData.Sum(x => x.Expense);
    public decimal Balance => DayData.Sum(x => x.Balance);
}