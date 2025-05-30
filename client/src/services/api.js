import axios from "axios";

const API = axios.create({
    baseURL: "http://localhost:5185/api", 
});
export default API;
