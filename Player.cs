using System.Drawing;


class Player : Actor
{
    // Movement
    protected int speed;

    // Action
    protected float muzzleX;
    protected float muzzleY;
    int score;
    int lives;

    public Player(float startX, float startY, int lives)
    {
        // Graphics
        sprite = new Bitmap(Space_Invaders.Properties.Resources.player);
        width = 45;
        height = 30;

        // Movement
        x = startX;
        y = startY - height;
        speed = 10;

        // Action
        muzzleX = x + (width / 2);
        muzzleY = y;
        score = 0;
        this.lives = lives;
    }



    /* --  MOVEMENT  -- */
    public void Move(int direction, Size screenSize)         // -1 = left, +1 = right
    {
        float newLocation = x + (direction * speed);
        if (newLocation >= 0 && newLocation <= (screenSize.Width - width))
            x = newLocation;

        // Update muzzle location
        muzzleX = x + width / 2;
        muzzleY = y;
    }



    /* --  ACTION  -- */
    public (float x, float y) GetMuzzleLocation() => (muzzleX, muzzleY);

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