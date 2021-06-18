using System.Drawing;

delegate void Notify();

class Player : Actor
{
    int score;
    int lives;

    public event Notify GameOver;
    

    public Player((float x, float y) start, int lives) : base()
    {
        sprite = new Bitmap(Space_Invaders.Properties.Resources.player);
        dimensions = (45, 30);

        // Movement
        location = (start.x, start.y - dimensions.height);
        speed = 10;

        // Attack
        score = 0;
        this.lives = lives;
    }



    /* --  MOVEMENT  -- */
    public void Move(int direction, Size screenSize)         // -1 = left, +1 = right
    {
        float new_location = location.x + (direction * speed);
        if (new_location >= 0 && new_location <= (screenSize.Width - dimensions.width))
            location.x = new_location;

        // update muzzle location
        muzzleLocation = (this.location.x + (this.dimensions.width / 2), this.location.y);
    }



    /* --  ATTACK  -- */
    public int GetScore() => score;

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetLives() => lives;

    public override bool IsHit(Bullet bullet)
    {
        float x = bullet.GetLocation().x;
        float y = bullet.GetLocation().y;
        if (x >= location.x && x <= location.x + dimensions.width && y <= location.y + dimensions.height && y >= location.y)
        {
            lives--;
            if (lives == 0)
                GameOver();
            return true;
        }
        return false;
    }
}