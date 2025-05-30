import React from "react";

const InvoiceFilter = ({ onSearch, onReset }) => {
    return (
        <div className="row mb-3 align-items-end">
            <div className="col-md-2">
                <input type="date" className="form-control" placeholder="Start Date" />
            </div>
            <div className="col-md-2">
                <input type="date" className="form-control" placeholder="End Date" />
            </div>
            <div className="col-md-3">
                <input type="text" className="form-control" placeholder="Search Invoice or Customer" />
            </div>
            <div className="col-md-2 d-flex gap-2">
                <button className="btn btn-dark w-100" onClick={onSearch}>Search</button>
                <button className="btn btn-primary w-100" onClick={onReset}>Reset</button>
            </div>
        </div>
    );
};

export default InvoiceFilter;
