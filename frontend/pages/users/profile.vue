<template>
  <v-container>
    <v-card>
      <v-card-title>
        <span class="headline">Профиль пользователя</span>
      </v-card-title>

      <v-card-text>
        <v-form v-model="valid" ref="form">
          <v-text-field
            v-model="user.firstName"
            label="Имя"
            :rules="[v => !!v || 'Это поле обязательно']"
            required
          />
          <v-text-field
            v-model="user.lastName"
            label="Фамилия"
            :rules="[v => !!v || 'Это поле обязательно']"
            required
          />
          <v-text-field
            v-model="user.middleName"
            label="Отчество"
            :rules="[v => !!v || 'Это поле обязательно']"
            required
          />
          <v-text-field
            v-model="user.email"
            label="Email"
            :rules="[v => !!v || 'Это поле обязательно', v => /.+@.+\..+/.test(v) || 'Email должен быть действительным']"
            required
          />
          <!-- <v-text-field
            v-model="user.phone"
            label="Номер телефона"
            :rules="[v => !!v || 'Это поле обязательно']"
            required
          /> -->
          <InputPhone v-model.lazy="user.phone" :v="$v.user.phone" :required="true"
          :errorMessage="`Введите корректный номер телефона`" :countryCode="countryCode" />
          <v-select
            v-model="user.gender"
            :items="genders"
            label="Пол"
            required
          ></v-select>
          <v-text-field
            v-model="user.address"
            label="Адрес доставки"
          ></v-text-field>
          <v-btn @click="saveProfile" :disabled="!valid">Сохранить</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import UserLocalStorage from "@/utils/service.localStorage";
import { repository_factory } from "@/API/repositories-factory.js"
import InputPhone from "@/components/common/input-phone.vue";
import { countryRusCode, phoneRequirements } from "@/assets/js/consts.js";
import {
  required,
  minLength,
  maxLength
} from "vuelidate/lib/validators";

const usersRepositoryFactory = repository_factory.get('users')

export default {
  components:{
    InputPhone
  },
  data() {
    return {
      user: {
        id: 0,
        firstName: '',
        lastName: '',
        middleName: '',
        email: '',
        gender: null,
        address: '',
        role: 'string'
        },
        genders: [
          { text: 'Мужской', value: 1 },
          { text: 'Женский', value: 2 },
          { text: 'Не указан', value: 0 }
        ],
        valid: false,
        countryCode: "RU",
        countryRusCodeList: countryRusCode,
    };
  },
  async created(){
    try{
      const userId = UserLocalStorage.GET_USERID() ? UserLocalStorage.GET_USERID() : 0;
      const token = UserLocalStorage.GET_TOKEN ? UserLocalStorage.GET_TOKEN() : null;
      if (!userId || !token){
        this.$router.push('/')
      }
      const response = await usersRepositoryFactory.getUser(userId, token);
      if (!response){
        this.$router.push('/')
      }
      this.user = response.data;
    }
    catch(e){
      console.log(e);
      this.$router.push('/')
    }
  },
  methods:{
    async saveProfile(){
      try{
        const userId = UserLocalStorage.GET_USERID() ? UserLocalStorage.GET_USERID() : 0;
        const token = UserLocalStorage.GET_TOKEN ? UserLocalStorage.GET_TOKEN() : null;
        await usersRepositoryFactory.putUser(
        userId,
        token,
        {
          ...this.user
        });
        this.$store.dispatch('userData/fetchUserData')
      }
      catch(e){
        console.log(e)
      }
    }
  },
  validations: {
      user:{
        phone: {
        required,
        minLength: minLength(phoneRequirements.MIN_LENGTH),
        maxLength: maxLength(phoneRequirements.MAX_LENGTH),
      },
      }
    }
};
</script>

<style scoped>
.headline {
  font-weight: bold;
}
</style>
