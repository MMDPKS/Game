using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game;

namespace Rabochiy
{
    public partial class Form2 : Form
        {
            const int mapSize = 8;
            const int cellSize = 100;
            int currentPlayer;
            Button prevButton;

            bool isMoving;
            int[,] map = new int[mapSize, mapSize];
            Button[,] buttons = new Button[mapSize, mapSize];
            Image whiteFigure;
            Image blackFigure;
            Image goldFigure;
            public Form2()
            {
                InitializeComponent();

                whiteFigure = new Bitmap(new Bitmap(@"\Rabochiy — 2\e.png"), new Size(cellSize - 10, cellSize - 10));
                blackFigure = new Bitmap(new Bitmap(@"\Rabochiy — 2\k.png"), new Size(cellSize - 10, cellSize - 10));
                goldFigure = new Bitmap(new Bitmap(@"\Rabochiy — 2\z.png"), new Size(cellSize - 10, cellSize - 10));

                this.Text = "Checkers";

                Init();
            }

            public void Init()
            {
                currentPlayer = 1;
                isMoving = false;
                prevButton = null;


                map = new int[mapSize, mapSize] {
                { 1,2,2,0,0,0,2,3 },
                { 0,2,2,0,2,3,2,0 },
                { 0,2,0,0,2,2,2,0 },
                { 0,2,0,0,0,0,0,3 },
                { 0,0,0,0,0,0,2,0 },
                { 2,2,2,2,0,2,2,2 },
                { 0,0,0,2,0,0,0,0 },
                { 3,2,0,0,0,2,0,3 }
            };
                CreateMap();
                Button1();
                Button2();

            }

