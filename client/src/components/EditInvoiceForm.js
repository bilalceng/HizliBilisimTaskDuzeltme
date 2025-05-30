import React, { useEffect, useState } from "react";
import CustomerService from "../services/CustomerService";
import InvoiceService from "../services/InvoiceService";

const EditInvoiceForm = ({ invoiceId, onClose, onInvoiceUpdated }) => {
    const [customers, setCustomers] = useState([]);
    const [formData, setFormData] = useState({
        customerId: "",
        invoiceNumber: "",
        invoiceDate: "",
        totalAmount: ""
    });
    const [errors, setErrors] = useState({});
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [customerData, invoiceData] = await Promise.all([
                    CustomerService.getAllCustomers(),
                    InvoiceService.getInvoiceById(invoiceId)
                ]);

                // Safely get customers array
                const loadedCustomers = customerData?.$values ?? customerData ?? [];
                setCustomers(loadedCustomers);

                setFormData({
                    customerId: invoiceData.customerId.toString(),
                    invoiceNumber: invoiceData.invoiceNumber,
                    invoiceDate: invoiceData.invoiceDate.slice(0, 10),
                    totalAmount: invoiceData.totalAmount.toString()
                });
            } catch (error) {
                console.error("Failed to load data", error);
            }
        };

        if (invoiceId) fetchData();
    }, [invoiceId]);

    const validate = () => {
        const newErrors = {};
        if (!formData.customerId) newErrors.customerId = "Customer is required";
        if (!formData.invoiceNumber) newErrors.invoiceNumber = "Invoice number is required";
        if (!formData.invoiceDate) newErrors.invoiceDate = "Invoice date is required";
        if (formData.totalAmount === "" || isNaN(formData.totalAmount)) {
            newErrors.totalAmount = "Total amount is required and must be a number";
        } else if (Number(formData.totalAmount) < 0) {
            newErrors.totalAmount = "Total amount must be positive";
        }
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validate()) return;

        const updatedInvoice = {
            invoiceId,
            ...formData,
            totalAmount: parseFloat(formData.totalAmount),
            invoiceDate: new Date(formData.invoiceDate),
            recordDate: new Date()
        };

        try {
            setSubmitting(true);
            await InvoiceService.updateInvoice(updatedInvoice);
            alert("Invoice updated successfully.");
            if (onInvoiceUpdated) await onInvoiceUpdated();
            if (onClose) onClose();
        } catch (err) {
            alert("Failed to update invoice.");
            console.error(err);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="card card-body shadow p-4">
            <h4 className="mb-3">Edit Invoice</h4>

            <div className="mb-3">
                <label htmlFor="customerId" className="form-label">Customer</label>
                <select
                    id="customerId"
                    name="customerId"
                    className="form-select"
                    value={formData.customerId}
                    onChange={handleChange}
                >
                    <option value="">-- Select Customer --</option>
                    {customers.map((c) => (
                        <option key={c.customerId} value={c.customerId}>
                            {c.title}
                        </option>
                    ))}
                </select>
                {errors.customerId && <div className="text-danger">{errors.customerId}</div>}
            </div>

            <div className="mb-3">
                <label htmlFor="invoiceNumber" className="form-label">Invoice Number</label>
                <input
                    type="text"
                    id="invoiceNumber"
                    name="invoiceNumber"
                    className="form-control"
                    value={formData.invoiceNumber}
                    onChange={handleChange}
                />
                {errors.invoiceNumber && <div className="text-danger">{errors.invoiceNumber}</div>}
            </div>

            <div className="mb-3">
                <label htmlFor="invoiceDate" className="form-label">Invoice Date</label>
                <input
                    type="date"
                    id="invoiceDate"
                    name="invoiceDate"
                    className="form-control"
                    value={formData.invoiceDate}
                    onChange={handleChange}
                />
                {errors.invoiceDate && <div className="text-danger">{errors.invoiceDate}</div>}
            </div>

            <div className="mb-3">
                <label htmlFor="totalAmount" className="form-label">Total Amount</label>
                <input
                    type="number"
                    id="totalAmount"
                    name="totalAmount"
                    className="form-control"
                    value={formData.totalAmount}
                    onChange={handleChange}
                    min="0"
                    step="0.01"
                />
                {errors.totalAmount && <div className="text-danger">{errors.totalAmount}</div>}
            </div>

            <button type="submit" className="btn btn-primary" disabled={submitting}>
                {submitting ? "Updating..." : "Update Invoice"}
            </button>
        </form>
    );
};

export default EditInvoiceForm;
