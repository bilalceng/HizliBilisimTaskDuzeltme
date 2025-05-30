import React, { useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

const UserProfile = () => {
    const [userName, setUserName] = useState("Guest");

    useEffect(() => {
        try {
            const token = localStorage.getItem("token");
            if (token) {
                const decoded = jwtDecode(token);

                // The username is stored under the claim URI for ClaimTypes.Name
                // This URI is usually:
                // "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
                const usernameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

                const nameFromToken = decoded[usernameClaim] || "Guest";
                setUserName(nameFromToken);
            }
        } catch (error) {
            console.error("Failed to decode token", error);
            setUserName("Guest");
        }
    }, []);

    return (
        <div className="d-flex flex-column align-items-center">
            <img
                src="https://randomuser.me/api/portraits/men/75.jpg"
                alt="Profile"
                className="rounded-circle mb-2"
                width={80}
                height={80}
            />
            <strong>{userName}</strong>
        </div>
    );
};

export default UserProfile;
