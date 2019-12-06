import request from '@/utils/request'

export function getAll(parentId) {
    var url = '/api/category/getall'
    if (parentId != null) {
        url += '?parentId=' + parentId
    }
    return request({
        url: url,
        method: 'get'
    })
}
