import { TableHeaders, UserAuthorize} from '@/assets/js/const.localStorage.js';

const USER_ID = UserAuthorize.USER_ID;
const TOKEN = UserAuthorize.TOKEN;

export default class {
	static GET_TABLE_HEADERS() {
		if (this.IS_PROCESS_CLIENT()) {
			return JSON.parse(localStorage.getItem(TableHeaders))
		}
	}
	static SET_TABLE_HEADERS(value) {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.setItem(TableHeaders, JSON.stringify(value));
		}
	}
	static REMOVE_TABLE_HEADERS() {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.removeItem(TableHeaders);
		}
	}
	static GET_TOKEN() {
		if (this.IS_PROCESS_CLIENT()) {
			return localStorage.getItem(TOKEN);
		}
	}
	static SET_TOKEN(value) {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.setItem(TOKEN, value);
		}
	}
	static REMOVE_TOKEN() {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.removeItem(TOKEN);
		}
	}
	static GET_USERID() {
		if (this.IS_PROCESS_CLIENT()) {
			return localStorage.getItem(USER_ID);
		}
	}
	static SET_USERID(value) {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.setItem(USER_ID, value);
		}
	}
	static REMOVE_USERID() {
		if (this.IS_PROCESS_CLIENT()) {
			localStorage.removeItem(USER_ID);
		}
	}
	static IS_PROCESS_CLIENT() {
		return process.client;
	}
}