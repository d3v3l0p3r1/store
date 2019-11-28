import request from '@/utils/request'

export function getProducts(category, page, limit) {
    var url = 'product/getall?page=' + page + '&take=' + limit
    if (category != null) {
        url += '&catID=' + category
    }

    return request({
        url: url,
        method: 'GET'
    })
}

export function getCategories(id) {
    var url = 'Category/getall'
    if (id != null) {
        url += '?parentId=' + id
    }
    return request({
        url: url,
        method: 'GET'
    })
}

export function getProduct(id) {
    return request({
        url: 'product?id=' + id,
        method: 'GET'
    })
}

export function createProduct(data) {
    return request({
        url: 'product/create',
        method: 'post',
        data,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

export function updateProduct(data) {
    return request({
        url: 'product/update',
        method: 'post',
        data,
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    })
}

export function getKinds() {
    return request({
        url: 'kind/getAll?take=100',
        method: 'get'
    })
}

export function remove(id) {
    return request({
        url: '/product?id=' + id,
        method: 'delete'
    })
}
