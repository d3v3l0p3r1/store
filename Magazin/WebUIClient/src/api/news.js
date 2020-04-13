import request from '@/utils/request'

export function getNews() {
    var url = '/api/news'
    return request({
        url: url,
        method: 'post',
        data: orderModel
    })
}