using System.Drawing;

public class Bullet : Actor
{	
	public Bullet(int speed) : base()
	{
		sprite = new Bitmap(Space_Invaders.Properties.Resources.bullet);
		width = 5;
		height = 10;

		this.speed = speed;
		isAlive = false;
	}


	/* --  MOVEMENT  -- */
	public void Shoot( (float x, float y) spawn )
    {
		x = spawn.x;
		y = spawn.y;
		isAlive = true;
	}

	public void Move(int direction, Size screenSize)				// +1 = downwards, -1 = upwards
    {
		if (y <= 0 || y >= screenSize.Height)
			isAlive = false;
		else
			y += (direction * speed);
	}


	/* --  ATTACK  -- */
	public void Destroy()
    {
		isAlive = false;
    }
}
