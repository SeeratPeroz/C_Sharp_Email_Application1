using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Charp_Email_Application
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofdAttachment;
        String fileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "File(.jpg,.png)|*.png;*.jpg;|Pdf Files|*.pdf";
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdAttachment.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Smpt Client Details
                //gmail >> smtp server : smtp.gmail.com, port : 587 , ssl required
                //yahoo >> smtp server : smtp.mail.yahoo.com, port : 587 , ssl required
                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = Convert.ToInt32("587");
                clientDetails.Host = "smtp.gmail.com";
                clientDetails.EnableSsl = true;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential("your gmail", "your password");

                //Message Details
                //sdfasdf
              
                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress("your gmail");
                mailDetails.To.Add(txtRecipientEmail.Text.Trim());
                //for multiple recipients
                //mailDetails.To.Add("another recipient email address");
                //for bcc
                //mailDetails.Bcc.Add("bcc email address")
                mailDetails.Subject = txtSubject.Text.Trim();
                mailDetails.Body = rtbBody.Text.Trim();


                //file attachment
                if (fileName.Length > 0)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailDetails.Attachments.Add(attachment);
                }

                clientDetails.Send(mailDetails);
                MessageBox.Show("Your mail has been sent.");
                fileName = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
