A simple 2d project that helps me navigating through unity GUI efficiently, understanding  
	playerControl script,  
	physics script,  
	animation creation,  
	sprite slicing,  
	game object creation,  
along with the purposes of some of the components such as rigidbody, collider, animator and others.  
  
Disclaimer: I do not claim ownership of the sprite arts and codes used in this project.  

General notes (on going):  
	-FixedUpdate() will be called every fixed framerate frames, so it is good to put your physics code here  
	-In order to move an object, checking for collision is a must. Start with Rigidbody2D.Cast(),  
		it will return number of collision when you trying to move an object to a specific distance in a  
		specific direction.  
	-Make sure you are not checking for collision when you are standing still, to do so  
		make a variable that stores minDistance we must move in order to check for collision  
	-In order to control an object, we need to store the reference to the object, and we can do it in Start()  
		start vs awaken, start is called once in script life time, awake is called when script object is  
		initialized, whether the script is enableded not. This can happen later in the game's life time.  
	-set animator parameter using the animator reference in your script to trigger animations and change animation  
		properties
