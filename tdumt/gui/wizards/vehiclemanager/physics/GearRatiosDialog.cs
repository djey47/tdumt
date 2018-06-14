using System;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;

namespace TDUModdingTools.gui.wizards.vehiclemanager.physics
{
    public partial class GearRatiosDialog : Form
    {
        #region Properties
        /// <summary>
        /// Array of gear ratios
        /// </summary>
        public string[] RatiosArray
        {
            get { return _RatiosArray; }
            set { _RatiosArray = value;}
        }
        private string[] _RatiosArray;
        #endregion

        #region Members
        /// <summary>
        /// Gear count
        /// </summary>
        private readonly int _GearCount = 0;

        /// <summary>
        /// Array of gear ratio textboxes
        /// </summary>
        private TextBox[] _RatioBoxes;

        /// <summary>
        /// Array of previous gear ratios
        /// </summary>
        private readonly string[] _PreviousGearRatios = null;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="gearCount"></param>
        /// <param name="ratiosArray"></param>
        public GearRatiosDialog(int gearCount, string[] ratiosArray)
        {
            InitializeComponent();

            if (gearCount > 0 && gearCount <= 7)
            {
                _GearCount = gearCount;
                _RatiosArray = ratiosArray;

                // EVO_133: Sets previous gear ratios
                _PreviousGearRatios = new string[ratiosArray.Length];
                Array.Copy(ratiosArray, _PreviousGearRatios, _PreviousGearRatios.Length);
            }

            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines box contents
        /// </summary>
        private void _InitializeContents()
        {
            // Control array
            _RatioBoxes = new TextBox[]
                {
                    gearFTextBox,
                    gear1TextBox,
                    gear2TextBox,
                    gear3TextBox,
                    gear4TextBox,
                    gear5TextBox,
                    gear6TextBox,
                    gear7TextBox
                };

            // According to gear count...
            for (int i = 0; i <= _GearCount; i++)
                _RatioBoxes[i].Enabled = true;
        }

        /// <summary>
        /// Updates gear ratio values
        /// </summary>
        private void _RefreshRatios()
        {
            if (_RatiosArray != null)
            {
                for (int i = 0; i <= _GearCount; i++)
                    _RatioBoxes[i].Text = _RatiosArray[i];
            }     
        }
        #endregion

        #region Events
        private void GearRatiosDialog_Load(object sender, EventArgs e)
        {
            // Box loading
            _RefreshRatios();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on 'OK' button
            // Gets entered values then converts ',' to '.'
            for (int i = 0 ; i < _RatiosArray.Length ; i++)
            {
                string currentRatio = _RatioBoxes[i].Text;

                if (!string.IsNullOrEmpty(currentRatio))
                    _RatiosArray[i] = currentRatio.Replace('.', ',');
            }
        }

        private void controlTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Slider has been moved
                double gearCoef = 1d - controlTrackBar.Value/10d;

                // Updating gear ratios...
                for (int i = 1; i <= _GearCount; i++)
                {
                    if (!string.IsNullOrEmpty(_PreviousGearRatios[i]))
                    {
                        double currentRatio = double.Parse(_PreviousGearRatios[i]);
                        double newRatio = Math.Round(currentRatio*gearCoef, 3);

                        _RatiosArray[i] = newRatio.ToString();
                    }
                }

                _RefreshRatios();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion
    }
}