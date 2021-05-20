using System;
using System.Windows.Forms;


class AlienArray
{
    public readonly int rows;
    public readonly int columns;
    readonly Alien[,] aliens;
    readonly int space_x;                      // pixels between aliens horizontally
    readonly int space_y;                      // pixels between aliens vertically

    (float x, float y) location;               // the location of the top-left corner of the block of aliens
    int direction;                             // 1 = right, -1 = left
    int speed_x;                               // how fast the block moves horizontally
    int speed_y;                               // how fast the block moves vertically

    readonly Timer moveTimer = new Timer();

    public AlienArray((int x, int y) start)
    {
        rows = 5;
        columns = 11;
        aliens = new Alien[rows, columns];
        space_x = 20;
        space_y = 5;

        location = start;
        direction = 1;
        speed_x = 15;
        speed_y = 35;

        FillArray();

        moveTimer.Enabled = true;
        moveTimer.Interval = 700;
        moveTimer.Tick += new EventHandler(Tick);
    }


    private void Tick(object sender, EventArgs e)
    {
        Move();
    }


    public Alien this[int i, int j]
    {
        get => aliens[i, j];
    }


    private void FillArray()
    {
        float x = location.x;       // current location
        float y = location.y;

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                if (i == 0)
                    aliens[i, j] = new Alien(Space_Invaders.Properties.Resources.alien30, (x, y), 30);
                else if (i == 1 || i == 2)
                    aliens[i, j] = new Alien(Space_Invaders.Properties.Resources.alien20, (x, y), 20);
                else
                    aliens[i, j] = new Alien(Space_Invaders.Properties.Resources.alien10, (x, y), 10);

                x = x + aliens[0, j].GetDimensions().width + space_x;
            }

            y = y + aliens[0, 0].GetDimensions().height + space_y;
            x = 0;
        }
    }


    public void Move()
    {
        if (location.x + (direction * speed_x) <= 0 || location.x + (direction * speed_x) >= 350)
        {
            location.y += speed_y;
            direction *= -1;
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (aliens[i, j] != null)
                        aliens[i, j].Descend(speed_y);
        }
        else
        {
            location.x += (direction * space_x);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (aliens[i, j] != null)
                        aliens[i, j].Move(direction, speed_x);
        }
    }


    // returns the alien that has been hit if true, null otherwise
    public Alien IsHit(Bullet bullet)
    {
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                if (aliens[i, j] != null && aliens[i, j].IsHit(bullet))
                {
                    Alien hitAlien = aliens[i, j];
                    aliens[i, j] = null;
                    return hitAlien;
                }
        return null;
    }


    // returns a random alien in the array that will fire the lazer
    public Alien GetAttackingAlien()
    {
        Random rand = new Random();
        int randomX; 
        int randomY;

        while (true) { 
            randomX = rand.Next(rows - 1);
            randomY = rand.Next(columns - 1);

            if (aliens[randomX, randomY] != null)
                return aliens[randomX, randomY];
        }
    }
}