using System.Drawing;

class Alien : Actor
{
    readonly int points;                             // how many points this alien is worth


    public Alien(Bitmap sprite, (float x, float y) location, int points) : base()
    {
        this.sprite = sprite;
        dimensions = (40, 25);

        this.location = location;

        isAlive = true;
        this.points = points;
    }


    /*  -- MOVEMENT --  */
    public void Move(int direction, float speed)
    {
        location.x += (direction * speed);
        muzzleLocation = (location.x + (dimensions.width / 2), location.y);
    }
    
    public void Descend(int speed)
    {
        location.y += speed;
    }



    /*  -- ATTACK --  */
    public int GetPoints() => points;

    public void SetIsAlive(bool state)
    {
        isAlive = state;
    }
}
