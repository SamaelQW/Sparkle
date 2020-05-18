conn = new Mongo();

db = conn.getDB("network");
db.users.drop();
db.posts.drop();



load("./User/add_user.js");
load("./Friend/add_friend.js");
load("./db_data.js");

add_user("admin", "admin", "admin@sparkle.com", "Taras","Sharko","20", ISODate("1990-10-10"), [], 1, []);
add_user("sofa", "sofa", "legkasofia@gmail.com", "Sofia", "Lehka", "20", ISODate("1999-11-18"), [], 1, []);
add_user("pie", "pie", "pie@sparkle.com", "Pie", "Tree", "2", ISODate("2018-01-01"), [], 1, []);

addFriends("admin", ["sofa", "pie"]);
addFriends("sofa", ["pie", "admin"]);

for(i = 0; i < names.length; i++) {
    var nameInd = random(0, names.length-1);
    var surnameInd = random(0, surnames.length-1);
    var name = names[nameInd];
    var surname = surnames[surnameInd];
    var userName = name.slice(0,3) + surname.slice(surname.length-4);
    var password = userName;
    var email = name+surname+"@sparkle.com";
    var dateOfBirth = new Date(`${random(1970,2012)}-${random(1,13)}-${random(1,28)}`);
    var age = new Date(Date.now()).getFullYear() - new Date(Date.parse(dateOfBirth)).getFullYear();
    var userStatus = 1;
    add_user(userName,password,email,name,surname,age, dateOfBirth,[],userStatus,[]);

}


