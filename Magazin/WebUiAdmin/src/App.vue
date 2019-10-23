import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'

<template>
  <div id="app">
    <el-container style="height: 100%; border: 1px solid #eee">
      <div id="nav">

        <el-aside width="200px">
          <el-menu>
            <el-menu-item>
              <router-link to="/">Home</router-link>
            </el-menu-item>
            <el-menu-item>
              <router-link to="/about">About</router-link>
            </el-menu-item>
          </el-menu>
        </el-aside>

      </div>

      <el-container>
        <el-main>
          <router-view />
        </el-main>
      </el-container>
    </el-container>

  </div>
</template>
