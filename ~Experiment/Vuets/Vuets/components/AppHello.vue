<template>
    <div>
        <div>{{display}}</div>
        <input type="button" @click="hello()" value="hello" />
        <input type="text" v-model.number="a" /> + <input type="text" v-model.number="b" /> = {{plus}}
    </div>
</template>

<script>
    export default {
        props: {
            text: { default() { 'Vue' } }
        },

        data() {
            return {
                a: 1,
                b: 2,
                display: `Hello ${this.text}`
            }
        },

        computed: {
            plus() { return this.a + this.b; }
        },

        watch: {
            a() { this.b = this.a; }
        },

        methods: {
            hello() {
                this.display = "Hello App.vue!";
            }
        }
    }
</script>

<style>
</style>