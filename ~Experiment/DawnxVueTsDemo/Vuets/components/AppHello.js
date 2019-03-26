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
var default_1 = /** @class */ (function (_super) {
    __extends(default_1, _super);
    function default_1() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.a = 1;
        _this.b = 2;
        _this.display = "Hello " + _this.text;
        return _this;
    }
    Object.defineProperty(default_1.prototype, "plus", {
        get: function () { return this.a + this.b; },
        enumerable: true,
        configurable: true
    });
    default_1.prototype.wahch_a = function () { this.b = this.a; };
    default_1.prototype.hello = function () {
        this.display = "Hello App.ts!";
    };
    __decorate([
        vue_property_decorator_1.Prop({ default: 'Vue' })
    ], default_1.prototype, "text", void 0);
    __decorate([
        vue_property_decorator_1.Watch('a')
    ], default_1.prototype, "wahch_a", null);
    default_1 = __decorate([
        vue_class_component_1.default({
            template: "\n<template>\n    <div>\n        <div>{{display}}</div>\n        <input type=\"button\" @click=\"hello()\" value=\"hello\" />\n        <input type=\"text\" v-model.number=\"a\" /> + <input type=\"text\" v-model.number=\"b\" /> = {{plus}}\n    </div>\n</template>"
        })
    ], default_1);
    return default_1;
}(vue_1.default));
exports.default = default_1;
//# sourceMappingURL=App.js.map