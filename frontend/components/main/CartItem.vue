<template>
  <v-list-item>
    <v-avatar
        color="grey"
        rounded="0"
        size="150"
    >
      <v-img :src="'data:image/png;base64,' + product.image" cover></v-img>
    </v-avatar>
    <v-list-item-content>
      <v-list-item-title>{{ product.name }}</v-list-item-title>
      <v-list-item-subtitle>Цена: {{ product.price }} руб.</v-list-item-subtitle>
      <v-list-item-subtitle>
        Количество:
        <v-row
          justify="start"
          align="center"
        >
          <v-col cols="auto">
            <v-btn @click="decrement">-</v-btn>
          </v-col>

          <v-col cols="auto">
            <v-text-field
              v-model="itemQuantity"
              type="number"
              class="text-center"
              style="max-width: 40px"
              readonly
            ></v-text-field>
          </v-col>

          <v-col cols="auto">
            <v-btn @click="increment">+</v-btn>
          </v-col>
        </v-row>
      </v-list-item-subtitle>
    </v-list-item-content>
    <v-list-item-action>
      <v-btn icon @click="removeProduct">
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </v-list-item-action>
  </v-list-item>
</template>

<script>
import { repository_factory } from "@/API/repositories-factory.js";

const basketsRepositoryFactory = repository_factory.get("baskets");


export default {
  props: {
    value:{
      type: Number,
      required: true
    },
    product: {
      type: Object,
      required: true,
    },
    userId: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
    };
  },
  computed:{
    itemQuantity:{
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    }
  },
  methods: {
    updateQuantity() {
      this.$emit('update-quantity', this.product);
    },
    async increment() {
      try {
        const newQuantity = this.itemQuantity + 1;
        await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
          id: this.product.id,
          quantity: newQuantity,
        });
        this.itemQuantity = newQuantity; 
      } catch (error) {
        console.error("Ошибка при вызове action:", error);
      }
    },
    async decrement() {
      try {
        if (this.itemQuantity > 0) {
          const newQuantity = this.itemQuantity - 1;
          await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
            id: this.product.id,
            quantity: newQuantity,
          });
          this.itemQuantity = newQuantity;
          if (newQuantity == 0){
            this.$emit('removeItem', this.product);
          } 
        }
      } catch (error) {
        console.error("Ошибка при вызове action:", error);
      }
    },
    async removeProduct(){
      await basketsRepositoryFactory.UpdateItemInBasket(this.userId, {
            id: this.product.id,
            quantity: 0,
          });
      this.$emit("removeItem",this.product);
    }
  },
};
</script>
