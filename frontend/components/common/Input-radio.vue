<template>
    <label class="input-radio">
        <input type="radio" v-model="radioModel" :value="valueRadio" :name="nameradio" required @blur="v.$touch()"/>
        <span class="inputRadio__item"></span>
        <span class="inputRadio__text">
            <slot></slot>
        </span>

    </label>
</template>

<script>
export default{
    props:{
        v:{
            type: Object,
            required: true,
        },
        required:{
            type: Boolean,
            default: false
        },
        valueRadio:{
        },
        nameradio:{
            type: String,
            default: ''
        }
    },
    computed:{
        radioModel:{
            get(){
                return this.value;
            },
            set(value){
                this.v.$touch();
                this.$emit('input', value);
            }
        }
    }
}
</script>