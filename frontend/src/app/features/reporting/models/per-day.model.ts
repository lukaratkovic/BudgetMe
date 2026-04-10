import {BankTransaction, BankTransactionDto} from "../../transactions/models/bank-transaction.model";

export interface PerDayReportDto {
  date: string;
  income: number;
  expense: number;
  balance: number;
  transactions: BankTransactionDto[]
}

export interface PerDayReportData {
  date: Date;
  income: number;
  expense: number;
  balance: number;
  transactions: BankTransaction[]
}
