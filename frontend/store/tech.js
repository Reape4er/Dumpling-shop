export const state = () => ({
  isDesktop: false,
  isTablet: false,
});

export const getters = {
  isDesktop: (state) => state.isDesktop,
  isTablet: (state) => state.isTablet,
};

export const mutations = {
  updateIsDesktop(state, newValue) {
    state.isDesktop = newValue;
  },
  updateIsTablet(state, newValue) {
    state.isTablet = newValue;
  },
};
