using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SharpSynth
{
    class Oscillator : GroupBox
    {
        public Waveform Waveform { get; private set; }

        public bool OscillatorEnabled => ((CheckBox)this.Controls["OscLabel"]).Checked;

        public int OscillatorNumber { get; set; }

        private CheckBox OscLabel;

        private Button lastSelected;

        private TrackBar Volume;

        private Label DetuneValueLabel;

        private Wave oscillator;
        public Oscillator()
        {
            this.oscillator = new Wave();
            this.Size = new Size(249, 219);
            this.BackColor = Color.White;

            this.Controls.Add(OscLabel = new CheckBox
            {
                Name = "OscLabel",
                Location = new Point(50, 0),
                Size = new Size(17, 17),
                Checked = true,
                BackColor = Color.White
            });
            OscLabel.CheckedChanged += OscillatorDisabled;

            this.Controls.Add(new Label()
            {
                Name = "WaveformLabel",
                Text = "Waveform",
                Location = new Point(54, 45),
                Size = new Size(56, 13)
            });

            this.Controls.Add(new Label()
            {
                Name = "DetuneLabel",
                Text = "Detune",
                Location = new Point(54, 137),
                Size = new Size(42, 13)
            });

            this.Controls.Add(DetuneValueLabel = new Label()
            {
                Name = "DetuneValueLabel",
                Text = "0 cents",
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(57, 184),
                Size = new Size(171, 14)
            });

            this.Controls.Add(new Button()
            {
                Name = "Sine",
                Text = "Sine",
                Location = new Point(57, 62),
                Size = new Size(53, 23),
                Enabled = false // the default waveform is sine so it should be disabled
            });

            this.Controls.Add(new Button()
            {
                Name = "Square",
                Text = "Square",
                Location = new Point(116, 62),
                Size = new Size(53, 23)
            });

            this.Controls.Add(new Button()
            {
                Name = "Saw",
                Text = "Saw",
                Location = new Point(175, 62),
                Size = new Size(53, 23)
            });

            this.Controls.Add(new Button()
            {
                Name = "Triangle",
                Text = "Triangle",
                Location = new Point(57, 91),
                Size = new Size(53, 23)
            });

            this.Controls.Add(new Button()
            {
                Name = "Noise",
                Text = "Noise",
                Location = new Point(116, 91),
                Size = new Size(53, 23)
            });

            this.Controls.Add(Volume = new TrackBar()
            {
                Name = "Volume",
                TickStyle = TickStyle.Both,
                Orientation = Orientation.Vertical,
                Minimum = 0,
                Maximum = 10,
                Value = 8,
                Location = new Point(3, 45),
                Size = new Size(45, 171)
            });
            Volume.ValueChanged += Volume_ValueChanged;

            this.Controls.Add(new TrackBar()
            {
                Name = "Detune",
                TickStyle = TickStyle.BottomRight,
                Orientation = Orientation.Horizontal,
                Minimum = -100,
                Maximum = 100,
                Value = 0,
                ForeColor = Color.LightGray,
                Location = new Point(57, 153),
                Size = new Size(171, 45)
            });

            // assign click event handler to buttons
            foreach (Control control in this.Controls.OfType<Button>())
            {
                Button button = (Button)control;
                button.FlatStyle = FlatStyle.Flat;
                button.Click += WaveBtn_Click;
                button.Font = new Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular);
            }

            foreach (Control control in this.Controls.OfType<TrackBar>())
            {
                TrackBar trackbar = (TrackBar)control;
                if (trackbar.Name == "Detune")
                {
                    trackbar.ValueChanged += DetuneValue_Changed;
                }
            }

            foreach (Control control in this.Controls.OfType<Label>())
            {
                Label label = (Label)control;
                if (label.Name == "DetuneValueLabel")
                {
                    label.DoubleClick += DetuneValue_Reset;
                }
            }
        }

        private void Volume_ValueChanged(object sender, EventArgs e)
        {
            Main form = (Main)this.FindForm();
            oscillator.amplitude = Convert.ToInt16((Volume.Value / 10.0) * short.MaxValue);
        }

        private void OscillatorDisabled(object sender, EventArgs e)
        {
            if (OscillatorEnabled == false)
            {
                // keep track of which waveform was last selected so that when the oscillator is re-enabled it is picked again
                foreach (Control control in this.Controls.OfType<Button>())
                {
                    Button button = (Button)control;
                    if (button.Enabled == false)
                    {
                        lastSelected = button;
                    }
                }
                foreach (Control control in this.Controls)
                {
                    if (control.Name != "OscLabel")
                    {
                        control.Enabled = false;
                    }
                    else
                    {
                        control.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (Control control in this.Controls)
                {
                    control.Enabled = true;
                    lastSelected.Enabled = false;
                }
            }
        }

        private void DetuneValue_Changed(object sender, EventArgs e)
        {
            TrackBar trackbar = (TrackBar)sender;
            DetuneValueLabel.Text = trackbar.Value.ToString() + " cents";
            Main form = (Main)this.FindForm();
            oscillator.frequencyOffset = trackbar.Value / 10.0f;
        }

        private void DetuneValue_Reset(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            foreach (Control control in this.Controls.OfType<TrackBar>())
            {
                TrackBar trackbar = (TrackBar)control;
                if (trackbar.Name == "Detune")
                {
                    trackbar.Value = 0;
                }
            }
        }

        private void WaveBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            this.Waveform = (Waveform)Enum.Parse(typeof(Waveform), button.Text); // set the waveform

            // disable clicked waveform button when selected
            foreach (Control control in this.Controls.OfType<Button>())
            {
                if (control.Text == this.Waveform.ToString())
                {
                    control.Enabled = false;
                }
                else
                {
                    control.Enabled = true;
                }
            }
        }

        public void Oscillator_KeyDown(object sender, KeyEventArgs e)
        {
            this.oscillator.SharpSynth_KeyDown(sender, e);
        }

        public void Oscillator_KeyUp(object sender, KeyEventArgs e)
        {
            this.oscillator.SharpSynth_KeyUp(sender, e);
        }


    }
}
