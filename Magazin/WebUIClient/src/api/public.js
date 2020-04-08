import request from '@/utils/request'

export function getCarousel() {
    var url = '/api/public/GetCarousel'
    return request({
        url: url,
        method: 'get'
    })
}
