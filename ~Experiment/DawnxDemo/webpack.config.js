var path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const tsNameof = require("ts-nameof");

module.exports = {

    entry: {
        ['main-ts']: './Vuets/apps/main-ts',
        ['main-vue']: './Vuets/apps/main-vue'
    },

    output: {
        publicPath: "/js/",
        path: path.join(__dirname, '/wwwroot/js/'),
        filename: '[name].js'
    },

    resolve: {
        extensions: ['.ts', '.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },

    module: {
        rules: [{
            test: /\.tsx?$/,
            loader: 'ts-loader',
            options: {
                getCustomTransformers: () => ({ before: [tsNameof] })
            }
        }, {
            test: /\.vue$/,
            loader: 'vue-loader'
        }, {
            test: /\.css$/,
            use: [
                'vue-style-loader',
                'css-loader'
            ]
        }, {
            test: /\.html$/,
            use: 'raw-loader'
        }]
    },

    plugins: [
        new VueLoaderPlugin()
    ]

};
