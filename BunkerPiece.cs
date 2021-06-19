using System.Drawing;

public class BunkerPiece : Actor
{
	// Action
	int health;

	public BunkerPiece(Bitmap sprite, float startX, float startY)
	{
		// Graphics
		this.sprite = sprite;
		width = 35;
		height = 30;

		// Movement
		x = startX;
		y = startY;

		// Action
		health = 3;
		isAlive = true;
	}

	public int GetHealth() => health;

    public void TakeDamage()
    {
		health--;
    }

	public void Kill()
    {
		isAlive = false;
    }
}
