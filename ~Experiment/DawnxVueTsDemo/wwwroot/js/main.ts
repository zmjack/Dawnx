import Vue from "vue";
import Hello from "./App.vue";

new Vue({
    el: "#app",
    template: '<Hello/>',
    components: { Hello }

    //methods() {

    //},
    //render: function (h) {
    //    return h('div', this.name);
    //}
});
