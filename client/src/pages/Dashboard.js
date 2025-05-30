import React, { useState, useEffect } from "react";
import Sidebar from "../components/Sidebar";
import InvoiceActions from "../components/InvoiceActions";
import InvoiceTable from "../components/InvoiceTable";
import InvoiceService from "../services/InvoiceService";
import CustomerService from "../services/CustomerService";
import AddInvoiceForm from "../components/AddInvoiceForm";
import EditInvoiceForm from "../components/EditInvoiceForm";

const Dashboard = () => {
    const [invoices, setInvoices] = useState([]);
    const [customers, setCustomers] = useState([]);
    const [loading, setLoading] = useState(true);

    // Date range for filtering
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");

    // Pagination states
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 5;

    // Modal states
    const [showAddModal, setShowAddModal] = useState(false);
    const [editInvoiceId, setEditInvoiceId] = useState(null);

    useEffect(() => {
        fetchData();
    }, []);

    useEffect(() => {
        // Lock background scroll when edit modal is open
        document.body.style.overflow = editInvoiceId || showAddModal ? "hidden" : "auto";
    }, [editInvoiceId, showAddModal]);

    useEffect(() => {
        // Reset page if current page is out of bounds
        if (currentPage > Math.ceil(invoices.length / itemsPerPage)) {
            setCurrentPage(1);
        }
    }, [invoices, currentPage]);

    const fetchData = async () => {
        setLoading(true);
        try {
            // Fetch customers
            const customerData = await CustomerService.getAllCustomers();
            const customerArray = customerData?.$values ?? customerData ?? [];
            setCustomers(customerArray);

            // Fetch invoices
            let invoiceData;
            if (startDate && endDate) {
                const formattedStart = new Date(startDate).toISOString().split("T")[0];
                const formattedEnd = new Date(endDate).toISOString().split("T")[0];
                invoiceData = await InvoiceService.getInvoicesByDateRange(formattedStart, formattedEnd);
            } else {
                invoiceData = await InvoiceService.getAllInvoices();
            }
            const invoicesArray = invoiceData?.$values ?? invoiceData ?? [];

            // Merge customer title into invoices
            const invoicesWithCustomerTitle = invoicesArray.map((invoice) => {
                const customer = customerArray.find((c) => c.customerId === invoice.customerId);
                return {
                    ...invoice,
                    customerTitle: customer ? customer.title : "Unknown Customer",
                };
            });

            setInvoices(invoicesWithCustomerTitle);
        } catch (error) {
            console.error("Failed to fetch data", error);
            alert("Failed to fetch data. Please try again later.");
        } finally {
            setLoading(false);
        }
    };

    // Filter handlers
    const handleFilterByDate = async () => {
        if (!startDate || !endDate) {
            alert("Please select both start and end dates");
            return;
        }
        if (new Date(startDate) > new Date(endDate)) {
            alert("Start date cannot be after end date");
            return;
        }
        setCurrentPage(1);
        await fetchData();
    };

    const handleResetFilter = async () => {
        setStartDate("");
        setEndDate("");
        setCurrentPage(1);
        await fetchData();
    };

    // Delete invoice handler
    const handleDeleteInvoice = async (id) => {
        if (!window.confirm("Are you sure you want to delete this invoice?")) return;

        try {
            await InvoiceService.deleteInvoice(id);
            setInvoices((prev) => prev.filter((inv) => inv.invoiceId !== id));

            const newCount = invoices.length - 1;
            if (newCount <= itemsPerPage * (currentPage - 1)) {
                setCurrentPage((prev) => Math.max(prev - 1, 1));
            }
        } catch (error) {
            alert("Failed to delete invoice. Please try again.");
            console.error(`Failed to delete invoice with ID ${id}:`, error);
        }
    };

    // Pagination calculations
    const indexOfLastInvoice = currentPage * itemsPerPage;
    const indexOfFirstInvoice = indexOfLastInvoice - itemsPerPage;
    const currentInvoices = invoices.slice(indexOfFirstInvoice, indexOfLastInvoice);
    const totalPages = Math.ceil(invoices.length / itemsPerPage);

    const goToPreviousPage = () => {
        if (currentPage > 1) setCurrentPage(currentPage - 1);
    };

    const goToNextPage = () => {
        if (currentPage < totalPages) setCurrentPage(currentPage + 1);
    };

    // Handlers for modals
    const handleInvoiceAdded = async () => {
        setShowAddModal(false);
        await fetchData();
    };

    const handleInvoiceUpdated = async () => {
        setEditInvoiceId(null);
        await fetchData();
    };

    return (
        <div className="d-flex" style={{ minHeight: "100vh", flexDirection: "row" }}>
            <Sidebar />
            <div
                className="container mt-4 d-flex flex-column"
                style={{ position: "relative", flexGrow: 1, paddingBottom: "60px" }}
            >
                <h2 className="mb-4">Invoice Dashboard</h2>

                <InvoiceActions
                    onAdd={() => setShowAddModal(true)}
                    onLogout={() => {
                        localStorage.removeItem("token");
                        window.location.href = "/login";
                    }}
                />

                {/* Date range inputs and filter buttons */}
                <div className="d-flex gap-2 align-items-center mb-3">
                    <input
                        type="date"
                        value={startDate}
                        onChange={(e) => setStartDate(e.target.value)}
                        className="form-control"
                        placeholder="Start Date"
                    />
                    <input
                        type="date"
                        value={endDate}
                        onChange={(e) => setEndDate(e.target.value)}
                        className="form-control"
                        placeholder="End Date"
                    />
                    <button className="btn btn-primary" onClick={handleFilterByDate}>
                        Filter
                    </button>
                    <button className="btn btn-secondary" onClick={handleResetFilter}>
                        Reset
                    </button>
                </div>

                {loading ? (
                    <p>Loading invoices...</p>
                ) : (
                    <>
                        <InvoiceTable
                            invoices={currentInvoices}
                            onEdit={(id) => setEditInvoiceId(id)}
                            onDelete={handleDeleteInvoice}
                        />

                        {/* Pagination */}
                        {invoices.length > itemsPerPage && (
                            <div
                                style={{
                                    position: "fixed",
                                    bottom: 20,
                                    left: 0,
                                    right: 0,
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                    gap: "12px",
                                    backgroundColor: "white",
                                    padding: "8px 16px",
                                    boxShadow: "0 -2px 8px rgba(0,0,0,0.1)",
                                    zIndex: 1000,
                                    maxWidth: "100%",
                                }}
                            >
                                <button
                                    className="btn btn-outline-primary"
                                    onClick={goToPreviousPage}
                                    disabled={currentPage === 1}
                                >
                                    &lt;
                                </button>
                                <span
                                    style={{
                                        minWidth: "120px",
                                        textAlign: "center",
                                        fontWeight: "500",
                                    }}
                                >
                  Page {currentPage} of {totalPages}
                </span>
                                <button
                                    className="btn btn-outline-primary"
                                    onClick={goToNextPage}
                                    disabled={currentPage === totalPages}
                                >
                                    &gt;
                                </button>
                            </div>
                        )}
                    </>
                )}

                {/* Add Invoice Modal */}
                {showAddModal && (
                    <div
                        className="modal d-block"
                        tabIndex="-1"
                        style={{
                            backgroundColor: "rgba(0,0,0,0.5)",
                            position: "fixed",
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            zIndex: 1050,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                        }}
                    >
                        <div className="modal-dialog modal-lg modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Add New Invoice</h5>
                                    <button
                                        type="button"
                                        className="btn-close"
                                        onClick={() => setShowAddModal(false)}
                                        aria-label="Close"
                                    ></button>
                                </div>
                                <div className="modal-body">
                                    <AddInvoiceForm
                                        customers={customers}
                                        onInvoiceAdded={handleInvoiceAdded}
                                        onClose={() => setShowAddModal(false)}
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                )}

                {/* Edit Invoice Modal */}
                {editInvoiceId && (
                    <div
                        className="modal d-block"
                        tabIndex="-1"
                        style={{
                            backgroundColor: "rgba(0, 0, 0, 0.5)",
                            position: "fixed",
                            top: 0,
                            left: 0,
                            right: 0,
                            bottom: 0,
                            zIndex: 1050,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                        }}
                    >
                        <div className="modal-dialog modal-lg modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Edit Invoice</h5>
                                    <button
                                        type="button"
                                        className="btn-close"
                                        onClick={() => setEditInvoiceId(null)}
                                        aria-label="Close"
                                    ></button>
                                </div>
                                <div className="modal-body">
                                    <EditInvoiceForm
                                        invoiceId={editInvoiceId}
                                        customers={customers}
                                        onInvoiceUpdated={handleInvoiceUpdated}
                                        onClose={() => setEditInvoiceId(null)}
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Dashboard;
