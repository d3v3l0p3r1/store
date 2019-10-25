import request from '@/utils/request'

export function login(data) {
  return request({
    url: 'api/account/login',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request({
    url: 'api/account/info',
    method: 'get',
    params: { token }
  })
}

export function logout() {
  return request({
    url: 'api/account/logout',
    method: 'post'
  })
}
