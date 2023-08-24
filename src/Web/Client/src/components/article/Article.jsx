import Comment from "../Shared/Comment";

const Article = ({title, content, owner}) => {
    //get comments from server
    let comments;
    const commentsData = () => fetch('change me to server url', {method: 'GET',})
        .then((comms) => comments = comms.json().map(comm => <Comment data={comm}></Comment>))
        .then((data) => console.log(data));
    
    return (
        <div>
            <h1>{title}. Create by {owner}</h1>
        
            <div>{content}</div>

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