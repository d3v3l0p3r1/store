<template>
<v-container class="pa-12">
    <h3>Информация о заказе</h3>     
    <div>
        <div>
        <span>Имя</span>
        <v-text-field solo></v-text-field>

        <span>Фамилия</span>
        <v-text-field solo ></v-text-field>
        </div>
        
    </div>
    <div>
        <v-text-field full-width label="Адрес доставки"></v-text-field>
    </div>
    <div>
        <v-text-field full-width label="Телефон" type="phone"></v-text-field>
    </div>
    
</v-container>
    
</template>

<script>
export default {
    name: 'CreateOrder'
    
}
</script>