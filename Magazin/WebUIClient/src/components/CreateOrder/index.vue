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
      <span>Способ оплаты</span>
    </div>

    <div>
      <table>
        <tbody>
          <tr>
            <td />
          </tr>
        </tbody>
      </table>
    </div>

    <div>
      <v-btn :loading="isLoading" dark @click="sumbitOrder()">Подтвердить заказ</v-btn>
    </div>

  </v-container>

</template>

<script>
import { createOrder } from '@/api/order'
import { mapMutations } from 'vuex'
import { cache, card } from "@/utils/paymentType"

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
            },
            isLoading: false
        }
    },
    computed: {
    },
    methods: {
        async sumbitOrder() {
            const orderModel = {
                address: this.orderInfo.address,
                comment: this.orderInfo.comment,
                name: this.orderInfo.lastName + ' ' + this.orderInfo.firstName,
                phone: this.orderInfo.phone,
                products: this.$store.state.products
            }
            try {
            this.isLoading = true
            const res = await createOrder(orderModel)
            this.isLoading = false
            this.addOrder(res)
            this.$router.push({ path: '/orders' })
            } finally {
              this.isLoading = false
            }
        },
        ...mapMutations(['addOrder'])
    }
}
</script>
