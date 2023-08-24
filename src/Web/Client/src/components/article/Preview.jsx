export default function Preview({name, shortDesc}) {

    return(
        <div>
            // like a preview 
            <img src={'change-to-server-url' + name}></img>

            // Name of article 
            <h2>{name}</h2>
            
            // short description about article
            <div>{shortDesc}</div>
        </div>
    );
}