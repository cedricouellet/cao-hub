import { writable, type Writable } from "svelte/store";

export const persistentStore = <T>(
    key: string,
    initialValue: T): Writable<T> => {
    const storedValue = localStorage.getItem(key);
    const parsedValue = storedValue ? JSON.parse(storedValue) : initialValue;

    const store = writable<T>(parsedValue);

    store.subscribe((value) => {
        localStorage.setItem(key, JSON.stringify(value));
    });

    return store;
};