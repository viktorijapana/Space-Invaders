using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;


public class GameWindow : Form
{
    readonly Timer gameTimer;
    readonly Player player;
    readonly Bullet bullet;         // player bullet
    readonly Bullet lazer;          // enemy bullet
    readonly RandomShip ship;
    AlienArray aliens;
    Bunker[] bunkers;
    bool goLeft, goRight, shoot = false;

    readonly Label scoreText;
    readonly Label livesText;


    public GameWindow(int lives)
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
            Interval = 2
        };

        gameTimer.Tick += new EventHandler(OnTick);

        // Classes
        player = new Player((ClientSize.Width / 2, ClientSize.Height), lives);
        player.GameOver += GameOver;
        bullet = new Bullet(30);
        lazer = new Bullet(18);
        ship = new RandomShip(ClientSize);
        aliens = new AlienArray(ClientSize, (0, 130));
        bunkers = new Bunker[4];
        for (int i = 0; i < bunkers.Length; ++i)
            bunkers[i] = new Bunker( (120 + i * 230, 500) );

        // UI
        Font textFont = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);

        scoreText = new Label()
        {
            Location = new Point(50, 20),
            Size = new Size(255, 57),
            Text = $"SCORE: {player.GetScore()}",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            BackColor = Color.Black,
            ForeColor = Color.White
        };

        livesText = new Label()
        {
            Location = new Point((int)(ClientSize.Width - textFont.Size * 8), 20),
            Size = new Size(255, 57),
            Text = $"LIVES: {player.GetLives()}",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            BackColor = Color.Black,
            ForeColor = Color.White
        };


        // add labels to the form
        Controls.Add(scoreText);
        Controls.Add(livesText);
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

            foreach (Bunker bunker in bunkers)
                if (bunker.IsHit(bullet) != null)
                    bullet.Destroy();
        }

        if (!lazer.IsAlive())
            lazer.Shoot(aliens.GetAttackingAlien().GetMuzzleLocation());

        if (lazer.IsAlive())
        {
            lazer.Move(1, ClientSize);
            if (player.IsHit(lazer))
                lazer.Destroy();

            foreach (Bunker bunker in bunkers)
                if (bunker.IsHit(lazer) != null)
                    lazer.Destroy();
        }

        if (ship.IsAlive())
            ship.Move();

        if (aliens.IsDestroyed())           // new level starts, reset the alien array, at a lower position
            aliens = new AlienArray(ClientSize, (0, 130));

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



    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // do nothing
        // overriden to eliminate the flicker when we redraw the screen
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
