import { persistentStore } from "../persistent-store";
import api from "$lib/api";

export const storeCategoryStore = persistentStore<StoreCategory[]>("receipt-management.store-categories", []);

export async function fetchStoreCategories() {
    try {
        const storeCategories = await api.get("/receipt-management/store-categories");
        storeCategoryStore.set(storeCategories);
    } catch (err) {
        throw new Error("Failed to fetch store categories", err);
    }
}

export async function createStoreCategory(storeCategory: StoreCategoryCreateParams) {
    try {
        const created = await api.post("/receipt-management/store-categories", storeCategory) as StoreCategory;
        storeCategoryStore.update((storeCategories) => [...storeCategories, created]);
    } catch (err) {
        throw new Error("Failed to create store category", err);
    }
}

export async function deleteStoreCategory(id: number) {
    try {
        const deleted = await api.del(`/receipt-management/store-categories/${id}`) as StoreCategory;
        storeCategoryStore.update((storeCategories) =>
            storeCategories.filter(existing => existing.id !== deleted.id)
        );
    } catch (err) {
        throw new Error("Failed to delete store category", err);
    }
}

export async function updateStoreCategory(storeCategory: StoreCategoryUpdateParams) {
    try {
        const updated = await api.put(`/receipt-management/store-categories/${storeCategory.id}`, storeCategory) as StoreCategory;
        storeCategoryStore.update((storeCategories) => {
            return storeCategories.map((existing) => {
                if (existing.id == storeCategory.id) {
                    return updated;
                } else {
                    return existing;
                }
            });
        });
    } catch (err) {
        throw new Error("Failed to update store category", err);
    }
}