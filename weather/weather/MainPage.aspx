<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="weather.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
.Initial
{
  display: block;
  padding: 4px 18px 4px 18px;
  float: left;
  color: Black;
  font-weight: bold;
  background-color: #808080 ;
}
.Initial:hover
{
  color: Black ;
}
.Clicked
{
  float: left;
  display: block;
  padding: 4px 18px 4px 18px;
  color: Black;
  font-weight: bold;
  background-color: #b6ff00 ;
  color: black;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
      <tr>
        <td>
          <asp:Button Text="Get Temperatures by Country" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="Filter by Range" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <asp:Button Text="Tab 3" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />
          <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">
               <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <div class="form-group col-md-8"> 
                    <%--<select id="lstCountries" multiple="true" runat="server" size="15">
    
                    </select>--%>
                    
                    <asp:ListBox runat="server" ID="totalcountries" SelectionMode="Multiple"></asp:ListBox>
                    <%--<input type="button" id="btnSelected" OnClick="getAverageTemparature();" value="Get Selected" runat="server"/>--%>
                    <asp:Button runat="server" ID="btnSample" Text="DisplayAverageTemperature" OnClick="getAverageTemparature" />
            <br />

                    <%--<asp:Button ID="Button2" runat="server" Text="Call Button Click" OnClick="getAverageTemparature" />--%>
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                </div>
               
                  </td>
                </tr>
              </table>
                
            </asp:View>
            <asp:View ID="View2" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                     <asp:TextBox ID="from" placeholder="Enter Min range" runat="server"></asp:TextBox>
                    <asp:TextBox ID="to" placeholder="Enter Max range" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="cities" Text="searchCities" OnClick="searchCities" />

            
                    <%--<asp:Button ID="Button2" runat="server" Text="Call Button Click" OnClick="getAverageTemparature" />--%>
                    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
        
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <h3>
                      View 3
                    </h3>
                  </td>
                </tr>
              </table>
            </asp:View>
          </asp:MultiView>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
