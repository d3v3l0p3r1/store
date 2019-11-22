import request from '@/utils/request'

export function getAll(page, limit) {
    return request({
        url: '/Contractor/GetAll?page=' + page + '&take=' + limit,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/Contractor',
        method: 'post',
        data,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

export function update(data) {
    return request({
        url: '/Contractor',
        method: 'patch',
        data,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

export function get(id) {
    return request({
        url: '/Contractor?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/Contractor?id=' + id,
        method: 'delete'
    })
}
