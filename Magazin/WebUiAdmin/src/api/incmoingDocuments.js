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

export function update(data) {
    return request({
        url: '/IncomingDocument',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/IncomingDocument?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/IncomingDocument?id=' + id,
        method: 'delete'
    })
}

export function apply(id) {
    return request({
        url: '/IncomingDocument/Apply?id=' + id,
        method: 'post'
    })
}

export function discard(id) {
    return request({
        url: '/IncomingDocument/discard?id=' + id,
        method: 'post'
    })
}
