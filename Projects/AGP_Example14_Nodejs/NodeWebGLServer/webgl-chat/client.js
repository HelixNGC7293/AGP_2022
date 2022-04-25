var socket = io() || {};
socket.isReady = false;

window.addEventListener('load', function() {
	var execInUnity = function(method) {
		if (!socket.isReady) return;
		var args = Array.prototype.slice.call(arguments, 1);
		myGameInstance.SendMessage("Network Player Manager", method, args.join(','));
	};

	var execInUnityArray = function(method) {
		if (!socket.isReady) return;
		var args = Array.prototype.slice.call(arguments, 1);
		myGameInstance.SendMessage("Network Player Manager", method, args[0]);
	};


	socket.on('move', function(id, x, y, z) {
		execInUnity('Move', id, x, y, z);
	});

	socket.on('rotate', function(id, x, y, z ,w) {
		execInUnity('Rotate', id, x, y, z, w);
	});

	socket.on('talk', function(id, message) {
		execInUnity('Talk', id, message);
	});

	socket.on('destroy', function(id) {
		execInUnity('DestroyPlayer', id);
	});

	socket.on('maze', function(mazeStr) {
		execInUnityArray('Maze', mazeStr);
	});

	var name = 'Unknown';
	var message = 'Hello!';
	var nameButton = document.getElementById('name');
	var messageButton = document.getElementById('message');
	var update = function() {
		myGameInstance.SendMessage("Player", "Talk", '@' + name + ': ' + message);
	};
	nameButton.addEventListener('click', function() {
		name = window.prompt('Your Name', 'Unknown');
		update();
	});
	messageButton.addEventListener('click', function() {
		message = window.prompt('Message (English only)', 'Hello!');
		update();
	});
});
