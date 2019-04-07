import Vue from 'vue'
import Vuets from '../Vuets'

import template from './main_vue.ts.html'
import './main_vue.ts.css'

Vue.use(Vuets);

new Vue({
    el: '#app',
    template
});
