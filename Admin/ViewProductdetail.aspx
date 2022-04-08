<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewProductdetail.aspx.cs" Inherits="Admin_ViewProductdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">View Product Details</h4>
            </div>
            <div class="col-lg-9 col-sm-8 col-md-8 col-xs-12">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-xs-12">
                <div class="white-box">
                    <div class="row">
                        <div class="col-md-12" style="margin-bottom: 20px;">
                                <strong>Availibility</strong>
                                <br />
                                <p class="text-muted">
                                    <asp:Literal runat="server" ID="ltravail"></asp:Literal>
                                </p>
                            </div>
                            <div class="col-md-12" style="margin-bottom: 20px;">
                                <strong>ShortDescription</strong>
                                <br />
                                <p class="text-muted">
                                    <asp:Literal runat="server" ID="ltrcolon"></asp:Literal>
                                </p>
                            </div>
                        <div class="col-md-12">
                            <strong>Description</strong>
                            <br />
                            <p class="text-muted">
                                <asp:Literal runat="server" ID="ltrfrmst"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

