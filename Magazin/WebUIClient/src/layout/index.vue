<template>
    <div class="app-wrapper">
        <div class="main-container">
            <div class="fixed-header">
                <navbar />
            </div>
            <app-main />
        </div>
    </div>
    
</template>

<script>
import { NabVar } from './components/NavBar'
export default {
    name: 'Layout',
    components: { NabVar }
}
</script>