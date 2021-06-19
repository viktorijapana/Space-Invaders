using System;
using System.Drawing;
using System.Windows.Forms;


class AlienArray
{
    readonly Alien[,] block;

    // Graphics
    public readonly int rows;
    public readonly int columns;
    readonly Size screenSize;
    readonly int spaceX;                       // pixels between aliens horizontally
    readonly int spaceY;                       // pixels between aliens vertically
    readonly int width;                        // width of a single alien
    readonly int height;                       // height of a single alien

    // Movement
    int direction;                             // 1 = right, -1 = left
    readonly float speedX;                     // how fast the block moves horizontally
    readonly int speedY;                       // how fast the block moves vertically
    bool hasDescended;

    // Properties
    int aliveAliens;                           // number of alive aliens in the array currently
    int firstColumn;                           // the index of the first column that has at least one alien
    int lastColumn;                            // the index of the last column that has at least one alien
    int lastRow;                               // the index of the last row that has at least one alien
    float firstColX;                           // location of the left edge of the first full column of aliens
    float firstColY;
    float lastColX;                            // location of the right edge of the last full column of aliens
    float lastRowY;                            // location of the bottom edge of the last full row of aliens

    readonly Timer moveTimer;


    public AlienArray(Size screenSize, float startX, float startY)
    {
        // Graphics
        rows = 5;
        columns = 11;
        block = new Alien[rows, columns];
        this.screenSize = screenSize;
        spaceX = 20;
        spaceY = 5;

        // Movement
        direction = 1;
        speedX = 15;
        speedY = 35;
        hasDescended = true;

        // Properties
        firstColX = startX;
        firstColY = startY;

        FillArray();
        width = block[0, 0].GetDimensions().width;
        height = block[0, 0].GetDimensions().height;

        lastColX = (columns * width) + (columns - 1) * spaceX;
        lastRowY = firstColY + (rows * height) + (rows - 1) * spaceY;

        aliveAliens = rows * columns;
        firstColumn = 0;
        lastColumn = columns - 1;
        lastRow = rows - 1;

        moveTimer = new Timer
        {
            Enabled = true,
            Interval = 700
        };

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
        float x = firstColX;       // starting location
        float y = firstColY;

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                if (i == 0)
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien30, x, y, 30);
                else if (i == 1 || i == 2)
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien20, x, y, 20);
                else
                    block[i, j] = new Alien(Space_Invaders.Properties.Resources.alien10, x, y, 10);

                x = x + block[0, j].GetDimensions().width + spaceX;
            }

            y = y + block[0, 0].GetDimensions().height + spaceY;
            x = 0;
        }
    }


    public void Move()
    {
        if ((firstColX <= 0 || lastColX >= screenSize.Width) && !hasDescended)
        {
            firstColY += speedY;
            lastRowY += speedY;
            direction *= -1;
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Descend(speedY);
            hasDescended = true;
        }
        else
        {                    
            firstColX += (direction * speedX);
            lastColX += (direction * speedX);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Move(direction, speedX);

            if (hasDescended) 
                hasDescended = false;
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


    public Alien IsHit(Bullet bullet)
    {
        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < columns; ++j)
                if (block[i, j].IsAlive() && block[i, j].IsHit(bullet))
                {
                    Alien hitAlien = block[i, j];
                    block[i, j].Kill();
                    aliveAliens--;

                    // Check if the last columns and last row of the block have been destroyed,
                    // to adjust for hitting the end of the screen
                    if (j == firstColumn && aliveAliens != 0)
                        while (IsFirstColumnDestroyed())
                        {
                            firstColumn++;
                            firstColX = firstColX + width + spaceX;
                        }

                    if (j == lastColumn && aliveAliens != 0)
                        while (IsLastColumnDestroyed())
                        {
                            lastColumn--;
                            lastColX = lastColX - width - spaceX;
                        }

                    if (i == lastRow && aliveAliens != 0)
                        while (IsLastRowDestroyed())
                        {
                            lastRow--;
                            lastRowY = lastRowY - height - spaceY;
                        }
                    return hitAlien;
                }
        return null;
    }


    // Returns a random alien in the array that will fire the lazer
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


    // Returns true if the bottom existing row of the alien array touches the bottom of the screen
    public bool IsAtBottom() => lastRowY >= screenSize.Height;


    // Returns true if all the aliens in the array are destroyed
    public bool IsDestroyed() => aliveAliens == 0;
}