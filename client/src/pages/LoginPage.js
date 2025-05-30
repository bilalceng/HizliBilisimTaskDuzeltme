import React, { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { login as loginService } from "../services/authService";
import { AuthContext } from "../AuthContext";


const LoginPage = () => {
    const { login } = useContext(AuthContext);
    const [form, setForm] = useState({ userName: "", password: "" });
    const [errors, setErrors] = useState({});
    const [serverError, setServerError] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const validate = () => {
        const errs = {};
        if (!form.userName) errs.userName = "UserName is required";
        else if (form.userName.length > 50)
            errs.userName = "UserName cannot be longer than 50 characters";

        if (!form.password) errs.password = "Password is required";
        else if (form.password.length < 6 || form.password.length > 100)
            errs.password = "Password must be between 6 and 100 characters";

        return errs;
    };

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
        setErrors({ ...errors, [e.target.name]: "" });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setServerError("");
        const errs = validate();
        if (Object.keys(errs).length > 0) {
            setErrors(errs);
            return;
        }

        try {
            setLoading(true);
            console.log("Sending login request with form:", form);

            const data = await loginService(form);
            console.log("Login response received:", data);

            login(data.token); // call login from context
            console.log("Navigating to dashboard...");
            navigate("/dashboard", { replace: true });
            console.log("Navigation function called");
        } catch (err) {
            console.error("Login error:", err);
            if (err.response && err.response.status === 401) {
                setServerError("Invalid username or password.");
            } else if (err.response && err.response.data) {
                setServerError(err.response.data.message || "Unexpected error occurred.");
            } else {
                setServerError("Failed to connect to server. Please try again.");
            }
        } finally {
            console.log("Login process finished. Loading state set to false.");
            setLoading(false);
        }
    };

    return (
        <div className="d-flex vh-100 justify-content-center align-items-center bg-light">
            <form onSubmit={handleSubmit} className="p-4 rounded shadow" style={{ width: "360px", backgroundColor: "#e9f7ef" }}>
                <h2 className="mb-4 text-success text-center">Login</h2>

                {serverError && (
                    <div className="alert alert-danger" role="alert">
                        {serverError}
                    </div>
                )}

                <div className="mb-3">
                    <label htmlFor="userName" className="form-label fw-semibold text-success">
                        UserName
                    </label>
                    <input
                        type="text"
                        name="userName"
                        id="userName"
                        value={form.userName}
                        onChange={handleChange}
                        className={`form-control ${errors.userName ? "is-invalid" : ""}`}
                        placeholder="Enter your username"
                    />
                    {errors.userName && <div className="invalid-feedback">{errors.userName}</div>}
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label fw-semibold text-success">
                        Password
                    </label>
                    <input
                        type="password"
                        name="password"
                        id="password"
                        value={form.password}
                        onChange={handleChange}
                        className={`form-control ${errors.password ? "is-invalid" : ""}`}
                        placeholder="Enter your password"
                    />
                    {errors.password && <div className="invalid-feedback">{errors.password}</div>}
                </div>

                <button
                    type="submit"
                    disabled={loading}
                    className="btn btn-success w-100 fw-bold"
                >
                    {loading ? "Logging in..." : "Login"}
                </button>
            </form>
        </div>
    );
};

export default LoginPage;
