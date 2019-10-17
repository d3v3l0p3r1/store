/// <binding BeforeBuild='clean, copy' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        clean: ['wwwroot/lib/kendo'],        
        copy: {
            build: {
                expand: true,
                cwd: 'Content/vendor/kendo/',
                src: '**',
                dest: 'wwwroot/lib/kendo/',
                filter: 'isFile'
            }
        },
        
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');

    grunt.registerTask('build', ['clean','copy']);

};