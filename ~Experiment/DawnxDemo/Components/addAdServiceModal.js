Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
Vue.component('add-ad-service-modal', {
    template: "\n<div>\n    <a-modal title=\"\u65B0\u5EFAAD\u670D\u52A1\"\n                :visible=\"visible\"\n                v-on:ok=\"handleOk\"\n                :confirmLoading=\"confirmLoading\"\n                v-on:cancel=\"handleCancel\">\n\n        <div class=\"form-horizontal\">\n            <input type=\"hidden\" v-model=\"project\" />\n            <input type=\"hidden\" v-model=\"authServer\" />\n\n            <div class=\"form-group\">\n                <label class=\"col-sm-4 control-label\">ConfigName</label>\n                <div class=\"col-sm-8\">\n                    <input type=\"email\" class=\"form-control\" v-model=\"configName\">\n                </div>\n            </div>\n            <div class=\"form-group\">\n                <label class=\"col-sm-4 control-label\">AuthCallbackUrl</label>\n                <div class=\"col-sm-8\">\n                    <input type=\"password\" class=\"form-control\" v-model=\"authCallbackUrl\">\n                </div>\n            </div>\n        </div>\n    </a-modal>\n</div>",
    props: {
        ve: undefined,
        veId: '',
    },
    data: function () {
        return {
            project: '',
            configName: '',
            authServer: '',
            authCallbackUrl: '',
            visible: false,
            confirmLoading: false,
        };
    },
    methods: {
        handleOk: function (e) {
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
        },
        handleCancel: function (e) {
            console.log('Clicked cancel button');
            this.visible = false;
        },
    },
    mounted: function () {
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
    }
});
//# sourceMappingURL=addAdServiceModal.js.map