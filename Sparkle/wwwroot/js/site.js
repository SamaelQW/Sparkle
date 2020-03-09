// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.post__like').click(function () {
    var form = $(this).closest('form');
    var id = $(form).find('input[name="id"]').val();
    $.get('Home/LikePressed', { postId: id }).done(function (res) {
        var span = $(form).find('.counter');
        var btn = $(form).find('.post__like');
        $(btn).toggleClass('liked');

        if ($(btn).hasClass('liked')) {
            span.text(parseInt(span.text()) + 1);
        }
        else {
            span.text(parseInt(span.text()) - 1);
        }
    });


});



function renderNewComment() {
    let newComment;

    var id = $('.postId').text();

    var OwnerName = $('input[name="OwnerName"]').val();
    var OwnerSurname = $('input[name="OwnerSurname"]').val();
    var OwnerUserName = $('input[name="OwnerUserName"]').val();
    var Body = $('textarea[name="Body"]').val();
    var data = { postId: id, model: { Body: Body, OwnerName: OwnerName, OwnerSurname: OwnerSurname, OwnerUserName: OwnerUserName, PostId: id } };

    $.post(`/AddComment`, data).done(function (res) {
        $('.no-comments').css('display', 'none');
        $('.post-content__comments').append(`${res}`);
        $('textarea').val('');
    });


    
};

    



function removePost(postId) {
    $.post(`RemovePost/${postId}`);
    window.location.href = "/Index" ;
}