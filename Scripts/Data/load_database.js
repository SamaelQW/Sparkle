conn = new Mongo();

db = conn.getDB("network");
db.users.drop();
db.posts.drop();

load("./User/add_user.js");
load("./Friend/add_friend.js");

add_user("admin", "admin", "admin@sparkle.com", "Taras","Sharko","20", ISODate("1990-10-10"), [], 1, []);
add_user("sofa", "sofa", "legkasofia@gmail.com", "Sofia", "Lehka", "20", ISODate("1999-11-18"), [], 1, []);
add_user("ivan", "ivan", "pie@sparkle.com", "Ivan", "Ivaniv", "2", ISODate("2018-01-01"), [], 1, []);
add_user("marta","marta", "ysharko@sparkle.com", "Marta", "Kyzuk", "15", ISODate("2005-03-09"), [],1,[]);
add_user("olya", "olya","olena@sparkle.com", "Olya", "Dorosh", "21", ISODate("1998-04-12"),[],1,[]);
add_user("petro", "petro","stepan@sparkle.com", "Petro", "Sharko", "25", ISODate("1995-03-21"),[],1,[]);

addFriends("admin", ["sofa", "ivan"]);
addFriends("sofa", ["admin"]);
addFriends("ivan", ["marta","sofa"]);
addFriends("olya", ["marta"]);
addFriends("marta", ["olya"]);