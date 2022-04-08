<%@ Page Title="" Language="C#" MasterPageFile="~/Coolline.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumbs-wrap style-2">
        <div class="container">
            <h1 class="page-title">Contact Us</h1>
            <ul class="breadcrumbs">
                <li><a href="Default.aspx">Home</a></li>
                <li>Contact Us</li>
            </ul>
        </div>
    </div>
    <div id="content" class="page-content-wrap">

        <div class="container">

            <div class="row">
                <div class="col-sm-4">

                    <h2 class="section-title">Contact Details</h2>
                    
                    <asp:Literal ID="ltradd" runat="server"></asp:Literal>
                    <%-- <div class="content-element4">
              <ul class="contact-info v-type">
                <li class="info-item">
                  <i class="licon-map-marker"></i>
                  <div class="item-info">
                    <span>9870 St Vincent Place, <br> Glasgow, DC 45 Fr 45.</span>
                  </div>
                </li>
                <li class="info-item">
                  <i class="licon-telephone2"></i>
                  <div class="item-info">
                    <span>Phone:</span>
                    <a href="#">987-654-3210</a>
                  </div>
                </li>
                <li class="info-item">
                  <i class="licon-at-sign"></i>
                  <div class="item-info">
                    <span>E-mail:</span>
                    <a href="mailto:#">chxckjhv@gam</a>
                  </div>
                </li>
                <li class="info-item">
                  <i class="licon-clock3"></i>
                  <div class="item-info">
                    <span>Open 8am-5pm: <br> Monday - Saturday</span>
                  </div>
                </li>
                
              </ul>
            </div>--%>

                    <ul class="social-icons style-2">

                        <li><a href="#"><i class="icon-facebook"></i></a></li>
                        <li><a href="#"><i class="icon-twitter"></i></a></li>
                        <li><a href="#"><i class="icon-gplus-3"></i></a></li>
                        <li><a href="#"><i class="icon-linkedin-3"></i></a></li>
                        <li><a href="#"><i class="icon-youtube-play"></i></a></li>
                        <li><a href="#"><i class="icon-instagram-5"></i></a></li>

                    </ul>

                </div>
                <div class="col-sm-8">
                    <div runat="server">
                        <h2>We Want to Hear From You</h2>
                        <p>We’re here to help. If you’ve got a question, we’d love to chat.</p>
                        <div class="row flex-row">
                            <div class="col-xs-6">
                                <label for="">
                                    Full Name:
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtname" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtname" runat="server" ValidationGroup="AddEmployee" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="col-xs-6">
                                <label for="">
                                    Company Name:
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtcompany" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtcompany" runat="server" ValidationGroup="AddEmployee" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row flex-row">
                            <div class="col-xs-6">
                                <label for="">
                                    Email:
                       <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtemail" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                       </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtemail" runat="server" ValidationGroup="AddEmployee" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-6">
                                <label for="">
                                    ContactNo:
                       <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtcontact" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                       </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtcontact" runat="server" ValidationGroup="AddEmployee" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row flex-row">
                            <div class="col-sm-12">
                                <label for="">
                                    Product Related Detail:
                       <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtproduct" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                       </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtproduct" runat="server" ValidationGroup="AddEmployee" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div class="row flex-row">
                            <div class="col-sm-12">
                                <label for="">
                                    Message:
                       <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtmessage" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                       </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox ID="txtmessage" runat="server" ValidationGroup="AddEmployee" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row flex-row">
                            <div class="col-sm-12">
                                <%-- <asp:LinkButton runat="server" ID="lbSubmit" ValidationGroup="AddEmployee" class="btn btn-style-5" OnClick="lbSubmit_Click">Submit</asp:LinkButton>--%>
                                <asp:LinkButton runat="server" ID="lbSubmit" OnClick="lbSubmit_Click" ValidationGroup="AddEmployee" CssClass="btn btn-style-5">Submit Request</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="page-content-wrap">

        <div class="container">

            <div class="row">
                <div runat="server">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2630.581105337015!2d72.83393672213708!3d21.184074383663184!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3be04e43dafafdc1%3A0x70fb5b4f91f86eb5!2sShlok+Business+Centre!5e0!3m2!1sen!2sin!4v1551857370313" width="600" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>
                </div>
            </div>
        </div>
    </div>

</asp:Content>













