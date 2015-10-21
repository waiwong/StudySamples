<%@ Page Title="Flight" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Flight.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
            <asp:Label ID="Label1" runat="server" Text="StartCity："></asp:Label>
            &nbsp;
            <asp:DropDownList ID="text1" runat="server" Width="110px">
                <asp:ListItem>北京</asp:ListItem>
                <asp:ListItem>上海</asp:ListItem>
                <asp:ListItem>广州</asp:ListItem>
                <asp:ListItem>深圳</asp:ListItem>
                <asp:ListItem>成都</asp:ListItem>
                <asp:ListItem>昆明</asp:ListItem>
                <asp:ListItem>海口</asp:ListItem>
                <asp:ListItem>西安</asp:ListItem>
                <asp:ListItem>杭州</asp:ListItem>
                <asp:ListItem Value="厦门"></asp:ListItem>
                <asp:ListItem>重庆</asp:ListItem>
                <asp:ListItem>青岛</asp:ListItem>
                <asp:ListItem>大连</asp:ListItem>
                <asp:ListItem>南京</asp:ListItem>
                <asp:ListItem>武汉</asp:ListItem>
                <asp:ListItem>沈阳</asp:ListItem>
                <asp:ListItem>乌鲁木齐</asp:ListItem>
                <asp:ListItem>长沙</asp:ListItem>
                <asp:ListItem>福州</asp:ListItem>
                <asp:ListItem>桂林</asp:ListItem>
                <asp:ListItem>哈尔滨</asp:ListItem>
                <asp:ListItem>贵阳</asp:ListItem>
                <asp:ListItem>郑州</asp:ListItem>
                <asp:ListItem>温州</asp:ListItem>
                <asp:ListItem>济南</asp:ListItem>
                <asp:ListItem>宁波</asp:ListItem>
                <asp:ListItem>天津</asp:ListItem>
                <asp:ListItem>太原</asp:ListItem>
                <asp:ListItem>南宁</asp:ListItem>
                <asp:ListItem>南昌</asp:ListItem>
                <asp:ListItem>长春</asp:ListItem>
                <asp:ListItem>合肥</asp:ListItem>
                <asp:ListItem>晋江</asp:ListItem>
                <asp:ListItem>兰州</asp:ListItem>
                <asp:ListItem>烟台</asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
   </div>
  <div>
            <asp:Label ID="Label2" runat="server" Text="ArriveCity："></asp:Label>
            <asp:DropDownList ID="text2" runat="server" Width="110px">
                <asp:ListItem>北京</asp:ListItem>
                <asp:ListItem>上海</asp:ListItem>
                <asp:ListItem>广州</asp:ListItem>
                <asp:ListItem>深圳</asp:ListItem>
                <asp:ListItem>成都</asp:ListItem>
                <asp:ListItem>昆明</asp:ListItem>
                <asp:ListItem>海口</asp:ListItem>
                <asp:ListItem>西安</asp:ListItem>
                <asp:ListItem>杭州</asp:ListItem>
                <asp:ListItem Value="厦门"></asp:ListItem>
                <asp:ListItem>重庆</asp:ListItem>
                <asp:ListItem>青岛</asp:ListItem>
                <asp:ListItem>大连</asp:ListItem>
                <asp:ListItem>南京</asp:ListItem>
                <asp:ListItem>武汉</asp:ListItem>
                <asp:ListItem>沈阳</asp:ListItem>
                <asp:ListItem>乌鲁木齐</asp:ListItem>
                <asp:ListItem>长沙</asp:ListItem>
                <asp:ListItem>福州</asp:ListItem>
                <asp:ListItem>桂林</asp:ListItem>
                <asp:ListItem>哈尔滨</asp:ListItem>
                <asp:ListItem>贵阳</asp:ListItem>
                <asp:ListItem>郑州</asp:ListItem>
                <asp:ListItem>温州</asp:ListItem>
                <asp:ListItem>济南</asp:ListItem>
                <asp:ListItem>宁波</asp:ListItem>
                <asp:ListItem>天津</asp:ListItem>
                <asp:ListItem>太原</asp:ListItem>
                <asp:ListItem>南宁</asp:ListItem>
                <asp:ListItem>南昌</asp:ListItem>
                <asp:ListItem>长春</asp:ListItem>
                <asp:ListItem>合肥</asp:ListItem>
                <asp:ListItem>晋江</asp:ListItem>
                <asp:ListItem>兰州</asp:ListItem>
                <asp:ListItem>烟台</asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
   
   </div>
    <div>
            <asp:Label ID="Label3" runat="server" Text="StartTime："></asp:Label>
            <asp:TextBox ID="text3" Runat="server" Width="100px" Height="22" 
                ></asp:TextBox>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                Text="SelectDate" />
   </div>
   <div>
       <asp:Calendar
