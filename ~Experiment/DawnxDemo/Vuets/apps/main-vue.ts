import Vue from 'vue'
import Vts from '..'

import template from './main-vue.ts.html'
import './main-vue.ts.css'

Vue.use(Vts);

new Vue({
    el: '#app',
    template,
});
