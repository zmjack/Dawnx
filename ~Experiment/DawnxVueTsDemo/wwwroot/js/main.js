Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var App_vue_1 = require("./App.vue");
new vue_1.default({
    el: "#app",
    template: '<Hello/>',
    components: { Hello: App_vue_1.default }
    //methods() {
    //},
    //render: function (h) {
    //    return h('div', this.name);
    //}
});
//# sourceMappingURL=main.js.map