using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;


class AlienArray
{
    public readonly int rows;
    public readonly int columns;
    readonly Alien[,] block;
    readonly Size screenSize;
    readonly int spaceX;                       // pixels between aliens horizontally
    readonly int spaceY;                       // pixels between aliens vertically
    readonly (int width, int height) alienDimensions;


    (float x, float y) start;
    (float x, float y) location;               // the location of the top-left corner of the block of aliens
    int direction;                             // 1 = right, -1 = left
    readonly int baseSpeed;                    // initial horizontal speed of the block
    int speedX;                                // how fast the block moves horizontally
    int speedY;                                // how fast the block moves vertically

    int aliveAliens;                           // number of alive aliens in the array currently
    int firstColumn;                           // the index of the first column that has at least one alien
    int lastColumn;                            // the index of the last column that has at least one alien
    int lastRow;                               // the index of the last row that has at least one alien

    readonly Timer moveTimer = new Timer();

    public AlienArray(Size screenSize)
    {
        rows = 2;
        columns = 2;
        block = new Alien[rows, columns];
        this.screenSize = screenSize;
        spaceX = 20;
        spaceY = 5;

        start = (0, 130);
        location = start;
        direction = 1;
        baseSpeed = 15;
        speedX = 15;
        speedY = 35;

        FillArray();
        alienDimensions = block[0, 0].GetDimensions();

        aliveAliens = rows * columns;
        firstColumn = 0;
        lastColumn = columns - 1;
        lastRow = rows - 1;

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
        get => block[i, j];
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
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien30, (x, y), 30);
                else if (i == 1 || i == 2)
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien20, (x, y), 20);
                else
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien10, (x, y), 10);

                x = x + block[0, j].GetDimensions().width + spaceX;
            }

            y = y + block[0, 0].GetDimensions().height + spaceY;
            x = 0;
        }
    }


    public void Move()
    {
        if ((location.x + (direction * speedX) <= (0 - (firstColumn * (alienDimensions.width + 2 * spaceX)))) || 
            (location.x + (direction * speedX) >= (screenSize.Width - ((lastColumn + 3) * (alienDimensions.width)))))
        {
            location.y += speedY;
            direction *= -1;
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Descend(speedY);
        }
        else
        {
            location.x += (direction * spaceX);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Move(direction, speedX);
        }
    }


    bool IsFirstColumnDestroyed()
    {
        for (int i = 0; i < rows; ++i)
            if (block[i, firstColumn].IsAlive())
                return false;
        return true;
    }

    bool IsLastColumnDestroyed()
    {
        for (int i = 0; i < rows; ++i)
            if (block[i, lastColumn].IsAlive())
                return false;
        return true;
    }

    bool IsLastRowDestroyed()
    {
        for (int j = 0; j < columns; ++j)
            if (block[lastRow, j].IsAlive())
                return false;
        return true;
    }


    // returns the alien that has been hit if true, null otherwise
    public Alien IsHit(Bullet bullet)
    {
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                if (block[i, j].IsAlive() && block[i, j].IsHit(bullet))
                {
                    Alien hitAlien = block[i, j];
                    block[i, j].SetIsAlive(false);
                    aliveAliens--;

                    if (j == firstColumn && IsFirstColumnDestroyed())
                        firstColumn++;

                    if (j == lastColumn && IsLastColumnDestroyed())
                        lastColumn--;

                    if (i == lastRow && IsLastRowDestroyed())
                        lastRow--;

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

            if (block[randomX, randomY].IsAlive())
                return block[randomX, randomY];
        }
    }


    // returns true if the bottom existing row of the alien array touches the bottom of the screen
    public bool IsAtBottom(Size screenSize)
    {
        for (int j = 0; j < columns; ++j)
            if (block[lastRow, j].IsAlive() && block[lastRow, j].GetLocation().y + block[lastRow, j].GetDimensions().height == screenSize.Height)
                return true;
        return false;
    }


    // returns true if all the aliens in the array are destroyed
    public bool IsDestroyed() => aliveAliens == 0;

    // put the aliens back at the start of the new level
    public void Reset()
    {
        moveTimer.Enabled = false;

        speedX = baseSpeed;
        location.x = start.x + 200;
        location.y = start.y;
        start = location;

        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                block[i, j].SetIsAlive(true);

        moveTimer.Enabled = true;
    }
}