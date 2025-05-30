import API from "./api";;

export const login = async (credentials) => {
    try {
        const response = await API.post("/auth/login", credentials);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const register = async (data) => {
    try {
        const response = await API.post("/auth/register", data);
        return response.data;
    } catch (error) {
        throw error;
    }
};
