import request from '@/utils/request'

export function getDocuments(page, limit) {
    return request({
        url: '/OutcomingDocument/GetAll?page=' + page + '&take=' + limit,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/OutcomingDocument',
        method: 'post',
        data
    })
}

export function update(data) {
    return request({
        url: '/OutcomingDocument',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/OutcomingDocument?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/OutcomingDocument?id=' + id,
        method: 'delete'
    })
}

export function apply(id) {
    return request({
        url: '/OutcomingDocument/Apply?id=' + id,
        method: 'post'
    })
}

export function discard(id) {
    return request({
        url: '/OutcomingDocument/discard?id=' + id,
        method: 'post'
    })
}
