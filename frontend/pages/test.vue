<template>
    <v-container>
      <v-list>
        <v-list-item-group>
          <CartItem
            v-for="product in products"
            :key="product.id"
            :product="product"
            @update-quantity="updateQuantity"
          />
        </v-list-item-group>
      </v-list>
      <v-divider></v-divider>
      <v-card class="mx-auto my-4" max-width="400">
        <v-card-title>Итоговая цена: {{ totalPrice }} руб.</v-card-title>
      </v-card>
    </v-container>
  </template>
  
  <script>
  import CartItem from '@/components/main/CartItem.vue';
  
  export default {
    name: 'Cart',
    components: {
      CartItem,
    },
    data() {
      return {
        products: [
          { id: 1, name: 'Товар 1', price: 100, quantity: 1 },
          { id: 2, name: 'Товар 2', price: 200, quantity: 1 },
          { id: 3, name: 'Товар 3', price: 300, quantity: 1 },
        ],
      };
    },
    computed: {
      totalPrice() {
        return this.products.reduce((total, product) => {
          return total + product.price * product.quantity;
        }, 0);
      },
    },
    methods: {
      updateQuantity(updatedProduct) {
        const productIndex = this.products.findIndex(
          (product) => product.id === updatedProduct.id
        );
        if (productIndex !== -1) {
          this.products[productIndex].quantity = updatedProduct.quantity;
        }
      },
    },
  };
  </script>
  