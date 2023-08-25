import { useState } from "react";
import Preview from "../preview/Preview";
import styles from "./ArticlesPage.module.css"
import CreateArticleForm from "../create-article-form/CreateArticleForm";
const mockData = [
    {
        id: 1,
        name : "How to get strong in game",
        shortDesc : "Its guide how to get strong in game always",
        src : "https://i.ytimg.com/vi/qH7JG-omrRU/maxresdefault.jpg"
    },
    {
        id: 2,
        name : "Template1",
        shortDesc : "Template1",
        src : "https://i.ytimg.com/vi/qH7JG-omrRU/maxresdefault.jpg"
    },
    {
        id: 3,
        name : "Template2",
        shortDesc : "Template2",
        src : "https://i.ytimg.com/vi/qH7JG-omrRU/maxresdefault.jpg"
    },
];
export default function Main() {
    const [previews, setPreviews] = useState(mockData);

    return (
        <div>
            <CreateArticleForm/>
            <ul className={styles.previewList}>
                {mockData.map(preview => 
                <Preview key={preview.id} id={preview.id} name={preview.name} shortDesc={preview.shortDesc} src={preview.src}/>)
            }
            </ul>
        </div>
        
    );
}