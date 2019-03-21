<template>
    <div class="huangtao">
        <div @click="change()">{{msg}}</div>
    </div>
</template>

<script>
    export default {
        data () {
            return {
                msg: '这是App.vue内容'
            }
        },

        methods: {
            change(){
                this.msg = "new Hello World!"
            }
        }
    }
</script>

<style>
    html {
        background: green;
        color: #fff;
        font-size: 20px;
    }
</style>