import request from '@/utils/request'

export function fetch(page, limit) {
    var url = 'carousel/getall?page=' + page + '&take=' + limit
    return request({
        url: url,
        method: 'GET'
    })
}

export function create(data) {
    return request({
        url: '/Carousel',
        method: 'post',
        data
    })
}
