    
$(document).ready(function () {

    $(".search-filter #type").change(function () {
        this.form.submit();
    });

    $(".search-filter #searchnote-searchbar").change(function () {
        this.form.submit();
    });

    $(".search-filter #category").change(function () {
        this.form.submit();
    });

    $(".search-filter #university").change(function () {
        this.form.submit();
    });

    $(".search-filter #course").change(function () {
        this.form.submit();
    });

    $("#country").change(function () {
        this.form.submit();
    });

    $(".search-filter #rating").change(function () {
        this.form.submit();
    });

});
