/// <binding BeforeBuild='sass' />
"use strict";

var gulp = require("gulp"),
    sass = require("gulp-sass"); // добавляем модуль sass

var paths = {
    webroot: "./wwwroot/"
};

// регистрируем задачу для конвертации файла scss в css
gulp.task("style", function () {
    return gulp.src('Styles/style.scss')
        .pipe(sass())
        .pipe(gulp.dest(paths.webroot + '/css'));
});

gulp.task("login", function () {
    return gulp.src('Styles/login.scss')
        .pipe(sass())
        .pipe(gulp.dest(paths.webroot + '/css'));
});