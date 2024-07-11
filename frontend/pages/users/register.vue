<template>
  <div class="register">
    <form class="register-form" @submit.prevent="submit">
      <h3>Регистрация</h3>

      <div>
        <InputText v-model="form.f" :v="$v.form.f" :inputText="'Фамилия'" :errorMessage="'введите фамилию'"
          :inputId="'fId'" />
      </div>
      <div>
        <InputText v-model="form.i" :v="$v.form.i" :inputText="'Имя'" :errorMessage="'введите имя'" :inputId="'iId'" />
      </div>
      <div>
        <InputText v-model="form.o" :v="$v.form.o" :inputText="'Отчество'" :errorMessage="'введите отчество'"
          :inputId="'oId'" />
      </div>

      <!-- <InputDate :v="$v.form.date" /> -->

      <div>
        <InputText v-model="form.email" :v="$v.form.email" :inputText="'Email'"
          :errorMessage="'введите действительную почту'" :inputId="'emailId'" />
      </div>
      <div>
        <InputPassword v-model="form.password" :v="$v.form.password" />
      </div>
      <div>
        <InputPassword v-model="form.passwordConfirm" :v="$v.form.passwordConfirm" />
      </div>
      <div class="register__input">
        <h2>укажите свой гендер из двух гендеров</h2>
        <div class="register_input--gender">
          <InputRadio v-for="genderType in genderTypes" :key="genderType.id" v-model="form.gender" :v="$v.form.gender"
            :required="false" :nameradio="nameradio" :valueRadio="genderType.value">
            {{ genderType.text }}
          </InputRadio>
        </div>
      </div>
      <div class="reg-form__phone">
        <InputPhone v-model.lazy="form.phone" :v="$v.form.phone" :required="true"
          :errorMessage="`Введите корректный номер телефона`" :countryCode="countryCode" />
      </div>
      <button>зарегистрироваться</button>
    </form>
  </div>
</template>

<script>
import InputText from "@/components/common/input-text";
import InputPassword from "@/components/common/input-password";
import {
  required,
  email,
  minLength,
  maxLength,
  sameAs,
} from "vuelidate/lib/validators";
import InputRadio from "@/components/common/Input-radio.vue";
import InputPhone from "@/components/common/input-phone.vue";
import InputDate from "@/components/common/input-date.vue";
import { countryRusCode, phoneRequirements } from "@/assets/js/consts.js";
import { req } from "vuelidate/lib/validators/common";
import { repository_factory } from "@/API/repositories-factory.js"

const usersRepositoryFactory = repository_factory.get('users')

export default {
  components: {
    InputPhone,
    InputText,
    InputRadio,
    InputPassword,
    InputDate
  },
  data() {
    return {
      form: {
        f: "",
        i: "",
        o: "",
        date: null,
        email: "",
        password: "",
        passwordConfirm: "",
        gender: 0,
        phone: "",
      },
      genderTypes: [
        { id: 1, text: "Женский", value: 0 },
        { id: 2, text: "Мужской", value: 1 },
        { id: 3, text: "Не выбран", value: 2 },
      ],
      nameradio: "gender",
      countryCode: "RU",
      countryRusCodeList: countryRusCode,
    };
  },
  validations: {
    form: {
      f: {
        required,
      },
      i: {
        required,
      },
      o: {
        required,
      },
      email: {
        required,
        email,
      },
      gender: {},
      date: {},
      password: {
        required,
      },
      passwordConfirm: {
        required,
        sameAsPassword: sameAs("password"),
      },
      phone: {
        required,
        minLength: minLength(phoneRequirements.MIN_LENGTH),
        maxLength: maxLength(phoneRequirements.MAX_LENGTH),
      },
    },
  },
  methods: {
    async submit() {
      if (this.$v.form.$pending || this.$v.form.$error) {
        return;
      }
      const userData = {
        FirstName: this.form.i,
        LastName: this.form.f,
        MiddleName: this.form.o,
        Email: this.form.email,
        Password: this.form.password,
        Phone: this.form.phone,
        gender: this.form.gender,
        role: "pepe"
      }
      await usersRepositoryFactory.createUser(userData)
      console.log(this.form);
    },
  },
};
</script>

<style lang="scss" scoped>
.register {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: left;
}
</style>
