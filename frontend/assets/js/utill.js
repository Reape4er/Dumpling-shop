export const getRandomInt = ()=>{
    const min = 0;
    const max = 10000;
    return Math.floor(Math.random()*max-min)+min
};