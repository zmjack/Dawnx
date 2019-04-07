var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var vue_1 = require("vue");
var vue_class_component_1 = require("vue-class-component");
var vue_property_decorator_1 = require("vue-property-decorator");
var HeroicBattle_ts_html_1 = require("./HeroicBattle.ts.html");
require("./HeroicBattle.ts.css");
var default_1 = /** @class */ (function (_super) {
    __extends(default_1, _super);
    function default_1() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.hp = 0;
        _this.ad = 0;
        _this.m_hp = 0;
        _this.m_ad = 0;
        _this.gameOver = true;
        _this.result = '';
        return _this;
    }
    default_1.prototype.newGame = function () {
        this.hp = 100;
        this.ad = Math.floor(10 + Math.random() * 10);
        this.m_hp = 200;
        this.m_ad = Math.floor(10 + Math.random() * 10);
        this.gameOver = false;
    };
    default_1.prototype.attack = function () {
        this.m_hp -= this.ad;
        if (this.m_hp < 0) {
            this.m_hp = 0;
            this.gameOver = true;
            this.result = 'YOU WIN!';
        }
    };
    default_1.prototype.mAttack = function () {
        this.hp -= this.m_ad;
        if (this.hp < 0) {
            this.hp = 0;
            this.gameOver = true;
            this.result = 'You are dead';
        }
    };
    default_1.prototype.attackRound = function () {
        this.mAttack();
        this.attack();
    };
    default_1.prototype.addHP = function () {
        this.hp += 50;
        if (this.hp > 100)
            this.hp = 100;
        this.mAttack();
    };
    default_1.prototype.mounted = function () {
        this.newGame();
    };
    __decorate([
        vue_property_decorator_1.Prop({ default: 'anonymous' })
    ], default_1.prototype, "player", void 0);
    default_1 = __decorate([
        vue_class_component_1.default({ template: HeroicBattle_ts_html_1.default })
    ], default_1);
    return default_1;
}(vue_1.default));
exports.default = default_1;
//# sourceMappingURL=HeroicBattle.js.map