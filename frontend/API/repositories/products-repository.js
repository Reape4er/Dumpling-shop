import baseProductsRepositories from "./base-products-repositories/base-products-repositories";

const resources = "products";

export default {

  async postProduct(productData) {
    try{
      let route = `${resources}`;
      return await baseProductsRepositories.post(route, productData, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async getProduct(id) {
    try{
      let route = `${resources}/${id}`;
      return await baseProductsRepositories.get(route, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async getProducts() {
    try{
      let route = `${resources}`;
      return await baseProductsRepositories.get(route, {
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async getProductsByName(nameFragment) {
    try{
      let route = `${resources}/Name`;
      return await baseProductsRepositories.get(route, {
        params:{
          nameFragment: nameFragment
        },
        headers: {
          "Content-Type": "application/json",
        },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async putProduct(id, productData) {
    try{
      let route = `${resources}`;
      return await baseProductsRepositories.put(route, productData, {
          params:{
              id: id
          },
          headers: {
              "Content-Type": "application/json",
          },
      });
    }
    catch(e){
      console.error(e)
    }

  },

  async deleteProduct(id) {
    try{
      let route = `${resources}`;
      return await baseProductsRepositories.delete(route, {
        params:{
          id: id
        },
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
