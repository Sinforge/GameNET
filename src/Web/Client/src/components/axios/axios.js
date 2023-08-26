import axios from "axios";

const config = JSON.parse(require("../assets/config.dev.json"))
const API_URL = config["api-url"]

export default axios.create({
    baseURL: API_URL
});

export const axiosPrivate = axios.create({
    baseURL: API_URL,
    withCredentials: true,
    headers: { 'Content-Type': 'application/json' },  
})