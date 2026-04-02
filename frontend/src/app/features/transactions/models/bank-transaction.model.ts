export interface BankTransaction {
  Id: string;
  Amount: number;
  Type: string;
  TransactionTime: Date;
  Description: string;
}

export interface SaveTransactionDto {
  TransactionTypeId: string;
  Amount: number;
  TransactionTime: Date;
  Description?: string;
}
