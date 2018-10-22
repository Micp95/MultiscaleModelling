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
            comboBoxTypeOfInclusion.SelectedIndex = 0;
            _isStartedSimulation = false;

            _currentSimullation = new StandardSimulation();
            ChangeEnableGrowOptions(true);
            ChangeEnableGrowButtons(true);
            ChangeEnableInclusionsOptions(true);

            exportToolStripMenuItem.Enabled = false;
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

        private ConfigurationInclusions GetConfigurationInclusions()
        {

            return new ConfigurationInclusions()
            {
                InclusionType = (InclusionType)comboBoxTypeOfInclusion.SelectedIndex,
                NumberOfInclusions = (int)numericAmountOfInclusions.Value,
                SizeOfInclusions = (int)numericSizeOfInclusions.Value
            };
        }

        private void StartStandatdSimulation()
        {
            ChangeEnableGrowOptions(false);
            ChangeEnableInclusionsOptions(false);

            var config = GetConfiguration();

            _currentSimullation.Initialize(config);
            _currentSimullation.SeedGrains(config);
            _isStartedSimulation = true;
        }
        private void Render(Bitmap map)
        {
            if (map != null)
                pictureBox1.Image = ResizeBitmap(map, pictureBox1.Width, pictureBox1.Height);
        }

        private void RenderStep()
        {
            var map = _currentSimullation.GetBitmap();
            Render(map);
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

        private void ChangeEnableGrowOptions(bool enable)
        {
            numericUpDownWidth.Enabled = enable;
            numericUpDownHeight.Enabled = enable;
            numericUpDownNumberOfGrain.Enabled = enable;
            comboBoxNeighbourhood.Enabled = enable;
            comboBoxBC.Enabled = enable;
        }
        private void ChangeEnableInclusionsOptions(bool enable)
        {
            numericAmountOfInclusions.Enabled = enable;
            numericSizeOfInclusions.Enabled = enable;
            comboBoxTypeOfInclusion.Enabled = enable;
            buttonAddInclusions.Enabled = enable;
        }
        private void ChangeEnableGrowButtons(bool enable)
        {
            buttonStart.Enabled = enable;
            buttonStop.Enabled = enable;
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
            _currentSimullation.Restart();
            RenderStep();
            _isStartedSimulation = false;
            timer1.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            ChangeEnableInclusionsOptions(true);
            ChangeEnableGrowOptions(true);
            ChangeEnableGrowButtons(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _currentSimullation.NextStep();
            RenderStep();
            if (_currentSimullation.IsEndSimulation())
            {
                ChangeEnableGrowButtons(false);
                ChangeEnableInclusionsOptions(true);
                exportToolStripMenuItem.Enabled = true;
                timer1.Enabled = false;
            }
        }


        private void comboBoxBC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxNeighbourhood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Structure Files (*.bmp, *.txt)|*.bmp; *.txt";
            openFileDialog1.Title = "Select a structure file";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var name = openFileDialog1.FileName;

                if (name.Contains(".txt"))
                    _currentSimullation.ImportFromFile(FileTypeEnum.Text, name);
                else if (name.Contains(".bmp"))
                    _currentSimullation.ImportFromFile(FileTypeEnum.Bmp, name);

                _isStartedSimulation = true;
                RenderStep();

                ChangeEnableGrowOptions(false);
                ChangeEnableGrowButtons(false);
                ChangeEnableInclusionsOptions(true);
                exportToolStripMenuItem.Enabled = true;
            }
        }

        private void toTextToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileGialog = new SaveFileDialog();
            saveFileGialog.Filter = "Structure Files (*.txt)|*.txt";
            saveFileGialog.Title = "Select path for file";


            if (saveFileGialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var name = saveFileGialog.FileName;
                _currentSimullation.ExportToFile(FileTypeEnum.Text, name);

            }
        }

        private void toBitmapToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileGialog = new SaveFileDialog();
            saveFileGialog.Filter = "Structure Files (*.bmp)|*.bmp";
            saveFileGialog.Title = "Select path for file";


            if (saveFileGialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var name = saveFileGialog.FileName;
                _currentSimullation.ExportToFile(FileTypeEnum.Bmp, name);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentSimullation.IsMapEmpty())
            {
                var globalConfig = GetConfiguration();
                _currentSimullation.Initialize(globalConfig);
            }
            var config = GetConfigurationInclusions();
            _currentSimullation.AddInclusions(config);
            RenderStep();
        }
    }
}
