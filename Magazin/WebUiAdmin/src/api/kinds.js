import request from '@/utils/request'

export function getKinds(page, limit) {
    return request({
        url: '/Kind/GetAll?page=' + page + '&take=' + limit,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/Kind',
        method: 'post',
        data
    })
}

export function update(data) {
    return request({
        url: '/Kind',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/Kind?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/Kind?id=' + id,
        method: 'delete'
    })
}
