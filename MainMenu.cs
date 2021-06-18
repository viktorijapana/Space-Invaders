using System;
using System.Drawing;
using System.Windows.Forms;

public class MainMenu : Form
{
    readonly Label title;
    readonly Label clickableText;
    readonly PictureBox alienPic1;
    readonly Label alienPic1Text;
    readonly PictureBox alienPic2;
    readonly Label alienPic2Text;
    readonly PictureBox alienPic3;
    readonly Label alienPic3Text;
    readonly PictureBox randomShip;
    readonly Label randomShipText;

    public MainMenu()
    {
        Font textFont = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);

        title = new Label()
        {
            Location = new Point(0, 62),
            Size = new Size(1030, 70),
            Text = "Space Invaders",
            TextAlign = ContentAlignment.TopCenter,
            Font = textFont,
            ForeColor = Color.White
        };

        clickableText = new Label() 
        {
            Location = new Point(270, 142),
            Margin = new Padding(0),
            Size = new Size(500, 70),
            Text = "Click here to start game!",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            ForeColor = Color.White
        };
        clickableText.MouseHover += new EventHandler(OnHover);
        clickableText.MouseLeave += new EventHandler(EndHover);
        clickableText.Click += new EventHandler(OnClick);

        randomShip = new PictureBox()
        {
            Image = Space_Invaders.Properties.Resources.random_spaceship,
            Location = new Point(330, 334),
            Size = new Size(70, 50),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        randomShipText = new Label()
        {
            Location = new Point(420, 325),
            Margin = new Padding(0),
            Size = new Size(350, 70),
            Text = "= ? MYSTERY",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            ForeColor = Color.White
        };

        alienPic1 = new PictureBox()
        {
            Image = Space_Invaders.Properties.Resources.alien10,
            Location = new Point(330, 415),
            Size = new Size(60, 50),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        alienPic1Text = new Label() 
        {
            Location = new Point(420, 410),
            Margin = new Padding(0),
            Size = new Size(350, 70),
            Text = "= 10 POINTS",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            ForeColor = Color.White
        };

        alienPic2 = new PictureBox()
        {
            Image = Space_Invaders.Properties.Resources.alien20,
            Location = new Point(330, 493),
            Size = new Size(60, 50),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        alienPic2Text = new Label()
        {
            Location = new Point(420, 490),
            Margin = new Padding(0),
            Size = new Size(350, 70),
            Text = "= 20 POINTS",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            ForeColor = Color.White
        };

        alienPic3 = new PictureBox()
        {
            Image = Space_Invaders.Properties.Resources.alien30,
            Location = new Point(330, 572),
            Size = new Size(60, 50),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        alienPic3Text = new Label()
        {
            Location = new Point(420, 572),
            Margin = new Padding(0),
            Size = new Size(350, 70),
            Text = "= 30 POINTS",
            TextAlign = ContentAlignment.TopLeft,
            Font = textFont,
            ForeColor = Color.White
        };


        Name = "Space Invaders";
        BackColor = Color.Black;
        ClientSize = new Size(1030, 710);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Controls.Add(title);
        Controls.Add(clickableText);
        Controls.Add(randomShip);
        Controls.Add(randomShipText);
        Controls.Add(alienPic1);
        Controls.Add(alienPic1Text);
        Controls.Add(alienPic2);
        Controls.Add(alienPic2Text);
        Controls.Add(alienPic3);
        Controls.Add(alienPic3Text);
    }


    private void OnHover(object sender, EventArgs e)
    {
        clickableText.ForeColor = Color.Green;
    }

    private void EndHover(object sender, EventArgs e)
    {
        clickableText.ForeColor = Color.White;
    }

    private void OnClick(object sender, EventArgs e)
    {
        DifficultySelector ds = new DifficultySelector();
        Hide();
        ds.Show();
    }
}
