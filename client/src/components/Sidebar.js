import React, { useState } from "react";
import UserProfile from "./UserProfile";
import { FaFileInvoice, FaUsers } from "react-icons/fa";

const Sidebar = () => {
    const [activeTab, setActiveTab] = useState("invoices");

    const handleTabClick = (tab) => {
        setActiveTab(tab);
        // In future: add your logic here based on `tab`
        console.log(`Clicked on ${tab}`);
    };

    const linkClass = (tab) =>
        `btn text-start w-100 px-0 mb-2 ${
            activeTab === tab ? "btn-primary text-white" : "btn-link text-dark"
        }`;

    return (
        <div
            className="bg-light p-3 d-flex flex-column align-items-start"
            style={{ width: "220px", minHeight: "100vh" }}
        >
            <div className="w-100 text-center mb-4">
                <UserProfile />
            </div>

            <div className="w-100">
                <h6 className="text-uppercase text-muted mt-3">Sections</h6>
                <ul className="list-unstyled">
                    <li>
                        <button
                            className={linkClass("invoices")}
                            onClick={() => handleTabClick("invoices")}
                        >
                            <FaFileInvoice className="me-2" />
                            Invoices
                        </button>
                    </li>
                    <li>
                        <button
                            className={linkClass("customers")}
                            onClick={() => handleTabClick("customers")}
                        >
                            <FaUsers className="me-2" />
                            Customers
                        </button>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default Sidebar;
