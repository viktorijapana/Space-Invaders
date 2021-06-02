using System;
using System.Drawing;
using System.Windows.Forms;

public class MainMenu : Form
{
    private readonly Label title;
    private readonly Label clickableText;
    private readonly PictureBox alienPic1;
    private readonly Label alienPic1Text;
    private readonly PictureBox alienPic2;
    private readonly Label alienPic2Text;
    private readonly PictureBox alienPic3;
    private readonly Label alienPic3Text;
    private readonly PictureBox randomShip;
    private readonly Label randomShipText;

    public MainMenu()
    {
        title = new Label();
        clickableText = new Label();
        alienPic1 = new PictureBox();
        alienPic1Text = new Label();
        alienPic2 = new PictureBox();
        alienPic2Text = new Label();
        alienPic3 = new PictureBox();
        alienPic3Text = new Label();
        randomShip = new PictureBox();
        randomShipText = new Label();
        ((System.ComponentModel.ISupportInitialize)(alienPic1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(alienPic2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(alienPic3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(randomShip)).BeginInit();
        SuspendLayout();


        /*  --  Title  --  */
        title.Location = new Point(0, 62);
        title.Size = new Size(1030, 70);
        title.Text = "Space Invaders";
        title.TextAlign = ContentAlignment.TopCenter;
        title.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        title.ForeColor = Color.White;


        /*  --  Clickable text  --  */
        clickableText.Location = new Point(270, 142);
        clickableText.Margin = new Padding(0);
        clickableText.Size = new Size(500, 70);
        clickableText.Text = "Click here to start game!";
        clickableText.TextAlign = ContentAlignment.TopLeft;
        clickableText.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        clickableText.ForeColor = Color.White;
        clickableText.MouseHover += new EventHandler(OnHover);
        clickableText.MouseLeave += new EventHandler(EndHover);
        clickableText.Click += new EventHandler(OnClick);


        /*  --  Alien pic 1  --  */
        alienPic1.Image = Space_Invaders.Properties.Resources.alien10;
        alienPic1.Location = new Point(330, 415);
        alienPic1.Size = new Size(60, 50);
        alienPic1.SizeMode = PictureBoxSizeMode.StretchImage;


        /*  --  Alien pic 1 text  --  */
        alienPic1Text.Location = new Point(420, 410);
        alienPic1Text.Margin = new Padding(0);
        alienPic1Text.Size = new Size(350, 70);
        alienPic1Text.Text = "= 10 POINTS";
        alienPic1Text.TextAlign = ContentAlignment.TopLeft;
        alienPic1Text.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        alienPic1Text.ForeColor = Color.White;


        /*  --  Alien pic 2  --  */
        alienPic2.Image = Space_Invaders.Properties.Resources.alien20;
        alienPic2.Location = new Point(330, 493);
        alienPic2.Size = new Size(60, 50);
        alienPic2.SizeMode = PictureBoxSizeMode.StretchImage;


        /*  --  Alien pic 2 text  --  */
        alienPic2Text.Location = new Point(420, 490);
        alienPic2Text.Margin = new Padding(0);
        alienPic2Text.Size = new Size(350, 70);
        alienPic2Text.Text = "= 20 POINTS";
        alienPic2Text.TextAlign = ContentAlignment.TopLeft;
        alienPic2Text.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        alienPic2Text.ForeColor = Color.White;


        /*  --  Alien pic 3  --  */
        alienPic3.Image = Space_Invaders.Properties.Resources.alien30;
        alienPic3.Location = new Point(330, 572);
        alienPic3.Size = new Size(60, 50);
        alienPic3.SizeMode = PictureBoxSizeMode.StretchImage;


        /*  --  Alien pic 3 text  --  */
        alienPic3Text.Location = new Point(420, 572);
        alienPic3Text.Margin = new Padding(0);
        alienPic3Text.Size = new Size(350, 70);
        alienPic3Text.Text = "= 30 POINTS";
        alienPic3Text.TextAlign = ContentAlignment.TopLeft;
        alienPic3Text.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        alienPic3Text.ForeColor = Color.White;


        /*  --  Random ship  --  */
        randomShip.Image = Space_Invaders.Properties.Resources.random_spaceship;
        randomShip.Location = new Point(330, 334);
        randomShip.Size = new Size(70, 50);
        randomShip.SizeMode = PictureBoxSizeMode.StretchImage;


        /*  --  Random ship text  --  */
        randomShipText.Location = new Point(420, 325);
        randomShipText.Margin = new Padding(0);
        randomShipText.Size = new Size(350, 70);
        randomShipText.Text = "= ? MYSTERY";
        randomShipText.TextAlign = ContentAlignment.TopLeft;
        randomShipText.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
        randomShipText.ForeColor = Color.White;


        /*  --  Main menu  --  */
        Name = "MainMenu";
        BackColor = Color.Black;
        ClientSize = new Size(1030, 710);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Controls.Add(title);
        Controls.Add(clickableText);
        Controls.Add(alienPic1);
        Controls.Add(alienPic1Text);
        Controls.Add(alienPic2);
        Controls.Add(alienPic2Text);
        Controls.Add(alienPic3);
        Controls.Add(alienPic3Text);
        Controls.Add(randomShip);
        Controls.Add(randomShipText);
        ((System.ComponentModel.ISupportInitialize)(alienPic1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(alienPic2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(alienPic3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(randomShip)).EndInit();
        ResumeLayout(false);
    }


    private void OnHover(object sender, System.EventArgs e)
    {
        clickableText.ForeColor = Color.Green;
    }

    private void EndHover(object sender, System.EventArgs e)
    {
        clickableText.ForeColor = Color.White;
    }

    private void OnClick(object sender, System.EventArgs e)
    {
        DifficultySelector ds = new DifficultySelector();
        Hide();
        ds.Show();
    }
}
