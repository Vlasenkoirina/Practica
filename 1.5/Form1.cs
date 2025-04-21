using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private enum ShapeType { Rectangle, Circle }
        private ShapeType currentShape = ShapeType.Rectangle;

        private readonly Color[] colors = {
            Color.Red, Color.Green, Color.Blue,
            Color.Yellow, Color.Purple, Color.Orange
        };

        private float rotationAngle = 0;
        private float scaleFactor = 1.0f;
        private float skewX = 0;
        private float skewY = 0;
        private bool isAnimating = false;

        // Элементы управления
        private Panel panelCanvas;
        private ComboBox comboShape;
        private ComboBox comboColor;
        private TrackBar trackSize;
        private TrackBar trackRotate;
        private TrackBar trackSkewX;
        private TrackBar trackSkewY;
        private CheckBox chkAnimation;
        private Button btnReset;
        private Timer timerAnimation;

        public Form1()
        {
            InitializeCustomComponents();
            SetupControls();
        }

        private void InitializeCustomComponents()
        {
            // Настройка формы
            this.Text = "Демонстратор трансформаций";
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.White;

            // Панель для рисования
            panelCanvas = new Panel
            {
                Location = new Point(200, 0),
                Size = new Size(600, 600),
                BackColor = Color.WhiteSmoke,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            panelCanvas.Paint += PanelCanvas_Paint;
            this.Controls.Add(panelCanvas);

            // ComboBox для выбора фигуры
            comboShape = new ComboBox
            {
                Location = new Point(10, 10),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboShape.Items.AddRange(new object[] { "Прямоугольник", "Круг" });
            comboShape.SelectedIndexChanged += ComboShape_SelectedIndexChanged;
            this.Controls.Add(comboShape);

            // ComboBox для выбора цвета
            comboColor = new ComboBox
            {
                Location = new Point(10, 50),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboColor.Items.AddRange(new object[] { "Красный", "Зеленый", "Синий", "Желтый", "Фиолетовый", "Оранжевый" });
            comboColor.SelectedIndexChanged += (s, e) => panelCanvas.Invalidate();
            this.Controls.Add(comboColor);

            // TrackBar для размера
            trackSize = new TrackBar
            {
                Location = new Point(10, 100),
                Width = 180,
                Minimum = 10,
                Maximum = 300,
                Value = 100,
                TickFrequency = 10
            };
            trackSize.ValueChanged += TrackBar_ValueChanged;
            this.Controls.Add(trackSize);
            this.Controls.Add(new Label { Text = "Размер", Location = new Point(10, 130) });

            // TrackBar для вращения
            trackRotate = new TrackBar
            {
                Location = new Point(10, 170),
                Width = 180,
                Minimum = 0,
                Maximum = 360,
                TickFrequency = 15
            };
            trackRotate.ValueChanged += TrackBar_ValueChanged;
            this.Controls.Add(trackRotate);
            this.Controls.Add(new Label { Text = "Вращение", Location = new Point(10, 200) });

            // TrackBar для наклона по X
            trackSkewX = new TrackBar
            {
                Location = new Point(10, 240),
                Width = 180,
                Minimum = -50,
                Maximum = 50
            };
            trackSkewX.ValueChanged += TrackBar_ValueChanged;
            this.Controls.Add(trackSkewX);
            this.Controls.Add(new Label { Text = "Наклон X", Location = new Point(10, 270) });

            // TrackBar для наклона по Y
            trackSkewY = new TrackBar
            {
                Location = new Point(10, 300),
                Width = 180,
                Minimum = -50,
                Maximum = 50
            };
            trackSkewY.ValueChanged += TrackBar_ValueChanged;
            this.Controls.Add(trackSkewY);
            this.Controls.Add(new Label { Text = "Наклон Y", Location = new Point(10, 330) });

            // CheckBox для анимации
            chkAnimation = new CheckBox
            {
                Text = "Анимация",
                Location = new Point(10, 370),
                Width = 180
            };
            chkAnimation.CheckedChanged += ChkAnimation_CheckedChanged;
            this.Controls.Add(chkAnimation);

            // Кнопка сброса
            btnReset = new Button
            {
                Text = "Сброс",
                Location = new Point(10, 410),
                Width = 180
            };
            btnReset.Click += BtnReset_Click;
            this.Controls.Add(btnReset);

            // Таймер анимации
            timerAnimation = new Timer
            {
                Interval = 50
            };
            timerAnimation.Tick += TimerAnimation_Tick;
        }

        private void SetupControls()
        {
            comboShape.SelectedIndex = 0;
            comboColor.SelectedIndex = 0;
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panelCanvas.BackColor);

            var centerX = panelCanvas.Width / 2;
            var centerY = panelCanvas.Height / 2;

            // Матрица трансформаций
            var transform = new Matrix();
            transform.Translate(centerX, centerY);
            transform.Rotate(rotationAngle);
            transform.Scale(scaleFactor, scaleFactor);
            transform.Shear(skewX / 10f, skewY / 10f);

            g.Transform = transform;

            // Рисуем фигуру
            using (var brush = new SolidBrush(colors[comboColor.SelectedIndex]))
            {
                if (currentShape == ShapeType.Rectangle)
                {
                    g.FillRectangle(brush, -50, -50, 100, 100);
                }
                else
                {
                    g.FillEllipse(brush, -50, -50, 100, 100);
                }
            }
        }

        private void UpdateTransforms()
        {
            scaleFactor = trackSize.Value / 100f;
            rotationAngle = trackRotate.Value;
            skewX = trackSkewX.Value;
            skewY = trackSkewY.Value;

            panelCanvas.Invalidate();
        }

        private void StartAnimation()
        {
            if (isAnimating)
            {
                timerAnimation.Start();
            }
            else
            {
                timerAnimation.Stop();
            }
        }

        // Обработчики событий
        private void ComboShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentShape = comboShape.SelectedIndex == 0 ?
                ShapeType.Rectangle : ShapeType.Circle;
            panelCanvas.Invalidate();
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateTransforms();
        }

        private void ChkAnimation_CheckedChanged(object sender, EventArgs e)
        {
            isAnimating = chkAnimation.Checked;
            StartAnimation();
        }

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            rotationAngle = (rotationAngle + 2) % 360;
            trackRotate.Value = (int)rotationAngle;
            panelCanvas.Invalidate();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            trackSize.Value = 100;
            trackRotate.Value = 0;
            trackSkewX.Value = 0;
            trackSkewY.Value = 0;
            chkAnimation.Checked = false;
            UpdateTransforms();
        }
    }
}