<template>
    <Transition name="modalWindow">
        <div v-if="!!component" class="modal" :class="classes">
            <div class="modal__dialog">
                <div @click="close">
                    <ButtonClose class="modal__close-btn" />
                </div>
                <component
                :is="component"
                :componentData="componentData"
                @close="close"
                class="modal__body" /> 
            </div>
        </div>
    </Transition>
</template>

<script>
import { ModalBus } from "@/event-bus/modal-bus";
import ButtonClose from "@/components/common/button-close.vue";

export default {
    created(){
        ModalBus.$on('open', this.open);
        ModalBus.$on('close', this.close);
    },
    components:{
        ButtonClose
    },

    data(){
        return{
            component: null,
            classes: '',
            title: null,
            componentData: {}
        };
    },

    methods:{
        open({component, classes='', title='', componentData=''}){
            this.component = component;
            this.classes = classes;
            this.title = title;
            this.componentData = componentData;
            this.$disableBodyScroll();
            document.addEventListener('keyup',this.onEscKeyUp);
        },

        close(){
            this.$enableBodyScroll();
            document.removeEventListener('keyup',this.onEscKeyUp);
            this.component = null;
        },

        onEscKeyUp(event){
            if (event.key === 'Esc'){
                this.close();
            };
        },
    }
}
</script>

<style lang="scss">
    .modal{
        position: fixed;
        top: 0;
        width: 100vw;
        height: 100vh;
        left: 0;
        //right: 0;
        background-color: rgba(40,40,40, 0.6);
        z-index: 100000;
        display: flex;
        justify-content: center;
        align-items: center;
        .modal__dialog {
            width: fit-content;
            min-width: 320px;
            flex-direction: column;
            box-sizing: border-box;
            padding: 16px;
            background-color: $white;
            @media(min-width: $pad){
                position: relative;
                max-width: 387px;
                margin: 0 15px;
                padding: 14px 14px 24px 24px;
                border-radius: 4px;
                box-shadow: 0 0 15px rgba(0,0,0,0.25);
            }
        }
        .modal__close-btn {
            position: absolute;
            top: 0;
            right: 0;
            z-index: 1;
        }
    }
</style>