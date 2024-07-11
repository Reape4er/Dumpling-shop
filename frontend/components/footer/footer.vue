<template>
    <footer class = 'footer'>
        <div class = 'footer-wrapper'>
            <div class = 'footer__copyright'>
                {{ copyrightText }}
            </div>
        </div>        
    </footer>
</template>

<script>
    export default {
        computed: {
            copyrightText(){
                const currentYear = new Date().getFullYear();
                return `© 2023 - ${currentYear}, Дом Пельменей. Все права защищены.`; 
            }
        }
    }
</script>

<style lang="scss">
.footer {
    position: fixed;
    left: 0;
    bottom: 0;
    width: 100%;
    background: linear-gradient(white, blue, red);
    text-align: center;
}
</style>