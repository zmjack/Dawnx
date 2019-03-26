import Vue from "vue";
import Hello from "./components/AppHello.vue";

new Vue({
    el: "#app_vue",
    template: '<Hello text="Vue!"/>',
    components: { Hello }
});
