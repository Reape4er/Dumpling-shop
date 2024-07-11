
<template>
    <nuxt-link class="header-logo" to="/">
        <picture>
            <source
                media="(min-width: 1025px)"
                srcset="/images/logo/horizontal-logo.png"
                sizes="(min-width: 1025px) 90px, 100vw"
            />
            <source
                media="(min-width: 768px)"
                srcset="/images/logo/mobile-logo.png"
                sizes="(min-width: 768px) 90px, 100vw"
            />
            <img
                class="header-logo__image"
                src="/images/logo/mobile-logo.png"
                alt="LOGO"
                
            />
        </picture>
    </nuxt-link>
</template>

<style lang="scss" scoped>
    .header-logo {
        display: flex; 
        justify-content: left;
        padding-top: 5px;
        img {
            width: 50px; // Ширина для мобильных устройств
        }
        @media (min-width: $pad) {
            padding-top: 0px;
            img {
                width: 50px; // Ширина для мобильных устройств
            }
        }
        @media (min-width: $desktop-mid) {
            padding-top: 0px;
            img {
                width: 250px; // Ширина для десктопных экранов
            }
        }
        .header-logo__image {
            aspect-ratio: auto;
        }
    }
</style>