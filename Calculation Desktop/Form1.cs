using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculation_Desktop
{
    public partial class Form1 : Form
    {
        // Colors for theme
        private Color PrimaryColor = Color.FromArgb(63, 81, 181);         // #3F51B5
        private Color PrimaryDarkColor = Color.FromArgb(48, 63, 159);     // #303F9F
        private Color AccentColor = Color.FromArgb(255, 87, 34);          // #FF5722
        private Color BackgroundColor = Color.FromArgb(250, 250, 250);    // #FAFAFA
        private Color TextColor = Color.FromArgb(33, 33, 33);             // #212121

        private ProgressBar progressBar;

        public Form1()
        {
            InitializeComponent();
            ApplyCustomStyles();
        }

        private void ApplyCustomStyles()
        {
            // Set Form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BackgroundColor;
            this.Size = new Size(900, 600);

            // Add custom title bar
            Panel titleBar = CreateTitleBar();
            this.Controls.Add(titleBar);

            // Add main content area
            Panel mainPanel = CreateMainPanel();
            this.Controls.Add(mainPanel);

            // Add custom progress bar (initially hidden)
            progressBar = CreateProgressBar();
            this.Controls.Add(progressBar);
        }

        private Panel CreateTitleBar()
        {
            // Title bar
            Panel titleBar = new Panel
            {
                BackColor = PrimaryColor,
                Dock = DockStyle.Top,
                Height = 50
            };

            // Title label
            Label titleLabel = new Label
            {
                Text = "Calculation App",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 12)
            };
            titleBar.Controls.Add(titleLabel);

            // Add control buttons
            AddControlButtons(titleBar);

            return titleBar;
        }

        private void AddControlButtons(Panel titleBar)
        {
            // Close Button
            Button closeButton = new Button
            {
                Text = "X",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Red,
                Size = new Size(40, 40),
                Location = new Point(this.Width - 50, 5),
                FlatStyle = FlatStyle.Flat
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => this.Close();
            titleBar.Controls.Add(closeButton);

            // Minimize Button
            Button minimizeButton = new Button
            {
                Text = "-",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = PrimaryDarkColor,
                Size = new Size(40, 40),
                Location = new Point(this.Width - 90, 5),
                FlatStyle = FlatStyle.Flat
            };
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar.Controls.Add(minimizeButton);
        }

        private Panel CreateMainPanel()
        {
            // Main Panel
            Panel mainPanel = new Panel
            {
                Size = new Size(800, 400),
                BackColor = Color.White,
                Location = new Point((this.Width - 800) / 2, 80),
                BorderStyle = BorderStyle.None
            };
            mainPanel.Paint += (s, e) => DrawRoundedRectangle(e.Graphics, mainPanel.ClientRectangle, 20, Color.LightGray);

            // Welcome and description labels
            AddLabels(mainPanel);

            // "Get Started" button
            Button getStartedButton = CreateGetStartedButton(mainPanel);
            mainPanel.Controls.Add(getStartedButton);

            return mainPanel;
        }

        private void AddLabels(Panel mainPanel)
        {
            // Welcome label
            Label welcomeLabel = new Label
            {
                Text = "Welcome to Calculation App!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = PrimaryDarkColor,
                AutoSize = true,
                Location = new Point((mainPanel.Width - 400) / 2, 30)
            };
            mainPanel.Controls.Add(welcomeLabel);

            // Description label
            Label descriptionLabel = new Label
            {
                Text = "Perform advanced calculations and manage your tasks with ease.",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = TextColor,
                AutoSize = true,
                MaximumSize = new Size(700, 0),
                Location = new Point((mainPanel.Width - 700) / 2, 80)
            };
            mainPanel.Controls.Add(descriptionLabel);
        }

        private Button CreateGetStartedButton(Panel mainPanel)
        {
            Button getStartedButton = new Button
            {
                Text = "Get Started",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(200, 50),
                Location = new Point((mainPanel.Width - 200) / 2, 200),
                BackColor = AccentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            getStartedButton.FlatAppearance.BorderSize = 0;
            getStartedButton.MouseEnter += (s, e) => getStartedButton.BackColor = Color.FromArgb(230, 81, 0); // Darker Accent Color
            getStartedButton.MouseLeave += (s, e) => getStartedButton.BackColor = AccentColor; // Reset to original
            getStartedButton.Click += GetStartedButton_Click;

            return getStartedButton;
        }

        private async void GetStartedButton_Click(object sender, EventArgs e)
        {
            // Show progress bar
            progressBar.Visible = true;

            // Simulate a loading process
            await System.Threading.Tasks.Task.Delay(3000); // Simulate a 3-second load

            // Hide progress bar
            progressBar.Visible = false;

            // Navigate to the next form
            Form2 nextForm = new Form2();
            nextForm.Show();

            // Close current form
            this.Hide();
        }

        private ProgressBar CreateProgressBar()
        {
            return new ProgressBar
            {
                Size = new Size(400, 20),
                Location = new Point((this.Width - 400) / 2, 500),
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Visible = false // Initially hidden
            };
        }

        private void DrawRoundedRectangle(Graphics graphics, Rectangle bounds, int radius, Color borderColor)
        {
            using (var pen = new Pen(borderColor, 2))
            {
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int diameter = radius * 2;
                    path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                    path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                    path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                    path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                    path.CloseFigure();

                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
