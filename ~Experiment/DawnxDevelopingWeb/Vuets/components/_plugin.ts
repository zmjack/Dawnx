import _Vue from 'vue'

/*
 * import all components
 */
import HeroicBattleTs from './HeroicBattle'
import HeroicBattleVue from './HeroicBattle.vue'

/**
 * Install all components into Vue.
 * Call `Vue.use(Vuets)` to install.
 */
export default function Vuets(Vue: typeof _Vue, options?: any): void {

    /* Set tags for all components */
    Vue.component('heroic-battle-ts', HeroicBattleTs);
    Vue.component('heroic-battle-vue', HeroicBattleVue);
}
