<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="Admin_AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">Add Product</h4>
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

                    <div class="row">
                        <div class="col-sm-12 col-lg-12 col-xs-12">
                            <div class="form-group">
                                <label for="typeahead">
                                    Product Category
                                </label>
                                <div>
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="typeahead">
                                    Product Type
                                </label>
                                <div>
                                    <asp:DropDownList ID="ddlproducttype" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Product Feature
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtfea" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Product Name
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="AddContact" ControlToValidate="txtname" ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtname" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Product Image
                                </label>
                                <div>
                                    <asp:FileUpload runat="server" ID="file" />
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="">
                                    Product Specification
                                </label>
                                <div>
                                    <asp:FileUpload runat="server" ID="file1" />
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="">
                                    Product Brouchure
                                </label>
                                <div>
                                    <asp:FileUpload runat="server" ID="file2" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="">
                                    Product Rating
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtrat" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="">
                                    Product Color
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtclr" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label for="">
                                    Product Availibility
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtavail" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="">
                                    Product ShortDescription
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtshortdes" TextMode="MultiLine" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="">
                                    Product  Description
                                </label>
                                <div>
                                    <asp:TextBox runat="server" ID="txtdesc" TextMode="MultiLine" ValidationGroup="AddContact" CssClass="form-control"></asp:TextBox>
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
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCategory" HeaderText="Product Category" />
                                    <asp:BoundField DataField="PType" HeaderText="Product Type" />
                                    <asp:BoundField DataField="Feature" HeaderText="Product Feature" />
                                    <asp:BoundField DataField="Name" HeaderText="Product Name" />
                                    <asp:BoundField DataField="Rating" HeaderText="Product Rating" />
                                    <asp:BoundField DataField="Color" HeaderText="Product Color" />   
                                    <asp:TemplateField HeaderText="Product Image">
                                        <ItemTemplate>
                                            <img src='<%# Eval("Image") %>' alt="Image" style="height: 150px; width: 150px;">
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="Product Specification">
                                        <ItemTemplate>
                                            <img src='<%# Eval("Specification") %>' alt="Image" style="height: 150px; width: 150px;">
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Brouchure">
                                        <ItemTemplate>
                                            <img src='<%# Eval("Brouchure") %>' alt="Image" style="height: 150px; width: 150px;">
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                 <%--   <asp:BoundField DataField="ShortDescription" HeaderText="Product ShortDescription" />
                                    <asp:BoundField DataField="Description" HeaderText="Product Description" />--%>

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbEdit" CssClass="btn btn-primary btn-circle waves-effect btn-sm fa fa-edit btn-outline" CommandArgument='<%# Eval("ID") %>' OnClick="lbEdit_Click"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="IbInfo" CssClass="btn-outline btn btn-info btn-circle waves-effect btn-sm fa fa-info" CommandArgument='<%# Eval("ID") %>' OnClick="IbInfo_Click"></asp:LinkButton>
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
