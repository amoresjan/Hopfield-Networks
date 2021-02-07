using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HopfieldNetworks
{
    public partial class Form1 : Form
    {
        bool hasSelectedInitialPattern = false;
        int numOfPatternsAdded = 0;
        int energyValue = 0;
        SolidBrush black = new SolidBrush(Color.Black);
        SolidBrush white = new SolidBrush(Color.White);
        List<Bitmap> patternsList = new List<Bitmap>(5);
        NeuralNetwork neuralNetwork;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            resetValues();
        }

        private void resetValues()
        {
            hasSelectedInitialPattern = false;
            numOfPatternsAdded = 0;
            energyValue = 0;

            button2.Enabled = false;
            button3.Enabled = false;
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";

            PictureBox[] patterns = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };

            foreach(PictureBox pb in patterns)
            {
                pb.Image = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!hasSelectedInitialPattern)
            { 
                MessageBox.Show("You must select initial pattern.", "Neural Network Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetValues();

            button2.Enabled = true;
            label4.Text = "100";
            label5.Text = numOfPatternsAdded.ToString();
            label6.Text = energyValue.ToString();

            neuralNetwork = new NeuralNetwork(100);
            Random random = new Random();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < 10; i++) 
            {
                for(int j = 0; j < 10; j++)
                {
                    graphics.FillRectangle(random.Next(2) == 0 ? white : black, i * 10, j * 10, 10, 10);
                }

            }

            pictureBox1.Image = bitmap;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                button3.Enabled = true;

                Bitmap bm = new Bitmap(openFileDialog1.FileName);
                List<Neuron> pixels = new List<Neuron>(100);

                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        Neuron neuron = new Neuron();
                        neuron.State =  ColorsAreClose(bm.GetPixel(i, j)) == true ? NeuronStates.AlongField : NeuronStates.AgainstField;
                        pixels.Add(neuron);
                    }

                }

                neuralNetwork.AddPattern(pixels);

                switch (numOfPatternsAdded)
                {
                    case 0:
                        pictureBox2.Enabled = true;
                        pictureBox2.Image = bm;
                        patternsList.Add(bm);
                        break;
                    case 1:
                        pictureBox3.Enabled = true;
                        pictureBox3.Image = bm;
                        patternsList.Add(bm);
                        break;
                    case 2:
                        pictureBox4.Enabled = true;
                        pictureBox4.Image = bm;
                        patternsList.Add(bm);
                        break;
                    case 3:
                        pictureBox5.Enabled = true;
                        pictureBox5.Image = bm;
                        patternsList.Add(bm);
                        break;
                    case 4:
                        pictureBox6.Enabled = true;
                        pictureBox6.Image = bm;
                        patternsList.Add(bm);
                        break;
                    default:
                        break;
                }
                numOfPatternsAdded++;
                label5.Text = numOfPatternsAdded.ToString();

            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            hasSelectedInitialPattern = true;
            drawPattern(patternsList[4]);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            hasSelectedInitialPattern = true;
            drawPattern(patternsList[3]);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            hasSelectedInitialPattern = true;
            drawPattern(patternsList[2]);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            hasSelectedInitialPattern = true;
            drawPattern(patternsList[1]);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            hasSelectedInitialPattern = true;
            drawPattern(patternsList[0]);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        void drawPattern(Bitmap input)
        {
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    graphics.FillRectangle(ColorsAreClose(input.GetPixel(i, j)) ? black : white, i * 10, j * 10, 10, 10);

                }

            }

            pictureBox1.Image = bitmap;
        }

        bool ColorsAreClose(Color color)
        {
            return (color.R * color.R + color.G * color.G + color.B * color.B) < 100;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
