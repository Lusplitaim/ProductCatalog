export interface CreateEditProduct {
    name: string;
    description: string;
    price: number;
    note?: string;
    specialNote?: string;
    categoryId: number;
}