<template>
  <div class="app-container">
    <UserList @current-change="handleCurrentChange" />    
  </div>
</template>

<script>
import UserList from '@/components/UsersList'

export default {
    name: 'Users',
    components: { UserList },
    data() {
        return {            
            
        }
    },
    methods: {
        handleCurrentChange(val) {
        }      
    }
}
</script>
