import request from '@/utils/request'

export function getUploadUrl() {
    var baseurl = process.env.VUE_APP_BASE_API
    return baseurl + '/File/SaveFile'
}

export function getFileUrl(id) {
    var baseurl = process.env.VUE_APP_BASE_API
    return baseurl + '/File/GetFile?id=' + id
}

export function uploadImage(data) {
    return request({
        url: '/File/SaveFile',
        method: 'post',
        data,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
      })
}