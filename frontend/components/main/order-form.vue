<template>
  <v-container>
    <v-form @submit.prevent="submitOrder">
      <!-- <v-text-field v-model="shippingAddress" label="Адрес доставки" required></v-text-field> -->
      <v-autocomplete
        v-model="shippingAddress"
        required
        label="Адрес доставки"
        :items="userAddresses"
      ></v-autocomplete>
      <v-select disabled label="Оплата наличными"></v-select>
      <v-btn type="submit">Подтвердить заказ</v-btn>
    </v-form>
  </v-container>
  
</template>

<script>
import { mapGetters } from "vuex"; 

export default {
  data() {
    return {
      shippingAddress: '',
      paymentMethod: ''
    };
  },
  computed:{
    ...mapGetters({
      user: "userData/user"
    }),
    userAddresses(){
      console.log(this.user)
      if(!this.user || this.user.address == null){
        return []
      }
      if(typeof this.user.address === 'string')
        return [this.user.address]
      return this.user.address
    }
  },
  methods: {
    submitOrder() {
      this.$emit('submitOrder',this.shippingAddress);
    }
  },
  props: {
    fullBasket: {
      type: Array,
      required: true
    },

  }
};
</script>
