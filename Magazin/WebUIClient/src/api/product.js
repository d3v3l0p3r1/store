import request from '@/utils/request'

export function getAll(page, take) {
    return request({
        url: '/api/product/getall?page=' + page + '&take=' + take,
        method: 'get'
    })
}
