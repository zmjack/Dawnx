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
var axios_1 = require("axios");
var vue_class_component_1 = require("vue-class-component");
var vue_property_decorator_1 = require("vue-property-decorator");
var AddAdServiceModal = /** @class */ (function (_super) {
    __extends(AddAdServiceModal, _super);
    function AddAdServiceModal() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.ve = undefined;
        _this.veId = '';
        _this.project = '';
        _this.configName = '';
        _this.authServer = '';
        _this.authCallbackUrl = '';
        _this.visible = false;
        _this.confirmLoading = false;
        return _this;
    }
    AddAdServiceModal.prototype.handleOk = function (e) {
        var _this = this;
        this.confirmLoading = true;
        var model;
        model.Project = this.project;
        model.ConfigName = this.configName;
        model.AuthServer = this.authServer;
        model.AuthCallbackUrl = this.authCallbackUrl;
        axios_1.default
            .post('/ComponentApis/AdServiceModal', model)
            .then(function (response) {
            var data = response.data;
            alert(data);
        });
        setTimeout(function () {
            _this.visible = false;
            _this.confirmLoading = false;
        }, 2000);
    };
    AddAdServiceModal.prototype.handleCancel = function (e) {
        console.log('Clicked cancel button');
        this.visible = false;
    };
    AddAdServiceModal.prototype.mounted = function () {
        var _this = this;
        if (this.ve !== undefined) {
            this.ve.$on(this.veId + ":show", function (project) {
                _this.project = project;
                _this.configName = 'Debug';
                _this.authServer = 'http://sz-papi:1100/Auth';
                _this.authCallbackUrl = '';
                _this.visible = true;
            });
        }
    };
    __decorate([
        vue_property_decorator_1.Prop({ default: undefined })
    ], AddAdServiceModal.prototype, "ve", void 0);
    __decorate([
        vue_property_decorator_1.Prop({ default: undefined })
    ], AddAdServiceModal.prototype, "veId", void 0);
    AddAdServiceModal = __decorate([
        vue_class_component_1.default({
            template: "\n<div>\n    <a-modal title=\"\u65B0\u5EFAAD\u670D\u52A1\"\n                :visible=\"visible\"\n                v-on:ok=\"handleOk\"\n                :confirmLoading=\"confirmLoading\"\n                v-on:cancel=\"handleCancel\">\n\n        <div class=\"form-horizontal\">\n            <input type=\"hidden\" v-model=\"project\" />\n            <input type=\"hidden\" v-model=\"authServer\" />\n\n            <div class=\"form-group\">\n                <label class=\"col-sm-4 control-label\">ConfigName</label>\n                <div class=\"col-sm-8\">\n                    <input type=\"email\" class=\"form-control\" v-model=\"configName\">\n                </div>\n            </div>\n            <div class=\"form-group\">\n                <label class=\"col-sm-4 control-label\">AuthCallbackUrl</label>\n                <div class=\"col-sm-8\">\n                    <input type=\"password\" class=\"form-control\" v-model=\"authCallbackUrl\">\n                </div>\n            </div>\n        </div>\n    </a-modal>\n</div>"
        })
    ], AddAdServiceModal);
    return AddAdServiceModal;
}(vue_1.default));
exports.default = AddAdServiceModal;
//Vue.component('add-ad-service-modal', new ExtendedVue {
//    template: `
//<div>
//    <a-modal title="新建AD服务"
//                :visible="visible"
//                v-on:ok="handleOk"
//                :confirmLoading="confirmLoading"
//                v-on:cancel="handleCancel">
//        <div class="form-horizontal">
//            <input type="hidden" v-model="project" />
//            <input type="hidden" v-model="authServer" />
//            <div class="form-group">
//                <label class="col-sm-4 control-label">ConfigName</label>
//                <div class="col-sm-8">
//                    <input type="email" class="form-control" v-model="configName">
//                </div>
//            </div>
//            <div class="form-group">
//                <label class="col-sm-4 control-label">AuthCallbackUrl</label>
//                <div class="col-sm-8">
//                    <input type="password" class="form-control" v-model="authCallbackUrl">
//                </div>
//            </div>
//        </div>
//    </a-modal>
//</div>`,
//    props: {
//        ve: undefined,
//        veId: '',
//    },
//    data() {
//        return {
//            project: '',
//            configName: '',
//            authServer: '',
//            authCallbackUrl: '',
//            visible: false,
//            confirmLoading: false,
//        }
//    },
//    methods: {
//        handleOk(e) {
//            this.confirmLoading = true;
//            var model: AddAdServiceModel;
//            model.Project = this.project;
//            model.ConfigName = this.configName;
//            model.AuthServer = this.authServer;
//            model.AuthCallbackUrl = this.authCallbackUrl;
//            axios
//                .post('/ComponentApis/AdServiceModal', model)
//                .then((response) => {
//                    var data = response.data;
//                    alert(data);
//                });
//            setTimeout(() => {
//                this.visible = false;
//                this.confirmLoading = false;
//            }, 2000);
//        },
//        handleCancel(e) {
//            console.log('Clicked cancel button');
//            this.visible = false
//        },
//    },
//    mounted() {
//        if (this.ve !== undefined) {
//            this.ve.$on(`${this.veId}:show`, (project) => {
//                this.project = project;
//                this.configName = 'Debug';
//                this.authServer = 'http://sz-papi:1100/Auth';
//                this.authCallbackUrl = '';
//                this.visible = true;
//            });
//        }
//    }
//});
//# sourceMappingURL=addAdServiceModal.js.map