using System.Drawing;

public class Actor
{
	// Graphics
	protected Bitmap sprite;
	protected int width;			// width of the sprite
	protected int height;			// height of the sprite

	// Movement
	protected float x;				// x coordinate of the actor's current location on the screen
	protected float y;				// y coordinate of the actor's current location on the screen

	// Action
	protected bool isAlive;



	/* --  GRAPHICS  -- */
	public Bitmap GetSprite() => sprite;

	public (int width, int height) GetDimensions() => (width, height);


	/* --  MOVEMENT  -- */
	public (float x, float y) GetLocation() => (x, y);


	/* --  ACTION  -- */
	public bool IsAlive() => isAlive;

	public bool IsHit(Bullet bullet)
	{
		float bulletX = bullet.GetLocation().x;
		float bulletY = bullet.GetLocation().y;
		if (bulletX >= x + 1 && bulletX <= x + width && bulletY <= y + height && bulletY >= y)
			return true;
		
		return false;
	}
}