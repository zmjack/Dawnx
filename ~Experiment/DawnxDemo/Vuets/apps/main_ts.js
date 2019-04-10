Object.defineProperty(exports, "__esModule", { value: true });
var ant_design_vue_1 = require("ant-design-vue");
require("ant-design-vue/dist/antd.css");
var vue_1 = require("vue");
var Vuets_1 = require("../Vuets");
var main_ts_ts_html_1 = require("./main_ts.ts.html");
require("./main_ts.ts.css");
vue_1.default.use(ant_design_vue_1.default);
vue_1.default.use(Vuets_1.default);
new vue_1.default({
    el: '#app',
    template: main_ts_ts_html_1.default
});
//# sourceMappingURL=main_ts.js.map