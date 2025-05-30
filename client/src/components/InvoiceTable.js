import React from "react";

const InvoiceTable = ({ invoices, onEdit, onDelete }) => {
    const formatDate = (dateString) => {
        const date = new Date(dateString);
        return date.toLocaleDateString("en-GB"); // dd/mm/yyyy
    };

    return (
        <div className="table-responsive">
            <table className="table table-bordered table-hover align-middle text-center">
                <thead className="table-light">
                <tr>
                    <th>ID</th>
                    <th>Customer</th>
                    <th>Invoice Number</th>
                    <th>Invoice Date</th>
                    <th>Total Amount</th>
                    <th>Record Date</th>
                    <th>Actions</th>
                </tr>
                </thead>
                <tbody>
                {invoices.map((invoice, index) => (
                    <tr key={index}>
                        <td>{invoice.invoiceId}</td>
                        <td>{invoice.customerTitle}</td>  {/* Updated here */}
                        <td>{invoice.invoiceNumber}</td>
                        <td>{formatDate(invoice.invoiceDate)}</td>
                        <td>{invoice.totalAmount}</td>
                        <td>{formatDate(invoice.recordDate)}</td>
                        <td>
                            <button
                                className="btn btn-warning btn-sm me-2"
                                onClick={() => onEdit(invoice.invoiceId)}
                            >
                                Edit
                            </button>

                            <button
                                className="btn btn-danger btn-sm"
                                onClick={() => onDelete(invoice.invoiceId)}
                            >
                                Delete
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
            <p className="text-end fw-bold">Total Invoices: {invoices.length}</p>
        </div>
    );
};

export default InvoiceTable;
