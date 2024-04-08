import request from '@/utils/request'

export function createOrder(orderModel) {
    var url = '/api/order'
    return request({
        url: url,
        method: 'post',
        data: orderModel
    })
}