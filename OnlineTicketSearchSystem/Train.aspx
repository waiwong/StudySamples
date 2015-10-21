<%@ Page Title="Train" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Train.aspx.cs" Inherits="About" %>

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
            
            
            
   </div>
   <div>
       
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:Button ID="btn" Text="Search" Runat="server" Width="72px" Height="24" 
        onclick="btn_Click1"></asp:Button>
       
</div>
   <div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="SqlDataSource1" BackColor="White" 
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" Visible="False">
                <Columns>
                    <asp:BoundField DataField="TrainCode" HeaderText="TrainCode" 
                        SortExpression="TrainCode" />
                    <asp:BoundField DataField="FirstStation" HeaderText="FirstStation" 
                        SortExpression="FirstStation" />
                    <asp:BoundField DataField="LastStation" HeaderText="LastStation" 
                        SortExpression="LastStation" />
                    <asp:BoundField DataField="StartStation" HeaderText="StartStation" 
                        SortExpression="StartStation" />
                    <asp:BoundField DataField="StartTime" HeaderText="StartTime" 
                        SortExpression="StartTime" />
                    <asp:BoundField DataField="ArriveStation" HeaderText="ArriveStation" 
                        SortExpression="ArriveStation" />
                    <asp:BoundField DataField="ArriveTime" HeaderText="ArriveTime" 
                        SortExpression="ArriveTime" />
                    <asp:BoundField DataField="KM" HeaderText="KM" 
                        SortExpression="KM" />
                    <asp:BoundField DataField="UseDate" HeaderText="UseDate" 
                        SortExpression="UseDate" />
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
    
        SelectCommand="SELECT * FROM Train_Info WHERE (StartStation like '%'+@StartStation+'%') AND (ArriveStation like '%'+@ArriveStation+'%') " 
        
        
        
        InsertCommand="INSERT INTO Train_Info(TrainCode, FirstStation, LastStation, StartStation, StartTime, ArriveStation, ArriveTime, KM, UseDate) VALUES (@TrainCode, @FirstStation,@LastStation, @StartStation, @StartTime, @ArriveStation, @ArriveTime, @KM, @UseDate)">
    <InsertParameters>
        <asp:SessionParameter Name="TrainCode" SessionField="TrainCode" />
        <asp:SessionParameter Name="FirstStation" SessionField="FirstStation" />
        <asp:SessionParameter Name="LastStation" SessionField="LastStation" />
        <asp:SessionParameter Name="StartStation" SessionField="StartStation" />
        <asp:SessionParameter Name="StartTime" SessionField="StartTime" />
        <asp:SessionParameter Name="ArriveStation" SessionField="ArriveStation" />
        <asp:SessionParameter Name="ArriveTime" SessionField="ArriveTime" />
        <asp:SessionParameter Name="KM" SessionField="KM" />
        <asp:SessionParameter Name="UseDate" SessionField="UseDate" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="text1" Name="StartStation" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="text2" Name="ArriveStation" 
            PropertyName="SelectedValue" />
    </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