id="cal"
style="Z-INDEX: 101; LEFT: 262px; POSITION: absolute; TOP: 167px"
runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" 
                DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                Height="180px" Width="200px" Visible="False" 
           onselectionchanged="cal_SelectionChanged" >
       <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
       <NextPrevStyle VerticalAlign="Bottom" />
       <OtherMonthDayStyle ForeColor="#808080" />
       <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
       <SelectorStyle BackColor="#CCCCCC" />
       <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
       <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
       <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
</div>
   <div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn" Text="Search" Runat="server" Width="72px" Height="24" 
        onclick="btn_Click1"></asp:Button>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="id" DataSourceID="SqlDataSource1" BackColor="White" 
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" Visible="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                        ReadOnly="True" SortExpression="id" Visible="False" />
                    <asp:BoundField DataField="Company" HeaderText="Company" 
                        SortExpression="Company" />
                    <asp:BoundField DataField="AirlineCode" HeaderText="AirlineCode" 
                        SortExpression="AirlineCode" />
                    <asp:BoundField DataField="StartDrome" HeaderText="StartDrome" 
                        SortExpression="StartDrome" />
                    <asp:BoundField DataField="ArriveDrome" HeaderText="ArriveDrome" 
                        SortExpression="ArriveDrome" />
                    <asp:BoundField DataField="StartTime" HeaderText="StartTime" 
                        SortExpression="StartTime" />
                    <asp:BoundField DataField="ArriveTime" HeaderText="ArriveTime" 
                        SortExpression="ArriveTime" />
                    <asp:BoundField DataField="Mode" HeaderText="Mode" SortExpression="Mode" />
                    <asp:BoundField DataField="AirlineStop" HeaderText="AirlineStop" 
                        SortExpression="AirlineStop" />
                    <asp:BoundField DataField="Week" HeaderText="Week" SortExpression="Week" />
                    <asp:BoundField DataField="startCity" HeaderText="startCity" 
                        SortExpression="startCity" Visible="False" />
                    <asp:BoundField DataField="lastCity" HeaderText="lastCity" 
                        SortExpression="lastCity" Visible="False" />
                    <asp:BoundField DataField="theDate" HeaderText="theDate" 
                        SortExpression="theDate" Visible="False" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <br />

   </div>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:flightConnectionString %>" 
    
        SelectCommand="SELECT id, Company, AirlineCode, StartDrome, ArriveDrome, StartTime, ArriveTime, Mode, AirlineStop, Week, startCity, lastCity, theDate FROM flight_Info WHERE (startCity = @startCity) AND (lastCity = @lastCity) AND (theDate = @theDate)" 
        
        InsertCommand="INSERT INTO flight_Info(Company, AirlineCode, StartDrome, ArriveDrome, StartTime, ArriveTime, Mode, AirlineStop, Week, startCity, lastCity, theDate) VALUES (@Company, @AirlineCode, @StartDrome, @ArriveDrome, @StartTime, @ArriveTime, @Mode, @AirlineStop, @Week,@startCity, @lastCity, @theDate)" 
        onselecting="SqlDataSource1_Selecting">
    <InsertParameters>
        <asp:SessionParameter Name="Company" SessionField="Company" />
        <asp:SessionParameter Name="AirlineCode" SessionField="AirlineCode" />
        <asp:SessionParameter Name="StartDrome" SessionField="StartDrome" />
        <asp:SessionParameter Name="ArriveDrome" SessionField="ArriveDrome" />
        <asp:SessionParameter Name="StartTime" SessionField="StartTime" />
        <asp:SessionParameter Name="ArriveTime" SessionField="ArriveTime" />
        <asp:SessionParameter Name="Mode" SessionField="Mode" />
        <asp:SessionParameter Name="AirlineStop" SessionField="AirlineStop" />
        <asp:SessionParameter Name="Week" SessionField="Week" />
        <asp:ControlParameter ControlID="text1" Name="startCity" PropertyName="Text" />
        <asp:ControlParameter ControlID="text2" Name="lastCity" PropertyName="Text" />
        <asp:ControlParameter ControlID="text3" Name="theDate" PropertyName="Text" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="text1" Name="startCity" PropertyName="Text" />
        <asp:ControlParameter ControlID="text2" Name="lastCity" PropertyName="Text" />
        <asp:ControlParameter ControlID="text3" Name="theDate" PropertyName="Text" />
    </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
