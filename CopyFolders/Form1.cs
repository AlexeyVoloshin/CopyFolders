using System;
using System.IO;
using System.Windows.Forms;

namespace CopyFolders
{
    public partial class CopyFiles : Form
    {
        string sourceDir;
        string sourceDir2;
        string backupDir;
        public CopyFiles()
        {
            InitializeComponent();
        }
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
                sourceDir2 = destination.SelectedPath;
            } 
        }
        void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog destination = new FolderBrowserDialog();
            if (destination.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = destination.SelectedPath;
                backupDir = destination.SelectedPath;
            }
        }
        void button4_Click(object sender, EventArgs e)
        {
            try
            {
              int  calcCopyField = 0;
                sourceDir = textBox1.Text;
                sourceDir2 = textBox2.Text;
                backupDir = textBox3.Text;
                while (true)
                {
                    if (sourceDir == "")
                    {
                        MessageBox.Show("Select directory");
                        button4.Enabled = true;
                        break;
                    }
                    else if (sourceDir2 == "")
                    {
                        MessageBox.Show("Select directory");
                        button4.Enabled = true;
                        break;
                    }
                     else if (backupDir == "")
                        {
                        MessageBox.Show("Select directory");
                        button4.Enabled = true;
                        break;
                        }else { 
                            string[] FilesList = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
                            string[] FilesList2 = Directory.GetFiles(sourceDir2, "*", SearchOption.AllDirectories);
                            button4.Enabled = false;
                        foreach (string f in FilesList)
                        {
                            string fNames = f.Substring(sourceDir.Length + 1);
                            string dNames = fNames.Remove(fNames.LastIndexOf('\\') + 1);
                            string fName = f.Substring(f.LastIndexOf('\\') + 1);
                            int result = 0;
                            foreach (string i in FilesList2)
                            {
                                // Remove path from the file name.
                                string fName2 = i.Substring(i.LastIndexOf('\\') + 1);
                                try
                                {
                                    if (Equals(fName, fName2))
                                    {
                                        result =  1;
                                        break;
                                    }
                                }
                                // Catch exception if the file was already copied.
                                catch (IOException copyError)
                                {
                                    Console.WriteLine(copyError.Message);
                                }
                            }
                            try
                            {
                                if (Equals(result, 0))
                                {
                                    Directory.CreateDirectory(Path.Combine(backupDir, dNames));
                                    File.Copy(Path.Combine(sourceDir, fNames), Path.Combine(backupDir, fNames), false);
                                    calcCopyField += 1;
                                }
                            }
                            // Catch exception if the file was already copied.
                            catch (IOException copyError)
                            {
                                
                                    if (MessageBox.Show(copyError.Message+":  Продолжить копирование?", icon: MessageBoxIcon.Information, caption: "Внимание!", buttons: MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                    button4.Enabled = true;
                    MessageBox.Show("Скопировано: " + calcCopyField+" файлов");
                    break;
                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                MessageBox.Show(dirNotFound.Message, icon: MessageBoxIcon.Information, caption: "Внимание!", buttons: MessageBoxButtons.OK);
                
            }
        }
    }
}

    

    

