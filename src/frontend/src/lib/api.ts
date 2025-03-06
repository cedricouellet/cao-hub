const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

async function request(endpoint: string, params: APIRequestParams) {
    if (endpoint.startsWith("/")) {
        endpoint = endpoint.substring(1, endpoint.length);
    }

    const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
        method: params.method,
        headers: {
            "Content-Type": "application/json",
            ...params.headers,
        },
        body: params.body ? JSON.stringify(params.body) : undefined,
    });

    if (!response.ok) {
        throw new Error(`API Error: ${response.status} ${response.statusText}`);
    }

    return response.json();
}

export const get = async (endpoint: string) => request(endpoint, {
    method: "GET"
});

export const post = async (endpoint: string, body: object = {}, headers: object = {}) => request(endpoint, {
    method: "POST",
    body,
    headers
});

export const put = async (endpoint: string, body: object = {}, headers: object = {}) => request(endpoint, {
    method: "PUT",
    body,
    headers
});

export const del = async (endpoint: string) => request(endpoint, {
    method: "DELETE"
});

export default {
    get,
    post,
    put,
    del,
};