import baseAuthRepositories from "./base-auth-repositories/base-auth-repositories";

const resources = "Authentication";

export default {
  async authenticateUser(userData) {
    try{
      console.log(userData)
      let route = `${resources}`;
      return await baseAuthRepositories.post(route, userData, {
        headers: {
          "Content-Type": "application/json",
        },
      })
    }
    catch(e){
      console.error(e)
    }
  },
}