import axios from "axios";

const baseURL = "http://127.0.0.1:5003";
export default axios.create({ baseURL });
