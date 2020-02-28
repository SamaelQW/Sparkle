// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function likePressed(postId) {
    $.post(`/Home/LikePressed/${postId}`);
   
};



function renderNewComment() {
    let newComment = $.get('/Post/NewComment');
    let elem = document.getElementsByClassName('post-content__comments')[0];
    elem.innerHTML += newComment;
}


function removePost(postId) {
    $.post(`RemovePost/${postId}`);
    window.location.href = "/Index" ;
}