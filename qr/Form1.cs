using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessRefinery.Barcode;
using System.Drawing.Imaging;

namespace qr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        /*{
            QRCode barcode = new QRCode();
            string url = "http://www.naver.com";    // QR코드에 저장할 내용
            barcode.Code = url;
            barcode.ModuleSize = 6.0f;
            barcode.Resolution = 300;
            barcode.drawBarcode2ImageFile("naver.png");  // 프로젝트 폴더의 Debug 경로내에 파일이 생성됩니다.

            MessageBox.Show("done.");
        }*/
        {
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = ZXing.BarcodeFormat.QR_CODE;

            barcodeWriter.Options.Width = this.pictureBox1.Width;
            barcodeWriter.Options.Height = this.pictureBox1.Height;

            string strQRCode = "http://www.google.com";

            this.pictureBox1.Image = barcodeWriter.Write(strQRCode);
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            barcodeWriter.Write(strQRCode).Save(deskPath + @"\test.jpg", ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZXing.BarcodeReader barcodeReader = new ZXing.BarcodeReader();
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            System.IO.FileInfo fi = new System.IO.FileInfo(deskPath + @"\test.jpg");

            if (fi.Exists)
            {
                var barcodeBitmap = (Bitmap)Image.FromFile(deskPath + @"\test.jpg");
                var result = barcodeReader.Decode(barcodeBitmap);

                this.textBox1.Text = result.Text;
            }
            else
            {
                MessageBox.Show("Please generate QRcode first.\n Press barcode maker button.", "Nofile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
