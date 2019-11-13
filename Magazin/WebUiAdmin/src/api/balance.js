import request from '@/utils/request'

export function getBalance(page, take, cat) {
    var url = '/Balance/GetProductBalance?page=' + page + '&take=' + take
    if (cat != null) {
        url += '&cat=' + cat
    }
    return request({
        url: url,
        method: 'get'
    })
}

export function getMoves(page, take, cat) {
    var url = '/Balance?page=' + page + '&take=' + take
    if (cat != null) {
        url += '&cat=' + cat
    }
    return request({
        url: url,
        method: 'get'
    })
}
