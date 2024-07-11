<template>
    <div class="shop-text-input">
        <div class="shop-text-input__control"
        :class="{'shop-text-input__control--error':this.v.$error}"
        >
            <div class="shop-text-input__slot">
                <div class="shop-text-input__field">
                    <input class="shop-text-input__input"
                    :type="inputType"
                    v-model.lazy="password"
                    :id="idForPassword"
                    />

                    <label class="shop-text-input__label"
                    :class="{'shop-text-input__label--focus':password}"
                    :for="idForPassword">
                    Пароль
                    </label>
                    
                    <button
                    class="shop-text-input__show-password-toggle"
                    @click="togglePasswordVisiability"
                    >
                    </button>
                </div>
            </div>
            <div class="shop-text-input__messages">
                <div class="shop-text-input__message">
                    введите пароль
                </div>
                <div class="shop-text-input__messages shop-text-input__message--persistent">
                    <button class="shop-text-input__password-recovery-btn" type="button" @click="openPasswordRecovery">
                        Восстановить пароль
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
    import {getRandomInt} from "@/assets/js/utill.js"
    export default{
        props:{
            value:{
                type: String,
                default: ''
            },
            v:{
                type: Object,
                required: true,
            }
        },
        data(){
            return{
                showPassword: false,
            }
        },
        computed:{
            password:{
                get(){
                    return this.value;
                },
                set(value){
                    this.v.$touch();
                    this.$emit('input', value);
                }},
            idForPassword(){
                return `${getRandomInt()}-password`;
            },
            inputType(){
                return this.showPassword?'text':'password';
            }
        },
        methods:{
            togglePasswordVisiability(){
                this.showPassword = !this.showPassword;
            },
            openPasswordRecovery(){
                this.$emit('onPasswordRecoveryClick');
            }
        }
    }
</script>

<style lang="scss">
.shop-text-input__message--persistent{
    display: block;
    margin-top: 3px;
    line-height: 1.4;
}
.shop-text-input__show-password-toggle{
    position: absolute;
    top: 50%;
    right: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    width: 48px;
    height: 48px;
    padding: 0;
    background: transparent;
    background-image: url("data:image/svg+xml,%3Csvg width='16' height='16' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M15.898 7.688C15.755 7.493 12.35 2.9 8 2.9S.244 7.493.102 7.688a.528.528 0 000 .622C.244 8.506 3.65 13.098 8 13.098s7.755-4.592 7.898-4.787a.527.527 0 000-.623zM8 12.043c-3.204 0-5.98-3.048-6.8-4.044.82-.997 3.589-4.044 6.8-4.044 3.204 0 5.979 3.048 6.8 4.045-.82.997-3.59 4.043-6.8 4.043z' fill='%237E7E7E'/%3E%3Cpath d='M8 4.836A3.168 3.168 0 004.835 8 3.168 3.168 0 008 11.166 3.168 3.168 0 0011.165 8 3.168 3.168 0 008 4.836zm0 5.275A2.112 2.112 0 015.89 8c0-1.164.946-2.11 2.11-2.11 1.163 0 2.11.946 2.11 2.11 0 1.163-.947 2.11-2.11 2.11z' fill='%237E7E7E'/%3E%3C/svg%3E");
    background-repeat: no-repeat;
    background-position: center;
    cursor: pointer;
    transform: translateY(-50%);
}
.shop-text-input__input[type="text"] ~ .shop-text-input__show-password-toggler { 
        background-image: url("data:image/svg+xml,%3Csvg width='16' height='16' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M15.898 7.688C15.755 7.493 12.35 2.9 8 2.9S.244 7.493.102 7.688a.528.528 0 000 .622C.244 8.506 3.65 13.098 8 13.098s7.755-4.592 7.898-4.787a.527.527 0 000-.623zM8 12.043c-3.204 0-5.98-3.048-6.8-4.044.82-.997 3.589-4.044 6.8-4.044 3.204 0 5.979 3.048 6.8 4.045-.82.997-3.59 4.043-6.8 4.043z' fill='%237E7E7E'/%3E%3Cpath d='M8 4.836A3.168 3.168 0 004.835 8 3.168 3.168 0 008 11.166 3.168 3.168 0 0011.165 8 3.168 3.168 0 008 4.836zm0 5.275A2.112 2.112 0 015.89 8c0-1.164.946-2.11 2.11-2.11 1.163 0 2.11.946 2.11 2.11 0 1.163-.947 2.11-2.11 2.11z' fill='%237E7E7E'/%3E%3Crect x='1.297' y='1.831' width='1.625' height='18.147' rx='.813' transform='rotate(-42.255 1.297 1.83)' fill='%237E7E7E' stroke='%23fff' stroke-width='.5'/%3E%3C/svg%3E"); 
        background-repeat: no-repeat;
    }
.shop-text-input__password-recovery-btn{
    padding: 0;
    font-size: 11px;
    line-height: 1.4;
    color: $blue;
    text-decoration: none;
    text-align: left;
    background-color: transparent;
    cursor: pointer;
}
</style>