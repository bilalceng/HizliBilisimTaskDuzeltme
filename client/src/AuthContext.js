// src/AuthContext.js
import React, { createContext, useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [checkingAuth, setCheckingAuth] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token) {
            try {
                const decoded = jwtDecode(token);
                const isExpired = decoded.exp * 1000 < Date.now(); // exp is in seconds

                if (!isExpired) {
                    setIsAuthenticated(true);
                } else {
                    console.warn("Token expired.");
                    localStorage.removeItem("token");
                    setIsAuthenticated(false);
                }
            } catch (error) {
                console.error("Invalid token:", error);
                localStorage.removeItem("token");
                setIsAuthenticated(false);
            }
        } else {
            setIsAuthenticated(false);
        }

        setCheckingAuth(false);
    }, []);

    const login = (token) => {
        try {
            const decoded = jwtDecode(token);
            const isExpired = decoded.exp * 1000 < Date.now();

            if (isExpired) {
                console.warn("Tried to login with expired token.");
                return;
            }

            localStorage.setItem("token", token);
            setIsAuthenticated(true);
        } catch (error) {
            console.error("Invalid token on login:", error);
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, logout, checkingAuth }}>
            {children}
        </AuthContext.Provider>
    );
};
