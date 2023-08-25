const Comment = ({data}) => {
    return (
        <div>
            // User which leave a comment
            <div>{data.username}</div>
            
            // User avatar
            <img>{data.avatar}</img>

            // Date of comment
            <div>{data.date}</div>

            // Content of comment
            <div>{data.content}</div>
        </div>
    );
}
export default Comment;