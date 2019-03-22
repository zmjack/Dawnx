var path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = {

    entry: {
        main: './wwwroot/js/main'
    },

    output: {
        publicPath: "/js/",
        path: path.join(__dirname, '/wwwroot/js/'),
        filename: 'main.build.js'
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
