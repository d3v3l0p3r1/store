import request from '@/utils/request'

export function getAll(id) {
    var url = '/Category/GetAll'
    if (id != null) {
        url += '?parentId=' + id
    }

    return request({
        url: url,
        method: 'get'
    })
}

export function create(data) {
    return request({
        url: '/Category',
        method: 'post',
        data
    })
}

export function update(data) {
    return request({
        url: '/Category',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/Category?id=' + id,
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/Category?id=' + id,
        method: 'delete'
    })
}
