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
