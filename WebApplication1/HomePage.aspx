<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplication1.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Header -->
    <!-- Banner -->
    <section id="banner">
        <h1>Welcome to Mo-BBHA</h1>
        <p>A free responsive gene selection service website.</p>
    </section>
    <form runat="server">
        <!-- One -->
        <section id="one" class="wrapper">
            <div class="inner">
                <header class="align-center">

                    <h2>What do we provide?</h2>
                </header>
                <div class="flex flex-3">
                    <article>
                        <header>
                            <h3>Desktop Application</h3>
                        </header>
                        <p>Now you can download the executable file from our website so you can get the medical results of your dataset.</p>
                        <footer>
                            <asp:Button ID="Button1" runat="server" Text="Download" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                                OnClick="Downloadbtn1_Click" />
                        </footer>
                    </article>
                    <article>
                        <header>
                            <h3>Online Application</h3>
                        </header>
                        <p>With our online application service you can upload your .csv files and get the results.</p>
                        <footer>
                            <br />
                            <asp:Button ID="Button2" runat="server" Text="TAKE ME THERE!!" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                                OnClick="Therebtn_Click" />
                        </footer>
                    </article>
                    <article>
                        <header>
                            <h3>Mobile Android Application</h3>
                        </header>
                        <p>Download the Mobile Application version of our service that allows you to carry a portable laboratory with you everywhere you go.</p>
                        <footer>
                            <asp:Button ID="Button3" runat="server" Text="Download" Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                                OnClick="Downloadbtn2_Click" />
                        </footer>
                    </article>
                </div>
            </div>
        </section>

        <!-- Two -->
        <section id="two" class="wrapper style1 special">
            <div class="inner">
                <header>
                    <h2>Our Team</h2>
                    <p>The team responsible to make this work done.</p>
                </header>
                <div class="flex flex-4">
                    <div class="box person">
                        <div class="image round">
                            <img src="images/pic03.jpg" alt="Person 1" />
                        </div>
                        <h3>Anas Alamin</h3>
                        <p>Software Engineer</p>
                    </div>
                    <div class="box person">
                        <div class="image round">
                            <img src="images/mypic.jpg" alt="Person 2" />
                        </div>
                        <h3>Omar Gobran Bey</h3>
                        <p>Software Engineer</p>
                    </div>
                </div>
            </div>
        </section>
        <section id="contact" class="wrapper special">
            <div class="inner">
                <header>
                    <h2>Please fill the following to send us an E-Mail.</h2>
                    <p>We reach you wherever you are</p>
                </header>

                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                    <p>
                        Your Name:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                ControlToValidate="YourName" ValidationGroup="save" /><br />
                        <asp:TextBox ID="YourName" Style="border-width: thick; border-radius: 30px; -moz-appearance: none; -webkit-appearance: none; -ms-appearance: none; appearance: none; background: rgba(255, 255, 255, 0.075); border-radius: 30; border: none; border: solid 1px #dbdbdb; color: inherit; outline: 0; border-color: #5385c1; margin-left: 10%; padding: 0 1em; text-decoration: none; width: 80% !important;"
                            runat="server" Width="250px" /><br />
                        <span>E-mail Address:</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="YourEmail" ValidationGroup="save" /><br />
                        <asp:TextBox ID="YourEmail" Style="border-width: thick; border-radius: 30px; -moz-appearance: none; -webkit-appearance: none; -ms-appearance: none; appearance: none; background: rgba(255, 255, 255, 0.075); border-radius: 30; border: none; margin-left: 10%; border: solid 1px #dbdbdb; color: inherit; outline: 0; border-color: #5385c1; padding: 0 1em; text-decoration: none; width: 80% !important;"
                            runat="server" Width="250px" />
                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator23"
                            SetFocusOnError="true" Text="Example: username@gmail.com" ControlToValidate="YourEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"
                            ValidationGroup="save" /><br />
                        Subject:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                ControlToValidate="YourSubject" ValidationGroup="save" /><br />
                        <asp:TextBox ID="YourSubject" Style="border-width: thick; border-radius: 30px; -moz-appearance: none; -webkit-appearance: none; -ms-appearance: none; appearance: none; background: rgba(255, 255, 255, 0.075); border-radius: 30; border: none; border: solid 1px #dbdbdb; margin-left: 10%; color: inherit; outline: 0; border-color: #5385c1; padding: 0 1em; text-decoration: none; width: 80% !important;"
                            runat="server" Width="400px" /><br />
                        Your Question:
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                ControlToValidate="Comments" ValidationGroup="save" /><br />
                        <asp:TextBox ID="Comments" runat="server"
                            Style="border-width: thick; border-radius: 30px; -moz-appearance: none; -webkit-appearance: none; -ms-appearance: none; appearance: none; background: rgba(255, 255, 255, 0.075); border-radius: 30; border: none; margin-left: 10%; border: solid 1px #dbdbdb; color: inherit; outline: 0; border-color: #5385c1; padding: 0 1em; text-decoration: none; width: 80% !important;"
                            TextMode="MultiLine" Rows="10" Width="400px" />
                    </p>
                    <p>
                        <asp:Button ID="btnSubmit" runat="server" Text="Send"
                            Style="margin-bottom: 10px; margin-top: 10px; background-color: #5385c1; color: #ffffff !important;"
                            OnClick="Submitbtn_Click" ValidationGroup="save" />
                    </p>
                </asp:Panel>
                <p>
                    <asp:Label ID="DisplayMessage" runat="server" Visible="false" />
                </p>
            </div>
        </section>
    </form>
</asp:Content>
