<template>
    <div class="header-dropdown">
        <div class="header-dropdown__dialog">
            <slot></slot>
        </div>
        <div class="header-dropdown__overlay" @click="close">
            
        </div>
    </div>
</template>

<script>
export default {
    created(){
        document.addEventListener('keyup',this.onEscKeyUp);
    },
    methods: {
        onEscKeyUp(event){
            if (event.key === 'Esc'){
                this.close();
            }
        },
        
        close(){
            this.$emit('on-closed');
        }
    },
    beforeDestroy(){
        document.removeEventListener('keyup',this.onEscKeyUp);
    }
}
</script>

<style>
.header-dropdown{
    margin-top: 5px;
    margin-right: 5px;
    position: absolute;
    right: 0;
    padding: 3px;
    border: 1px solid black;
    background-color: gainsboro;
    border-radius: 3px;
}
</style>