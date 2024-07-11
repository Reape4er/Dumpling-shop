<template>
  <header ref="header" class="header">
    <div class="header-main">
      <div class="header-row">
        <Logo />
        <NavBar />
        <SearchBar />
        <Tools v-if="fetchedUser" :isDesktop="isDesktop" />
      </div>
    </div>
  </header>
</template>

<script>
import Logo from "@/components/header/logo";
import NavBar from "@/components/header/nav-bar.vue";
import SearchBar from "@/components/header/v-search-bar";
import resizeObserverMX from "@/mixins/resize-observer-mx";
import { mapGetters } from "vuex";

import UserLocalStorage from "@/utils/service.localStorage";
import { UserAuthorize } from '@/assets/js/const.localStorage.js';
import { repository_factory } from "@/API/repositories-factory.js"

const usersRepositoryFactory = repository_factory.get('users')

export default {
  components: {
    Logo,
    NavBar,
    SearchBar,
    Tools: () => import("@/components/header/tools"),
  },
  data() {
    return {
      fetchedUser: true,
      // isDesktop: true,
    };
  },
  mixins: [resizeObserverMX],
  computed: {
    ...mapGetters({
      isDesktop: "tech/isDesktop",
      isTablet: "tech/isTablet",
    }),
  
  },
};
</script>

<style>
  .header{
    background-color: black;
    padding-top: 5px;
    left: 0;
    top: 0;
    width: 100%;
    .header-row{
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
    }
  }
</style>