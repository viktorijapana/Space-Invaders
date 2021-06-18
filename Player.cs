using System.Drawing;


class Player : Actor
{
    int score;
    int lives;

    public Player(float startX, float startY, int lives) : base()
    {
        sprite = new Bitmap(Space_Invaders.Properties.Resources.player);
        width = 45;
        height = 30;

        // Movement
        x = startX;
        y = startY - height;
        speed = 10;

        // Attack
        score = 0;
        this.lives = lives;
    }



    /* --  MOVEMENT  -- */
    public void Move(int direction, Size screenSize)         // -1 = left, +1 = right
    {
        float newLocation = x + (direction * speed);
        if (newLocation >= 0 && newLocation <= (screenSize.Width - width))
            x = newLocation;

        // update muzzle location
        muzzleX = x + width / 2;
        muzzleY = y;
    }



    /* --  ATTACK  -- */
    public int GetScore() => score;

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetLives() => lives;

    public void TakeDamage()
    {
        lives--;
    }
}