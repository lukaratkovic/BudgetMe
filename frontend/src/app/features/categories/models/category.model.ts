export interface Category  {
  id: string;
  name: string;
  description: string;
  transactionType: string;
  isSystem: boolean;
}

export interface CreateCategoryDto {
  name: string;
  description?: string;
  transactionTypeId: string;
}

export interface UpdateCategoryDto {
  name: string;
  description?: string;
  transactionTypeId: string;
}
