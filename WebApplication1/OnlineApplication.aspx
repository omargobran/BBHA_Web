<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OnlineApplication.aspx.cs" Inherits="WebApplication1.OnlineApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="header">
        <div class="layerHeader">
            <h2 style="font-size: 50px; padding-top: 100px;">Online Application</h2>
            <p>Try it out now</p>
        </div>
    </header>
    <section id="three" class="wrapper special">
        <div class="inner">
            <article>
                <header>
                    <h3 style="padding-top: 20px;">Praesent placerat magna</h3>
                </header>
                <p>Praesent dapibus, neque id cursus faucibus, tortor neque egestas augue, eu vulputate magna eros eu erat. Aliquam erat volutpat. Nam dui mi, tincidunt quis, accumsan porttitor lorem ipsum.</p>
            </article>
        </div>
    </section>
    <form runat="server">
        <section id="two" class="wrapper style1 special">
            <div class="inner">
                <header>
                    <h2>Online Feature Selection</h2>
                    <p>Try the best Feature Selection for genes on the internet.</p>
                </header>
                <div class="flex flex-4">
                    <asp:Button ID="browsebtn1" Text="Browse your file" runat="server" Style="background-color: transparent !important; color: #fff !important; border-style: solid; border-width: 1px; border-color: #fff; margin-bottom: 10px; margin-top: 10px; animation: animate 1.5s linear infinite; border-radius: 30px;" />
                    <asp:FileUpload ID="FileUpload1" runat="server" Style="display: none" />
                    <asp:Button ID="uploadbtn1" runat="server" Text="Upload" Style="background-color: #ffffff !important; color: rgb(83, 133, 193) !important; margin-bottom: 10px; margin-top: 10px;" OnClick="Uploadbtn1_Click" />
                    <asp:Button ID="runbtn1" runat="server" Text="Run" Style="background-color: #ffffff !important; color: rgb(83, 133, 193) !important; margin-bottom: 10px; margin-top: 10px;" OnClick="Runbtn1_Click" />
                    <asp:Button ID="downloadbtn1" runat="server" Text="Download" Style="background-color: #ffffff !important; color: rgb(83, 133, 193) !important; margin-bottom: 10px; margin-top: 10px;" OnClick="Downloadbtn1_Click" />
                </div>
                <div>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="" Style="font-size: 30px; font-weight: 200"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text="" Style="font-size: 30px; font-weight: 200"></asp:Label><br />
                    <asp:Label ID="Label3" runat="server" Text="" Style="font-size: 30px; font-weight: 200"></asp:Label>
                </div>
            </div>
        </section>
        <section id="four" class="wrapper special">
            <div class="inner">
                <header>
                    <h2>Online Disease Predictor</h2>
                    <p>Try the best Predictor for diseases on the internet.</p>
                </header>
                <div class="flex flex-4">
                    <asp:Button ID="browsebtn2" runat="server" Text="Browse your file" Style="background-color: transparent !important; margin-bottom: 10px; margin-top: 10px; color: #5385c1 !important; border-style: solid; border-color: #5385c1; border-width: 1px; animation: animate1 1.5s linear infinite; border-radius: 30px" />
                    <asp:FileUpload ID="FileUpload2" runat="server" Style="display: none" />
                    <asp:Button ID="uploadbtn2" runat="server" Text="Upload" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                        OnClick="Uploadbtn2_Click" />
                    <asp:Button ID="runbtn2" runat="server" Text="Run" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                        OnClick="Runbtn2_Click" />
                    <asp:Button ID="downloadbtn2" runat="server" Text="Download" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                        OnClick="Downloadbtn2_Click" />
                </div>
                <div>
                    <br />
                    <asp:Label ID="Label4" runat="server" Style="font-size: 30px; font-weight: 200"></asp:Label><br />
                    <asp:Label ID="Label5" runat="server" Style="font-size: 30px; font-weight: 200"></asp:Label><br />
                    <asp:Label ID="Label6" runat="server" Style="font-size: 30px; font-weight: 200"></asp:Label>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
