<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddMenumaster.aspx.cs" Inherits="Admin_AddMenumaster" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">Contact</h4>
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
                                    Url
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txturl" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txturl" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Content
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtcon" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtcon" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    ParentId
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtpid" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtpid" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Sequence
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtseq" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtseq" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Type
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txttype" ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txttype" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                                   
                         <div class="form-group">
                                 <label for="">
                                   IsShown 
                                     <asp:CheckBox ID="status"  runat="server"/>
                               </label>
                            </div>
                           
                             <div class="form-group">
                                <label for="">
                                    ID
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtid" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtid" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                             <div class="form-group">
                                <label for="">
                                    PageTitle
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txttit" ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txttit" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                             <div class="form-group">
                                <label for="">
                                    PageDescription
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtdes" ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtdes" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                             <div class="form-group">
                                <label for="">
                                    PageKeyword
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtkey" ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtkey" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
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
                                   <asp:BoundField DataField="Url" HeaderText="Url" />
                                   <asp:BoundField DataField="[Content]" HeaderText="Content" />
                                   <asp:BoundField  DataField="ParentId" HeaderText="ParentId"/>
                                   <asp:BoundField  DataField="Sequence" HeaderText="Sequence"/>
                                   <asp:BoundField  DataField="Type" HeaderText="Type"/>
                                   <asp:BoundField  DataField="IsShown" HeaderText="IsShown"/>  
                                   <asp:BoundField  DataField="ID" HeaderText="ID"/>  
                                   <asp:BoundField  DataField="PageTitle" HeaderText="PageTitle"/>  
                                   <asp:BoundField  DataField="PageDescription" HeaderText="PageDescription"/>  
                                   <asp:BoundField  DataField="PageKeyword" HeaderText="PageKeyword"/>    
                                    
                                  <asp:TemplateField HeaderText="Action">
                                     <ItemTemplate>
                                          <asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-primary btn-circle waves-effect btn-sm fa fa-edit btn-outline" CommandArgument='<%# Eval("MenuId") %>' OnClick="lbEdit_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbDelete" OnClientClick="return confirm('Are You Sure Want To Delete ?')" CssClass="btn-outline btn btn-danger btn-circle waves-effect btn-sm fa fa-times" CommandArgument='<%# Eval("MenuId") %>' OnClick="lbDelete_Click"></asp:LinkButton>
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
