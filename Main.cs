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

namespace SharpSynth
{
    public partial class Main : Form
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        public Main()
        {
            InitializeComponent();
        }

        private void SharpSynth_KeyDown(object sender, KeyEventArgs e)
        {
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            float frequency = 400f;
            // algorithm for sine wave generation is Sample = Amplitude * sin(t * i)
            // where t is the angular frequency and i is a unit of time
            for (int i = 0; i < SAMPLE_RATE; ++i)
            {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
            }
            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
            // WAVE File Format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    short blockAlign = BITS_PER_SAMPLE / 8;
                    int subChunk2Size = SAMPLE_RATE * blockAlign;
                    //RIFF Header
                    binaryWriter.Write(new[] { 'R', 'I', 'F', 'F'}); // ChunkID
                    binaryWriter.Write(36 + subChunk2Size); // ChunkSize
                    binaryWriter.Write(new[] { 'W', 'A', 'V', 'E' }); // Format
                    binaryWriter.Write(new[] { 'f', 'm', 't', ' ' }); // Subchunk1ID
                    binaryWriter.Write(16); // Subchunk1Size
                    binaryWriter.Write(Convert.ToInt16(1)); // AudioFormat (PCM = 1)
                    binaryWriter.Write(Convert.ToInt16(1)); // NumChannels (Mono = 1, Stereo = 2, ... )
                    binaryWriter.Write(SAMPLE_RATE); // SampleRate
                    binaryWriter.Write(SAMPLE_RATE * blockAlign); // ByteRate
                    binaryWriter.Write(blockAlign); // BlockAlign
                    binaryWriter.Write(BITS_PER_SAMPLE); // BitsPerSample
                    binaryWriter.Write(new[] { 'd', 'a', 't', 'a' }); // Subchunk2ID
                    binaryWriter.Write(subChunk2Size); // Subchunk2Size
                    binaryWriter.Write(binaryWave); // Data

                    memoryStream.Position = 0;
                    new SoundPlayer(memoryStream).Play();
                }
            }

        }
    }
}
