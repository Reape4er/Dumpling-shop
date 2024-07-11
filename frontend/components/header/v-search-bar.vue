<template>
    <div class="v-search-bar mb-1">
    <v-autocomplete
        v-model="select"
        :items="items"
        :search-input.sync="search"
        hide-no-data
        hide-selected
        item-text="name"
        item-value="name"
        label="Поиск по товарам"
        placeholder="Введите для поиска"
        return-object
        @change="onSelect"
        color="#d3a272"
    />
    </div>  
</template>

<script>
import debounce from 'debounce';
import { repository_factory } from "@/API/repositories-factory.js";


const productsRepositoryFactory = repository_factory.get('products');
export default {
data: () => ({
    select: null,
    search: null,
    items: []
}),
watch: {
    search(val) {
    // Задержка запроса
    this.debounceSearch(val);
    }
},
created() {
    // Создание функции debounce
    this.debounceSearch = debounce(this.fetchData, 500);
},
methods: {
    onSelect(item) {
    if(item){
        // Логика при выборе элемента из списка
        this.$router.push({ path: `/products/${item.id}` });
    }
    },
    async fetchData(query) {
        try{
        const response = await productsRepositoryFactory.getProductsByName(query);
        this.items = response.data
        console.log(this.items);
        }
        catch(e){
            console.log(e)
        }

    }
}
};
</script>

<style>
.v-search-bar{
    background-color: white;
    border-radius: 5px;
}
</style>