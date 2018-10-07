using MultiscaleModelling.Interfaces;
using MultiscaleModelling.Models;
using MultiscaleModelling.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiscaleModelling
{
    public partial class Form1 : Form
    {
        private ISimulation _currentSimullation;
        private bool _isStartedSimulation;
        public Form1()
        {
            InitializeComponent();

            comboBoxBC.SelectedIndex = 0;
            comboBoxNeighbourhood.SelectedIndex = 0;
            _isStartedSimulation = false;
        }

        private Configuration GetConfiguration()
        {

            return new Configuration()
            {
                Width= (int)numericUpDownWidth.Value,
                Height= (int)numericUpDownHeight.Value,
                BoundaryConditions=(BCEnum)comboBoxBC.SelectedIndex,
                Neighbourhood=(NeighbourhoodEnum)comboBoxNeighbourhood.SelectedIndex,
                NumberOfGrains= (int)numericUpDownNumberOfGrain.Value
            };
        }

        private void StartStandatdSimulation()
        {
            _currentSimullation = new StandardSimulation();

            var config = GetConfiguration();

            _currentSimullation.Initialize(config);
            _isStartedSimulation = true;
        }
        private void Render(Bitmap map)
        {
            if (map != null)
                pictureBox1.Image = ResizeBitmap(map, pictureBox1.Width, pictureBox1.Height);
        }
        private Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }
            return result;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!_isStartedSimulation)
            {
                StartStandatdSimulation();
            }
            timer1.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            StartStandatdSimulation();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _currentSimullation.NextStep();
            var map =_currentSimullation.GetBitmap();
            Render(map);
        }
    }
}
