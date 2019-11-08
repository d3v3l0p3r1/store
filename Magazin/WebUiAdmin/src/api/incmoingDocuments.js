import request from '@/utils/request'

export function getDocuments(page, limit) {
    return request({
        url: '/IncomingDocument/GetAll?page=' + page + '&take=' + limit,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/IncomingDocument',
        method: 'post',
        data
    })
}

export function get(id) {
    return request({
        url: '/IncomingDocument?id=' + id,
        method: 'get'
    })
}
