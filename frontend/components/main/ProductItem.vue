<template>
  <v-card class="mx-auto my-4" height="72vh">
    <v-img
      :src="'data:image/png;base64,' + product.image"
      height="300px"
    ></v-img>
    <v-card-title class="headline">{{ truncatedName  }}</v-card-title>
    <v-card-subtitle>Цена: {{ product.price }}₽</v-card-subtitle>
    <!-- <v-card-text>{{ product.description }}</v-card-text> -->
    <v-card-text>
      <div v-if="product.stockQuantity > 0" class="product-stock">
        <v-chip class="ma-2" color="green" dark>В наличии</v-chip>
        <v-chip class="ma-2" outlined>Количество: {{ product.stockQuantity }}</v-chip>
      </div>
      <div v-else class="product-out-of-stock">
        <v-chip class="ma-2" color="red" dark>Нет в наличии</v-chip>
      </div>
    </v-card-text>
    <v-card-actions>
      <v-btn class="ProductItem__button" :to="'/products/' + product.id" block>Подробнее</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    product: {
      type: Object,
      required: true
    }
  },
  computed:{
    truncatedName() {
      const maxLength = 40; // Максимальная длина строки
      if (this.product.name.length > maxLength) {
        return this.product.name.substring(0, maxLength) + '...'; // Обрезка и добавление многоточия
      }
      return this.product.name;
    }
  }
}
</script>

<style scoped>
.product-stock {
  display: flex;
  align-items: center;
  justify-content: center;
}
.product-out-of-stock {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
