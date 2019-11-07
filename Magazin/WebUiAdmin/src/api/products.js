import request from '@/utils/request'

export function getProducts(category, page, limit) {
    return request({
        url: 'product/getall?page=' + page + '&catID=' + category + '&take=' + limit,
        method: 'GET'
    })
}

export function getCategories() {
    return request({
        url: 'Category/getall',
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
        url: 'kind/getAll',
        method: 'get'
    })
}