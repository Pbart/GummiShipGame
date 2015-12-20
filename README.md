# GummiShipClone
Game Project for a 3D galaga/Kingdom Hearts 2 gummi ship clone game

In here will contain how to use github, a simple coding standards and folder management for scripts and assets
for the project. This will be amended as the project continues to make sure we cover all bases 

How to use Github:


A fairly obvious coding standard but just in case 
Coding Standards:

1) When creating an attribute or a variable, you must use camel case if it is more than one word.

Ex: float fireRate;

2) When creating an attribute or a variable, group them my type first then by accessor

Ex: public int health;
    private int speed;
	
	public float fireRate

3) When creating a attribute or variable, name them according to what they represent so there will be less confusion
on what the variable does

Ex: public int playerHealth;

4) When creating a method, function or property, the first letter of each word must be capitalized

Ex: void OnTriggerEnter()
Ex: float FireRate{}

5) When creating a method or fuction, name the method/function based on what that method will be doing.
If what the method is doing is a bit ambiguous for some reason, you must use /// above the method name and 
describe what it and it's parameters do 

Ex:
///used to keep the player in the bounds of the camera screen.
void KeepPlayerInScreen{}
{
}

6) When making a method, function or property, curly braces must not follow the closing parentheses.
This is for consistence across all scripts and classes made in the project

Ex: void OnTriggerEnter()
   {
   
   } 

7) When you comment out a line of code that your are no longer using, delete that line of code to prevent some one else from 
reusing that code in your scripts

Folder Management:

All assets such as prefabs, textures and material will belong in their own folders inside of a resources folder
All scripts made will belong into a scripts folder separated based on what the scripts do. If a scripts moves an enemy,
then it belongs into a enemy scripts folder whick will be inside a folder called scripts (Ex: Assets/Scripts/EnemyScripts/EnemyMove.cs)

 
