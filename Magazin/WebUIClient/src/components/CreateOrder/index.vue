<template>
  <div>
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
        <v-select v-model="orderInfo.paymentType" :items="paymentTypes" solo />
      </div>

      <div>
        <h3>Ваш заказ</h3>
        <v-simple-table>
          <template v-slot:default>
            <table>
              <thead>
                <th>Название</th>
                <th>Всего</th>
              </thead>
              <tbody>
                <tr v-for="item in orderProducts" :key="item.product.id">
                  <td>{{ item.product.title }}</td>
                  <td>{{ item.product.price * item.count }}</td>
                </tr>
                <tr>
                  <td><b>ИТОГ</b></td>
                  <td><b>{{ Summ }}</b></td>
                </tr>
              </tbody>
            </table>
          </template>
        </v-simple-table>
      </div>
      <div class="mt-6">
        <v-btn :loading="isLoading" dark @click="sumbitOrder()">Подтвердить заказ</v-btn>
      </div>
    </v-container>
    <v-dialog v-model="showDialog" width="300" persistent>
      <v-card>
        <v-card-title>Заказ успешно оплачен</v-card-title>
        <v-card-text>Информацию о заказе вы можете узнать на странице отслеживания заказов</v-card-text>
        <v-card-actions>
          <v-btn dark block @click="submit()">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { createOrder } from '@/api/order'
import { mapMutations } from 'vuex'
import paymentType from '@/utils/paymentType'

export default {
    name: 'CreateOrder',
    components: {},
    data() {
        return {
            orderInfo: {
                firstName: null,
                lastName: null,
                address: null,
                phone: null,
                comment: null,
                paymentType: null
            },
            isLoading: false,
            showDialog: false
        }
    },
    computed: {
      paymentTypes() {
        return [
          { text: 'Наличными курьеру', value: paymentType.cache },
          { text: 'Картой', value: paymentType.card }
        ]
      },
      orderProducts() {
        const products = this.$store.state.products
        return products
      },
      Summ() {
          let summ = 0
          for (var i = 0; i < this.$store.state.products.length; i++) {
            summ += this.$store.state.products[i].product.price * this.$store.state.products[i].count
          }
          return summ
        }
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
            this.showDialog = true
            } finally {
              this.isLoading = false
            }
        },
        submit() {
          this.showDialog = false
          this.$router.push({ path: '/orders' })
        },
        ...mapMutations(['addOrder'])
    }
}
</script>
