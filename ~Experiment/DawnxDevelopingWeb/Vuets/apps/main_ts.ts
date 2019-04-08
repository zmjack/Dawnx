import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/antd.css'

import Vue from 'vue'
import Vuets from '../Vuets'

import template from './main_ts.ts.html'
import './main_ts.ts.css'

Vue.use(Antd);
Vue.use(Vuets);

new Vue({
    el: '#app',
    template
});
