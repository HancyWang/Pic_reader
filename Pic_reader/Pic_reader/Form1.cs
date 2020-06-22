using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pic_reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //加载文件内容到内存中，存放在FileMngr的链表中
            #region
            if (this.folderBrowserDialog_selectFolder.ShowDialog() == DialogResult.OK)
            {
                string strPath = folderBrowserDialog_selectFolder.SelectedPath;//获取打开的文件路径名

                var workDataFilePathes = Directory.GetFiles(strPath, "*.dat");

                foreach (var file in workDataFilePathes)
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs, Encoding.ASCII);

                    byte[] buffer = new byte[768054];
                   

                    br.Read(buffer, 0, 768054);

                    br.Close();
                    fs.Close();

                    string newfile = file + ".bmp";

                    fs = new FileStream(newfile, FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII);

                    byte[] head_buffer = new byte[54] 
                    { 0x42,0x4D,0x36,0xB8,0x0B,0x00,0x00,0x00,0x00,0x00,0x36,0x00,0x00,0x00,0x28,0x00,
                      0x00,0x00,0x20,0x03,0x00,0x00,0xE0,0x01,0x00,0x00,0x01,0x00,0x10,0x00,0x00,0x00,
                      0x00,0x00,0x00,0xB8,0x0B,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                      0x00,0x00,0x00,0x00,0x00,0x00
                    };

                    bw.Write(head_buffer, 0, 54);

                    for (int i = 480-1; i >=0; i--)
                    {
                        bw.Write(buffer, i*800*2, 800*2);
                    }
                    //bw.Write(buffer, 0, 768000);

                    bw.Close();
                    fs.Close();

                }

            }
            else
            {
                return;
            }
            #endregion
        }
    }
}
