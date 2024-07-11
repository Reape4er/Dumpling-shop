<template>
    <nav class="navigation">
      <ul>
        <li><nuxt-link to="/recipes">Рецепты</nuxt-link></li>
        <li><nuxt-link to="/products">Каталог продуктов</nuxt-link></li>
        <li><nuxt-link to="/cart">Корзина</nuxt-link></li>
      </ul>
    </nav>
  </template>
  
  <script>
  export default {
    // Здесь может быть ваш скрипт, если он вам нужен
  }
  </script>
  
  <style>
  .navigation ul {
    list-style-type: none;
    padding: 0;
    display: flex;
    justify-content: center;
  }
  
  .navigation li {
    margin: 0 10px;
  }
  
  .navigation li a {
    text-decoration: none;
    color: #d3a272;
    font-weight: bold;
  }
  
  /* Добавьте любые дополнительные стили, которые соответствуют вашему дизайну */
  </style>