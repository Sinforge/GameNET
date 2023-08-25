import { useState } from "react";
import { useContext } from "react";
import { AuthContext } from "../../../../providers/AuthProvider";
// make it only for auth users

const CreateArticleForm = () => {
    const {user, setUser} = useContext(AuthContext);
    const clearData = {
        "Title": '',
        "Text": '',
        "Owner" : user?user.name:''
    }
    
    const [data, setData] = useState(clearData);

    
    const checkCorrect = (e) => {
        e.preventDefault();
        console.log(data);
        setData(clearData)
    }
    return (
        <div>
            { user ?
                <form>
                    <input placeholder="title" value={data.Title} onChange={ e => setData({...data, "Title": e.target.value})} />
                    <textarea placeholder="text" value={data.Text}onChange= {e => setData({...data, "Text": e.target.value})}/>
                    {/* todo : get user from context*/}

                    <input type="submit" onClick={e => checkCorrect(e)} value={"Send"}/>
                </form>
                :
                <div>You not authorize to create article</div>
        }
        </div>
        
    )
}
export default CreateArticleForm;