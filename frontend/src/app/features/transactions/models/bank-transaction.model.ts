export interface BankTransaction {
  id: string;
  amount: number;
  type: string;
  transactionTypeId: string;
  category: string;
  categoryId: string;
  transactionTime: Date;
  description: string;
}

export interface BankTransactionDto {
  id: string;
  amount: number;
  type: string;
  transactionTypeId: string;
  category: string;
  categoryId: string;
  transactionTime: string;
  description: string;
}

export interface CreateBankTransactionDto {
  transactionTypeId: string;
  amount: number;
  transactionTime: Date | string;
  description?: string;
  categoryId: string;
}

export interface UpdateBankTransactionDto {
  id: string;
  transactionTypeId: string;
  amount: number;
  transactionTime: Date | string;
  categoryId: string;
  description?: string;
}
