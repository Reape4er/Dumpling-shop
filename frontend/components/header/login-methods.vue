<template>
    <div class="login-methods">
        <button class = "shopBtn" type="button" @click="openSignIn">Войти</button>
        <nuxt-link :to="`/users/register`">зарегестрироваться</nuxt-link>
    </div>
</template>

<script>
import { ModalBus } from "@/event-bus/modal-bus";
import SignIn from "@/components/header/sign-in";
export default{
    methods:{
        openSignIn(){
            this.$parent.$emit('on-closed');
            ModalBus.$emit('open', {component: SignIn});
        }
    }
}
</script>

<style>
.login-methods{
    display: flex;
    flex-direction: column;
}
</style>