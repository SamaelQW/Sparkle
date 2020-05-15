function add_user(username,
                password,
                email,
                name,
                surname,
                age,
                dateOfBirth,
                userPosts,
                userStatus,
                friends) {
    var obj = {
        username: username,
        password: password,
        email: email,
        name: name,
        surname: surname,
        age: age,
        dateOfBirth: dateOfBirth,
        userPosts: userPosts,
        userStatus: userStatus,
        friends: friends
    }

    db.users.insertOne(obj);
}