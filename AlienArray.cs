﻿using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;


// TODO reset: we can do it but just wiping this instance and creating a new one? Yeah, that would be best
class AlienArray
{
    public readonly int rows;
    public readonly int columns;
    readonly Alien[,] block;
    readonly Size screenSize;
    readonly int spaceX;                       // pixels between aliens horizontally
    readonly int spaceY;                       // pixels between aliens vertically
    readonly (int width, int height) alienDimensions;

    int direction;                             // 1 = right, -1 = left
    float speedX;                              // how fast the block moves horizontally
    int speedY;                                // how fast the block moves vertically
    bool hasDescended;

    int aliveAliens;                           // number of alive aliens in the array currently
    int firstColumn;                           // the index of the first column that has at least one alien
    int lastColumn;                            // the index of the last column that has at least one alien
    int lastRow;                               // the index of the last row that has at least one alien
    (float x, float y) firstColLoc;            // location of the left edge of the first full column of aliens
    (float x, float y) lastColLoc;             // location of the right edge of the last full column of aliens
    float lastRowLoc;                          // location of the bottom edge of the last full row of aliens

    readonly Timer moveTimer = new Timer();


    public AlienArray(Size screenSize, (float x, float y) startLoc)
    {
        rows = 5;
        columns = 11;
        block = new Alien[rows, columns];
        this.screenSize = screenSize;
        spaceX = 20;
        spaceY = 5;

        direction = 1;
        speedX = 15;
        speedY = 35;
        hasDescended = true;

        firstColLoc = startLoc;

        FillArray();
        alienDimensions = block[0, 0].GetDimensions();

        lastColLoc = ((columns * alienDimensions.width) + ((columns - 1) * spaceX), firstColLoc.y);
        lastRowLoc = firstColLoc.y + (rows * alienDimensions.height) + ((rows - 1) * spaceY);

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
        float x = firstColLoc.x;       // starting location
        float y = firstColLoc.y;

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
        if ((firstColLoc.x <= 0 || lastColLoc.x >= screenSize.Width) && !hasDescended)
        {
            firstColLoc.y += speedY;
            lastColLoc.y += speedY;
            direction *= -1;
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Descend(speedY);
            hasDescended = true;
        }
        else
        {                    
            firstColLoc.x += (direction * speedX);
            lastColLoc.x += (direction * speedX);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    if (block[i, j].IsAlive())
                        block[i, j].Move(direction, speedX);

            if (hasDescended) hasDescended = false;
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
                    speedX += 1;

                    if (j == firstColumn && aliveAliens != 0)
                        while (IsFirstColumnDestroyed())
                        {
                            firstColumn++;
                            firstColLoc.x = firstColLoc.x + alienDimensions.width + spaceX;
                        }

                    if (j == lastColumn && aliveAliens != 0)
                        while (IsLastColumnDestroyed())
                        {
                            lastColumn--;
                            lastColLoc.x = lastColLoc.x - alienDimensions.width - spaceX;
                        }

                    if (i == lastRow && aliveAliens != 0)
                        while (IsLastRowDestroyed())
                        {
                            lastRow--;
                            lastRowLoc = lastRowLoc - alienDimensions.height - spaceY;
                        }

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
    public bool IsAtBottom() => lastRowLoc >= screenSize.Height;


    // returns true if all the aliens in the array are destroyed
    public bool IsDestroyed() => aliveAliens == 0;
}