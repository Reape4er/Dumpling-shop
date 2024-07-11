<template>
    <div class="shop-date-input">
        <div class="shop-date-input__control"
        :class="{'shop-text-date__control--error': this.v.$error}">
        <div class="shop-date-input__slot">
            <div class="shop-date-input__field">
                <input  class="shop-date-input__input"
                        type="date"
                        v-model.lazy="dateText"
                        :id="fieldName"
                        @blur="v.$touch()"
                        
                />
                <label class="shop-date-input__label"
                :class="{'shop-dare-input__label--focus': dateText}"
                :for="fieldName"
              >
              {{ textField }}
          </label>
            </div>
        </div>
    </div>  
    </div>
</template>

<script>
export default {
       
    props:{
        value:{
            type: String,
            default: ''
        },
        v: {
            type: Object,
            required: true
            
        },
        fieldName:{
            type: String,
            default: ''
        },
        textField: {
            type: String,
            default: ''
        },
        requierd: {
            type: Boolean,
            default: false
        }

    },

    computed: {
        dateText:{ get(){
                    return this.value;
                    }, 
                set(value){
                    this.v.$touch();
                    this.$emit('input', value);
                    console.log(value); 
                }
        }
    }

}
</script>