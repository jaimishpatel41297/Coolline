<%@ Page Title="" Language="C#" MasterPageFile="~/Coolline.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .c1 {
            margin-top: 19%;
            padding-top: 30px;
            padding-bottom: 1px;
            border-top: 4px solid #00cdff;
        }
       a:hover{
           color:#00CDFF;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumbs-wrap style-2">
        <div class="container">
            <h1 class="page-title">Product</h1>
            <ul class="breadcrumbs">
                <li><a href="Default.aspx">Home</a></li>
                <li>Product</li>
            </ul>
        </div>
    </div>
    <div class="page-section-bg">
        <div class="container">
            <asp:Literal runat="server" ID="productheader"></asp:Literal>
            <br />
            <br />
            <h3>Product Lineup</h3>
            <hr style="border: 0.5px solid;width: 32.0%;">
            <div  class="entry-box">
                <div class="row">
                    <asp:Literal ID="ltrproduct" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>

    <%-- <div id="content" class="page-content-wrap">
        <div class="container extra-width" style="width:1539px;">

            <div class="insta-gallery content-element5" style="margin-left:84px;">
                <div id="instafeed" class="instagram-feed" >
                    <h4>INVERTER & NON-INVERTER PRODUCT</h4>
                  <asp:Literal ID="ltrproduct" runat="server"></asp:Literal>

                    <%-- <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/35575df757f341862511d9e0da045110/5D210296/t51.2885-15/sh0.08/e35/s640x640/23161859_144225369545900_1734825943274356736_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/35575df757f341862511d9e0da045110/5D210296/t51.2885-15/sh0.08/e35/s640x640/23161859_144225369545900_1734825943274356736_n.jpg?_nc_ht=scontent.cdninstagram.com"/></a>
                        <div class='description-inner'>
                            <h4 class='project-title'><a href='#'> Name </a></h4>
                            <ul class='project-cats'>
                           <li><a href='#'>Feature </a></li>
                            </ul>
                          <p>Description</p>
                          <a href='#' class='info-btn'>Product details</a>
                           </div>
                    </li>--%>

    <%--  <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/f697dddac1dbe84f2b34996519be0e1b/5D247E1F/t51.2885-15/sh0.08/e35/s640x640/23347545_140604276590226_3123242347261853696_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/f697dddac1dbe84f2b34996519be0e1b/5D247E1F/t51.2885-15/sh0.08/e35/s640x640/23347545_140604276590226_3123242347261853696_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/fa2f79023dddcdd685b4c02af2d88acd/5CEC388E/t51.2885-15/sh0.08/e35/s640x640/23164460_833200426840168_7778836052579450880_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/fa2f79023dddcdd685b4c02af2d88acd/5CEC388E/t51.2885-15/sh0.08/e35/s640x640/23164460_833200426840168_7778836052579450880_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/d8ef9aae6894c3f2dcbbca46f0b501db/5CECFB4D/t51.2885-15/sh0.08/e35/s640x640/23164017_154278791844839_8227304052157841408_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/d8ef9aae6894c3f2dcbbca46f0b501db/5CECFB4D/t51.2885-15/sh0.08/e35/s640x640/23164017_154278791844839_8227304052157841408_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/f63ed7e8dbc6a9cf6364af904a7c4aa0/5D222AF6/t51.2885-15/sh0.08/e35/s640x640/23279835_1368373293271680_1823138433749483520_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/f63ed7e8dbc6a9cf6364af904a7c4aa0/5D222AF6/t51.2885-15/sh0.08/e35/s640x640/23279835_1368373293271680_1823138433749483520_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/64f6963ce2a465a36dfc5055395d89c4/5D0A2428/t51.2885-15/sh0.08/e35/s640x640/23279310_1326945710767708_2008653284037885952_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/64f6963ce2a465a36dfc5055395d89c4/5D0A2428/t51.2885-15/sh0.08/e35/s640x640/23279310_1326945710767708_2008653284037885952_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/ecce32cd49fe5c1868602be085f957b3/5D2808D5/t51.2885-15/sh0.08/e35/s640x640/23347399_1981308695450060_930567010423668736_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/ecce32cd49fe5c1868602be085f957b3/5D2808D5/t51.2885-15/sh0.08/e35/s640x640/23347399_1981308695450060_930567010423668736_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>
                    <li class="nv-instafeed-item"><a class="fancybox nv-lightbox" data-fancybox="instagram" href="https://scontent.cdninstagram.com/vp/2cab0207155aeaf8790011cecc0da1c6/5D0215AE/t51.2885-15/sh0.08/e35/s640x640/23421578_141626423153176_7303543864818663424_n.jpg?_nc_ht=scontent.cdninstagram.com" title="">
                        <img src="https://scontent.cdninstagram.com/vp/2cab0207155aeaf8790011cecc0da1c6/5D0215AE/t51.2885-15/sh0.08/e35/s640x640/23421578_141626423153176_7303543864818663424_n.jpg?_nc_ht=scontent.cdninstagram.com"></a></li>--%>

    <%--  </div>--%>
    <%-- <div id="instafeed" class="instagram-feed" data-instagram="9"></div>--%>
    <%--</div>

           
        </div>

    </div>--%>
</asp:Content>

