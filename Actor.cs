using System.Drawing;

public class Actor
{
	protected Bitmap sprite;
	protected int width;
	protected int height;

	protected float x;
	protected float y;
	protected int speed;

	protected float muzzleX;
	protected float muzzleY;
	protected bool isAlive;

	public Actor()
	{
		muzzleX = x + (width / 2);
		muzzleY = y;
	}


	/* --  DRAWING  -- */
	public Bitmap GetSprite() => sprite;

	public (int width, int height) GetDimensions() => (width, height);


	/* --  MOVEMENT  -- */
	public (float x, float y) GetLocation() => (x, y);


	/* --  ATTACK  -- */
	public (float x, float y) GetMuzzleLocation() => (muzzleX, muzzleY);

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