Object.defineProperty(exports, "__esModule", { value: true });
/*
 * import all components
 */
var HeroicBattle_1 = require("./HeroicBattle");
var HeroicBattle_vue_1 = require("./HeroicBattle.vue");
/**
 * Install all components into Vue.
 * Call `Vue.use(Vuets)` to install.
 */
function Vuets(Vue, options) {
    /* Set tags for all components */
    Vue.component('heroic-battle-ts', HeroicBattle_1.default);
    Vue.component('heroic-battle-vue', HeroicBattle_vue_1.default);
}
exports.default = Vuets;
//# sourceMappingURL=_plugin.js.map