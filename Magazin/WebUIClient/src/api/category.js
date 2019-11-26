import request from '@/utils/request'

export function getAll() {
    return request({
        url: '/api/category/getall',
        method: 'get'
    })
}
