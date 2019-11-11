import request from '@/utils/request'

export function getDocumentStatusEnum() {
    return request({
        url: '/Enums/GetDocumentStatuses',
        method: 'get'
    })
}