        public void Button1()
            {
                Button b1 = new Button();
                b1.Location = new System.Drawing.Point(this.ClientRectangle.Width / 1 - 1 / 2, this.ClientRectangle.Height / 2 - 80);
                b1.Size = new Size(125, 40);
                b1.TabIndex = 0;
                b1.Text = "Уровень 2";
                b1.UseVisualStyleBackColor = true;
                b1.Click += new EventHandler(button1_Click);
                Controls.Add(b1);
            }
            public void Button2()
            {
                Button b2 = new Button();
                b2.Location = new System.Drawing.Point(this.ClientRectangle.Width / 1 - 1 / 2, this.ClientRectangle.Height / 2 - 40);
                b2.Size = new Size(125, 40);
                b2.TabIndex = 0;
                b2.Text = "Restart";
                b2.UseVisualStyleBackColor = true;
                b2.Click += new EventHandler(button2_Click);
                Controls.Add(b2);
            }
            public void CreateMap()
            {
                this.Width = (mapSize + 1) * cellSize;
                this.Height = (mapSize + 1) * cellSize;

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        Button button = new Button();
                        button.Location = new Point(j * cellSize, i * cellSize);
                        button.Size = new Size(cellSize, cellSize);
                        button.Click += new EventHandler(OnFigurePress);
                        if (map[i, j] == 1)
                            button.Image = whiteFigure;
                        else if (map[i, j] == 2) button.Image = blackFigure;
                        else if (map[i, j] == 3) button.Image = goldFigure;

                        button.BackColor = GetPrevButtonColor(button);
                        button.ForeColor = Color.Red;

                        buttons[i, j] = button;

                        this.Controls.Add(button);
                    }
                }
            }
            public Color GetPrevButtonColor(Button prevButton)
            {
                if ((prevButton.Location.Y / cellSize % 2) != 0)
                {
                    if ((prevButton.Location.X / cellSize % 2) == 0)
                    {
                        return Color.Green;
                    }
                }
                if ((prevButton.Location.Y / cellSize) % 2 == 0)
                {
                    if ((prevButton.Location.X / cellSize) % 2 != 0)
                    {
                        return Color.Green;
                    }
                }
                return Color.White;
            }

            public void OnFigurePress(object sender, EventArgs e)
            {
                if (prevButton != null)
                    prevButton.BackColor = Color.White;

                Button pressedButton = sender as Button;

                if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] != 0 && map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == currentPlayer)
                {
                    CloseSteps();
                    pressedButton.BackColor = Color.Red;
                    DeactivateAllButtons();
                    pressedButton.Enabled = true;
                    ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize]);

                    if (isMoving)
                    {
                        CloseSteps();
                        pressedButton.BackColor = Color.White;
                        ActivateAllButtons();
                        isMoving = false;
                    }
                    else
                        isMoving = true;
                }
                else
                {
                    if (isMoving)
                    {
                        int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                        map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];
                        map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                        pressedButton.Image = prevButton.Image;
                        prevButton.Image = null;
                        isMoving = false;
                        CloseSteps();
                        ActivateAllButtons();
                    }
                }

                prevButton = pressedButton;
            }

            public void ShowSteps(int IcurrFigure, int JcurrFigure, int currFigure)
            {
                switch (currFigure = 1)
                {
                    case 1:
                        if (IsInsideBorders(IcurrFigure + 1, JcurrFigure))
                        {
                            if (map[IcurrFigure + 1, JcurrFigure] == 0)
                            {
                                buttons[IcurrFigure + 1, JcurrFigure].BackColor = Color.Yellow;
                                buttons[IcurrFigure + 1, JcurrFigure].Enabled = true;
                            }
                        }

                        if (IsInsideBorders(IcurrFigure - 1, JcurrFigure))
                        {
                            if (map[IcurrFigure - 1, JcurrFigure] == 0)
                            {
                                buttons[IcurrFigure - 1, JcurrFigure].BackColor = Color.Yellow;
                                buttons[IcurrFigure - 1, JcurrFigure].Enabled = true;
                            }
                        }
                        if (IsInsideBorders(IcurrFigure, JcurrFigure + 1))
                        {
                            if (map[IcurrFigure, JcurrFigure + 1] == 0)
                            {
                                buttons[IcurrFigure, JcurrFigure + 1].BackColor = Color.Yellow;
                                buttons[IcurrFigure, JcurrFigure + 1].Enabled = true;
                            }
                        }
                        if (IsInsideBorders(IcurrFigure, JcurrFigure - 1))
                        {
                            if (map[IcurrFigure, JcurrFigure - 1] == 0)
                            {
                                buttons[IcurrFigure, JcurrFigure - 1].BackColor = Color.Yellow;
                                buttons[IcurrFigure, JcurrFigure - 1].Enabled = true;
                            }
                        }
                        if (IsInsideBorders(IcurrFigure + 1, JcurrFigure))
                        {
                            if (map[IcurrFigure + 1, JcurrFigure] == 3)
                            {
                                buttons[IcurrFigure + 1, JcurrFigure].BackColor = Color.Yellow;
                                buttons[IcurrFigure + 1, JcurrFigure].Enabled = true;
                            }
                        }

                        if (IsInsideBorders(IcurrFigure - 1, JcurrFigure))
                        {
                            if (map[IcurrFigure - 1, JcurrFigure] == 3)
                            {
                                buttons[IcurrFigure - 1, JcurrFigure].BackColor = Color.Yellow;
                                buttons[IcurrFigure - 1, JcurrFigure].Enabled = true;
                            }
                        }
                        if (IsInsideBorders(IcurrFigure, JcurrFigure + 1))
                        {
                            if (map[IcurrFigure, JcurrFigure + 1] == 3)
                            {
                                buttons[IcurrFigure, JcurrFigure + 1].BackColor = Color.Yellow;
                                buttons[IcurrFigure, JcurrFigure + 1].Enabled = true;
                            }
                        }
                        if (IsInsideBorders(IcurrFigure, JcurrFigure - 1))
                        {
                            if (map[IcurrFigure, JcurrFigure - 1] == 3)
                            {
                                buttons[IcurrFigure, JcurrFigure - 1].BackColor = Color.Yellow;
                                buttons[IcurrFigure, JcurrFigure - 1].Enabled = true;
                            }
                        }
                        break;
                }
            }


            public bool DeterminePath(int IcurrFigure, int j)
            {
                if (map[IcurrFigure, j] == 0)
                {
                    buttons[IcurrFigure, j].BackColor = Color.Yellow;
                    buttons[IcurrFigure, j].Enabled = true;
                }
                else
                {
                    if (map[IcurrFigure, j] != 2)
                    {
                        buttons[IcurrFigure, j].BackColor = Color.Yellow;
                        buttons[IcurrFigure, j].Enabled = true;
                    }
                    return false;
                }
                return true;
            }

            public void CloseSteps()
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        buttons[i, j].BackColor = GetPrevButtonColor(buttons[i, j]);
                    }
                }
            }

            public bool IsInsideBorders(int ti, int tj)
            {
                if (ti >= mapSize || tj >= mapSize || ti < 0 || tj < 0)
                {
                    return false;
                }
                return true;
            }
            public void DeactivateAllButtons()
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        buttons[i, j].Enabled = false;
                    }
                }
            }

            public void ActivateAllButtons()
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        buttons[i, j].Enabled = true;
                    }
                }
            }
            private void button1_Click(object sender, EventArgs e)
            {
            Form3 f4 = new Form3();
            this.Visible = false;
            f4.ShowDialog();
        }

            private void button2_Click(object sender, EventArgs e)
            {
                this.Controls.Clear();
                Init();
            }
        }
    }