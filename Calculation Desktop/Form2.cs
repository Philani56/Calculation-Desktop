using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculation_Desktop
{
    public partial class Form2 : Form
    {
        private Color PrimaryColor = Color.FromArgb(63, 81, 181);         // #3F51B5
        private Color PrimaryDarkColor = Color.FromArgb(48, 63, 159);     // #303F9F
        private Color AccentColor = Color.FromArgb(255, 87, 34);          // #FF5722
        private Color BackgroundColor = Color.FromArgb(250, 250, 250);    // #FAFAFA
        private Color TextColor = Color.FromArgb(33, 33, 33);             // #212121

        public Form2()
        {
            InitializeComponent();
            ApplyCustomStyles();
        }

        private void ApplyCustomStyles()
        {
            // Set Form properties
            this.FormBorderStyle = FormBorderStyle.None; // Remove default borders
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BackgroundColor;
            this.Size = new Size(900, 600);

            // Add a custom title bar with smooth hover effects
            Panel titleBar = new Panel
            {
                BackColor = PrimaryColor,
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(titleBar);

            // Add a back button to navigate to Form1 with a hover effect
            Button backButton = new Button
            {
                Text = "<",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = PrimaryDarkColor,
                Size = new Size(50, 40),
                Location = new Point(10, 5),
                FlatStyle = FlatStyle.Flat
            };
            backButton.FlatAppearance.BorderSize = 0;
            backButton.MouseEnter += (s, e) => backButton.BackColor = Color.FromArgb(33, 43, 123); // Hover effect
            backButton.MouseLeave += (s, e) => backButton.BackColor = PrimaryDarkColor;
            backButton.Click += (s, e) =>
            {
                if (MessageBox.Show("Are you sure you want to go back?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Close();
                }
            };
            titleBar.Controls.Add(backButton);

            // Add a heading label for the title
            Label headingLabel = new Label
            {
                Text = "Advanced Operations",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point((this.Width - 300) / 2, 10)
            };
            titleBar.Controls.Add(headingLabel);

            // Main panel for operations
            Panel mainPanel = new Panel
            {
                Size = new Size(800, 400),
                BackColor = Color.White,
                Location = new Point((this.Width - 800) / 2, 100),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10)
            };
            mainPanel.Paint += (s, e) => DrawRoundedRectangle(e.Graphics, mainPanel.ClientRectangle, 20, Color.LightGray);
            this.Controls.Add(mainPanel);

            // Dropdown label
            Label dropdownLabel = new Label
            {
                Text = "Select Operation:",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = PrimaryDarkColor,
                AutoSize = true,
                Location = new Point(30, 50)
            };
            mainPanel.Controls.Add(dropdownLabel);

            // Dropdown list for operations with tooltips
            ComboBox operationsDropdown = new ComboBox
            {
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = TextColor,
                BackColor = BackgroundColor,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Size = new Size(400, 40),
                Location = new Point(30, 100)
            };
            operationsDropdown.Items.AddRange(new object[] {
                "Perimeter of Rectangle",
                "Area of Rectangle",
                "Area of Circle",
                "Volume of Square",
                "Volume of Cone",
                "Surface Area of Cylinder",
                "Surface Area of Sphere",
                "Volume of Rectangular Prism"
            });
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(operationsDropdown, "Choose an operation to perform");
            mainPanel.Controls.Add(operationsDropdown);

            // Input fields
            Label inputLabel = new Label
            {
                Text = "Enter the Values:",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = PrimaryDarkColor,
                AutoSize = true,
                Location = new Point(30, 150)
            };
            mainPanel.Controls.Add(inputLabel);

            TextBox inputTextBox1 = new TextBox
            {
                Font = new Font("Segoe UI", 14),
                ForeColor = TextColor,
                Size = new Size(200, 35),
                Location = new Point(30, 200),
                PlaceholderText = "Enter value 1"
            };
            mainPanel.Controls.Add(inputTextBox1);

            TextBox inputTextBox2 = new TextBox
            {
                Font = new Font("Segoe UI", 14),
                ForeColor = TextColor,
                Size = new Size(200, 35),
                Location = new Point(250, 200),
                PlaceholderText = "Enter value 2"
            };
            mainPanel.Controls.Add(inputTextBox2);

            // Result label
            Label resultLabel = new Label
            {
                Text = "Result: ",
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = PrimaryDarkColor,
                AutoSize = true,
                Location = new Point(30, 300)
            };
            mainPanel.Controls.Add(resultLabel);

            // Calculate button with hover effects and tooltips
            Button calculateButton = new Button
            {
                Text = "Calculate",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = AccentColor,
                Size = new Size(150, 40),
                Location = new Point(30, 250),
                FlatStyle = FlatStyle.Flat
            };
            calculateButton.FlatAppearance.BorderSize = 0;
            calculateButton.MouseEnter += (s, e) => calculateButton.BackColor = Color.FromArgb(255, 120, 34); // Hover effect
            calculateButton.MouseLeave += (s, e) => calculateButton.BackColor = AccentColor;
            calculateButton.Click += (s, e) => PerformCalculation(operationsDropdown.SelectedItem?.ToString(), inputTextBox1, inputTextBox2, resultLabel);
            toolTip.SetToolTip(calculateButton, "Click to calculate the result");
            mainPanel.Controls.Add(calculateButton);
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

        private double GetInput(string inputText)
        {
            if (double.TryParse(inputText, out double value))
            {
                return value;
            }
            else
            {
                throw new InvalidOperationException("Please enter a valid numeric value.");
            }
        }

        private void PerformCalculation(string operation, TextBox inputTextBox1, TextBox inputTextBox2, Label resultLabel)
        {
            try
            {
                double result = 0;

                // Input validation
                if (string.IsNullOrWhiteSpace(inputTextBox1.Text) || string.IsNullOrWhiteSpace(inputTextBox2.Text))
                {
                    MessageBox.Show("Please enter both values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double value1 = GetInput(inputTextBox1.Text);
                double value2 = GetInput(inputTextBox2.Text);

                // Perform calculation based on selected operation
                switch (operation)
                {
                    case "Perimeter of Rectangle":
                        result = 2 * (value1 + value2);
                        break;
                    case "Area of Rectangle":
                        result = value1 * value2;
                        break;
                    case "Area of Circle":
                        result = Math.PI * value1 * value1;
                        break;
                    case "Volume of Square":
                        result = Math.Pow(value1, 3);
                        break;
                    case "Volume of Cone":
                        result = (1.0 / 3.0) * Math.PI * value1 * value1 * value2;
                        break;
                    case "Surface Area of Cylinder":
                        result = 2 * Math.PI * value1 * (value1 + value2);
                        break;
                    case "Surface Area of Sphere":
                        result = 4 * Math.PI * value1 * value1;
                        break;
                    case "Volume of Rectangular Prism":
                        result = value1 * value2 * value2; // Using value2 for height here for simplicity
                        break;
                    default:
                        MessageBox.Show("Please select a valid operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Display result
                resultLabel.Text = $"Result: {result:F2}";
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
