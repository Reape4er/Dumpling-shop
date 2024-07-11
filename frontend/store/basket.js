import { repository_factory } from "@/API/repositories-factory.js"

const basketsRepositoryFactory = repository_factory.get('baskets')

export const state = () => ({
    basket: [],
});

export const mutations = {
    CLEAR_BASKET(state){
        state.basket = []
    },
    SET_BASKET(state, basketItems) {
        state.basket = basketItems;
    },
    
    ADD_TO_BASKET(state, product) {
        const existingItem = state.basket.find(item => item.id === product.id);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            const newItem = {
                itemId: product.id,
                quantity: 1
            };
            state.basket.push(newItem);
        }
    },

    INCREMENT_QUANTITY(state, { itemId }) {
        const item = state.basket.find(basketItem => basketItem.id === itemId);
        if (item) {
            item.quantity++;
        }
    },

    DECREMENT_QUANTITY(state, { itemId }) {
        const item = state.basket.find(basketItem => basketItem.id === itemId);
        if (item && item.quantity > 0) {
            item.quantity--;
        }
    },
};

export const actions = {
    async fetchBasket({ commit }, userId) {
        try {
            const response = await basketsRepositoryFactory.getBasket(userId)
            commit('SET_BASKET', response.data);
        } catch (error) {
            console.error('Ошибка при получении данных корзины:', error);
        }
    },

    async addItemInBasket({ commit, state }, { userId, itemId }) {
        const item = state.basket.find(basketItem => basketItem.id === itemId);
        if (!item) {
            try {
            await basketsRepositoryFactory.UpdateItemInBasket(userId, {
                    id: itemId,
                    quantity: 1
                }
            )
            commit('ADD_TO_BASKET', {
                id: itemId,
                quantity: 1
            });
            } catch (error) {
                console.error('Ошибка при добавлении товара:', error);
            }
        }
    },

    // async incrementQuantity({ commit, state }, { userId, itemId }) {
    //     const item = state.basket.find(basketItem => basketItem.id === itemId);

    //     if (item) {
    //         try {
    //             await basketsRepositoryFactory.UpdateItemInBasket(userId, {
    //                 id: itemId,
    //                 quantity: item.quantity + 1
    //             }
    //         )
    //         commit('INCREMENT_QUANTITY', { itemId });
    //         } catch (error) {
    //         console.error('Ошибка при увеличении количества товара:', error);
    //         }
    //     }
    // },

    // async decrementQuantity({ commit, state }, { userId, itemId }) {
    //     const item = state.basket.find(basketItem => basketItem.id === itemId);

    //     if (item && item.quantity > 0) {
    //         try {
    //             await basketsRepositoryFactory.UpdateItemInBasket(userId, {
    //                 id: itemId,
    //                 quantity: item.quantity - 1
    //             }
    //         )
    //         commit('DECREMENT_QUANTITY', { itemId });
    //         } catch (error) {
    //         console.error('Ошибка при уменьшении количества товара:', error);
    //         }
    //     }
    // },
};

export const getters = {
    basketItems: (state) => {
        return state.basket;
    },
    basketTotal: (state) => {
        return state.basket.reduce((total, item) => total + item.price * item.quantity, 0);
    },
    // getItem: (state) => (itemId) => {
    //     return state.basket.find(item => item.id == itemId);
    // },
};
