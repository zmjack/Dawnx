Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var App_1 = require("./App");
new vue_1.default({
    el: '#app',
    render: function (h) { return h(App_1.default); },
    components: {
        App: App_1.default
    },
    mounted: function () {
        alert(App_1.default.data);
    }
});
//# sourceMappingURL=main.js.map