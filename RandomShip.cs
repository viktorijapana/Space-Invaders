using System;
using System.Drawing;

public class RandomShip : Actor
{
	static int[] possible_points = { 50, 100, 150, 300 };
	readonly int points;                             // how many points this alien is worth

	public RandomShip()
	{
		sprite = new Bitmap(Space_Invaders.Properties.Resources.random_spaceship);
		dimensions = (40, 25);

		location = (0, 0);
		speed = 20;

		isAlive = false;
		points = new Random().Next(3);
	}


	public void Move()
    {
		location.x -= speed;
    }
}
