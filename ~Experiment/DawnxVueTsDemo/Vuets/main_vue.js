Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var AppHello_vue_1 = require("./components/AppHello.vue");
new vue_1.default({
    el: "#app_vue",
    template: '<Hello text="Vue!"/>',
    components: { Hello: AppHello_vue_1.default }
});
//# sourceMappingURL=main_vue.js.map