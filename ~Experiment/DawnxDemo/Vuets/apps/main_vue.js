Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var Vuets_1 = require("../Vuets");
var main_vue_ts_html_1 = require("./main_vue.ts.html");
require("./main_vue.ts.css");
vue_1.default.use(Vuets_1.default);
new vue_1.default({
    el: '#app',
    template: main_vue_ts_html_1.default
});
//# sourceMappingURL=main_vue.js.map