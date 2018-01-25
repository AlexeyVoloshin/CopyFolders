using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFolders
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                MessageBox.Show("Copy is Finished");
            }
            else
            {
                progressBar1.Value = progressBar1.Value + 1;
            }
        }
        string backupDir;
        string sourceDir; 
        

        void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog source = new FolderBrowserDialog();
            if (source.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = source.SelectedPath;
                sourceDir = source.SelectedPath;

            }
        }
        void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog destination = new FolderBrowserDialog();
            if (destination.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = destination.SelectedPath;
                backupDir = destination.SelectedPath;
            }
        }

        void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            timer1.Start();
            try
            {
                string[] FilesList = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
                /*--------------------------------------------------------------------------*/
                foreach (string dirPath in Directory.GetDirectories(sourceDir, "*",SearchOption.AllDirectories))
                {
                    try
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourceDir, backupDir));
                    }
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }
                foreach (string f in FilesList)
                {

                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    try
                    {
                        // Will not overwrite if the destination file already exists.
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }
            }

            //foreach (string f in txtList)
            //{
            //   File.Delete(f);
            //}
            // }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }

        }

       
    }
}
    
    

                    

            
        
    

