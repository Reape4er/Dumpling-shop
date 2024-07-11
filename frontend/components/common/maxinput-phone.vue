<template>
  <div
    class="shop-tel-input shop-text-input"
    :class="{
      'shop-tel-input--error': v.$error || isFieldNotDirty,
      'val-vti': !v.$invalid && phone,
    }"
  >
    <VueTelInput
      v-bind="telInputProps"
      ref="phoneInputRef"
      v-model="phone"
      :disabled="disabled"
      @focus="phoneInputFocused = true"
      @blur="vueTelInputBlur"
    >
      <template #arrow-icon>
        <div class="shop-tel-input__arrow-icon"></div>
      </template>
    </VueTelInput>

    <label
      class="shop-text-input__label"
      :class="{ 'shop-text-input__label--focus': phone || phoneInputFocused }"
      :for="idForPhone"
    >
      Телефон*
    </label>

    <div class="shop-text-input__messages">
      <div class="shop-text-input__message">
        {{ errorMessage }}
      </div>
    </div>
  </div>
</template>

<script>
import { VueTelInput } from "vue-tel-input";

export default {
  components: {
    VueTelInput,
  },

  data() {
    return {
      telInputProps: {
        autoDefaultCountry: false,
        defaultCountry: this.countryCode,
        mode: "international",
        styleClasses: "shop-text-input__field",

        inputOptions: {
          showDealCode: false,
          required: true,
          id: "",
          styleClasses: "shop-text-input__input",
          placeholder: "",
        },

        dropDownOptions: {
          showDealCodeInSelection: true,
          showFlags: true,
          showDealCodeInList: true,
        },

        validCharactersOnly: true,
        maxLen: 13,
      },

      phoneInputFocused: false,
      idForPhone: "",

      phoneObj: {
        formatted: "",
        valid: false,
        country: undefined,
      },
    };
  },

  created() {
    this.idForPhone = "phone-id";
    this.telInputProps.inputOptions.id = this.idForPhone;
  },

  computed: {
    phone: {
      get() {
        return this.value;
      },

      set(value) {
        this.v.$touch();
        this.$emit("input", value);
      },
    },
  },

  methods: {
    vueTelInputBlur() {
      this.phoneInputFocused = false;
      this.v.$touch();
    },
  },

  props: {
    value: {
      type: String,
      default: "",
    },
    v: {
      type: Object,
      required: true,
    },
    disabled: {
      type: Boolean,
      required: false,
    },
    isFieldNotDirty: {
      type: Boolean,
      required: false,
    },
    errorMessage: {
      type: String,
      default: "",
    },
    countryCode: {
      type: String,
      default: "",
    },
    required: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style lang="scss">
@import "assets/scss/vti-flags.scss";

.vti__flag {
  width: 20px;
  height: 15px;
  margin-right: 5px;
  margin-left: 5px;

  box-shadow: 0 0 1px 0 #888888;
  background-image: url("/images/vue-tel-input/vue-tel-input-flags.png");
  background-repeat: no-repeat;
  background-color: #dbdbdb;
  background-position: 20px 0;
}

.vue-tel-input {
  border-radius: 3px;
  display: flex;
  text-align: left;
  height: 46px;
  border: none;
}

.vue-tel-input.disabled .dropdown,
.vue-tel-input.disabled .selection,
.vue-tel-input.disabled input {
  cursor: no-drop;
}

.vue-tel-input:focus-within {
  box-shadow: none;
  border-color: transparent;
}

.vti__dropdown {
  display: flex;
  flex-direction: column;
  align-content: center;
  justify-content: center;
  position: relative;
  padding: 7px;
  cursor: pointer;

  background-color: #ffffff;
  border: 1px solid #bebebe;
  border-radius: 3px;

  &:focus {
    border-color: $blue;
    outline: 0;
  }
}

.vti__dropdown.show {
  max-height: 300px;
  overflow: scroll;
}

.vti__dropdown.open,
.vti__dropdown:hover {
  background-color: #f3f3f3;
}

.vti__selection {
  display: flex;
  align-items: center;
  justify-content: flex-start;
  width: 84px;

  font-size: 13px;
  color: $text-black;
}

.vti__selection .vti__country-code {
  color: #666666;
}

.vti__dropdown-list {
  position: absolute;
  z-index: 100;
  left: -1px;

  width: 330px;
  max-height: 200px;
  margin: 0;
  padding: 0;

  text-align: left;

  list-style: none;
  background-color: #ffffff;
  border: none;
  border-radius: 3px;
  box-shadow: 0 0 25px rgba(0, 0, 0, 0.25);
  overflow: hidden;
  overflow-y: scroll;

  &::-webkit-scrollbar {
    background-color: #ffffff;
    width: 16px;
    border-radius: 3px;
  }

  /* background of the scrollbar except button or resizer */
  &::-webkit-scrollbar-track {
    background-color: #ffffff;
    border-radius: 3px;
    overflow: hidden;
  }

  &::-webkit-scrollbar-track:hover {
    background-color: #f4f4f4;
  }

  /* scrollbar itself */
  &::-webkit-scrollbar-thumb {
    background-color: #babac0;
    border-radius: 16px;
    border: 5px solid #ffffff;
  }

  &::-webkit-scrollbar-thumb:hover {
    background-color: #a0a0a5;
    border: 4px solid #f4f4f4;
  }

  /* set button(top and bottom of the scrollbar) */
  &::-webkit-scrollbar-button {
    display: none;
  }
}

.vti__dropdown-list.below {
  top: 33px;
}

.vti__dropdown-list.above {
  top: auto;
  bottom: 100%;
}

.vti__dropdown-arrow {
  transform: scaleY(0.5);
  display: inline-block;
  color: #666666;
}

.vti__dropdown-item {
  cursor: pointer;
  padding: 11px 15px;
}

.vti__dropdown-item.highlighted {
  background-color: #f3f3f3;
}

.vti__dropdown-item.last-preferred {
  border-bottom: 1px solid #cacaca;
}

.vti__dropdown-item .vti__flag {
  display: inline-block;
  margin-right: 5px;
}

.vti__input {
  width: 100%;
  outline: none;

  margin-left: 8px;
  padding-left: 14px;

  font-family: $main-font-family;

  border: 1px solid #bebebe;
  border-radius: 3px;

  &:focus {
    border-color: $blue;
  }
}

.shop-tel-input {
  position: relative;

  .shop-text-input__label {
    left: 124px;
  }

  .shop-tel-input__arrow-icon {
    margin-left: auto;
    width: 12px;
    height: 12px;

    background-image: url("data:image/svg+xml,%3Csvg width='12' height='12' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath d='M5.17 9.05a1 1 0 001.66 0l3.32-4.92a1 1 0 00-.83-1.56H2.68a1 1 0 00-.83 1.56l3.32 4.92z' fill='%23717171'/%3E%3C/svg%3E");
    background-repeat: no-repeat;
    background-size: contain;
  }
}

.shop-tel-input--error {
  .vti__input,
  .vti__input:focus {
    border-color: $coral;
  }

  .vti__input::placeholder {
    color: $coral;
  }

  .shop-text-input__message {
    display: block;
  }
}

.val-vti {
  .vti__input,
  .vti__input:focus {
    border-color: $azure4;
  }
}
</style>
