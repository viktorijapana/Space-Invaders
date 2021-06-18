using System.Drawing;

public class Bunker
{
	readonly Bitmap[] bunkerImages;
	readonly BunkerPiece[] pieces;


	public Bunker(float startX, float startY)
	{
		bunkerImages = new Bitmap[9] {
			Space_Invaders.Properties.Resources.bunker1,
			Space_Invaders.Properties.Resources.bunker2,
			Space_Invaders.Properties.Resources.bunker3,
			Space_Invaders.Properties.Resources.bunker4,
			Space_Invaders.Properties.Resources.bunker5,
			Space_Invaders.Properties.Resources.bunker6,
			Space_Invaders.Properties.Resources.bunker7,
			Space_Invaders.Properties.Resources.bunker8,
			Space_Invaders.Properties.Resources.bunker9,
		};

		pieces = new BunkerPiece[9];
		int count = 0;
		float x = startX;
		float y = startY;

		for (int i = 0; i < pieces.Length; ++i)
		{
			pieces[i] = new BunkerPiece(bunkerImages[i], x, y);
			x += pieces[i].GetDimensions().width;
			count++;

			if (count == 3)
			{
				x = startX;
				y += pieces[i].GetDimensions().height;
				count = 0;
			}
		}
    }


	public BunkerPiece[] GetPieces() => pieces;


	public bool IsHit(Bullet bullet)
	{
		for (int i = 0; i < pieces.Length; ++i)
			if (pieces[i].IsAlive() && pieces[i].IsHit(bullet))
            {
				pieces[i].TakeDamage();
				if (pieces[i].GetHealth() == 0)
					pieces[i].Kill();

				return true;
			}

		return false;
	}
}
