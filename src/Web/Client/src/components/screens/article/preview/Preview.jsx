import styles  from './Preview.module.css'
import {Link} from 'react-router-dom'
export default function Preview({name, shortDesc, src, key, id}) {

    return(
        <Link className={styles.preview} to={`/article/${id}`}>
                {/* like a preview */}
                <img src={src} width={150} height={150}></img>

                <div className={styles.info}>
                        
                    {/* Name of article */} 
                    <h2 className={styles.title}>{name}</h2>
                    
                    {/* short description about article */}
                    <div>{shortDesc}</div>
                </div>

        </Link>
    );
}