var express = require('express');
var app = express();
var server = require('http').createServer(app);
var io = require('socket.io').listen(server);
var users = {};
var mongoose = require('mongoose');
server.listen(3000, function(){
    console.log("Server is up!");
});

mongoose.connect('mongodb://localhost/cat', function(err){
    if(err){
        console.log(err);
    }else{
        console.log("Connected to mongodb");
    }
});

var chatSchema = mongoose.Schema({
    nick: String,
    msg: String,
    created: {type: Date, default: Date.now}    
});

var Chat = mongoose.model('Message', chatSchema);

app.get('/', function(req, res){
    res.sendFile(__dirname + '/index.html');
});

function updateNicknames(){
    io.sockets.emit('usernames', Object.keys(users));
}
io.sockets.on('connection', function(socket){
    var query = Chat.find({});
    query.sort('-created').limit(8).exec(
    function(err, docs){
        if(err) throw err;
        socket.emit('load old msgs', docs);
        console.log("retrieving old messages");        
    });

    socket.on('new user', function(data, callback){
        if(data in users){
            callback(false);
        }else{
            callback(true);
            socket.nickname = data;
            users[socket.nickname] = socket;
            updateNicknames();
        }
    });
    socket.on('disconnect', function(data){
        console.log("Disconnected");
        if(!socket.nickname) return;
        delete users[socket.nickname];
        updateNicknames();
    });
    socket.on('send-message', function(data, callback){
        console.log("sent message");
        var msg = data.trim();
        if(msg.substr(0, 3) == '/w '){
            console.log("whipsering");
            msg = msg.substr(3);
            var ind = msg.indexOf(' ');
            if(ind != -1){
                var name = msg.substring(0, ind);
                var msg = msg.substring(ind+1);
                if(name in users){
                    console.log("user found");                    
                    users[name].emit('whisper', {
                        msg: msg,
                        nick: socket.nickname
                    });
                }else{
                    callback("Error: enter a valid user");
                }
            }else{
                callback("Error: please enter a message for youw whisper");
            }
        }else{
            var newMsg = new Chat({
                msg: msg, nick: socket.nickname
            });
            newMsg.save(function(err){
                if(err) throw err;
                io.emit('new-message', {
                    msg: msg, nick: socket.nickname
                });
            });
        }
        
    });

});