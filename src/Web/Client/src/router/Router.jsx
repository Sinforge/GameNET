import {Route, Routes, BrowserRouter} from 'react-router-dom'
import ArticleList from '../components/screens/article/article-page/ArticlesPage'
import Article from '../components/screens/article/Article';
import Header from '../components/header/Header';

const Router = () => {
    return <BrowserRouter>
        <Header/>
        <Routes>
            <Route exact element={<ArticleList/>}  path="/article"/>
            <Route element={<Article/>} path="/article/:id"/>
            <Route element={<div>Page not found</div>} path="*"/>
        </Routes>
    </BrowserRouter>
}
export default Router;