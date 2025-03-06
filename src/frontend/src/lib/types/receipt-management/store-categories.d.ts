interface StoreCategory {
    id: number;
    name: string;
}

interface StoreCategoryCreateParams {
    name: string;
}

interface StoreCategoryUpdateParams extends StoreCategoryCreateParams {
    id: number;
}