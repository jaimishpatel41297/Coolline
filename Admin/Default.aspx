<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />
  <meta name="msapplication-tap-highlight" content="no" />
  <meta name="description" content="The Happy Family Foundation" />
  <meta name="keywords" content="The Happy Family Foundation,family foundation" />
  <title>Coolline Website Admin Panel</title>
  <link rel="icon" href="plugins/images/weblogo2.png" sizes="32x32" />
  <link rel="apple-touch-icon-precomposed" href="" />
  <meta name="msapplication-TileColor" content="#00bcd4" />
  <meta name="msapplication-TileImage" content="plugins/images/favicon_final.png" />

  <!-- CORE CSS-->  
    <link href="css1/materialize.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css1/style1.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css1/custom-style.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css1/style-horizontal.css" type="text/css" rel="stylesheet" media="screen,projection" />
   <link href="css1/page-center.css" type="text/css" rel="stylesheet" media="screen,projection" />

  <!-- INCLUDED PLUGIN CSS ON THIS PAGE -->
  <link href="css1/prism.css" type="text/css" rel="stylesheet" media="screen,projection" />
  <link href="js1/plugins/perfect-scrollbar/perfect-scrollbar.css" type="text/css" rel="stylesheet" media="screen,projection" />
</head>
<body class="yellow">
    <!-- Start Page Loading -->
  <div id="loader-wrapper">
      <div id="loader"></div>        
      <div class="loader-section section-left"></div>
      <div class="loader-section section-right"></div>
  </div>
  <!-- End Page Loading -->
  <div id="login-page" class="row">
    <div class="col s12 z-depth-4 card-panel">
      <form class="login-form" runat="server">
        <div class="row">
          <div class="input-field col s12 center">
            <%--<img src="plugins/images/weblogo2.png" alt="" class="circle responsive-img valign profile-image-login" />--%>
            <p class="center login-form-text">Coolline</p>
          </div>
        </div>
        <div class="row margin">
          <div class="input-field col s12">
            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
            <label for="username" class="center-align">Username</label>
          </div>
        </div>
        <div class="row margin">
          <div class="input-field col s12">
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
             <label for="password" class="center-align">Password</label>
          </div>
        </div>
        
        <div class="row">
          <div class="input-field col s12">
                <asp:LinkButton runat="server" ID="btnLogin" CssClass="btn waves-light col s12" OnClick="btnLogin_Click" Text="Login"></asp:LinkButton>
          </div>
        </div>
        <div class="row">
        </div>
      </form>
    </div>
  </div>
  <script type="text/javascript" src="js1/jquery-1.11.2.min.js"></script>
  <script type="text/javascript" src="js1/materialize.js"></script>
  <script type="text/javascript" src="js1/prism.js"></script>
  <script type="text/javascript" src="js1/plugins/perfect-scrollbar/perfect-scrollbar.min.js"></script>  
  <script type="text/javascript" src="js1/plugins.js"></script>
</body>
</html>
