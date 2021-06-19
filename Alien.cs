using System.Drawing;

class Alien : Actor
{
    // Action
    protected float muzzleX;                        // x coordinate of the exit point of the bullet
    protected float muzzleY;                        // y coordinate of the exit point of the bullet
    readonly int points;                            // how many points this alien is worth

    public Alien(Bitmap sprite, float startX, float startY, int points)
    {
        // Graphics
        this.sprite = sprite;
        width = 40;
        height = 25;

        // Movement
        x = startX;
        y = startY;

        // Action
        isAlive = true;
        muzzleX = x + (width / 2);
        muzzleY = y;
        this.points = points;
    }


    /*  -- MOVEMENT --  */
    public void Move(int direction, float speed)
    {
        x += (direction * speed);
        
        // Update the location of the muzzle
        muzzleX = x + (width / 2);
        muzzleY = y;
    }
    
    public void Descend(int speed)
    {
        y += speed;
    }



    /*  -- ACTION --  */
    public (float x, float y) GetMuzzleLocation() => (muzzleX, muzzleY);

    public int GetPoints() => points;

    public void Kill()
    {
        isAlive = false;
    }
}
