export function getFileUrl(id) {
    var baseurl = process.env.VUE_APP_BASE_API
    return baseurl + '/File/GetFile?id=' + id
}