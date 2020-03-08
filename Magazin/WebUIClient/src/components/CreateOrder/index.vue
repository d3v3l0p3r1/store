<template>
<v-container class="pa-12">
    <div class="row">

        <div class="sm-6">
            <h3>Информация о заказе</h3>
        </div>
        <div>
            <v-text-field label="Имя"></v-text-field>
            <v-text-field label="Фамилия"></v-text-field>
        </div>

        <div class="sm-6">
        </div>
    </div>
</v-container>
    
</template>

<script>
export default {
    name: 'CreateOrder'
    
}
</script>