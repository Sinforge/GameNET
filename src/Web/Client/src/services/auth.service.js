import axios from "../components/axios/axios";


export function authorizeUser(data) {
    return axios.get(`/account/login`);
    
}
export function registerUser(user) {
    return axios.post(`/account/register`)
}
