import API from "./api";

const CustomerService = {

    getAuthHeaders: () => {
        const token = localStorage.getItem("token");
        return token ? {Authorization: `Bearer ${token}`} : {};
    },

    getAllCustomers: async () => {
        try {
            const headers = CustomerService.getAuthHeaders();
            const response = await API.get("/Customer", {headers});
            return response.data;
        } catch (error) {
            console.error("Error fetching customers:", error);
            throw error;
        }
    },

}
export default CustomerService;
