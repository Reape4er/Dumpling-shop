<template>
    <div class="tools">
        <UserLoggedIn v-if="user" :user="user" />
        <UserLogin class="mr-5" :isDesktop="isDesktop" v-else/>
    </div>
</template>

<script>
import UserLoggedIn from "@/components/header/user-logged-in";
import UserLogin from "@/components/header/user-login";
import { mapGetters } from "vuex";

export default {
    components:{
        UserLoggedIn,
        UserLogin
    },

    props: {
        isDesktop:Boolean
    },

    // data(){
    //     return{
    //         user: false
    //     }
    // },

    computed:{
        ...mapGetters({
            user: 'userData/user'
        })
    },
    created(){
        // console.log(this.$store.getters.userData)
    }
}
</script>