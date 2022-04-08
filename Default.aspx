<%@ Page Title="" Language="C#" MasterPageFile="~/Coolline.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>--%>
     <link rel="stylesheet" href="css/Project.css" />
     <link rel="stylesheet" href="/resources/demos/style.css"/>
     <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script>
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <style>
        * {
            box-sizing: border-box;
        }

        /*body {
            font-family: Verdana, sans-serif;
        }*/

        .mySlides {
            display: none;
        }

        img {
            vertical-align: middle;
        }

        /* Slideshow container */
        .slideshow-container {
            max-width: 100%;
            position: relative;
            margin: auto;
        }

        /* Caption text */
        .text {
            color: #f2f2f2;
            font-size: 15px;
            padding: 8px 12px;
            position: absolute;
            bottom: 8px;
            width: 100%;
            text-align: center;
        }

        /* Number text (1/3 etc) */
        .numbertext {
            color: #f2f2f2;
            font-size: 12px;
            padding: 8px 12px;
            position: absolute;
            top: 0;
        }

        /* The dots/bullets/indicators */
        .dot {
            height: 15px;
            width: 15px;
            margin: 0 2px;
            background-color: #bbb;
            border-radius: 50%;
            display: inline-block;
            transition: background-color 0.6s ease;
        }

        .active {
            background-color: #717171;
        }

        /* Fading animation */
        .fade {
            -webkit-animation-name: fade;
            -webkit-animation-duration: 1.5s;
            animation-name: fade;
            animation-duration: 1.5s;
        }

        @-webkit-keyframes fade {
            from {
                opacity: .4;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes fade {
            from {
                opacity: .4;
            }

            to {
                opacity: 1;
            }
        }

        /* On smaller screens, decrease text size */
        @media only screen and (max-width: 300px) {
            .text {
                font-size: 11px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div class="slideshow-container">

        <div class="mySlides fade">
            <div class="numbertext">1 / 3</div>
            <img src="images/1920x635_slide1.jpg" style="width: 100%" />
        </div>

        <div class="mySlides fade">
            <div class="numbertext">2 / 3</div>
            <img src="images/1920x765_slide2.jpg" style="width: 100%" />
        </div>

        <div class="mySlides fade">
            <div class="numbertext">3 / 3</div>
            <img src="images/1920x635_slide3.jpg" style="width: 100%" />
        </div>

    </div>--%>
    <asp:Literal runat="server" ID="ltrbann"></asp:Literal>

    <div id="calc-item" class="icons-box icons-bg style-2 type-2">
        <div class="flex-row fx-col-5">

            <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
            <div class="icons-wrap">

                <div class="icons-item">
                    <div class="item-box">
                        <i class="icon cicon-heating"></i>
                        <h3 class="icons-box-title"><a href="#">Heating</a></h3>
                        <p>Description of the Cool line System. </p>
                        <a href="#" class="btn btn-small btn-style-5">Learn more</a>
                    </div>
                </div>

            </div>

            <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
            <div class="icons-wrap">

                <div class="icons-item">
                    <div class="item-box">
                        <i class="icon cicon-cooling"></i>
                        <h3 class="icons-box-title"><a href="#">Cooling</a></h3>
                        <p>Description of the Cool line System. </p>
                        <a href="#" class="btn btn-small btn-style-5">Learn more</a>
                    </div>
                </div>

            </div>

            <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
            <div class="icons-wrap">

                <div class="icons-item">
                    <div class="item-box">
                        <i class="icon cicon-plumbing"></i>
                        <h3 class="icons-box-title"><a href="#">Plumbing</a></h3>
                        <p>Description of the Cool line System. </p>
                        <a href="#" class="btn btn-small btn-style-5">Learn more</a>
                    </div>
                </div>

            </div>

            <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
            <div class="icons-wrap">

                <div class="icons-item">
                    <div class="item-box">
                        <i class="icon cicon-air-quality"></i>
                        <h3 class="icons-box-title"><a href="#">Quality</a></h3>
                        <p>Description of the Cool line System. </p>
                        <a href="#" class="btn btn-small btn-style-5">Learn more</a>
                    </div>
                </div>

            </div>

            <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
            <div class="icons-wrap">

                <div class="icons-item">
                    <div class="item-box">
                        <i class="icon cicon-electircal"></i>
                        <h3 class="icons-box-title"><a href="#">Electric</a></h3>
                        <p>Description of the Cool line System. </p>
                        <a href="#" class="btn btn-small btn-style-5">Learn more</a>
                    </div>
                </div>

            </div>

        </div>

    </div>
    <!--<div style="text-align:center">
            <span class="dot"></span>
            <span class="dot"></span>
            <span class="dot"></span>
        </div>-->
    <!-- - - - - - - - - - - - - - End of Slider - - - - - - - - - - - - - - - - -->
    <!-- - - - - - - - - - - - - - Content - - - - - - - - - - - - - - - - -->

    <div id="content">

        <div class="page-section-bg4 type4 half-bg-col page-section-bg">

            <!--<div class="img-col-right">
                    <a href="https://www.youtube.com/embed/XvHWOQguSek?rel=0&amp;showinfo=0&amp;autohide=2&amp;controls=0&amp;playlist=J2Y_ld0KL-4&amp;enablejsapi=1" class="col-bg" data-bg="http://coolline.co.in/images/about%20ac-pic.png" data-fancybox="video"></a>
                </div>-->
            <br />
            <div class="container extra-size2">
                <div class="row">
                    <div class="col-md-6">
                        <p class="text-size-medium">
                            <br />
                            <br />
                            <img src="images/aboutus2.jpg" />
                        </p>
                    </div>
                    <div class="col-md-6">
                        <!--<h5 class="section-sub-title">Welcome!</h5>-->
                        <h1 class="section-title">ABOUT US</h1>
                        <p class="text-size-medium">
                            Cooline is authorized Sales and Service Dealer for reputed air conditioning brands such as Daikin. We are in the air conditioning industry since last 41 years. We are Daikin approved Sales & Service Dealer partner, part of a network of independent & Highly skilled engineers.
                                Our professional advice is based on many years of experience, technical know how and product knowledge. You can be certain that if we recommended an air conditioning system it will be the best possible solution for your valued requirement & because of services we could achieve No. 2 Dealer position for sales & services of Daikin in all India basis. 'In terms of price and quality only gets you into the game. Service wins the game." we were & we are winning this game positively encourages Dealer Partners to become part of an independent recognized organization whose purpose is to continually raise industry standards.
                        </p>
                        <a href="#" class="btn btn-small">More About Us</a>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <!-- page section -->
    <!--<div class="page-section testimonial-holder with-bg-img align-center" data-bg="http://coolline.co.in/images/Testimonal%20BG.png">

                <div class="container">
                    <div class="row">
                        <div class="col-md-4 col-sm-12">

                            <div class="content-element4">

                                <h5 class="section-sub-title">Coupons</h5>
                                <h2 class="section-title">Special Online Offers</h2>

                            </div>
                            <div class="carousel-type-1">

                                <div class="owl-carousel" data-max-items="1" data-autoplay="true">

                                    <div class="item-carousel">
                                        <a href="#" class="coupon">
                                            <div class="inner">
                                                <h2 class="price-title"><span>$</span>25 OFF</h2>
                                                <div class="disc-for">ON ANY REPAIR</div>
                                                <div class="btn btn-style-3 btn-small">Click to print</div>
                                                <p>Must be presented at the time of service. Can not be combined with any other offer.</p>
                                            </div>
                                        </a>
                                    </div>
                                   
                                </div>

                                <a href="#" class="btn btn-small">More Coupons</a>

                            </div>

                        </div>
                        <div class="col-md-12 col-sm-12">

                            <div class="content-element4">

                                <h5 class="section-sub-title">Testimonials</h5>
                                <h2 class="section-title">Our Branches</h2>

                            </div>


                            <div class="icons-box style-3 icons-bg">

                                <div class="flex-row fx-col-3">

                                    <div class="icons-wrap">

                                        <div class="icons-item">
                                            <div class="item-box">
                                                <i class="licon-home"></i>
                                                <h3 class="icons-box-title"><a href="#">Reg. Office</a></h3>
                                                <p>1st Floor,Dr. Mansukhlal Tower, Athwagate Circle, Surat </p>
                                                <a href="#" class="btn btn-small btn-style-5">Get Service Now</a>
                                            </div>
                                        </div>

                                    </div>
                        </div>
                    </div>
                </div>

            </div>-->

    <div class="page-section-bg2 parallax-section" data-bg="images/1920x1365_bg1.jpg" style="background-image: url(&quot;images/1920x1365_bg1.jpg&quot;);">

        <div class="container extra-width">

            <h2 class="section-title align-center">Our Branches</h2>

            <div class="icons-box style-4">

                <div class="flex-row fx-col-3">

                    <asp:Literal ID="ltrbranch" runat="server"></asp:Literal>

                    <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->
                    <%-- <div class="icons-wrap">

                                <div class="icons-item">
                                    <div class="item-box">
                                        <i class="icon-location"></i>
                                        <h5 class="icons-box-title"><a href="#">Reg. Office</a></h5>
                                        <p>1st Floor,Dr. Mansukhlal Tower, Athwagate Circle, Surat </p>
                                    </div>
                                </div>

                            </div>--%>

                    <!-- - - - - - - - - - - - - - Icon Box Item - - - - - - - - - - - - - - - - -->

                </div>

            </div>

        </div>

    </div>
    <!------------------- Tab view of project --------------------------->
    <div class="page-section type2" style="height:1000px">
        <div class="container">
            <div class="content-element4 align-center">
                <h2 class="section-title">our coolest projects</h2>
                <div id="tabs" >
                    <ul style="height: 100%;display: flex;justify-content: center;align-items: center;">
                        <li><a href="#tabs-1">All</a></li>
                        <li><a href="#tabs-2">Commercial</a></li>
                        <li><a href="#tabs-3">Appartment</a></li>
                    </ul>
                    <div id="tabs-1" >
                        <asp:Literal ID="ltrall" runat="server"></asp:Literal>
                    </div>
                    <div id="tabs-2">
                        <asp:Literal ID="ltrcom" runat="server"></asp:Literal>
                    </div>
                    <div id="tabs-3">
                        <asp:Literal ID="ltrres" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--<div class="page-section-bg">
        <div class="container">
            <div class="content-element4 align-center">
                <h2 class="section-title">our coolest projects</h2>
                <%--<h5 class="section-sub-title">look! why we are the best!</h5>--%>
    <%-- </div>

            <div class="entry-box">

                <div class="row">--%>
    <%--<asp:Literal ID="ltrproject" runat="server"></asp:Literal>--%>
    <%-- <div class="col-md-4 col-sm-12">
                                <!-- entry -->
                                <div class="entry">
                                    <!-- - - - - - - - - - - - - - Entry attachment - - - - - - - - - - - - - - - - -->
                                    <div class="thumbnail-attachment">
                                        <a href="#"><img src="images/290x208_img7.jpg" height="270" alt=""/></a>
                                    </div>
                                   <!-- - - - - - - - - - - - - - Entry body - - - - - - - - - - - - - - - - -->
                                    <div class="entry-body">
                                       <div class="entry-meta">
                                            <time class="entry-date" datetime="2016-08-27">August 27, 2018</time>
                                            <a href="#" class="entry-cat">News</a>
                                        </div>-->
                                        <h4 class="entry-title"><a href="#">Modern Office</a></h4>
                                        <p>Indoor Units- 15 units of Ceiling Mounted Duct type (High Static) Control system- Intelligent Touch Controller.</p>
                                        <a href="#" class="info-btn">Read more</a>
                                    </div>
                                </div>
                            </div>--%>
    <%--</div>
            </div>
        </div>
    </div>--%>

    <!-- call out-->
    <!-- page section -->



    <!-- banners -->
    <!--<div class="banners" style="background-image:url(http://coolline.co.in/images/Testimonal%20BG.png); height:100%">

                <div class="bg-col-left"></div>
                <div class="bg-col-right"></div>

                <div class="container">

                    <div class="row">
                        <div class="col-md-12">

                            <div class="banner-inner">
                                <div class="left-side">
                                    <h2 class="banner-title">Financing Available</h2>
                                    <p>With Approved Credit</p>
                                </div>
                                <div class="right-side">
                                    <a href="#" class="btn btn-style-5 btn-big">Learn More</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">

                            <div class="banner-inner">
                                <div class="left-side">
                                    <h2 class="banner-title">Join Our Team</h2>
                                    <p>We Have Jobs Available</p>
                                </div>
                                <div class="right-side">
                                    <a href="#" class="btn btn-style-4 btn-big">Learn More</a>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>

            </div>-->
    <!-- page section -->
    <div class="page-section-bg">
        <div class="container">
            <div class="carousel-type-2" style="height:111px;">
                <div class="owl-carousel" data-item-margin="30" data-max-items="6" data-autoplay="true">
                    <asp:Literal ID="clientimg" runat="server"></asp:Literal>
                    <%--<div class="item-carousel">
                       
                        <%--<a href="#"><img src="images/165x120_brend1.jpg" alt=""/></a>--%>
                    <%-- </div>--%>

                    <%--<div class="item-carousel">

                                <a href="#"><img src="images/165x120_brend2.jpg" alt=""/></a>

                            </div>
                            <div class="item-carousel">

                                <a href="#"><img src="images/165x120_brend4.jpg" alt=""/></a>

                            </div>

                            <div class="item-carousel">

                                <a href="#"><img src="images/165x120_brend5.jpg" alt=""/></a>

                            </div>

                            <div class="item-carousel">

                                <a href="#"><img src="images/165x120_brend6.jpg" alt=""/></a>

                            </div>
                            <div class="item-carousel">

                                <a href="#"><img src="images/165x120_brend7.jpg" alt=""/></a>

                            </div>--%>
                </div>
            </div>

        </div>

    </div>
    <!-- page section -->


    <!-- newsletter -->
    <div class="call-out-form">
        <div class="container">
            <div class="call-out-item">

                <span class="call-out-icon cicon-glove"></span>

                <div class="row">
                    <div class="col-md-3 col-sm-12">

                        <h2 class="nl-title" style="color: white">CONNECT WITH US</h2>
                        <h6 style="color: white"><span>Feel Free To Connect With Us Anytime</span> 987-654-3210</h6>

                    </div>
                    <div class="col-md-9 col-sm-12">

                        <div runat="server" class="form-wrap flex-row fx-col-3 style-2">

                            <div class="form-col">
                                <asp:TextBox runat="server" ID="txtname" CssClass="form-control" ValidationGroup="AddEmployee" placeholder="Name"></asp:TextBox>
                            </div>


                            <div class="form-col">
                                <asp:TextBox runat="server" ID="txtemail" CssClass="form-control" ValidationGroup="AddEmployee" placeholder="Email"></asp:TextBox>
                            </div>


                            <div class="form-col">
                                <asp:TextBox runat="server" ID="txtcompany" CssClass="form-control" ValidationGroup="AddEmployee" placeholder="Company Name"></asp:TextBox>
                                <%--<input type="tel" name="cf-phone" placeholder="Company Name *" required="required"/>--%>
                            </div>


                            <div class="form-col col-lg-12 col-md-6">
                                <asp:TextBox runat="server" ID="txtMeassage" CssClass="form-control" ValidationGroup="AddEmployee" TextMode="MultiLine" placeholder="Message"></asp:TextBox>
                                <%-- <textarea name="cf-message" placeholder="Your Message " rows="5"></textarea>--%>
                            </div>

                            <div class="form-col">
                                <asp:TextBox runat="server" ID="txtmob" CssClass="form-control" ValidationGroup="AddEmployee" placeholder="Mobile No"></asp:TextBox>
                                <%--<input type="tel" name="cf-phone" placeholder="Mobile Number *" required="required"/>--%>
                            </div>

                            <div class="form-col">
                                <asp:LinkButton runat="server" ID="lbSubmit" OnClick="lbSubmit_Click" ValidationGroup="AddEmployee" CssClass="btn btn-style-5 full-width">Submit Request</asp:LinkButton>
                                <%--<button type="submit" data-type="submit" class="btn btn-style-5 full-width">Submit Request</button>--%>
                            </div>

                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

