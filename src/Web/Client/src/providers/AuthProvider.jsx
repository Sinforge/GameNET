import { createContext, useState } from "react";
export const AuthContext = createContext();


const AuthProvider = ({children}) => {
    const [user, setUser] = useState(null)// null -not auth user

    return <AuthContext.Provider value={{user, setUser}}>{children}</AuthContext.Provider>
}
export default AuthProvider