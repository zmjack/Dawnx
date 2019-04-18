Object.defineProperty(exports, "__esModule", { value: true });
var _components_1 = require("./components/_components");
exports.default = _components_1.default;
var Vuets = /** @class */ (function () {
    function Vuets() {
    }
    Vuets.getCid = function (vue) {
        var tag = vue.$vnode.componentOptions.tag;
        if (vue.vEvent === undefined) {
            console.warn("[" + tag + "] Register events failed, because both `vEvent` are undefined.");
            return;
        }
        var cid = vue.$el.id;
        if (cid === '') {
            console.warn("[" + tag + "] Register events failed, because `id` is not specified.");
            return;
        }
        return cid;
    };
    Vuets.registerExports = function (vue) {
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            var exports = vue.exports;
            for (var _i = 0, exports_1 = exports; _i < exports_1.length; _i++) {
                var e = exports_1[_i];
                vue.vEvent.$on("#" + cid + "." + e.name, e.method);
            }
        }
    };
    Vuets.trigger = function (vue, exportName) {
        var params = [];
        for (var _i = 2; _i < arguments.length; _i++) {
            params[_i - 2] = arguments[_i];
        }
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            vue.vEvent.$emit("#" + cid + "." + exportName, params);
        }
    };
    Vuets.invoker = function (vue, exportName) {
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            var tag = vue.$vnode.componentOptions.tag;
            var exports = vue.exports;
            for (var _i = 0, exports_2 = exports; _i < exports_2.length; _i++) {
                var e = exports_2[_i];
                if (e.name === exportName)
                    return e.method;
            }
            console.error("[" + tag + "] Can not get function `" + exportName + "`.");
        }
    };
    return Vuets;
}());
exports.Vuets = Vuets;
//# sourceMappingURL=index.js.map