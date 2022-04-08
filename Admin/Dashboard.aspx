<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="cp_sms_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="row bg-title">
            <div class="col-lg-3 col-md-4 col-sm-4 col-xs-12">
                <h4 class="page-title">Dashboard</h4>
            </div>
            
            <!-- /.col-lg-12 -->
        </div>
        <!-- .row -->
        <!--<div class="row">
            <div class="col-lg-3 col-sm-3 col-xs-12">
                <div class="white-box analytics-info">
                    <h3 class="box-title">Total Users</h3>
                    <ul class="list-inline two-part">
                        <li>
                            <%--<div id="sparklinedash"></div>--%>
                        </li>
                        <li class="text-right"><%--<i class="ti-arrow-up text-success"></i>--%><span class="counter text-success">
                            <asp:Literal runat="server" ID="ltrTotalUsers" Text="0"></asp:Literal></span></li>
                    </ul>
                </div>
            </div>
            <div class="col-lg-3 col-sm-3 col-xs-12">
                <div class="white-box analytics-info">
                    <h3 class="box-title">Total Repeaters</h3>
                    <ul class="list-inline two-part">
                        <li>
                            <%--<div id="sparklinedash2"></div>--%>
                        </li>
                        <li class="text-right"><%--<i class="ti-arrow-up text-purple"></i>--%><span class="counter text-purple">
                            <asp:Literal runat="server" ID="ltrTotalResellers" Text="0"></asp:Literal></span></li>
                    </ul>
                </div>
            </div>
            <div class="col-lg-3 col-sm-3 col-xs-12">
                <div class="white-box analytics-info">
                    <h3 class="box-title">Total Fees</h3>
                    <ul class="list-inline two-part">
                        <li>
                            <%--<div id="sparklinedash3"></div>--%>
                        </li>
                        <li class="text-right"><%--<i class="ti-arrow-up text-info"></i>--%><span class="counter text-info">
                            <asp:Literal runat="server" ID="ltrSenderIdRequest" Text="0"></asp:Literal></span></li>
                    </ul>
                </div>
            </div>
            <div class="col-lg-3 col-sm-3 col-xs-12">
                <div class="white-box analytics-info">
                    <h3 class="box-title">Pending Fees</h3>
                    <ul class="list-inline two-part">
                        <li>
                            <%--<div id="sparklinedash4"></div>--%>
                        </li>
                        <li class="text-right">&nbsp; <%--<i class="ti-arrow-down text-danger"></i>--%><span class="text-danger"></span></li>
                    </ul>
                </div>
            </div>
        </div>-->
        <!--/.row -->
        <!-- .row -->
        <%--<div class="row">
                        <div class="col-md-12">
                            <div class="white-box">
                                <div class="row">
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <h3 class="box-title">Sales in 2016</h3>
                                        <p class="m-t-30">Lorem ipsum dolor sit amet, ectetur adipiscing elit. viverra tellus. ipsumdolorsitda amet, ectetur adipiscing elit.</p>
                                        <p>
                                            <br />
                                            Ectetur adipiscing elit. viverra tellus.ipsum dolor sit amet, dag adg ecteturadipiscingda elitdglj. vadghiverra tellus.
                                        </p>
                                    </div>
                                    <div class="col-md-8 col-sm-6 col-xs-12">
                                        <div id="morris-area-chart" style="height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
        <!--/.row -->
        <!-- .row -->
        <%--<div class="row">
                        <div class="col-md-4 col-sm-12 col-xs-12">
                            <div class="white-box">
                                <h3 class="box-title">Weather</h3>
                                <div class="weather-box">
                                    <div class="weather-top">
                                        <h2 class="pull-left">Monday
                                            <br>
                                            <small>7th May 2016</small></h2>
                                        <div class="today_crnt pull-right">
                                            <canvas class="sleet" width="44" height="44"></canvas>
                                            <span>32<sup>°F</sup></span>
                                        </div>
                                    </div>
                                    <div class="weather-info">
                                        <h5 class="font-bold">Weather info</h5>
                                        <div class="row">
                                            <div class="col-xs-6 p-r-10">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="pull-left">Wind</p>
                                                        <p class="pull-right font-bold">16km/h</p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="pull-left">Sunrise</p>
                                                        <p class="pull-right font-bold">05:20</p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="pull-left">Humanfeel</p>
                                                        <p class="pull-right font-bold">32 <sup>°F</sup></p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-6 p-l-10">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="pull-left">Sunset</p>
                                                        <p class="pull-right font-bold">21:05</p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <p class="pull-left">Pressure </p>
                                                        <p class="pull-right font-bold">22 in</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="weather-time">
                                        <ul class="list-unstyled weather-days row">
                                            <li class="col-xs-4 col-sm-2"><span>Tue</span>
                                                <canvas class="sleet" width="30" height="30"></canvas>
                                                <span>32<sup>°F</sup></span></li>
                                            <li class="col-xs-4 col-sm-2"><span>Wed</span>
                                                <canvas class="clear-day" width="30" height="30"></canvas>
                                                <span>34<sup>°F</sup></span></li>
                                            <li class="col-xs-4 col-sm-2"><span>Thu</span>
                                                <canvas class="partly-cloudy-day" width="30" height="30"></canvas>
                                                <span>35<sup>°F</sup></span></li>
                                            <li class="col-xs-4 col-sm-2"><span>Fri</span>
                                                <canvas class="cloudy" width="30" height="30"></canvas>
                                                <span>34<sup>°F</sup></span></li>
                                            <li class="col-xs-4 col-sm-2"><span>Sat</span>
                                                <canvas class="snow" width="30" height="30"></canvas>
                                                <span>30<sup>°F</sup></span></li>
                                            <li class="col-xs-4 col-sm-2"><span>Sun</span>
                                                <canvas class="wind" width="30" height="30"></canvas>
                                                <span>26<sup>°F</sup></span></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12">
                            <div class="white-box">
                                <h3 class="box-title">User Activity</h3>
                                <div class="steamline">
                                    <div class="sl-item">
                                        <div class="sl-left">
                                            <img class="img-circle" alt="user" src="../plugins/images/users/genu.jpg">
                                        </div>
                                        <div class="sl-right">
                                            <div><a href="#">Gohn Doe</a> <span class="sl-date">5 minutes ago</span></div>
                                            <p>Contrary to popular belief</p>
                                        </div>
                                    </div>
                                    <div class="sl-item">
                                        <div class="sl-left">
                                            <img class="img-circle" alt="user" src="../plugins/images/users/ritesh.jpg">
                                        </div>
                                        <div class="sl-right">
                                            <div><a href="#">Gohn Doe</a> <span class="sl-date">5 minutes ago</span></div>
                                            <p>Lorem Ipsum is simply dummy</p>
                                        </div>
                                    </div>
                                    <div class="sl-item">
                                        <div class="sl-left">
                                            <img class="img-circle" alt="user" src="../plugins/images/users/sonu.jpg">
                                        </div>
                                        <div class="sl-right">
                                            <div><a href="#">Gohn Doe</a> <span class="sl-date">5 minutes ago</span></div>
                                            <p>The standard chunk of ipsum </p>
                                        </div>
                                    </div>
                                    <div class="sl-item">
                                        <div class="sl-left">
                                            <img class="img-circle" alt="user" src="../plugins/images/users/ritesh.jpg">
                                        </div>
                                        <div class="sl-right">
                                            <div><a href="#">Gohn Doe</a> <span class="sl-date">5 minutes ago</span></div>
                                            <p>Contrary to popular belief</p>
                                        </div>
                                    </div>
                                    <div class="sl-item">
                                        <div class="sl-left">
                                            <img class="img-circle" alt="user" src="../plugins/images/users/govinda.jpg">
                                        </div>
                                        <div class="sl-right">
                                            <div><a href="#">Gohn Doe</a> <span class="sl-date">5 minutes ago</span></div>
                                            <p>The generated lorem ipsum</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-12 col-xs-12">
                            <div class="white-box">
                                <h3 class="box-title">Feeds</h3>
                                <ul class="feeds">
                                    <li>
                                        <div class="bg-info"><i class="fa fa-bell-o text-white"></i></div>
                                        You have 4 pending tasks. <span class="text-muted">Just Now</span></li>
                                    <li>
                                        <div class="bg-success"><i class="ti-server text-white"></i></div>
                                        Server #1 overloaded.<span class="text-muted">2 Hours ago</span></li>
                                    <li>
                                        <div class="bg-warning"><i class="ti-shopping-cart text-white"></i></div>
                                        New order received.<span class="text-muted">31 May</span></li>
                                    <li>
                                        <div class="bg-danger"><i class="ti-user text-white"></i></div>
                                        New user registered.<span class="text-muted">30 May</span></li>
                                    <li>
                                        <div class="bg-inverse"><i class="fa fa-bell-o text-white"></i></div>
                                        New Version just arrived. <span class="text-muted">27 May</span></li>
                                    <li>
                                        <div class="bg-purple"><i class="ti-settings text-white"></i></div>
                                        You have 4 pending tasks. <span class="text-muted">27 May</span></li>
                                </ul>
                            </div>
                        </div>
                    </div>--%>
        <!--/.row -->
        <div class="row" style="display: none;">
            <div class="col-md-12">
                <div class="white-box">
                    <h3 class="box-title">Recently Registered Users</h3>
                    <div class="table-responsive">
                        <section class="m-b-40">
                            <div class="sttabs tabs-style-bar">
                                <nav>
                                    <ul>
                                        <li><a href="#user" class="sticon ti-home"><span>Users</span></a></li>
                                        <li><a href="#reseller" class="sticon ti-gift"><span>Resseller</span></a></li>
                                    </ul>
                                </nav>
                                <div class="content-wrap text-center">
                                    <section id="user">
                                        <%--<h3>Best Clean Tab ever</h3>--%>
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th>User Id</th>
                                                    <th>Name</th>
                                                    <th>Email</th>
                                                    <th>Mobile</th>
                                                    <th>Login Type</th>
                                                    <th>Date</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView runat="server" ID="lvData">
                                                    <ItemTemplate>
                                                        <tr class="text-left">
                                                            <td><%# Eval("ID") %></td>
                                                            <td><%# Eval("NAME") %></td>
                                                            <td><%# Eval("EMAIL") %></td>
                                                            <td><%# Eval("MOBILENO") %></td>
                                                            <td><%# Eval("LOGIN_TYPE") %></td>
                                                            <td><span class="text-muted"><i class="fa fa-clock-o"></i>&nbsp;<%# Eval("DATE") %></span></td>
                                                            <td class="">
                                                                <div class='label label-table label-<%# Eval("STATUSCLASS") %>'><%# Eval("USERSTATUS") %></div>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>


                                    </section>
                                    <section id="reseller">
                                        <table class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <th>User Id</th>
                                                    <th>Name</th>
                                                    <th>Email</th>
                                                    <th>Mobile</th>
                                                    <th>Login Type</th>
                                                    <th>Date</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView runat="server" ID="lvDataResseler">
                                                    <ItemTemplate>
                                                        <tr class="text-left">
                                                            <td><%# Eval("ID") %></td>
                                                            <td><%# Eval("NAME") %></td>
                                                            <td><%# Eval("EMAIL") %></td>
                                                            <td><%# Eval("MOBILENO") %></td>
                                                            <td><%# Eval("LOGIN_TYPE") %></td>
                                                            <td><span class="text-muted"><i class="fa fa-clock-o"></i>&nbsp;<%# Eval("DATE") %></span></td>
                                                            <td class="">
                                                                <div class='label label-table label-<%# Eval("STATUSCLASS") %>'><%# Eval("USERSTATUS") %></div>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>
                                    </section>

                                </div>
                                <!-- /content -->
                            </div>
                            <!-- /tabs -->
                        </section>
                    </div>
                </div>
            </div>
        </div>
        <!-- .right-sidebar -->
        <div class="right-sidebar">
            <div class="slimscrollright">
                <div class="rpanel-title">Service Panel <span><i class="ti-close right-side-toggle"></i></span></div>
                <div class="r-panel-body">
                    <ul>
                        <li><b>Layout Options</b></li>
                        <li>
                            <div class="checkbox checkbox-info">
                                <input id="checkbox1" type="checkbox" class="fxhdr" />
                                <label for="checkbox1">Fix Header </label>
                            </div>
                        </li>
                        <li>
                            <div class="checkbox checkbox-warning">
                                <input id="checkbox2" type="checkbox" class="fxsdr" />
                                <label for="checkbox2">Fix Sidebar </label>
                            </div>
                        </li>
                        <li>
                            <div class="checkbox checkbox-success">
                                <input id="checkbox4" type="checkbox" class="open-close"/>
                                <label for="checkbox4">Toggle Sidebar </label>
                            </div>
                        </li>
                    </ul>
                    <ul id="themecolors" class="m-t-20">
                        <li><b>With Light sidebar</b></li>
                        <li><a href="javascript:void(0)" theme="default" class="default-theme working">1</a></li>
                        <li><a href="javascript:void(0)" theme="green" class="green-theme">2</a></li>
                        <li><a href="javascript:void(0)" theme="gray" class="yellow-theme">3</a></li>
                        <li><a href="javascript:void(0)" theme="blue" class="blue-theme">4</a></li>
                        <li><a href="javascript:void(0)" theme="purple" class="purple-theme">5</a></li>
                        <li><a href="javascript:void(0)" theme="megna" class="megna-theme">6</a></li>
                        <li><b>With Dark sidebar</b></li>
                        <br />
                        <li><a href="javascript:void(0)" theme="default-dark" class="default-dark-theme">7</a></li>
                        <li><a href="javascript:void(0)" theme="green-dark" class="green-dark-theme">8</a></li>
                        <li><a href="javascript:void(0)" theme="gray-dark" class="yellow-dark-theme">9</a></li>

                        <li><a href="javascript:void(0)" theme="blue-dark" class="blue-dark-theme">10</a></li>
                        <li><a href="javascript:void(0)" theme="purple-dark" class="purple-dark-theme">11</a></li>
                        <li><a href="javascript:void(0)" theme="megna-dark" class="megna-dark-theme">12</a></li>
                    </ul>
                    <ul class="m-t-20 chatonline">
                        <li><b>Chat option</b></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/varun.jpg" alt="user-img" class="img-circle" />
                            <span>Varun Dhavan <small class="text-success">online</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/genu.jpg" alt="user-img" class="img-circle" />
                            <span>Genelia Deshmukh <small class="text-warning">Away</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/ritesh.jpg" alt="user-img" class="img-circle" />
                            <span>Ritesh Deshmukh <small class="text-danger">Busy</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/arijit.jpg" alt="user-img" class="img-circle" />
                            <span>Arijit Sinh <small class="text-muted">Offline</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/govinda.jpg" alt="user-img" class="img-circle" />
                            <span>Govinda Star <small class="text-success">online</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/hritik.jpg" alt="user-img" class="img-circle" />
                            <span>John Abraham<small class="text-success">online</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/john.jpg" alt="user-img" class="img-circle" />
                            <span>Hritik Roshan<small class="text-success">online</small></span></a></li>
                        <li><a href="javascript:void(0)">
                            <img src="../plugins/images/users/pawandeep.jpg" alt="user-img" class="img-circle" />
                            <span>Pwandeep rajan <small class="text-success">online</small></span></a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- /.right-sidebar -->

        <script src="js/cbpFWTabs.js"></script>
        <script type="text/javascript">
            (function () {
                [].slice.call(document.querySelectorAll('.sttabs')).forEach(function (el) {
                    new CBPFWTabs(el);
                });

            })();
</script>
    </div>
</asp:Content>

