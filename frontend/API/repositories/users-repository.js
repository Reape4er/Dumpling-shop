import baseUsersRepositories from "./base-users-repositories/base-users-repositories";

const resources = "users";

export default {

  async createUser(userData) {
    try{
      let route = `${resources}`;
      return await baseUsersRepositories.post(route, userData, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },

  async getUser(id, token) {
    try{
      let route = `${resources}/${id}`;
      return await baseUsersRepositories.get(route, {
        // params: {
        //   id: id
        // },
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + token
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },

  async getAllUser() {
    try{
      let route = `${resources}`;
      return await baseUsersRepositories.get(route, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },

  async putUser(id, token, userData) {
    try{
      let route = `${resources}`;
      return await baseUsersRepositories.put(route, userData, {
        params: {
          id: id
        },
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + token
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },

  async deleteUser(id) {
    try{
      let route = `${resources}`;
      return await baseUsersRepositories.delete(route, {
        params:{
          id: id
        },
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + token
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },
};
