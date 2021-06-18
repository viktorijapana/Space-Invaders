using System.Drawing;

public class BunkerPiece : Actor
{
	int health = 3;

	public BunkerPiece((float x, float y) location, Bitmap sprite)
	{
		this.location = location;
		this.sprite = sprite;
		dimensions = (35, 30);
		isAlive = true;
	}


    public override bool IsHit(Bullet bullet)
    {
		float x = bullet.GetLocation().x;
		float y = bullet.GetLocation().y;
		if (x >= location.x + 1 && x <= location.x + dimensions.width && y <= location.y + dimensions.height && y >= location.y)
		{
			health--;
			if (health == 0)
				isAlive = false;

			return true;
		}
		return false;
	}
}
