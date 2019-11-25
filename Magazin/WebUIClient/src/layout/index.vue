<template>
    <div class="app-wrapper">
      <el-container>
        <el-header  style="padding: 0; margin:0;">              
          <NavBar />
        </el-header>

        <el-main>
          <AppMain />
        </el-main>

        <el-footer>

        </el-footer>
      </el-container>
    </div>    
</template>

<script>
import { NavBar, AppMain } from './components'
export default {
    name: 'Layout',
    components: { NavBar, AppMain }
}
</script>