import request from '@/utils/request'

export function getAll(page, take, category) {
    var url = '/api/product/getall?page=' + page + '&take=' + take
    if (category != null) {
        url += '&catID=' + category
    }
    return request({
        url: url,
        method: 'get'
    })
}

export function getDetail(id) {
    var url = '/api/product/get?id=' + id
    return request({
        url: url,
        method: 'get'
    })
}