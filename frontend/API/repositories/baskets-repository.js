import baseBasketsRepositories from "./base-baskets-repositories/base-baskets-repositories";

const resources = "api/basket";

export default {

  async UpdateItemInBasket(userId, productData) {
    try{
      let route = `${resources}/${userId}`;
      return await baseBasketsRepositories.patch(route, productData, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async getBasket(userId) {
    try{
      let route = `${resources}/${userId}`;
      return await baseBasketsRepositories.get(route, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }
  },
  async clearBasket(userId) {
    try{
      let route = `${resources}`;
      return await baseBasketsRepositories.delete(route, {
        params:{
          userId:userId
        },
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }
  }
};
