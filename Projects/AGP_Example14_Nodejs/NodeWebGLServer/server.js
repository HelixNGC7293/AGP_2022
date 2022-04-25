var express  = require('express');
var app      = express();
var http     = require('http').Server(app);
var io       = require('socket.io')(http);
var mazeGenerator  = require(__dirname + '/public/maze');

app.use(express.static(__dirname + '/webgl-chat'));
app.use('/public', express.static(__dirname + '/public'));

var maze = undefined;
var mazeDimension = 49;
maze = mazeGenerator(mazeDimension);

var mazeStr = "";
for(var i = 0; i < maze.length - 1; i++)
{
    mazeStr +=  maze[i] + "-";
}
 mazeStr +=  maze[maze.length - 1];


io.on('connection', function(socket) {
	var id = socket.id;
	console.log(id);
    //console.log(mazeStr);

    socket.on('unityLoad', function() {
        socket.emit('maze', mazeStr);
    });

	socket.on('move', function(x, y, z) {
		//console.log("move", x, y, z);
		socket.broadcast.emit('move', id, x, y, z);
	});

	socket.on('rotate', function(x, y, z, w) {
		socket.broadcast.emit('rotate', id, x, y, z, w);
	});

	socket.on('talk', function(message) {
		socket.broadcast.emit('talk', id, message);
	});

	socket.on('disconnect', function(message) {
		console.log(id);
		socket.broadcast.emit('destroy', id);
	});
});

http.listen(process.env.PORT || 3000, function(){
	console.log('listening on *:3000');
});


