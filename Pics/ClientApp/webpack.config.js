'use strict';

const webpack = require('webpack');
const path = require('path');

const bundleFolder = "./bundle/";
const srcFolder = "./src/";

module.exports = {
  entry: [
    srcFolder + "index.jsx"
  ],
  devtool: "source-map",
  output: {
    filename: "bundle.js",
    //publicPath: 'public/',
    path: path.resolve(__dirname, bundleFolder)
  },
  resolve: {
    extensions: ['.js', '.jsx']
  },
  module: {
    loaders: [
      {
        test: /\.jsx?$/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel-loader',
        query: {
          plugins: ['transform-object-rest-spread'],
          presets: ['react', 'es2015', 'stage-3']
        }
      }
    ]
    //rules: [
    //  {
    //    test: /\.jsx$/,
    //    exclude: /(node_modules)/,
    //    loader: "babel-loader",
    //    query: {
    //      plugins: ['transform-object-rest-spread'],
    //      presets: ["es2015", "stage-3", "react"]
    //    }
    //  }
    //]
  },
  plugins: [
  ]
};