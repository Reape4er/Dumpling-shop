import UserLocalStorage from "@/utils/service.localStorage";
import { UserAuthorize } from '@/assets/js/const.localStorage.js';
import { repository_factory } from "@/API/repositories-factory.js"

const usersRepositoryFactory = repository_factory.get('users')

const USER_ID = UserAuthorize.USER_ID;
const TOKEN = UserAuthorize.TOKEN;
 
export const state = () => {
	return {
		user: null,
		userId: 0,
		token: null,
		userIsFetchingNow: false
	}
};

export const getters = {
	user: state => state.user,
	userId: state => state.userId,
	token: state => state.token
};

export const mutations = {
	setUser(state, user) {
		const newUserObj = Object.assign({}, user);
		state.user = newUserObj;
	},
	removeUser(state) {
		state.user = null;
	},
	setUserId(state, userId) {
		UserLocalStorage.SET_USERID(userId);
		state.userId = userId;
		console.log(userId)
	},
	removeUserId(state) {
		UserLocalStorage.REMOVE_USERID();
		state.userId = 0;
	},
	setToken(state, token) {
		UserLocalStorage.SET_TOKEN(token);
		state.token = token;
		console.log(token)
	},
	removeToken(state) {
		UserLocalStorage.REMOVE_TOKEN();
		state.token = null;
	},
	startFetchingUser(state) {
		state.userIsFetchingNow = true;
	},
	releaseFetchingUser(state) {
		state.userIsFetchingNow = false;
	}
};

export const actions = {
	async fetchUserData({state, commit}) {
		console.log("старт получения пользователя")
		const userId = UserLocalStorage.GET_USERID() ? UserLocalStorage.GET_USERID() : 0;
		const token = UserLocalStorage.GET_TOKEN ? UserLocalStorage.GET_TOKEN() : null;
		if(process.server || state.userIsFetchingNow || userId == 0) {
			return;
		}
		commit('startFetchingUser');
		try {
			const response = await usersRepositoryFactory.getUser(userId, token)
			console.log(response.data)
			if (
				response?.status == "200"
			) {
				console.log(response.data)
				commit('setToken', token)
				commit('setUserId', response.data.id)
				return commit('setUser', response.data);
			}

			if(!response?.status == "200") {
				return commit('removeUser');
			}
		} catch (err) {
			if(!err.response) {
				throw err;
			}
		} finally {
			commit('releaseFetchingUser');
		}
	},

	fetchUserAuthorize({commit, state}) {
		if (localStorage.hasOwnProperty(USER_ID) && localStorage.hasOwnProperty(TOKEN)) {
			try {
				commit('setUserId', UserLocalStorage.GET_USERID());
				commit('setToken', UserLocalStorage.GET_TOKEN());
			}
			catch(e) {
				console.log(e);
				UserLocalStorage.REMOVE_USERID();
				UserLocalStorage.REMOVE_TOKEN();
			}
		}
	}
}