<template>
    <client-only>
        <div>
            <form class="search-bar" @submit.prevent="submitHandle">
                <vue-autosuggest
                    ref="autocomplete" 
                    v-model="query"
                    :suggestions = suggestions
                    :input-props="{id:'autosuggest__input', placeholder:'Поиск по товарам', class:'search-bar__input', name:'Search-query'}"
                    :getSuggestionValue = "getSuggestionValue"
                    @selected = "onSelected"
                    @opened =  "isExpanded = true"
                    @closed = "isExpanded = false"
                    @input = "fetchResults"
                >
                </vue-autosuggest> 
                <button>
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="24"
                        height="24"
                        viewBox="0 0 24 24"
                        fill="none"
                    >
                    <path
                        fill="#007AFF"
                        fill-rule="evenodd"
                        d="M16.255 15.295h1.054l5.656 5.68c.547.547.547 1.44 0 1.987a1.41 1.41 0 01-1.988 0l-5.669-5.667v-1.053l-.36-.373a8.673 8.673 0 01-7.123 1.973c-3.708-.627-6.67-3.72-7.123-7.453C.01 4.749 4.757.002 10.4.695c3.735.454 6.83 3.414 7.457 7.12a8.665 8.665 0 01-1.974 7.12l.373.36zm-12.953-6c0 3.32 2.68 6 6 6s6-2.68 6-6-2.68-6-6-6-6 2.68-6 6z"
                        clip-rule="evenodd"
                    />
                    </svg>
                </button>
            </form>
        </div>
    </client-only>
</template>

<script>
import { VueAutosuggest } from 'vue-autosuggest';
import debounce from 'debounce';

const DEBOUNCE_TIME = 500;
const MIN_REQUEST_LENGTH = 3;

export default {
  components: {
      VueAutosuggest
  },
  data() {
    return {
        query:"",
        suggestions: [],
        isExpanded: false,
        selected: null
    };
  },
  methods:{
    getSuggestionValue(suggestion) {
        return suggestion.item.name;
    },

    onSelected(suggestion){
        this.selected = suggestion
            ? suggstion.item
            : this.$refs.autocomplete.internalValue; 
        this.goToSearch;
    },
    
    goToSearch(){
        this.query = "";
        const normalizedSelected = this.selected.replace(/\//g, '').replace(/%/g, '');
        const encodedSuggestion = encodeURI(normalizedSelected).toLowerCase();
        const URL = `/${SEARCH_SPACE_NAME}/${encodedSuggestion}`;
        return this.$router.push();
    },

    fetchResults(){
        if (this.query.length < MIN_REQUEST_LENGTH){
            return;
        };
        this.debounceGetSuggestion();
    },

    async GetSuggestion(){
        if (this.query.length < MIN_REQUEST_LENGTH){
            return;
        };
    },

    debounceGetSuggestion: debounce(function(){
        this.GetSuggestion;        
    }, DEBOUNCE_TIME),
    
    submitHandle(){
        if (this.query.length === 0){
            return;
        };
        this.selected = this.query;
        this.goToSearch;
    }
    
  }
};

</script>
<style>
.search-bar{
    display: flex;
    flex-direction: row;   
}
</style>