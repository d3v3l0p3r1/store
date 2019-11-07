import request from '@/utils/request'

export function getDocuments(page, limit) {
    return request({
        url: '/IncomingDocument/GetAll?page=' + page + '&take=' + limit,
        method: 'GET'
    })
}
