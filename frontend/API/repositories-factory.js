import usersRepository from "./repositories/users-repository";
import productsRepository from "./repositories/products-repository";
import basketsRepository from "./repositories/baskets-repository";
import authRepository from "./repositories/auth-repository";
import ordersRepository from "./repositories/orders-repository";

const repositories = { 
    users: usersRepository,
    products: productsRepository,
    baskets: basketsRepository,
    auth: authRepository,
    orders: ordersRepository
};
export const repository_factory = { get: (name) => repositories[name] };
