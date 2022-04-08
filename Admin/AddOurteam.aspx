﻿<%@ Page Language="C#"  MasterPageFile="~/Admin/AdminMaster.master"  AutoEventWireup="true" CodeFile="AddOurteam.aspx.cs" Inherits="Admin_AddOurteam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">Our Team</h4>
            </div>
            <div class="col-lg-9 col-sm-8 col-md-8 col-xs-12">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
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
                   <%-- <asp:LinkButton runat="server" ID="Paytm" OnClick="Paytm_Click" CssClass="fcbtn btn btn-success btn-outline btn-1d waves-effect">Paytm</asp:LinkButton>
                    <asp:LinkButton runat="server" ID="PaytemStatus" OnClick="PaytemStatus_Click" CssClass="fcbtn btn btn-success btn-outline btn-1d waves-effect">PaytmStatus</asp:LinkButton>--%>
                    <div class="row">
                        <div class="col-sm-12 col-lg-12 col-xs-12">                            
                            <div class="form-group">
                                <label for="">
                                    Name
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtname" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtname" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Designation
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtdes" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtdes" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            
                            <div class="form-group">
                                <label for="">
                                    Photo
                                </label>
                                <div>
                                    <asp:FileUpload runat="server" ID="file" />
                                </div>
                           </div>

                             <div class="form-group">
                                <label for="">
                                    Description
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtdesc" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtdesc" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Facebook
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtfab" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtfab" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Twitter
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txttwt" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txttwt" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Google
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtglg" ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtglg" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Linkedin
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtlin" ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtlin" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Youtube
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtyou" ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtyou" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Status: 
                                     <asp:CheckBox ID="status" runat="server" />
                                </label>
                            </div>

                            <div class="text-right">
                                <asp:LinkButton runat="server" ID="lbSubmit" ValidationGroup="AddContact" OnClick="lbSubmit_Click" CssClass="fcbtn btn btn-success btn-outline btn-1d waves-effect">Submit</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lbUpdate" ValidationGroup="AddContact" OnClick="lbUpdate_Click" Visible="false" CssClass="fcbtn btn btn-primary btn-outline btn-1d waves-effect">Update</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="IbCancel" ValidationGroup="AddContact" OnClick="IbCancel_Click" CssClass="fcbtn btn btn-danger btn-outline btn-1d waves-effect">Cancel</asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-lg-12 col-xs-12 col-sm-12">
                <div class="white-box">
                    <h3 class="box-title m-b-0">Data</h3>
                    <div class="row">
                        <div class="col-sm-12 col-xs-12 table-responsive">
                            <asp:GridView runat="server" ID="gv" CssClass="gvDataTable table table-striped table-bordered" AutoGenerateColumns="false">
                               <Columns>
                                   <asp:BoundField DataField="Name" HeaderText="Name" />
                                   <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                    <asp:TemplateField  HeaderText="Member Image" >
                                        <ItemTemplate>
                                            <img src='<%# Eval("Photo") %>' alt="Image" style="height: 100px; width: 100px;">
                                        </ItemTemplate>                                       
                                 </asp:TemplateField>
                                   <asp:BoundField  DataField="Description" HeaderText="Description"/>
                                   <asp:BoundField  DataField="Facebook" HeaderText="Facebook"/>
                                   <asp:BoundField  DataField="Twitter" HeaderText="Twitter"/>
                                   <asp:BoundField  DataField="Google" HeaderText="Google"/>    
                                   <asp:BoundField  DataField="Linkedin" HeaderText="Linkedin"/>  
                                   <asp:BoundField  DataField="Youtube" HeaderText="Youtube"/>  
                                   <asp:BoundField  DataField="Status" HeaderText="Status"/>
                                 <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                   <asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-primary btn-circle waves-effect btn-sm fa fa-edit btn-outline" CommandArgument='<%# Eval("ID") %>' OnClick="lbEdit_Click"></asp:LinkButton>
                                   <asp:LinkButton runat="server" ID="lbDelete" OnClientClick="return confirm('Are You Sure Want To Delete ?')" CssClass="btn-outline btn btn-danger btn-circle waves-effect btn-sm fa fa-times" CommandArgument='<%# Eval("ID") %>' OnClick="lbDelete_Click"></asp:LinkButton>
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
