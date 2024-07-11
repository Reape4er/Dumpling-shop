export default ({app}, inject) => {
    const Disable = ()=>{
        document.body.classList.add('overflow-hidden');
    };

    const Enable = ()=>{
        document.body.classList.remove('overflow-hidden');
    };

    inject('disableBodyScroll', Disable);
    inject('enableBodyScroll', Enable);

}