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

export interface CreateBankTransactionDto {
  transactionTypeId: string;
  amount: number;
  transactionTime: Date;
  description?: string;
  categoryId: string;
}

export interface UpdateBankTransactionDto {
  id: string;
  transactionTypeId: string;
  amount: number;
  transactionTime: Date;
  categoryId: string;
  description?: string;
}
