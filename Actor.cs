using System.Drawing;

public class Actor
{
	protected Bitmap sprite;
	protected (int width, int height) dimensions;    // dimensions of the sprite

	protected (float x, float y) location;
	protected int speed;

	protected (float x, float y) muzzleLocation;
	protected bool isAlive;

	public Actor()
	{
		muzzleLocation = (location.x + (dimensions.width / 2), location.y);
	}


	/* --  DRAWING  -- */
	public Bitmap GetSprite() => sprite;

	public (int width, int height) GetDimensions() => dimensions;


	/* --  MOVEMENT  -- */
	public (float x, float y) GetLocation() => location;


	/* --  ATTACK  -- */
	public (float x, float y) GetMuzzleLocation() => muzzleLocation;

	public bool IsAlive() => isAlive;

	public virtual bool IsHit(Bullet bullet)
	{
		float x = bullet.GetLocation().x;
		float y = bullet.GetLocation().y;
		if (x >= location.x + 1 && x <= location.x + dimensions.width && y <= location.y + dimensions.height && y >= location.y)
		{
			isAlive = false;
			return true;
		}
		return false;
	}
}