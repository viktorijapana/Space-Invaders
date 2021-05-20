using System;
using System.Drawing;

public class Bullet : Actor
{	
	public Bullet() : base()
	{
		sprite = new Bitmap(Space_Invaders.Properties.Resources.bullet);
		dimensions = (5, 10);

		speed = 18;
		isAlive = false;
	}


	/* --  MOVEMENT  -- */
	public void Shoot((float x, float y) spawnPoint)
    {
		location = spawnPoint;
		isAlive = true;
	}

	public void Move(int direction, Size screenSize)				// +1 = downwards, -1 = upwards
    {
		if (location.y <= 0 || location.y >= screenSize.Height)
			isAlive = false;
		else
			location.y += (direction * speed);
	}


	/* --  ATTACK  -- */
	public void Destroy()
    {
		isAlive = false;
    }
}
