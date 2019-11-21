import request from '@/utils/request'

export function login(data) {
  return request({
    url: 'api/account/login',
    method: 'post',
    data
  })
}

export function getInfo() {
  return request({
    url: 'api/account/info',
    method: 'get'
  })
}

export function logout() {
  return request({
    url: 'api/account/logout',
    method: 'post'
  })
}

export function getUsers(page, take, type) {
  var url = '/User/GetAll?take=' + take + '&page=' + page
  if (type != null) {
    url += '&type=' + type
  }
  return request({
    url: url,
    method: 'get'
  })
}

export function get(id) {
  return request({
    url: '/User?id=' + id,
    method: 'get'
  })
}

export function update(data) {
  return request({
    url: '/User',
    method: 'patch',
    data
  })
}

export function create(data) {
  return request({
    url: '/User',
    method: 'post',
    data
  })
}

export function getRoles() {
  return request({
    url: '/User/getRoles',
    method: 'get'
  })
}
