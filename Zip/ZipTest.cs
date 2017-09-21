using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZipDemo
{
    public partial class Form1 : Form
    {
        BonkerZip bonkerZip;
        public Form1()
        {
            InitializeComponent();
            bonkerZip = new BonkerZip();
        }
        //最简单的一个例子
        private void button1_Click(object sender, EventArgs e)
        {
            BonkerZip zip = new BonkerZip();
            zip.AddFile(@"C:\Documents and Settings\Administrator\桌面\入党\单位鉴定.docx");//此路径是绝对路径
            zip.AddFile(@"C:\Documents and Settings\Administrator\桌面\入党\入党转正申请书.docx");
           zip.AddFile(@"C:\Documents and Settings\Administrator\桌面\vchelp");
           bool result= zip.CompressionZip("什么时候吃饭.zip");//压缩后文件的绝对路径
           if (!result)
           {
               MessageBox.Show(zip.errorMsg);
           }
           else
           {
               MessageBox.Show("压缩成功!");
           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bonkerZip.AddFile(openFileDialog.FileName);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Bonker.zip";
            s.InitialDirectory = "C:\\";
            s.Filter = "压缩文件(*.zip)|*.zip|所有文件(*.*)|*.*";
            s.FilterIndex = 1;
            bool result = false;
            if (s.ShowDialog() == DialogResult.OK)
            {
                result = bonkerZip.CompressionZip(s.FileName);//压缩后文件的绝对路径
            }
            if (!result)
            {
                MessageBox.Show(bonkerZip.errorMsg);
            }
            else
            {
                bonkerZip = new BonkerZip();
                MessageBox.Show("压缩成功!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            BonkerZip zip = new BonkerZip();
           bool result =  bonkerZip.DeCompressionZip("什么时候吃饭.zip", "什么时候吃饭2"); 
            if (!result)
            {
                MessageBox.Show(zip.errorMsg);
            }
            else
            {
                MessageBox.Show("解压成功!");
            }
        }
    }
}
