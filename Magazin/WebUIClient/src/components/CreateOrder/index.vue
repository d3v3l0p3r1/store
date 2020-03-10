<template>
  <v-container class="pa-12">
    <h3>Информация о заказе</h3>
    <div>
      <div>
        <span>Имя</span>
        <v-text-field v-model="orderInfo.firstName" solo />

        <span>Фамилия</span>
        <v-text-field v-model="orderInfo.lastName" solo />
      </div>
      <div>
        <span>Адрес доставки</span>
        <v-text-field v-model="orderInfo.address" solo full-width />
      </div>
      <div>
        <span>Телефон</span>
        <v-text-field v-model="orderInfo.phone" solo full-width type="phone" />
      </div>

      <div>
        <span>Комментарий</span>
        <v-textarea v-model="orderInfo.comment" solo full-width />
      </div>
    </div>

    <div>
      <v-btn @click="sumbitOrder()">Подтвердить заказ</v-btn>
    </div>

  </v-container>

</template>

<script>
import { createOrder } from '@/api/order'

export default {
    name: 'CreateOrder',
    data() {
        return {
            orderInfo: {
                firstName: null,
                lastName: null,
                address: null,
                phone: null,
                comment: null
            }
        }
    },
    methods: {
        sumbitOrder() {
            const orderModel = {
                address: this.orderInfo.address,
                comment: this.orderInfo.comment,
                name: this.orderInfo.lastName + ' ' + this.orderInfo.firstName,
                phone: this.orderInfo.phone,
                products: []
            }

            createOrder(orderModel)
        }
    }

}
</script>
