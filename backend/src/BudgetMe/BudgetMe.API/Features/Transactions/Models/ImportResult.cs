namespace BudgetMe.API.Features.Transactions.Models;

public class ImportResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = [];
    
    public static ImportResult<T> Success(T data) =>
        new() { IsSuccess = true, Data = data };
    
    public static ImportResult<T> Fail(List<string> errors) =>
        new() { IsSuccess = false, Errors = errors };
}