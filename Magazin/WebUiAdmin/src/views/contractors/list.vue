<template>
  <div class="app-container">
    <ContractorList :enable-edit=true />
  </div>
</template>

<script>
import ContractorList from '@/components/ContractorList/index'

export default {
    name: 'Users',
    components: { ContractorList },
    data() {
        return {
        }
    },
    methods: {
    }
}
</script>
