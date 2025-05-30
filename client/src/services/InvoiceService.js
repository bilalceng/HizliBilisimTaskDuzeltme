import API from "./api";

const InvoiceService = {
    getAuthHeaders: () => {
        const token = localStorage.getItem("token");
        return token ? { Authorization: `Bearer ${token}` } : {};
    },

    addInvoice: async (invoice) => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.post("/Invoice", invoice, { headers });
            return response.data;
        } catch (error) {
            console.error("Error adding invoice:", error);
            throw error;
        }
    },

    updateInvoice: async (invoice) => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.put("/Invoice", invoice, { headers });
            return response.data;
        } catch (error) {
            console.error("Error updating invoice:", error);
            throw error;
        }
    },

    getInvoicesByDateRange: async (startDate, endDate) => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.get("/Invoice", {
                params: { startDate, endDate },
                headers,
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching invoices by date range:", error);
            throw error;
        }
    },

    getInvoiceById: async (id) => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.get(`/Invoice/${id}`, { headers });
            return response.data;
        } catch (error) {
            console.error(`Error fetching invoice by ID ${id}:`, error);
            throw error;
        }
    },

    getAllInvoices: async () => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.get("/Invoice/all", { headers });
            return response.data;
        } catch (error) {
            console.error("Error fetching all invoices:", error);
            throw error;
        }
    },

    deleteInvoice: async (id) => {
        try {
            const headers = InvoiceService.getAuthHeaders();
            const response = await API.delete(`/Invoice/${id}`, { headers });
            return response.data;
        } catch (error) {
            console.error(`Error deleting invoice with ID ${id}:`, error);
            throw error;
        }
    },
};

export default InvoiceService;
