using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWERTY
{
    public partial class Form1 : Form
    {

        Random random = new Random();
        Stats stats = new Stats();
        
        public Form1()
        {
            InitializeComponent();
        }

    
        private void timer1_Tick(object sender, EventArgs e)
        {
            playAgainButton.Visible = false;
            //Dodanie losewej litery do kontrolki ListBox1
            listBox1.Items.Add((Keys)random.Next(65, 90));
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Game over");
                timer1.Stop();
                playAgainButton.Visible=true;
            }
        }

        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Jeżeli naciśnięto klawisz wyświetlony w Listbox, to usuwamy go i zwiększamy tempo
            if (!(listBox1.Items.Contains("Game over")))
            {
                if (listBox1.Items.Contains(e.KeyCode))
                {
                    listBox1.Items.Remove(e.KeyCode);
                    listBox1.Refresh();
                    if (timer1.Interval > 400)
                        timer1.Interval -= 10;
                    if (timer1.Interval > 250)
                        timer1.Interval -= 7;
                    if (timer1.Interval > 100)
                        timer1.Interval -= 2;
                    difficultyProgressBar.Value = 800 - timer1.Interval;
                    stats.Update(true);
                }
                else
                {
                    stats.Update(false);
                }

                correctLabel.Text = "Correct: " + stats.Correct;
                missedLabel.Text = "Missed: " + stats.Missed;
                totalLabel.Text = "Total: " + stats.Total;
                accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";
            }          
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            stats.Correct = 0;
            stats.Missed = 0;
            stats.Total = 0;
            stats.Accuracy = 0;
            listBox1.Items.Clear();
            difficultyProgressBar.Value = 0;
            timer1.Interval = 800;
            timer1.Enabled = true;
            correctLabel.Text = "Correct: 0";
            missedLabel.Text = "Missed: 0";
            totalLabel.Text = "Total: 0";
            accuracyLabel.Text = "Accuracy: 0" + "%";
        }


    
    }
}
