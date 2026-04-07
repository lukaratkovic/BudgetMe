export interface Binding {
  id: string;
  keyword: string;
  categoryId: string;
  category: string;
  transactionTypeId?: string;
  transactionType?: string;
}

export interface BindingDto {
  id: string;
  keyword: string;
  categoryId: string;
  category: string;
  transactionTypeId: string;
  transactionType: string;
}
