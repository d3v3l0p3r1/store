import request from '@/utils/request'

export function getCarousel() {
    var url = '/api/public/GetCarousel'
    return request({
        url: url,
        method: 'get'
    })
}

export function getLastProducts(count) {
    var url = '/api/public/GetLastProducts?count=' + count
    return request({
        url: url,
        method: 'get'
    })
}

export function getBrands() {
    var url = '/api/public/GetBrands'
    return request({
        url: url,
        method: 'get'
    })
}