<template>
    <div>
        <div class="card w-100">
            <div class="card-body">
                <h5 class="card-title">Player: {{ player }}</h5>
                <p class="card-text">
                    <b>Health:</b> {{ hp }}
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped health-bar" :style="'width:'+(hp*100/100)+'%'"></div>
                    </div>
                </p>
            </div>
        </div>

        <div class="text-center" style="color:red">
            <h3>&nbsp;{{ result }}&nbsp;</h3>
        </div>
        
        <div class="card w-100">
            <div class="card-body">
                <h5 class="card-title">Monster</h5>
                <p class="card-text">
                    <b>Health:</b> {{ m_hp }}
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped health-bar" :style="'width:'+(m_hp*100/200)+'%'"></div>
                    </div>
                </p>
            </div>
        </div>

        <div class="mt-4 text-right" v-if="gameOver===false">
            <button type="button" class="btn btn-primary" @click="attackRound">Attack</button>
            <button type="button" class="btn btn-primary" @click="addHP">+HP</button>
        </div>
        <div class="mt-4 text-center " v-else>
            <button type="button" class="btn btn-light" @click="newGame">New Game</button>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            player: { default() { return 'anonymous' } }
        },

        data() {
            return {
                hp: 0,
                ad: 0,

                m_hp: 0,
                m_ad: 0,

                gameOver: true,
                result: '',
            }
        },

        methods: {
            newGame() {
                this.hp = 100;
                this.ad = Math.floor(10 + Math.random() * 10);

                this.m_hp = 200;
                this.m_ad = Math.floor(10 + Math.random() * 10);

                this.gameOver = false;
            },
            attack() {
                this.m_hp -= this.ad;
                if (this.m_hp < 0) {
                    this.m_hp = 0;
                    this.gameOver = true;
                    this.result = 'YOU WIN!'
                }
            },
            mAttack() {
                this.hp -= this.m_ad;
                if (this.hp < 0) {
                    this.hp = 0;
                    this.gameOver = true;
                    this.result = 'You are dead'
                }
            },
            attackRound() {
                this.mAttack();
                this.attack();
            },
            addHP() {
                this.hp += 50;
                if (this.hp > 100)
                    this.hp = 100;

                this.mAttack();
            }
        },

        mounted() {
            this.newGame();
        }
    }
</script>

<style>
    .health-bar {
        background-color: red !important;
    }
</style>