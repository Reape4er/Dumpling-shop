import axios from "axios";

const baseURL = "http://localhost:5003";
export default axios.create({ baseURL });
