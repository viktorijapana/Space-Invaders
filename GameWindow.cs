using System;
using System.Drawing;
using System.Windows.Forms;


public class GameWindow : Form
{
    readonly Timer gameTimer;
    readonly Player player;
    readonly Bullet bullet;         // player bullet
    readonly Bullet lazer;          // enemy bullet
    readonly AlienArray aliens;
    readonly RandomShip ship;
    bool goLeft, goRight, shoot = false;

    public GameWindow()
    {
        // Initialization
        Text = "Space Invaders";
        ClientSize = new Size(1030, 710);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        // Drawing graphics
        SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Opaque, true);

        // Timer 
        gameTimer = new Timer()
        {
            Enabled = true,
            Interval = 10
        };

        gameTimer.Tick += new EventHandler(OnTick);

        // Classes
        player = new Player((ClientSize.Width / 2, ClientSize.Height), 3);
        player.GameOver += GameOver;
        bullet = new Bullet();
        lazer = new Bullet();
        aliens = new AlienArray(ClientSize);
        ship = new RandomShip(ClientSize);
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (goLeft)
            player.Move(-1, ClientSize);

        if (goRight)
            player.Move(1, ClientSize);

        if (shoot && !bullet.IsAlive())
            bullet.Shoot(player.GetMuzzleLocation());

        if (bullet.IsAlive())
        {
            bullet.Move(-1, ClientSize);
            Alien hitAlien = aliens.IsHit(bullet);
            if (hitAlien != null)
            {
                bullet.Destroy();
                player.AddScore(hitAlien.GetPoints());
            }

            if (ship.IsHit(bullet))
            {
                bullet.Destroy();
                player.AddScore(ship.GetPoints());
            }
        }

        //if (!lazer.IsAlive())
        //    lazer.Shoot(aliens.GetAttackingAlien().GetMuzzleLocation());

        //if (lazer.IsAlive())
        //{
        //    lazer.Move(1, ClientSize);
        //    if (player.IsHit(lazer))
        //        lazer.Destroy();
        //}

        if (ship.IsAlive())
            ship.Move();

        if (aliens.IsDestroyed())
            aliens.Reset();

        if (!aliens.IsDestroyed() && aliens.IsAtBottom(ClientSize))
            GameOver();

        Invalidate();       // redraw screen
    }

    private void GameOver()
    {
        gameTimer.Enabled = false;
    }



    /* --  DRAWING GRAPHICS  -- */
    private void DrawHUD(Graphics g)
    {
        int y = 20;     // the distance from the top of the screen
        Font drawFont = new Font("Segoe UI", 30);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);

        g.DrawString("SCORE:", drawFont, whiteBrush, new Point(50, y));
        g.DrawString(player.GetScore().ToString(), drawFont, greenBrush, new Point((int)drawFont.Size * 7, y));

        g.DrawString("LIVES:", drawFont, whiteBrush, new Point((int)(ClientSize.Width - drawFont.Size * 8), y));
        g.DrawString(player.GetLives().ToString(), drawFont, greenBrush, new Point((int)(ClientSize.Width - drawFont.Size * 3), y));
    }

    private void DrawAliens(Graphics g)
    {
        for (int i = 0; i < aliens.rows; ++i)
            for (int j = 0; j < aliens.columns; ++j)
                if (aliens[i, j].IsAlive())
                    g.DrawImage(aliens[i, j].GetSprite(), aliens[i, j].GetLocation().x, aliens[i, j].GetLocation().y,
                                aliens[i, j].GetDimensions().width, aliens[i, j].GetDimensions().height);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // do nothing
        // overriden to eliminate the flicker when we redraw the screen
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        DrawHUD(g);

        // draw the player
        g.DrawImage(player.GetSprite(), player.GetLocation().x, player.GetLocation().y, 
                    player.GetDimensions().width, player.GetDimensions().height);

        // draw bullets
        if (bullet.IsAlive())
            g.DrawImage(bullet.GetSprite(), bullet.GetLocation().x, bullet.GetLocation().y, 
                        bullet.GetDimensions().width, bullet.GetDimensions().height);

        if (lazer.IsAlive())
            g.DrawImage(lazer.GetSprite(), lazer.GetLocation().x, lazer.GetLocation().y,
                        lazer.GetDimensions().width, lazer.GetDimensions().height);

        // draw bunkers

        // draw aliens
        DrawAliens(g);

        // draw random ship
        if (ship.IsAlive())
            g.DrawImage(ship.GetSprite(), ship.GetLocation().x, ship.GetLocation().y,
                        ship.GetDimensions().width, ship.GetDimensions().height);
    }


    /* --  INPUT  -- */
    protected override void OnKeyDown(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                goLeft = true;
                break;
            case Keys.Right:
                goRight = true;
                break;
            case Keys.Space:
                shoot = true;
                break;
        }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left:
                goLeft = false;
                break;
            case Keys.Right:
                goRight = false;
                break;
            case Keys.Space:
                shoot = false;
                break;
        }
    }
}
