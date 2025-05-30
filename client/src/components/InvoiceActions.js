import React from "react";

const InvoiceActions = ({ onAdd, onLogout }) => {
    return (
        <div className="d-flex justify-content-between mb-3">
            <button className="btn btn-primary" onClick={onAdd}>Add New Invoice</button>
            <button className="btn btn-danger" onClick={onLogout}>Logout</button>
        </div>
    );
};

export default InvoiceActions;
