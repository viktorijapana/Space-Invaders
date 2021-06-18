using System.Drawing;

class Alien : Actor
{
    readonly int points;                             // how many points this alien is worth

    public Alien(Bitmap sprite, float startX, float startY, int points) : base()
    {
        this.sprite = sprite;
        width = 40;
        height = 25;

        x = startX;
        y = startY;

        isAlive = true;
        this.points = points;
    }


    /*  -- MOVEMENT --  */
    public void Move(int direction, float speed)
    {
        x += (direction * speed);
        muzzleX = x + (width / 2);
        muzzleY = y;
    }
    
    public void Descend(int speed)
    {
        y += speed;
    }



    /*  -- ATTACK --  */
    public int GetPoints() => points;

    public void Kill()
    {
        isAlive = false;
    }
}
