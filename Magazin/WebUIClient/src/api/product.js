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
