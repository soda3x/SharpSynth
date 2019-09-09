using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;

namespace SharpSynth
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            oscillator1.Oscillator_KeyDown(sender, e);
            oscillator2.Oscillator_KeyDown(sender, e);
            oscillator3.Oscillator_KeyDown(sender, e);
            oscillator4.Oscillator_KeyDown(sender, e);
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            oscillator1.Oscillator_KeyUp(sender, e);
            oscillator2.Oscillator_KeyUp(sender, e);
            oscillator3.Oscillator_KeyUp(sender, e);
            oscillator4.Oscillator_KeyUp(sender, e);
        }


    }
}
