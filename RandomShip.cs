using System;
using System.Drawing;
using System.Windows.Forms;

public class RandomShip : Actor
{
	readonly static int[] possible_points = { 50, 100, 150, 300 };
	readonly int points;                             // how many points this alien is worth
	readonly int start;
	readonly Timer launchTimer;
	readonly Random rand;
	readonly int low_time = 10000;
	readonly int high_time = 100000;

	public RandomShip(Size screenSize)
	{
		rand = new Random();

		sprite = new Bitmap(Space_Invaders.Properties.Resources.random_spaceship);
		width = 40;
		height = 25;

		start = screenSize.Width;
		x = start;
		y = 90;
		speed = 10;

		isAlive = false;
		points = possible_points[rand.Next(3)];

		launchTimer = new Timer()
		{
			Enabled = true,
			Interval = rand.Next(low_time, high_time)
        };

        launchTimer.Tick += new EventHandler(Tick);
    }

    private void Tick(object sender, EventArgs e)
    {
        isAlive = true;
		launchTimer.Interval = rand.Next(low_time, high_time);
    }


    public void Move()
    {
		if (x >= (-1) * width)
			x -= speed;
		else
        {
			isAlive = false;
			x = start;
        }
    }


	public int GetPoints() => points;
}
