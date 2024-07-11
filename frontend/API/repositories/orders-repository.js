import baseOrdersRepositories from "./base-orders-repositories/base-orders-repositories";

const resources = "/api/Orders";

export default {
  async postOrder(orderData) {
    try{
      let route = `${resources}`;
      console.log(orderData)
      return await baseOrdersRepositories.post(route, 
        orderData,
        {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

};
