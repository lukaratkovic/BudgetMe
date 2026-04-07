import {Category} from "../../categories/models/category.model";

export interface BankTransaction {
  id: string;
  amount: number;
  type: string;
  transactionTypeId: string;
  categories: Category[],
  transactionTime: Date;
  description: string;
}

export interface BankTransactionDto {
  id: string;
  amount: number;
  type: string;
  transactionTypeId: string;
  categories: Category[],
  transactionTime: string;
  description: string;
}

export interface CreateBankTransactionDto {
  transactionTypeId: string;
  amount: number;
  transactionTime: Date | string;
  description?: string;
  categoryIds: string[];
}

export interface UpdateBankTransactionDto {
  transactionTypeId: string;
  amount: number;
  transactionTime: Date | string;
  categoryIds: string[];
  description?: string;
}
