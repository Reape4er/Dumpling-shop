<template>
  <div class="shop-text-input">
    <div
      class="shop-text-input__control"
      :class="{ 'shop-text-input__control--error': this.v.$error }"
    >
      <div class="shop-text-input__slot">
        <div class="shop-text-input__field">
          <input
            class="shop-text-input__input"
            :class="{
              'err-form': v.$error || isFieldNotDirty,
              'val-form': !v.$invalid,
            }"
            type="text"
            :disabled="disabled"
            required
            v-model.lazy="text"
            :id="inputId"
            @blur="v.$touch()"
          />

          <label
            class="shop-text-input__label"
            :class="{ 'shop-text-input__label--focus': text }"
            :for="inputId"
          >
            {{ inputText }}

            <span v-if="required">*</span>
          </label>
        </div>
      </div>
      <div class="shop-text-input__messages">
        <div class="shop-text-input__message">
          {{ errorMessage }}
        </div>
        <slot name="action"> </slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    value: {
      type: String,
      default: "",
    },
    v: {
      type: Object,
      required: true,
    },
    required: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    isFieldNotDirty: {
      type: Boolean,
      default: false,
    },
    inputText: {
      type: String,
      default: "",
    },
    errorMessage: {
      type: String,
      default: "",
    },
    inputId: {
      type: String,
      default: "",
    },
  },
  computed: {
    text: {
      get() {
        return this.value;
      },
      set(value) {
        console.log(value);
        this.v.$touch();
        this.$emit("input", value);
      },
    },
  },
};
</script>
