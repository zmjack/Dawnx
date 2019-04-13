import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop } from 'vue-property-decorator'

import template from './HeroicBattle.ts.html'
import './HeroicBattle.ts.css'


@Component({ template })
export default class extends Vue {

    @Prop({ default: 'anonymous' }) player: string;

    hp = 0;
    ad = 0;

    m_hp = 0;
    m_ad = 0;

    gameOver = true;
    result = '';

    newGame() {
        this.hp = 100;
        this.ad = Math.floor(10 + Math.random() * 10);

        this.m_hp = 200;
        this.m_ad = Math.floor(10 + Math.random() * 10);

        this.gameOver = false;
    }

    attack() {
        this.m_hp -= this.ad;
        if (this.m_hp < 0) {
            this.m_hp = 0;
            this.gameOver = true;
            this.result = 'YOU WIN!'
        }
    }

    mAttack() {
        this.hp -= this.m_ad;
        if (this.hp < 0) {
            this.hp = 0;
            this.gameOver = true;
            this.result = 'You are dead';
        }
    }

    attackRound() {
        this.mAttack();
        this.attack();
    }

    addHP() {
        this.hp += 50;
        if (this.hp > 100)
            this.hp = 100;

        this.mAttack();
    }

    mounted() {
        this.newGame();
    }

}
