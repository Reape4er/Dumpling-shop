<template>
  <v-container>
    <v-row>
      <v-col
        v-for="product in products"
        :key="product.id"
        cols="12"
        sm="6"
        md="4"
        lg="4"
      >
        <ProductItem :product="product" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { repository_factory } from "@/API/repositories-factory.js";
import ProductItem from "@/components/main/ProductItem.vue";

const productsRepositoryFactory = repository_factory.get('products');

export default {
  components: {
    ProductItem
  },
  async asyncData() {
    const response = await productsRepositoryFactory.getProducts();
    return {
      products: response ? response.data : []
    };
  }
};
</script>
