<template>
  <v-app>
    <Header />
    <v-main>
			<nuxt :key="$route.fullPath" />
		</v-main>
    <Footer />
    <client-only>
      <Modal />
    </client-only>
  </v-app>
</template>

<script>
import Header from "@/components/header/header";
import Footer from "@/components/footer/footer";
import Modal from "@/components/common/modal";
export default {
  components: {
    Header,
    Footer,
    Modal,
  },
  mounted(){
    try{
      this.$store.dispatch('userData/fetchUserData')  
    }
    catch(e){
      console.error(e)
    }
  }
};
</script>

<style lang="scss">
html, body {
    font-family: $main-font-family;
}
</style>
