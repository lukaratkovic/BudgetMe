namespace BudgetMe.API.Exceptions;

public class InvalidIncomeExpenseSideException : Exception
{
    public InvalidIncomeExpenseSideException()
    {}
    
    public InvalidIncomeExpenseSideException(string message) : base(message)
    {}

    public InvalidIncomeExpenseSideException(string message, Exception inner) : base(message, inner)
    {}
}