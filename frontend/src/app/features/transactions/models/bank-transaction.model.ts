export interface BankTransaction {
  id: string;
  amount: number;
  type: string;
  transactionType: Date;
  description: string;
}

export interface CreateBankTransactionDto {
  TransactionTypeId: string;
  Amount: number;
  TransactionTime: Date;
  Description?: string;
  CategoryId: string;
}
