

export class ShopParams {
    brandId: number | null = null;
    typeId: number | null = null;
    orderBy: string = '';
    pageIndex: number = 1;  // Default value
    pageSize: number = 6;  // Default value\
    searchTerm?: string='';
}
