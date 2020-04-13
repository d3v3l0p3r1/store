import request from '@/utils/request'

export function fetch() {
    var url = '/brands/GetAll'
    return request({
        url: url,
        method: 'get'
    })
}

export function update(data) {
    return request({
        url: '/brands',
        method: 'patch',
        data
    })
}

export function get(id) {
    return request({
        url: '/brands?id=' + id,
        method: 'get'
    })
}
