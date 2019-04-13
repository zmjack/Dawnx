import Vue from 'vue'
import components from './components/_components'
declare var vuets: Vue

export default components;

export interface VEvent {
    vEvent: Vue;
    exports: Array<VExport>;
}

export interface VExport {
    name: string,
    method: Function,
}

export class Vuets {

    private static getCid(vue: Vue & VEvent): string {
        var tag = vue.$vnode.componentOptions.tag;

        if (vue.vEvent === undefined) {
            if (vuets === undefined) {
                console.warn(`[${tag}] Register events failed, because both \`vEvent\` and global varialbe \`vuets\` are undefined.`)
                return;
            }
            else vue.vEvent = vuets;
        }

        var cid = vue.$el.id;
        if (cid === '') {
            console.warn(`[${tag}] Register events failed, because \`id\` is not specified.`);
            return;
        }

        return cid;
    }

    public static registerExports(vue: Vue & VEvent) {
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            var exports = vue.exports;
            for (var e of exports) {
                vue.vEvent.$on(`#${cid}.${e.name}`, e.method);
            }
        }
    }

    public static trigger(vue: Vue & VEvent, exportName: string, ...params) {
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            vue.vEvent.$emit(`#${cid}.${exportName}`, params);
        }
    }

    public static invoker(vue: Vue & VEvent, exportName: string): Function {
        var cid = this.getCid(vue);
        if (cid !== undefined) {
            var tag = vue.$vnode.componentOptions.tag;
            var exports = vue.exports;
            for (var e of exports) {
                if (e.name === exportName) return e.method;
            }
            console.error(`[${tag}] Can not get function \`${exportName}\`.`);
        }
    }

}
