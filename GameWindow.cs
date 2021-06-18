using System;
using System.Drawing;
using System.Windows.Forms;


public class GameWindow : Form
{
    readonly Timer gameTimer;
    readonly Player player;
    readonly Bullet bullet;         // player bullet
    readonly Bullet lazer;          // enemy bullet
    AlienArray aliens;
    readonly Bunker[] bunkers;
    readonly RandomShip ship;
    bool goLeft, goRight, shoot = false;
    float alienStart;               // the vertical position of the alien array at the start of the level

    readonly Label scoreText;
    readonly Label livesText;


    public GameWindow(int lives, int lazerSpeed)
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
            Interval = 5
        };

        gameTimer.Tick += new EventHandler(OnTick);

        alienStart = 130;

        // Classes
        player = new Player( ClientSize.Width / 2, ClientSize.Height, lives );
        bullet = new Bullet(30);
        lazer = new Bullet(lazerSpeed);
        aliens = new AlienArray(ClientSize, 0, alienStart);
        ship = new RandomShip(ClientSize);
        bunkers = new Bunker[4];
        for (int i = 0; i < bunkers.Length; ++i)
            bunkers[i] = new Bunker(120 + i * 230, 500);


        // UI
        Font labelFont = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);

        scoreText = new Label()
        {
            Location = new Point(50, 20),
            Size = new Size(255, 57),
            Text = $"SCORE: {player.GetScore()}",
            TextAlign = ContentAlignment.TopLeft,
            Font = labelFont,
            BackColor = Color.Black,
            ForeColor = Color.White
        };

        livesText = new Label()
        {
            Location = new Point((int)(ClientSize.Width - labelFont.Size * 8), 20),
            Size = new Size(255, 57),
            Text = $"LIVES: {player.GetLives()}",
            TextAlign = ContentAlignment.TopLeft,
            Font = labelFont,
            BackColor = Color.Black,
            ForeColor = Color.White
        };


        // add labels to the form
        Controls.Add(scoreText);
        Controls.Add(livesText);

        // event to shut down the entire program when the window is closed
        FormClosing += new FormClosingEventHandler(GameWindow_FormClosing);
    }

    private void OnTick(object sender, EventArgs e)
    {
        if (goLeft)
            player.Move(-1, ClientSize);

        if (goRight)
            player.Move(1, ClientSize);

        BulletAction();

        LazerAction();

        if (ship.IsAlive())
            ship.Move();

        if (aliens.IsDestroyed())             // new level starts, reset the alien array, at a lower position
        {
            alienStart += 100;
            aliens = new AlienArray(ClientSize, 0, alienStart);
        }

        if (!aliens.IsDestroyed() && aliens.IsAtBottom())
            GameOver();

        Invalidate();       // redraw screen
    }

    private void GameOver()
    {
        gameTimer.Enabled = false;
        GameOverScreen g = new GameOverScreen();
        g.Show();
    }

    private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        Application.Exit();
    }



    /* --  ACTION  -- */
    void BulletAction()
    {
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
                scoreText.Text = $"SCORE: {player.GetScore()}";
                scoreText.Refresh();
            }

            if (ship.IsHit(bullet))
            {
                bullet.Destroy();
                player.AddScore(ship.GetPoints());
                scoreText.Text = $"SCORE: {player.GetScore()}";
                scoreText.Refresh();
            }

            foreach (Bunker bunker in bunkers)
                if (bunker.IsHit(bullet))
                    bullet.Destroy();
        }
    }

    void LazerAction()
    {
        if (!lazer.IsAlive())
            lazer.Shoot(aliens.GetAttackingAlien().GetMuzzleLocation());

        if (lazer.IsAlive())
        {
            lazer.Move(1, ClientSize);
            if (player.IsHit(lazer))
            {
                lazer.Destroy();
                player.TakeDamage();
                livesText.Text = $"LIVES: {player.GetLives()}";
                livesText.Refresh();
                if (player.GetLives() == 0)
                    GameOver();
            }

            foreach (Bunker bunker in bunkers)
                if (bunker.IsHit(lazer))
                    lazer.Destroy();
        }
    }


    /* --  GRAPHICS  -- */
    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // do nothing
        // overriden to eliminate the flicker when the screen is redrawn
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;

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
        foreach (Bunker bunker in bunkers)
            foreach (BunkerPiece piece in bunker.GetPieces())
                if (piece.IsAlive())
                    g.DrawImage(piece.GetSprite(), piece.GetLocation().x, piece.GetLocation().y,
                                piece.GetDimensions().width, piece.GetDimensions().height);

        // draw aliens
        for (int i = 0; i < aliens.rows; ++i)
            for (int j = 0; j < aliens.columns; ++j)
                if (aliens[i, j].IsAlive())
                    g.DrawImage(aliens[i, j].GetSprite(), aliens[i, j].GetLocation().x, aliens[i, j].GetLocation().y,
                                aliens[i, j].GetDimensions().width, aliens[i, j].GetDimensions().height);

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
