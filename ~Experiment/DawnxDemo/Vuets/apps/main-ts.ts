import Vue from 'vue'
import Vts from '..'

import template from './main-ts.ts.html'
import './main-ts.ts.css'

Vue.use(Vts);

new Vue({
    el: '#app',
    template,
    data() {
        return { vuets: new Vue() }
    }
});
