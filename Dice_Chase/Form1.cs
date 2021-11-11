using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Dice_Chase
{
    public partial class Form1 : Form
    {
        int counter=0; //how many times are remaining for the dice in the current round//
        int score = 0; //the user's total amount of points in a round//
        int X;//the coordinates of the dice//
        int Y;
        int value; //the amount of points each side of the dice is worth//
        bool check=true; // whether the dice has already been clicked in its current state//
        int record = 0; //the highest score of any user on the list//
        string champion;//the name of the player with the highest score//
        Random rng = new Random(); //a random number generator//
        public Form1()
        {
            InitializeComponent();
            //initializes the form, giving the dice, the label and the user name a default state//
            pictureBox1.ImageLocation = "dice1.png";
            label1.Text = "0";
            richTextBox1.Text = "Anonymous User";
            label4.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            check = true; // this signifies that the dice can be clicked to gather more points in the current state//
            label1.Text = score.ToString(); //this label is always updated with the user's current score//
            label4.Text = (20 - counter).ToString(); //shows how many more "dice throws" are left//
            counter = counter + 1; //how many times the dice has changed in a current round//
            //the following lines pick a random new location for the dice//
            X = rng.Next(0, 1025);
            Y = rng.Next(100, 545);
            pictureBox1.Location = new Point(X, Y);
            value = rng.Next(1 , 7); //picks a random value from 1 to 6, meaning how many points the user will get if they click it//
            pictureBox1.ImageLocation = "dice" + value.ToString() + ".png"; //gives the dice the appropriate image//

            if (counter > 20) //the game stops after 20 random dice movements//
            {
                timer1.Stop(); //stops the timer//
                MessageBox.Show("Your score was: " + score); //shows score//
                if (checkBox1.Checked == true) //if the checkbox is ticked then the game saves the score in a text file//
                {
                    StreamWriter sw = new StreamWriter("list.txt", true); //opens the text file to write//
                    if (radioButton1.Checked == true) //along with the score and name it also saves the difficulty the user had picked//
                    {
                        //saves score, name and difficulty//
                        sw.WriteLine(score.ToString() + " by " + richTextBox1.Text + " Difficulty: Normal ");
                    }
                    else if (radioButton2.Checked == true)
                    {
                        sw.WriteLine(score.ToString() + " by " + richTextBox1.Text + " Difficulty: Hard ");
                    }
                    
                    sw.Close();
                }
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //depending on the difficulty picked the timer interval is altered so the dice will move faster//
            if (radioButton1.Checked == true)
            {
                timer1.Interval = 1500;
            }
            else if (radioButton2.Checked == true)
            {
                timer1.Interval = 1000;
            }
            //everytime the game starts all the following variables need to be reset//
            score = 0;
            counter = 0;
            label1.Text = "0";
            timer1.Start(); //the timer starts//
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (check == true) { //this makes sure that the dice can only be clicked once between intervals//
                int multiplier = 1;
                if (radioButton2.Checked == true) { multiplier = 2; } //if the user has chosen the hard difficulty they are rewarded with more points//
                score = score + multiplier*value;
                check = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //when the difficulty is changed midgame, everything resets//
            timer1.Stop();
            score = 0;
            label1.Text = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            score = 0;
            label1.Text = "0";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if the checkbox is changed midgame everything resets//
            timer1.Stop();
            score = 0;
            label1.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            score = 0;
            counter = 0;
            //if this button is pressed the text file with the list of scores is shown to the user//
            StreamReader sr = new StreamReader("list.txt");
            try
            {
                string s = sr.ReadLine();
                StringBuilder sb = new StringBuilder();
                while (s != null)
                {
                    sb.Append(s + "\n");
                    s = sr.ReadLine();
                }
                MessageBox.Show(sb.ToString());
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if this button is pressed the program attempts to find the highest score on the list//
            try
            {
                StreamReader sr = new StreamReader("list.txt");
                string information = sr.ReadLine(); //the program checks each individual line//

                while (information != null)
                {
                    string[] words = information.Split();
                    string help = words[0];

                    int current_score = Convert.ToInt32(help); //the first word of each line is the score//
                    if (current_score > record)
                    {
                        record = current_score; //the record is replaced if a higher one is found//
                        champion = words[2];
                    }
                    information = sr.ReadLine();
                }
                MessageBox.Show(record.ToString());
            }
            catch
            {
                MessageBox.Show("Error");
            }
            
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
