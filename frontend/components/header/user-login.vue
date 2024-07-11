<template>
  <div>
    <div>
      <button v-if="!isDesktop" class="hederTool" @click="openSignIn">
        <div>
          <svg
            width="24"
            height="24"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fill-rule="evenodd"
              clip-rule="evenodd"
              d="M7 7c0-2.763 2.238-5 5-5 2.763 0 5 2.237 5 5 0 2.762-2.237 5-5 5-2.762 0-5-2.238-5-5zm5 8.382c3.713 0 7.903 2.068 7.903 2.868v1.687H4.132v-1.93c0-.8 4.155-2.625 7.868-2.625zm0-2.132c-3.338 0-10 1.675-10 5v2.5c0 .687.563 1.25 1.25 1.25h17.5c.688 0 1.25-.563 1.25-1.25v-2.5c0-3.325-6.662-5-10-5zm0-3.481a2.778 2.778 0 100-5.555 2.778 2.778 0 000 5.555z"
              fill="#d3a272"
            ></path>
          </svg>
        </div>
        <!-- <div class="header-tool__text">
          {{ isAuthorised ? email : "Войти" }}
        </div> -->
      </button>
      <button v-if="isDesktop" class="HeaderTool" @click="openDropDown">
        <div>
          <svg
            width="33"
            height="32"
            viewBox="0 0 33 32"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fill-rule="evenodd"
              clip-rule="evenodd"
              d="M16.5151 3C13.0163 3 10.1826 5.83379 10.1826 9.3325C10.1826 12.8312 13.0163 15.665 16.5151 15.665C20.0138 15.665 22.8476 12.8312 22.8476 9.3325C22.8476 5.83379 20.0138 3 16.5151 3ZM19.8398 9.33222C19.8398 7.49579 18.3516 6.00765 16.5152 6.00765C14.6788 6.00765 13.1907 7.49579 13.1907 9.33222C13.1907 11.1686 14.6788 12.6568 16.5152 12.6568C18.3516 12.6568 19.8398 11.1686 19.8398 9.33222ZM26.1722 23.5803C26.1722 22.5671 21.217 20.2557 16.5151 20.2557C11.8133 20.2557 6.85807 22.5671 6.85807 23.5803V25.3217H26.1722V23.5803ZM3.8501 23.5805C3.8501 19.3694 12.2882 17.248 16.5151 17.248C20.742 17.248 29.1801 19.3694 29.1801 23.5805V26.7468C29.1801 27.6175 28.4677 28.3299 27.597 28.3299H5.43322C4.5625 28.3299 3.8501 27.6175 3.8501 26.7468V23.5805Z"
              fill="#282828"
            />
          </svg>
        </div>
        <div class="header-tool__text">войти</div>
      </button>
    </div>
    <HeaderDropDown v-if="isDropDownOpen" @on-closed="closeDropDown">
      <LoginMethods />
    </HeaderDropDown>
  </div>
</template>

<script>
import LoginMethods from "@/components/header/login-methods";
import { ModalBus } from "@/event-bus/modal-bus";
const HeaderDropDown = () => import("@/components/common/header-dropdown");
const SignIn = () => import("@/components/header/sign-in");
// import { mapGetters } from "vuex";

export default {
  components: {
    HeaderDropDown,
    LoginMethods,
  },

  props: {
    isDesktop: Boolean,
  },
  data() {
    return {
      isDropDownOpen: false,
    };
  },
  methods: {
    openDropDown() {
      this.isDropDownOpen = true;
    },
    openSignIn() {
      ModalBus.$emit("open", {
        component: SignIn,
      });
    },
    closeDropDown() {
      this.isDropDownOpen = false;
    },
  },
  // computed: {
  //   ...mapGetters({
  //     email: "user/getEmail",
  //   }),
  // },
};
</script>
