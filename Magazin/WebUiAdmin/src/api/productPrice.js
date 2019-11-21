import request from '@/utils/request'

export function getAll(page, limit) {
    return request({
        url: '/ProductPrice/GetAll?page=' + page + '&take=' + limit,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/ProductPrice',
        method: 'post',
        data
    })
}

export function update(data) {
    return request({
        url: '/ProductPrice',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/ProductPrice?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/ProductPrice?id=' + id,
        method: 'delete'
    })
}
