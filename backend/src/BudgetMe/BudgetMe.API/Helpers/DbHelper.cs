using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Helpers;

public class DbHelper
{
    public static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        if (ex.InnerException is SqlException sqlEx)
        {
            // 2601 = duplicate key
            // 2627 = unique constraint violation
            return sqlEx.Number is 2601 or 2627;
        }

        return false;
    }
}