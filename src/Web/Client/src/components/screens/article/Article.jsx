import { useEffect } from "react";
import Comment from './Comment'
import { useParams } from "react-router-dom";
import { useState } from "react";
const Article = () => {
    const [data, setData] = useState({})
    const { id } = useParams();

    useEffect(() => {
        //get article by id
        //request to server
        setData({...data, 'id': id})
        
    }, [id])
    //get comments from server
    let comments;
    // TODO: extract to service
    const commentsData = () => fetch('change me to server url', {method: 'GET',})
        .then((comms) => comments = comms.json().map(comm => <Comment data={comm}></Comment>))
        .then((data) => console.log(data));
    
    return (
        <div>
            <h1>Article {id}</h1>
            <h1></h1>
        
            <div></div>

            {/* place for user comments
            need to render when user downscroll to comment  */}
            <div>
                <ul>
                    {comments ? comments : 'sorry, we got some error, while upload comments'}
                </ul>

            </div>
        </div>
    );
}
export default Article;