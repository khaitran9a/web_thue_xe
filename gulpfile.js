// JavaScript source code

var gulp = require('gulp');
var sass = require('gulp-sass');
var cssmin = require("gulp-cssmin");
var postcss = require('gulp-postcss');
var autoprefixer = require('autoprefixer');
var concat = require("gulp-concat");

var paths = {
    scss: "./wwwroot/scss/**/*.scss",
    css: "./wwwroot/css/",
    minCss: "./wwwroot/css/min/",
    concatCssDest: "./wwwroot/css/site.min.css",


    // js : "./wwwroot/js/**/*.js",
    // minJs : "./wwwroot/js/**/*.min.js",
    // concatJsDest : "./wwwroot/js/site.min.js",
    // concatCssDest : "./wwwroot/css/site.min.css",
};


gulp.task('sass', function (cb) {
    gulp
        .src(paths.scss)
        .pipe(sass())
        .pipe(gulp.dest(paths.css))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.minCss));
    cb();
});

gulp.task(
    'default',
    gulp.series('sass', function (cb) {
        gulp.watch(paths.scss, gulp.series('sass'));
        cb();
    })
);

var gulp = require('gulp'),
    cssmin = require("gulp-cssmin")
rename = require("gulp-rename");
const sass = require('gulp-sass')(require('sass'));

gulp.task('default', function () {
    return gulp.src('./Don_An_Nhom/Content/css/login.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(cssmin())
        .pipe(rename({
            // suffix: ".min"
        }))
        .pipe(gulp.dest('wwwroot/css/'));
});