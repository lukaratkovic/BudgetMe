import {TransactionTypes} from "../constants/transaction-type.constant";
import {BankTransaction, BankTransactionDto} from "../../features/transactions/models/bank-transaction.model";

export interface TransactionIncomeExpenseBalance {
  income: number;
  expense: number;
  balance: number;
}

export class TransactionTypeHelper {
  public static isIncome(typeId: string): boolean {
    return typeId.trim().toUpperCase() === TransactionTypes.IncomeId;
  }

  public static isExpense(typeId: string): boolean {
    return typeId.trim().toUpperCase() === TransactionTypes.ExpenseId;
  }

  public static getIncomeExpenseBalance(transaction: BankTransaction | BankTransactionDto): TransactionIncomeExpenseBalance {
    const income = this.isIncome(transaction.transactionTypeId) ? transaction.amount : 0;
    const expense = this.isExpense(transaction.transactionTypeId) ? transaction.amount : 0;
    const balance = income - expense;
    return {
      income: income,
      expense: expense,
      balance: balance
    };
  }
}
