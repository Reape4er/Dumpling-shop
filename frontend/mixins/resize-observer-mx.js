// import { ResizeObserver } from "@juggle/resize-observer";
// import { ScreenSize } from "@/assets/js/const";

// export default {
//   mounted() {
//     this.resizeObserver = new ResizeObserver((entries) => {
//       const clientWidth = entries[0].target.clientWidth;
//       const isDesktop = clientWidth > ScreenSize.DESKTOP - ScreenSize.SCROLL;
//       this.$store.commit("tech/updateIsDesktop", isDesktop);
//       const isTablet = clientWidth < ScreenSize.TABLET - ScreenSize.SCROLL;
//       this.$store.commit("tech/updateIsTablet", isTablet);
//     });
//     if (process.client) {
//       this.resizeObserver.observe(document.body);
//     } else {
//       this.$store.commit("tech/updateIsDesktop", true);
//       this.$store.commit("tech/updateIsTablet", false);
//     }
//   },
//   beforeDestroy() {
//     if (this.resizeObserver) {
//       this.resizeObserver.disconnect();
//     }
//   },
//   data() {
//     return {
//       resizeObserver: null,
//     };
//   },
// };
