<template>
    <div class="shop-text-input">
        <div class="shop-text-input__control"
        :class="{'shop-text-input__control--error':this.v.$error}"
        >
            <div class="shop-text-input__slot">
                <div class="shop-text-input__field">
                    <input class="shop-text-input__input"
                    :class="{'err-form':v.$error || isFieldNotDirty, 'val-form':!v.$invalid && email}"
                    type="email"
                    :disabled="disabled"
                    v-model.lazy="email"
                    :id="idForEmail"
                    @blur="v.$touch()"
                    />

                    <label class="shop-text-input__label"
                    :class="{'shop-text-input__label--focus':email}"
                    :for="idForEmail">
                    электронная почта
                    <span v-if="required">*</span>
                    </label>
                </div>
            </div>
            <div class="shop-text-input__messages">
                <div class="shop-text-input__message">
                    введите электронную почту
                </div>
                <slot name="action">
                    
                </slot>
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
            },
            required: {
                type: Boolean,
                default: false
            },
            disabled:{
                type: Boolean,
                default: false
            },
            isFieldNotDirty:{
                type: Boolean,
                default: false
            }
                
        },
        computed:{
            email:{
                get(){
                    return this.value;
                },
                set(value){
                    this.v.$touch();
                    this.$emit('input', value);
                }},
            idForEmail(){
                return `${getRandomInt()}-email`
            }
        }

    }
</script>

<style lang="scss">
.shop-text-input__field{
    position: relative;
}
.shop-text-input__input{
    box-sizing: border-box;
    flex: 1 1 auto;
    padding: 14px 12px;
    max-width: 100%;
    min-width: 0;
    width: 100%;
    font-family: $main-font-family;
    color: $text_gray;
    font-size: large;
    line-height: 18px;
    border-radius: 4px;
    border: 1px  solid;
    border-color: $gray-default;
    background-color: $white;
    outline: none;
    &:focus{border-color: $blue;}
}
.shop-text-input__label{
    position: absolute;
    top: 12px;
    left: 12px;
    font-size: 13px;
    transform-origin: top left;
    transition: all 0.3s cubic-bezier(0.25, 0.8, 0.5, 1);
    pointer-events: none;
}
.shop-text-input__input:focus ~ .shop-text-input__label,
.shop-text-input__label--focus {
    color: $gray-default;
    transform: translateY(-10px);
    scale: 0.85;
    transition: all 0.3s cubic-bezier(0.25, 0.8, 0.5, 1) 0s;
}
.shop-text-input__messages{
    min-height: 18px;
    display: flex;
}
.shop-text-input__message{
    display: none;
    margin-top: 4px;
    margin-left: 15px;
    font-size: 12px;
    line-height: 1.4;
    color: $orange;
}
.shop-text-input__control--error{
    .shop-text-input__message{
        display: block;
    }
    .shop-text-input__field{
        .shop-text-input__input{
            border-color: $orange;
        }
        .shop-text-input__label{
            color: $orange;
        }
    }
}
.err-form{
    color:$orange;
}
.val-form{
    color:$green;
}
</style>