import React, { createContext, useState, useEffect } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [checkingAuth, setCheckingAuth] = useState(true); // NEW: loading state

    useEffect(() => {
        // On mount, check token validity and update state
        const token = localStorage.getItem("token");
        setIsAuthenticated(token && token !== "");
        setCheckingAuth(false); // auth check done
    }, []);

    const login = (token) => {
        localStorage.setItem("token", token);
        setIsAuthenticated(true);
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
