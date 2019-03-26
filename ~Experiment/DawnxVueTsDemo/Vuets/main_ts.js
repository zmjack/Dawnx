Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var AppHello_1 = require("./components/AppHello");
new vue_1.default({
    el: "#app_ts",
    template: '<Hello text="Vue!" />',
    components: { Hello: AppHello_1.default }
});
//# sourceMappingURL=main_ts.js.map