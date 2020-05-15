function addPost(username, postBody, createdDate) {
    var user = db.find(`{username: ${username}}`);

    var item = {
        createdBy: user['name'] + ' ' + user['surname'],
        body: postBody,
        createdDate: createdDate,
        owner: username,
        comments: [],
        likes: [],
    }
    db.posts.insertOne(item);
}