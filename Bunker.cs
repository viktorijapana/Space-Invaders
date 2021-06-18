using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class Bunker
{
	Bitmap[] bunkerImages;
	BunkerPiece[] pieces;


	public Bunker((float x, float y) location)
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
		float x = location.x;
		float y = location.y;

		for (int i = 0; i < pieces.Length; ++i)
		{
			pieces[i] = new BunkerPiece((x, y), bunkerImages[i]);
			x += 35;
			count++;

			if (count == 3)
			{
				x = location.x;
				y += 30;
				count = 0;
			}
		}
    }


	public BunkerPiece[] GetPieces() => pieces;


	public BunkerPiece IsHit(Bullet bullet)
	{
		for (int i = 0; i < pieces.Length; ++i)
			if (pieces[i].IsAlive() && pieces[i].IsHit(bullet))
				return pieces[i];

		return null;
	}
}
