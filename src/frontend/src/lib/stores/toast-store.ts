import { writable, type Writable } from "svelte/store";
import { v4 as uuidv4 } from 'uuid';
export const toastStore: Writable<ToastData[]> = writable([]);

export function addToast(toast: ToastData, durationSeconds: number = 5) {
    toast.uuid ??= uuidv4();

    toastStore.update((toasts) => [...toasts, toast]);
    setTimeout(() => {
        toastStore.update((toasts) =>
            toasts.filter((existing) => existing.uuid !== toast.uuid))
    }, durationSeconds * 1000);
}