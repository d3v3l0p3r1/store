import request from '@/utils/request'

export function getDocuments() {
    return request({
        url: '/IncomingDocument/GetAll',
        method: 'GET'
    })
}
