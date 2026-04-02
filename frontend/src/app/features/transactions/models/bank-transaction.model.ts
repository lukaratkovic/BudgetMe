export interface BankTransaction {
  Id: string;
  Amount: number;
  Type: string;
}

export interface SaveTransactionDto {
  TransactionTypeId: string;
  Amount: number;
}
