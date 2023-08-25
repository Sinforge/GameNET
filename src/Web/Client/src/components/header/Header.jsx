import { Link } from "react-router-dom"
import styles from "./Header.module.css"
const Header = () => {
    return(
        
            <div className={styles.navbar}>
                <Link className={styles.link}to="/home">Home </Link>
                <Link className={styles.link}to="/article">Article </Link>
                <Link className={styles.link}to="/account">Account</Link>
            </div>
    )
}
export default Header;