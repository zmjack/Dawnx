var path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = {

    entry: {
        main_ts: './Vuets/main_ts',
        main_vue: './Vuets/main_vue',
    },

    output: {
        publicPath: "/js/",
        path: path.join(__dirname, '/wwwroot/js/'),
        filename: '[name].js',
    },

    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },

    module: {
        rules: [{
            test: /\.vue/,
            loader: 'vue-loader'
        }, {
            test: /\.css$/,
            use: [
                'vue-style-loader',
                'css-loader'
            ]
        }]
    },

    plugins: [
        new VueLoaderPlugin()
    ]

};
