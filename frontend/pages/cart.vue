<template>
    <v-container>
      <v-list>
        <v-list-item-group>
          <CartItem
            v-for="product in fullBasket"
            :key="product.id"
            :product="product"
            :userId="userId"
            v-model="product.quantity"
            @removeItem="removeProduct"
          />
        </v-list-item-group>
      </v-list>
      <v-divider></v-divider>
      <v-card class="mx-auto my-4" max-width="400">
        <v-card-title>Итоговая цена: {{ totalPrice }} руб.</v-card-title>
      </v-card>
      <OrderForm :fullBasket="fullBasket" @submitOrder="processOrder" />
    </v-container>
  </template>
  
  <script>
  import { mapGetters } from "vuex";
  import UserLocalStorage from "@/utils/service.localStorage";
  import CartItem from "@/components/main/CartItem";
  import OrderForm from "@/components/main/order-form";
  import { repository_factory } from "@/API/repositories-factory.js";
  
  const productsRepositoryFactory = repository_factory.get('products');
  const ordersRepositoryFactory = repository_factory.get('orders');
  const basketsRepositoryFactory = repository_factory.get('baskets');

  export default {
    components: {
      CartItem,
      OrderForm
    },
    data() {
      return {
        userId: UserLocalStorage.GET_USERID() ? Number(UserLocalStorage.GET_USERID()) : 0,
        fullBasket: []
      };
    },
    computed: {
      ...mapGetters({
        basket: "basket/basketItems",
      }),
      totalPrice() {
        return this.fullBasket.reduce((total, product) => {
          return total + product.price * product.quantity;
        }, 0).toFixed(2);
      },
    },
    async mounted() {
      await this.$store.dispatch('basket/fetchBasket', this.userId)
      this.basket?.forEach(async (basketItem) => {
        const response = await productsRepositoryFactory.getProduct(basketItem.id); 
        this.fullBasket.push({
          ...response.data,
          quantity: basketItem.quantity
        });
      });
    },
    methods: {
      async processOrder(address) {
        try {
          const postBasket = this.basket.map(item => {
            const { id, ...rest } = item;
            return { itemID: id, ...rest };
          });
          const orderDetails = {
            address: address,
            userId: this.userId,
            orderItems: postBasket
          }
          const response = await ordersRepositoryFactory.postOrder(orderDetails)
          // this.$store.commit("userData/CLEAR_BASKET")
          await basketsRepositoryFactory.clearBasket(this.userId); 
          this.fullBasket = []

        } catch (error) {
          console.error('Failed to process order', error);
        }
      },
      removeProduct(product) {
        this.fullBasket = this.fullBasket.filter(p => p.id !== product.id);
      },
    }
  };
  </script>
  