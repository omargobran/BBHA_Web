using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace WebApplication1
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                OnlineApplication.fileName1 = "";
                OnlineApplication.fileName2 = "";
            }
        }

        protected void Downloadbtn1_Click(object sender, EventArgs e)
        {
            string strURL = "~/Attachments/BBHA Desktop Installer.msi";
            byte[] myData = File.ReadAllBytes(Server.MapPath(strURL));
            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/x-msi";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"BBHA Desktop Installer.msi\"");
            Response.AddHeader("Content-Length", myData.Length.ToString());
            Response.TransmitFile(Server.MapPath(strURL));
            Response.Flush();
            Response.End();
        }

        protected void Downloadbtn2_Click(object sender, EventArgs e)
        {
            string strURL = "~/Attachments/BBHA Mobile.apk";
            byte[] myData = File.ReadAllBytes(Server.MapPath(strURL));
            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/vnd.android.package-archive";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"BBHA Mobile.apk\"");
            Response.AddHeader("Content-Length", myData.Length.ToString());
            Response.TransmitFile(Server.MapPath(strURL));
            Response.BinaryWrite(myData);
            Response.Flush();
            Response.End();
        }

        protected void Therebtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("OnlineApplication.aspx");
        }

        protected void SendMail()
        {
            // Gmail Address from where you send the mail
            var fromAddress = "anasalameen2018@gmail.com";
            // any address where the email will be sending
            var toAddress = YourEmail.Text.ToString();
            //Password of your gmail address
            const string fromPassword = "Password";
            // Passing the values and make a email formate to display
            string subject = YourSubject.Text.ToString();
            string body = "From: " + YourName.Text + "\n";
            body += "Email: " + YourEmail.Text + "\n";
            body += "Subject: " + YourSubject.Text + "\n";
            body += "Question: \n" + Comments.Text + "\n";
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }

        protected void Submitbtn_Click(object sender, EventArgs e)
        {
            if (YourEmail.Text == "" || YourName.Text == "" || YourSubject.Text == "" || Comments.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Please Fill All Boxes and Try Again", "alert('Please Fill All Boxes and Try Again')", true);
            }
            else
            {
                try
                {
                    //here on button click what will be done 
                    SendMail();
                    DisplayMessage.Text = "Your Comments after sending the mail";
                    DisplayMessage.Visible = true;
                    YourSubject.Text = "";
                    YourEmail.Text = "";
                    YourName.Text = "";
                    Comments.Text = "";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}