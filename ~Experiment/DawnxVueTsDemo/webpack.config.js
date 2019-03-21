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

    plugins: [
        new VueLoaderPlugin()
    ]

};
