using GameTic_Tac_Toc.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTic_Tac_Toc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;

        enPlayer PlayerTurn = enPlayer.Player1;

        enum enPlayer
        {
            Player1,
            player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            InProgress
        }


        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn2.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;

                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }


                
            }

            GameStatus.GameOver = false;

            return false;
        }

        public void CheckWinner()
        {

            if (CheckValues(btn1, btn2, btn3))
                return;
            
            if (CheckValues(btn4, btn5, btn6))
                return;
            
            if (CheckValues(btn7, btn8, btn9))
                return;
            
            if (CheckValues(btn1, btn4, btn7))
                return;
            
            if (CheckValues(btn2, btn5, btn8))
                return;
            
            if (CheckValues(btn3, btn6, btn9))
                return;
            
            if (CheckValues(btn1, btn5, btn9))
                return;
            
            if (CheckValues(btn3, btn5, btn7))
                return;

        }

        public void EndGame()
        {
            lblTurn.Text = "Game Over";
            switch (GameStatus.Winner)
            {

                case enWinner.Player1:

                    lblWinner.Text = "Player1";
                    break;

                case enWinner.Player2:

                    lblWinner.Text = "Player2";
                    break;

                default:

                    lblWinner.Text = "Draw";
                    break;

            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void ChangeImage(Button btn)
        {
            if(btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                   case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.player2;
                        lblTurn.Text = "Player 1";
                        btn.Tag = "X";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;

                    case enPlayer.player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player 2";
                        btn.Tag = "O";
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                       
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255);
            Pen Pen = new Pen(White);
            Pen.Width = 15;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 400, 152, 400, 500);
            e.Graphics.DrawLine(Pen, 550, 152, 550, 500);
            e.Graphics.DrawLine(Pen, 250, 260, 700, 260);
            e.Graphics.DrawLine(Pen, 250, 380, 700, 380);

        }

        private void RestButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;

        }

        private void RestartGame()
        {
            RestButton(btn1);
            RestButton(btn2);
            RestButton(btn3);
            RestButton(btn4);
            RestButton(btn5);
            RestButton(btn6);
            RestButton(btn7);
            RestButton(btn8);
            RestButton(btn9);

            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.InProgress;
            lblWinner.Text = "In Progress";

        }

        private void btn_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
