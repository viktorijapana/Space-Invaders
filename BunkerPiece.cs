using System.Drawing;

public class BunkerPiece : Actor
{
	int health;

	public BunkerPiece(Bitmap sprite, float startX, float startY)
	{
		this.sprite = sprite;
		width = 35;
		height = 30;

		x = startX;
		y = startY;

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
