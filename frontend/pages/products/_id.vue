<template>
  <v-container class="product-page">
    <v-row>
      <v-col cols="12" md="5">
        <v-img
          :src="'data:image/png;base64,' + product.image"
          alt="Product Image"
          class="product-image"
          max-height="400px"
          contain
        ></v-img>
      </v-col>
      <v-col cols="12" md="6">
        <v-card class="product-info">
          <v-card-title>{{ product.name }}</v-card-title>
          <v-card-text>
            <p>{{ product.description }}</p>
            <div class="product-price">Цена: {{ product.price }}₽</div>
          </v-card-text>
          <v-card-actions>
            <v-row class="align-center">
              <v-col cols="12" class="text-center ml-2" v-if="quantity < 1">
                <v-btn @click="addToCart" block>Добавить в корзину</v-btn>
              </v-col>
              <v-col cols="12" class="products_counter ml-2" v-else>
                <v-btn class="mr-2" @click="decrement">-</v-btn>
                <v-text-field
                  v-model="quantity"
                  type="number"
                  class="mx-2 text-center"
                  style="max-width: 60px; text-align: center;"
                  readonly
                />
                <v-btn class="ml-2" @click="increment">+</v-btn>
              </v-col>
            </v-row>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { repository_factory } from "@/API/repositories-factory.js";
import UserLocalStorage from "@/utils/service.localStorage";
import { mapGetters } from "vuex";

const basketsRepositoryFactory = repository_factory.get("baskets");
const productsRepositoryFactory = repository_factory.get("products");

export default {
  data() {
    return {
      productInCart: false,
      userId: UserLocalStorage.GET_USERID() ? UserLocalStorage.GET_USERID() : 0,
      quantity: 0,
    };
  },
  async asyncData({ params }) {
    const response = await productsRepositoryFactory.getProduct(params.id);
    return {
      product: response.data,
    };
  },
  methods: {
    async addToCart() {
      try {
        await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
          id: this.product.id,
          quantity: 1,
        });
        this.quantity++;
      } catch (error) {
        console.error("Ошибка при вызове action:", error);
      }
    },
    async increment() {
      try {
        this.quantity++;
        await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
          id: this.product.id,
          quantity: this.quantity,
        });
      } catch (error) {
        console.error("Ошибка при вызове action:", error);
      }
    },
    async decrement() {
      try {
        if (this.quantity > 0) {
          this.quantity--;
          await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
            id: this.product.id,
            quantity: this.quantity,
          });
        }
      } catch (error) {
        console.error("Ошибка при вызове action:", error);
      }
    },
  },
  computed: {
    ...mapGetters({
      basket: "basket/basketItems",
    }),
  },
  async created() {
    await this.$store.dispatch("basket/fetchBasket", this.userId);
    if (!this.basket.find((item) => item.id === this.product.id)) {
      return;
    }
    this.quantity = this.basket.find((item) => item.id === this.product.id)
      .quantity;
  },
};
</script>

<style scoped>
.products_counter{
  display: flex;
  align-items: center;
  justify-content: left;
}
.product-page {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 20px;
}
.product-image {
  max-width: 100%;
  height: auto;
}
.product-info {
  padding: 20px;
}
.product-price {
  font-size: 1.5em;
  margin-bottom: 20px;
}
.quantity-control {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}
.quantity-control button {
  width: 30px;
  height: 30px;
}
.quantity-control input {
  width: 50px;
  text-align: center;
}
</style>
