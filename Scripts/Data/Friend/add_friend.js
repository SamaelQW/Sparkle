function addFriends(userName, friends)
{
    function addFriend(user, friend){
        var item = db.users.find({username: friend}).next();
        var data = {
            friendId: item["_id"],
            friendName: item["name"],
            friendSurname: item["surname"],
            friendUsername: item["username"]
        }
        db.users.updateOne({_id: user["_id"]},
        {
            $push: {
                friends: data
            }
        });
    }

    var user = db.users.find({username: userName}).next();
    
    friends.forEach(element => {
        addFriend(user,element);
    });
}