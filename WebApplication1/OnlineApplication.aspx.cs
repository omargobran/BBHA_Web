using System;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.UI;
using MathWorks.MATLAB.ProductionServer.Client;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WebApplication1
{
    public partial class OnlineApplication : System.Web.UI.Page
    {
        const string seperator = "; ";
        public static int columns, rows;
        public static string fileName1 = "", fileName2 = "", tblData = "", str = "", header = "", tbl = "";
        public static int[] sel;

        protected void Page_Load(object sender, EventArgs e)
        {
            browsebtn1.Attributes.Add("onclick", "document.getElementById('" + FileUpload1.ClientID + "').click(); return false;");
            browsebtn2.Attributes.Add("onclick", "document.getElementById('" + FileUpload2.ClientID + "').click(); return false;");
        }

        public interface IBBHA_Select
        {
            void BBHA_Select(out string newFileName, out int featuresCountOld, out int featuresCountNew,
                out double acc, out byte[] imgArr, out string result, out int[] selection,
                string tblData, string str, int columns, int rows); // has to be the same name as function in MATLAB
        }

        protected void Uploadbtn1_Click(object sender, EventArgs e)
        {
            string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
            int len = FileUpload1.FileBytes.Length;
            if (FileUpload1.HasFile)
            {
                if (ext != ".csv")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Unsuccesful Upload! Please Select a .csv File and Try Again')", true);
                else if (len > 1048576)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Unsuccesful Upload! Maximum File Size Exceeded. Please Add a 1 MB File and Try Again')", true);
                else
                {
                    Label2.Text = "";
                    Label3.Text = "";
                    fileName1 = ""; tblData = ""; str = ""; header = ""; tbl = ""; sel = null;
                    FileUpload1.SaveAs(Server.MapPath("~/Uploads/" + FileUpload1.FileName));
                    fileName1 = FileUpload1.FileName;
                    var lines = File.ReadAllLines(Server.MapPath("~/Uploads/" + fileName1));
                    rows = lines.Count() - 1;
                    columns = lines[0].Split(',').Count();
                    int column = 1;
                    while (column != columns)
                    {
                        str += "%f,";
                        column++;
                    }
                    column = 1;
                    using (FileStream fs = File.Open(Server.MapPath("~/Uploads/" + fileName1), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (column > 1)
                                tblData += line + ",";
                            else
                                header = line;
                            column++;
                        }
                    }
                    Label1.Text = "File Uploaded Successfully";
                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select a File and Try Again')", true);
        }

        protected void Runbtn1_Click(object sender, EventArgs e)
        {
            if (fileName1 == "" || Label1.Text == "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Upload a File and Try Again')", true);
            else
            {
                string path = Server.MapPath("~/Uploads/");
                MWClient client = new MWHttpClient();
                try
                {
                    IBBHA_Select bh = client.CreateProxy<IBBHA_Select>
                    (new Uri("http://6eb90182.ngrok.io/BBHA"));
                    bh.BBHA_Select(out string newFileName, out int featuresCountOld, out int featuresCountNew,
                    out double acc, out byte[] imgArr, out string result, out int[] selection,
                    tblData, str, columns, rows);

                    sel = selection;
                    fileName1 = newFileName;
                    result = string.Concat(result, ';');
                    tbl = result;

                    StringBuilder csvcontent = new StringBuilder();
                    StringBuilder csvheader = new StringBuilder();
                    string newPath = Server.MapPath("~/Attachments/" + fileName1);
                    string[] DataRows = tbl.Split(seperator.ToCharArray());
                    foreach (string DataRow in DataRows)
                    {
                        csvcontent.AppendLine(DataRow);
                    }
                    string resultString = Regex.Replace(" ," + csvcontent.ToString(), @"^\s+$[\r\n]*", " ,", RegexOptions.Multiline);
                    resultString = Regex.Replace(resultString, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                    List<string> newHeader = new List<string>();
                    string headerString = "";

                    string[] headers = header.Split(',');
                    Array.Resize(ref sel, sel.Length + 1);
                    sel[sel.Length - 1] = 1;
                    for (int i = 0; i < sel.Length; i++)
                    {
                        if (sel[i] == 1)
                        {
                            newHeader.Add(headers[i]);
                        }
                    }
                    headerString = string.Join(",", newHeader);
                    csvheader.AppendLine(" ," + headerString);
                    File.WriteAllText(newPath, csvheader.ToString());
                    File.AppendAllText(newPath, resultString);

                    fileName1 = newFileName;
                    Label1.Text = "";
                    Label2.Text = "Old Number of Features = " + featuresCountOld.ToString() + " <br/>New Number of Features = " + featuresCountNew.ToString() + " <br/>Accuracy Rate = " + String.Format("{0:0.00}%", acc);
                    Label3.Text = newFileName;
                }
                catch (MATLABException ex)
                {
                    Console.WriteLine("{0} MATLAB exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("{0} Web exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("{0} MATLAB argument exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        protected void Downloadbtn1_Click(object sender, EventArgs e)
        {
            if (fileName1 != "" && Label3.Text != "")
            {
                string strURL = "~/Attachments/" + fileName1;
                byte[] myData = File.ReadAllBytes(Server.MapPath(strURL));
                Response.Clear();
                Response.BufferOutput = false;
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName1 + "\"");
                Response.AddHeader("Content-Length", myData.Length.ToString());
                Response.TransmitFile(Server.MapPath(strURL));
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Press Run and Try Again')", true);
            }
        }

        public interface IBBHA_Predict
        {
            void BBHA_Predict(out string newFileName, out int countDiseased, out int countHealthy,
            out byte[] imgArr, out string result,
            string tblData, string str, int columns, int rows); // has to be the same name as function in MATLAB
        }

        protected void Uploadbtn2_Click(object sender, EventArgs e)
        {
            string ext = System.IO.Path.GetExtension(FileUpload2.FileName);
            int len = FileUpload2.FileBytes.Length;
            if (FileUpload2.HasFile)
            {
                if (ext != ".csv")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Unsuccesful Upload! Please Select a .csv File and Try Again')", true);
                else if (len > 1048576)
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Unsuccesful Upload! Maximum File Size Exceeded. Please Add a 1 MB File and Try Again')", true);
                else
                {
                    Label5.Text = "";
                    Label6.Text = "";
                    fileName2 = ""; tblData = ""; str = ""; header = ""; tbl = ""; sel = null;
                    FileUpload2.SaveAs(Server.MapPath("~/Uploads/" + FileUpload2.FileName));
                    fileName2 = FileUpload2.FileName;
                    var lines = File.ReadAllLines(Server.MapPath("~/Uploads/" + fileName2));
                    rows = lines.Count() - 1;
                    columns = lines[0].Split(',').Count();
                    int column = 1;
                    while (column != columns)
                    {
                        str += "%f,";
                        column++;
                    }
                    column = 1;
                    using (FileStream fs = File.Open(Server.MapPath("~/Uploads/" + fileName2), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (column > 1)
                                tblData += line + ",";
                            else
                                header = line + ",prediction"; //bbha_predict
                            column++;
                        }
                    }
                    if (columns != 13)
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Unsuccesful Upload! Please Select a Dataset That Has 13 Columns')", true);
                    else
                        Label4.Text = "File Uploaded Successfully";
                }
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select a File and Try Again')", true);
        }

        protected void Runbtn2_Click(object sender, EventArgs e)
        {
            if (fileName2 == "" || Label4.Text == "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Upload a File and Try Again')", true);
            else
            {
                string path = Server.MapPath("~/Uploads/");
                MWClient client = new MWHttpClient();
                try
                {
                    IBBHA_Predict bh = client.CreateProxy<IBBHA_Predict>
                    (new Uri("http://6eb90182.ngrok.io/BBHA"));
                    bh.BBHA_Predict(out string newFileName, out int countDiseased, out int countHealthy,
                        out byte[] imgArr, out string result, tblData, str, columns, rows);

                    result = string.Concat(result.Trim('[', ']'), ';');
                    tbl = result;
                    fileName2 = newFileName;

                    StringBuilder csvcontent = new StringBuilder();
                    StringBuilder csvheader = new StringBuilder();
                    string newPath = Server.MapPath("~/Attachments/" + fileName2);
                    string[] DataRows = tbl.Split(seperator.ToCharArray());
                    csvheader.AppendLine(" ," + header);
                    foreach (string DataRow in DataRows)
                    {
                        csvcontent.AppendLine(DataRow);
                    }
                    string resultString = Regex.Replace(" ," + csvcontent.ToString(), @"^\s+$[\r\n]*", " ,", RegexOptions.Multiline);
                    resultString = Regex.Replace(resultString, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                    File.WriteAllText(newPath, csvheader.ToString());
                    File.AppendAllText(newPath, resultString);

                    fileName2 = newFileName;
                    Label4.Text = "";
                    Label5.Text = "Diseased Patients in Dataset = " + countDiseased.ToString() + " <br/>Healthy Patients in Dataset = " + countHealthy.ToString();
                    Label6.Text = newFileName;
                }
                catch (MATLABException ex)
                {
                    Console.WriteLine("{0} MATLAB exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("{0} Web exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("{0} MATLAB argument exception caught.", ex);
                    Console.WriteLine(ex.StackTrace);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }     

        protected void Downloadbtn2_Click(object sender, EventArgs e)
        {
            if (fileName2 != "" && Label6.Text != "")
            {
                string strURL = "~/Attachments/" + fileName2;
                byte[] myData = File.ReadAllBytes(Server.MapPath(strURL));
                Response.Clear();
                Response.BufferOutput = false;
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName2 + "\"");
                Response.AddHeader("Content-Length", myData.Length.ToString());
                Response.TransmitFile(Server.MapPath(strURL));
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Press Run and Try Again')", true);
            }
        }
    }
}