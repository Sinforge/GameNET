import axios from "../components/axios/axios"

export function getAllArticles() {
    return axios.get(`/article}`)
}
export function getArticleById(id) {
    return axios.get(`/article/${id}`)
}
export function getCommentsOfArticle(id) {
    return axios.get(`/article/${id}/comment`)
}
export function getReplyOfComment(articleId, commentId) {
    return axios.get(`/article${artilceId}/comment/${commentId}`)
}
