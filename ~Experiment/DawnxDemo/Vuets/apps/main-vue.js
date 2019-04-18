Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var __1 = require("..");
var main_vue_ts_html_1 = require("./main-vue.ts.html");
require("./main-vue.ts.css");
vue_1.default.use(__1.default);
new vue_1.default({
    el: '#app',
    template: main_vue_ts_html_1.default,
    data: function () {
        return { vuets: new vue_1.default() };
    }
});
//# sourceMappingURL=main-vue.js.map