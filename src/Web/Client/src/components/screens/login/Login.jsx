import { getJwtToken } from "../../../services/auth.service";
import { useContext, useState } from "react"
import { AuthContext } from "../../../providers/AuthProvider";

const Login = () => {
    const [form, setForm] =  useState({});
    const [user, setUser] = useContext(AuthContext);
    const verifyData = async () => {
        let response = authorizeUser(form)
        if(response.status === 200) {
            user.access_token = response.data.access_token;
        }
        else {
            //do something
        }
    }
    return(
        <form>
            <input key="userId" onChange={e => setForm({...form, userId: e.target.value})} />
            <input key="password"onChange={e=> setForm({...form, password: e.target.value})} />
            <button onClick={verifyData()}></button>
        </form>
    )
}
export default Login;