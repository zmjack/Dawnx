import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop, Watch } from 'vue-property-decorator';

@Component({
    template: `
<template>
    <div>
        <div>{{display}}</div>
        <input type="button" @click="hello()" value="hello" />
        <input type="text" v-model.number="a" /> + <input type="text" v-model.number="b" /> = {{plus}}
    </div>
</template>`
})
export default class extends Vue {

    @Prop({ default: 'Vue' }) text: string;

    a: number = 1;
    b: number = 2;
    display: string = `Hello ${this.text}`;

    get plus(): number { return this.a + this.b; }
    @Watch('a') wahch_a() { this.b = this.a; }

    hello() {
        this.display = "Hello App.ts!";
    }
}
