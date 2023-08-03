# ShooterGameplay

#Demo Video

https://github.com/MGurcan/ShooterGameplay/assets/78200658/8bcfd299-ba24-41e5-b0b7-3d487920a820


# Difficulties Handled
* Character Avatars and Animations: I faced challenges when creating character avatars and animations for my project. However, I managed to overcome this by seeking assistance from Mixamo, a website that provides 3D models and animations for characters. Using Mixamo's resources, I successfully created realistic and dynamic animations for my characters.

* Minimap Radar: While working on the minimap radar, I had trouble drawing the radar area according to the player's viewing angle. To tackle this, I decided to use different angled triangle sprites and rotated them as child components of the character. Unfortunately, I couldn't achieve the desired rotation, so I created a separate GameObject and used the transform's rotate methods to properly rotate the radar area, which resolved the issue.

* Crosshair and Aim: Implementing a functional crosshair and aligning it with the firing direction proved challenging. The problem arose because the character was slightly offset to the left, while the crosshair remained at the center of the screen. As a result, the target vector had an angle, leading to bullet inaccuracies. I managed to solve this by calculating the target vector using the camera's viewpoint, which lies between the crosshair and the aiming direction.

* Data Persistence and Scene Transitions: I encountered problems with data persistence and transferring game data during scene transitions and game restarts. However, I successfully overcame these issues by using ScriptableObjects. With ScriptableObjects, I was able to save and share data between scenes and game states effectively.

* Consecutive Firing Difficulty: Due to the limitations imposed by gun overheating, firing consecutively became challenging. To address this, I decided to reduce the frequency of enemy encounters, which helped me balance the game difficulty and added an element of strategic gameplay.

These challenges were significant learning experiences for me, and by overcoming them, I was able to improve my game development skills and create a more engaging and polished project.
