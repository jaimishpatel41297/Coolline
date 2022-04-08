<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddContact.aspx.cs" Inherits="Admin_AddContact" %>

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
                                    Address
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtadd" ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtadd" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    FullAddress
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtfulladd" ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtfulladd" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Longitude
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtlong" ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtlong" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Latitude
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtlat" ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtlat" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                         <div class="form-group">
                                 <label for="">
                                   Status: 
                                     <asp:CheckBox ID="status"  runat="server"/>
                               </label>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Email
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtemail" ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtemail" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group">
                                <label for="">
                                    Phone
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtphn" ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtphn" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                            
<%--<form action="#" method="post" role="form" class="contactForm">
  <div class="form-row">
    <div class="form-group col-lg-6">
      <input type="text" name="name" class="form-control" id="name" placeholder="Your Name" data-rule="minlen:4" data-msg="Please enter at least 4 chars" />
      <div class="validation"></div>
    </div>
    <div class="form-group col-lg-6">
      <input type="email" class="form-control" name="email" id="email" placeholder="Your Email" data-rule="email" data-msg="Please enter a valid email" />
      <div class="validation"></div>
    </div>
  </div>
  <div class="form-group">
    <input type="text" class="form-control" name="subject" id="subject" placeholder="Subject" data-rule="minlen:4" data-msg="Please enter at least 8 chars of subject" />
    <div class="validation"></div>
  </div>
  <div class="form-group">
    <textarea class="form-control" name="message" rows="5" data-rule="required" data-msg="Please write something for us" placeholder="Message"></textarea>
    <div class="validation"></div>
  </div>
  <div class="text-center">
    <asp:LinkButton id="submit" type="submit" runat="server" title="Send Message" OnClick="submit_Click" >Send Message</asp:LinkButton>
  </div>
</form>--%>


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
                                   <asp:BoundField DataField="Address" HeaderText="Address" />
                                   <asp:BoundField DataField="FullAddress" HeaderText="FullAddress" />
                                   <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                                   <asp:BoundField  DataField="Latitude" HeaderText="Latitude"/>
                                   <asp:BoundField  DataField="Status" HeaderText="Status"/>
                                   <asp:BoundField  DataField="Email" HeaderText="Email"/>
                                   <asp:BoundField  DataField="Phone" HeaderText="Phone"/>    
                                    
                                  <asp:TemplateField HeaderText="Action">
                                     <ItemTemplate>
                                          <asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-primary btn-circle waves-effect btn-sm fa fa-edit btn-outline" CommandArgument='<%# Eval("Id") %>' OnClick="lbEdit_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lbDelete" OnClientClick="return confirm('Are You Sure Want To Delete ?')" CssClass="btn-outline btn btn-danger btn-circle waves-effect btn-sm fa fa-times" CommandArgument='<%# Eval("Id") %>' OnClick="lbDelete_Click1"></asp:LinkButton>
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
