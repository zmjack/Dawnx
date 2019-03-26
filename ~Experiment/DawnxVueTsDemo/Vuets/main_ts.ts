import Vue from "vue";
import Hello from "./components/AppHello";

new Vue({
    el: "#app_ts",
    template: '<Hello text="Vue!" />',
    components: { Hello }
});
