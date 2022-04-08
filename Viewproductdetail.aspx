<%@ Page Title="" Language="C#" MasterPageFile="~/Coolline.master" AutoEventWireup="true" CodeFile="Viewproductdetail.aspx.cs" Inherits="Viewproductdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .c1 {
            margin-top: 3%;
            padding-bottom: 1px;
            margin-bottom: 5%;
            border-top: 4px solid #00cdff;
        }

        .c2 {
            margin-top: 9%;
            padding-bottom: 1px;
            margin-bottom: 3%;
            border-top: 4px solid #00cdff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-section-bg">
        <div class="container">
            <div class="col-lg-5" style="height: 100px; opacity: 0.8; margin-top: -48px; background-color: #0099CC;">
                <asp:Literal runat="server" ID="ltrtop"></asp:Literal>
            </div>
            <div class="entry-box">
                <div class="row" style="background-color: #eaecef">
                    <asp:Literal ID="ltrmid" runat="server"></asp:Literal>
                    <div class="col-lg-12" style="margin-top: 33px;">
                        <asp:Literal runat="server" ID="ltrmidin"></asp:Literal>
                        <div class='c1'></div>
                        <div runat="server" visible="false" id="ltrshow">
                            <h3 style="text-align: center;">Silent Feature</h3>
                            <div class="carousel-type-2">
                                <div class="owl-carousel" data-item-margin="30" data-max-items="3" data-autoplay="true">
                                    <asp:Literal ID="ltrfea" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class='c2'></div>
                        </div>
                        <asp:Literal runat="server" ID="ltrdonloadlink"></asp:Literal>
                    </div>
                </div>
        </div>
    </div>
</asp:Content>

