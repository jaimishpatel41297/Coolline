<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddBanner.aspx.cs" Inherits="AddAdvertisement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">Add Banner</h4>
            </div>
            <div class="col-lg-9 col-sm-8 col-md-8 col-xs-12">
            </div>
        </div>
        <div class="row">
            <div class="col-md-5 col-sm-12 col-xs-12">
                <div class="white-box">
                    <p class="text-muted m-b-10 font-13">
                        <div class="alert alert-success alert-dismissable" id="divSuccess" runat="server" visible="false">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            Success ! Changes Done Successfully
                        </div>
                        <div class="alert alert-danger alert-dismissable" id="divError" runat="server" visible="false">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            Failure ! Changes Not Done Successfully !
                        </div>
                    </p>
                    <div class="row">
                        <div class="col-sm-12 col-lg-12 col-xs-12">

                            <div class="form-group">
                               <label for="">
                                   Title:
                               </label>
                               <div>
                                   <asp:TextBox ID="txttitle" runat="server" ValidationGroup="AddEmployee" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>

                            
                            <div class="form-group">
                               <label for="">
                                   Sub Title:
                               </label>
                               <div>
                                   <asp:TextBox ID="txtsub" runat="server" ValidationGroup="AddEmployee" CssClass="form-control"></asp:TextBox>
                               </div>
                           </div>

                            <div class="form-group">
                                <label for="">
                                    Upload Banner(Size must be 1920x765)
                                </label>
                                <div>
                                    <asp:FileUpload runat="server" ID="file" />
                                </div>
                           </div>

                              <div class="form-group">
                                 <label for="">
                                   Status:
                                   <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddEmployee" ControlToValidate="txtsub" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                   </asp:RequiredFieldValidator>
                                     <asp:CheckBox ID="status"  runat="server"/>
                               </label>
                            </div>
                      
                            <div class="text-right">
                                <asp:LinkButton runat="server" ID="lbSubmit" ValidationGroup="AddEmployee" OnClick="lbSubmit_Click" CssClass="fcbtn btn btn-success btn-outline btn-1d waves-effect">Submit</asp:LinkButton>
                                <button type="reset" class=" fcbtn btn btn-danger btn-outline btn-1d waves-effect">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-7 col-lg-7 col-xs-12 col-sm-12">
                <div class="white-box">
                    <h3 class="box-title m-b-0">Data</h3>
                    <div class="row">
                        <div class="col-sm-12 col-xs-12">
                            <asp:GridView runat="server" ID="gv" CssClass="gvDataTable table-responsive table table-striped table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField  DataField="Title" HeaderText="TITLE"/>
                                     <asp:BoundField  DataField="SubTitle" HeaderText="SUB-TITLE"/>
                                    <asp:TemplateField  HeaderText="Banner Image" >
                                        <ItemTemplate>
                                            <img src='<%# Eval("Image") %>' alt="Image" style="height: 100px; width: 100px;">
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                     <asp:BoundField  DataField="Status" HeaderText="Status"/>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbDelete" OnClientClick="return confirm('Are You Sure Want To Delete ?')" CssClass="btn-outline btn btn-danger btn-circle waves-effect btn-sm fa fa-times" CommandArgument='<%# Eval("Id") %>' OnClick="lbDelete_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

