declare var Vue;
import axios from './axios'

Vue.component('add-ad-service-modal', {
    template: `
<div>
    <a-modal title="新建AD服务"
                :visible="visible"
                v-on:ok="handleOk"
                :confirmLoading="confirmLoading"
                v-on:cancel="handleCancel">

        <div class="form-horizontal">
            <input type="hidden" v-model="project" />
            <input type="hidden" v-model="authServer" />

            <div class="form-group">
                <label class="col-sm-4 control-label">ConfigName</label>
                <div class="col-sm-8">
                    <input type="email" class="form-control" v-model="configName">
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-4 control-label">AuthCallbackUrl</label>
                <div class="col-sm-8">
                    <input type="password" class="form-control" v-model="authCallbackUrl">
                </div>
            </div>
        </div>
    </a-modal>
</div>`,
    props: {
        ve: undefined,
        veId: '',
    },
    data() {
        return {
            project: '',
            configName: '',
            authServer: '',
            authCallbackUrl: '',

            visible: false,
            confirmLoading: false,
        }
    },
    methods: {
        handleOk(e) {
            this.confirmLoading = true;

            var model: AddAdServiceModel;
            model.Project = this.project;
            model.ConfigName = this.configName;
            model.AuthServer = this.authServer;
            model.AuthCallbackUrl = this.authCallbackUrl;

            axios
                .post('/ComponentApis/AdServiceModal', model)
                .then((response) => {
                    var data = response.data;
                    alert(data);
                });

            setTimeout(() => {
                this.visible = false;
                this.confirmLoading = false;
            }, 2000);
        },
        handleCancel(e) {
            console.log('Clicked cancel button');
            this.visible = false
        },
    },
    mounted() {
        if (this.ve !== undefined) {
            this.ve.$on(`${this.veId}:show`, (project) => {
                this.project = project;
                this.configName = 'Debug';
                this.authServer = 'http://sz-papi:1100/Auth';
                this.authCallbackUrl = '';
                this.visible = true;
            });
        }
    }
});