<template>
    <v-container>
      <v-menu
        v-model="menu"
        :close-on-content-click="false"
        offset-y
        nudge-bottom="20"
        z-index="100"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            v-bind="attrs"
            v-on="on"
          >
            {{ fullName }}
          </v-btn>
        </template>
        <v-list color="black" class="user-logged-in__menu">
          <v-list-item class="user-logged-in__menu-item" @click="goToProfile">
            <v-list-item-title class="user-logged-in__menu-title">Личный кабинет</v-list-item-title>
          </v-list-item>
          <v-list-item  class="user-logged-in__menu-item" @click="logout">
            <v-list-item-title class="user-logged-in__menu-title">Выйти</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-container>
</template>
  
<script>
export default {
data() {
    return {
    menu: false,
    };
},
computed:{
    fullName(){
      if (this.user){
        return `${this.user.lastName} ${this.user.firstName} ${this.user.middleName}`
      }
    }
},
props: {
    user:{
        type: Object,
        required: true
    }
},
methods: {
    goToProfile() {
      this.$router.push('/users/profile');
    },
    logout() {
    // Логика для выхода из аккаунта
      this.$store.commit('userData/removeUser');
      this.$store.commit('userData/removeUserId');
      this.$store.commit('userData/removeToken');
      this.$router.push('/')
    }
}
};
</script>

<style scoped>
/* .user-logged-in__menu{
  background-color: black;
  
} */
.user-logged-in__menu-item{
  background-color: black;
  transition: background-color 0.3s;
}
.user-logged-in__menu-item:hover{
  background-color: #e8b37f6f;
}
.user-logged-in__menu-title{
  color: #d3a272;
}

</style>