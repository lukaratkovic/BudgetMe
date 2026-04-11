import {BankTransactionDto} from "../../transactions/models/bank-transaction.model";
import {TreeNode} from "primeng/api";

export interface GroupedTransactionsTreeNode {
  data: {
    label: string;
    income: number;
    expense: number;
    balance: number;
  },
  children?: TreeNode[]
}

export interface DayData {
  date: Date;
  income: number;
  expense: number;
  balance: number;
  transactions: BankTransactionDto[]
}

export interface MonthData {
  month: number;
  year: number;
  income: number;
  expense: number;
  balance: number;
  dayData: DayData[];
}

export interface YearData {
  year: number;
  income: number;
  expense: number;
  balance: number;
  monthData: MonthData[];
}
